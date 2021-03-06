﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    /// <summary>
    /// Transforms the loaded image and shows a progress bar
    /// </summary>
    public partial class ProgressDialog : Form
    {
        #region Variables

        private readonly MosaicData _mData;
        private readonly ProgressData _pData;
        private Stopwatch _stopwatch;
        private int _progress;

        private ImageResizer _resizer;
        private ImageSlicer _slicer;
        private ColorAnalyzer _analyzer;
        private ImageBuilder _builder;

        #endregion

        #region Properties

        /// <summary>
        /// The finished image
        /// </summary>
        public Bitmap MosaicImage { get; private set; }

        #endregion

        #region Constructors

        public ProgressDialog(MosaicData mData)
        {
            InitializeComponent();

            InitBackgroundWorker();

            Utility.SetEnabled(Btn_OK, false);

            _mData = mData ??
                throw new ArgumentNullException("mData");

            Size newSize = Utility.GetNewImageSize(_mData.LoadedImage.Size,
                _mData.ElementSize);
            _pData = new ProgressData(this, newSize, _mData.ElementSize);

            SetMaxProgress();

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region UI

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BW_Builder.CancelAsync();
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Background

        /// <summary>
        /// Transforms the loaded image into a mosaic
        /// </summary>
        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            _stopwatch = Stopwatch.StartNew();

            try
            {
                ResizeImages();
                CheckCancel(e);
                UpdateProgressText(Strings.Slicing);

                SliceLoadedImage();
                CheckCancel(e);
                UpdateProgressText(Strings.Analyzing);

                AnalyzeColors();
                CheckCancel(e);
                UpdateProgressText(Strings.Building);

                BuildFinalImage();
            }
            catch (OutOfMemoryException)
            {
                e.Cancel = true;
                Btn_Cancel_Click(sender, e);

                MessageBox.Show(Strings.OutOfMemory);
            }

            _stopwatch.Stop();
        }

        private void BW_Builder_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Progress_Builder.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Updates the progress label
        /// </summary>
        private void BW_Builder_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            Clear(_resizer, _slicer, _analyzer, _builder);

            if (e.Cancelled || e.Error != null)
                return;

            Progress_Builder.Value = Progress_Builder.Maximum;

            string min = string.Empty;
            CultureInfo info = CultureInfo.InvariantCulture;

            if (_stopwatch.Elapsed.Minutes > 0)
                min = string.Format(info, "{0:00}:", _stopwatch.Elapsed.Minutes);

            string sec = string.Format(info, "{0:00}:",
                _stopwatch.Elapsed.Seconds);
            string ms = string.Format(info, "{0:000}",
                _stopwatch.Elapsed.Milliseconds);

            UpdateProgressText(string.Concat(Strings.Finished, min, sec, ms));

            Utility.SetEnabled(Btn_OK, true);
            Utility.SetEnabled(Btn_Cancel, false);
        }

        /// <summary>
        /// Resizes the loaded image and the mosaic elements
        /// </summary>
        private void ResizeImages()
        {
            _resizer = new ImageResizer(_mData, _pData);
            _resizer.Execute();
        }

        /// <summary>
        /// Slices the loaded image into BlockLines
        /// </summary>
        private void SliceLoadedImage()
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage, _pData);
            _slicer.Execute();
        }

        /// <summary>
        /// Compares the loaded image and the mosaic elements
        ///  and generates new Blockl´Lines
        /// </summary>
        private void AnalyzeColors()
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImageLines, _pData);
            _analyzer.Execute();
        }

        /// <summary>
        /// Generates a new image from the BlockLines
        ///  and resizes it to the original size
        /// </summary>
        private void BuildFinalImage()
        {
            _builder = new ImageBuilder(_analyzer.NewImageLines, _pData);
            _builder.Execute();

            MosaicImage = ImageResizer.Resize(_builder.FinalImage,
                _resizer.OriginalSize);
        }

        /// <summary>
        /// Updates the progress label
        /// </summary>
        private void UpdateProgressText(string text)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    Label_Progress.Text = text;
                }));
            }
            catch { }
        }

        /// <summary>
        /// Increments the value shown in the progress bar
        /// </summary>
        public void IncrementProgress()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    _progress = Utility.Clamp(_progress + 1,
                        Progress_Builder.Minimum, Progress_Builder.Maximum);

                    BW_Builder.ReportProgress(_progress);
                }));
            }
            catch { }
        }

        #endregion

        /// <summary>
        /// Calls Clear on the IClearables
        /// </summary>
        private static void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                if (c != null)
                    c.Clear();

            GC.Collect();
        }

        private void InitBackgroundWorker()
        {
            BW_Builder.ProgressChanged +=
              new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkerCompleted);
        }

        /// <summary>
        /// Sets the max value of the progress bar
        /// </summary>
        private void SetMaxProgress()
        {
            int maxProgress = _mData.Paths.Count + _pData.Lines * 3;
            Progress_Builder.Maximum = maxProgress;
        }

        /// <summary>
        /// Checks if the BackgroundWoker was cancelled
        /// </summary>
        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}