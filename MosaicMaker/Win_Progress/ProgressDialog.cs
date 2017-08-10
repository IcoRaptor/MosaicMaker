using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MosaicMakerNS
{
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

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            _stopwatch = Stopwatch.StartNew();

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

            _stopwatch.Stop();
        }

        private void BW_Builder_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Progress_Builder.Value = e.ProgressPercentage;
        }

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

        private void ResizeImages()
        {
            _resizer = new ImageResizer(_mData, _pData);
            GC.SuppressFinalize(_resizer);
            _resizer.Execute();
        }

        private void SliceLoadedImage()
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage, _pData);
            GC.SuppressFinalize(_slicer);
            _slicer.Execute();
        }

        private void AnalyzeColors()
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImageLines, _pData);
            GC.SuppressFinalize(_analyzer);
            _analyzer.Execute();
        }

        private void BuildFinalImage()
        {
            _builder = new ImageBuilder(_analyzer.NewImageLines, _pData);
            GC.SuppressFinalize(_builder);
            _builder.Execute();

            MosaicImage = ImageResizer.Resize(_builder.FinalImage,
                _resizer.OriginalSize);
        }

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

        private void SetMaxProgress()
        {
            int maxProgress = _mData.Paths.Count + _pData.Lines * 3;
            Progress_Builder.Maximum = maxProgress;
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}