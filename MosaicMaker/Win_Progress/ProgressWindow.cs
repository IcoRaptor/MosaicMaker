using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace MosaicMaker
{
    public partial class ProgressWindow : Form
    {
        #region Variables

        private List<string> _paths = new List<string>();
        private MosaicData _data;
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

        public ProgressWindow(MosaicData data)
        {
            InitializeComponent();

            Utility.SetEnabled(Btn_OK, false);

            _data = data;
            InitBackgroundWorker();

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region Elements

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var n in _data.Names)
                _paths.Add(_data.NamePath[(string)n]);

            using (new ActionTimer(2))
            {
                ResizeImages(e);
            }
            UpdateProgress(15, "Slicing loaded image...");

            using (new ActionTimer(1.5f))
            {
                SliceLoadedImage(e);
            }
            UpdateProgress(10, "Analyzing colors...");

            using (new ActionTimer(1.5f))
            {
                AnalyzeColors(e);
            }
            UpdateProgress(5, "Building final image...");

            using (new ActionTimer(2))
            {
                BuildFinalImage(e);
            }
        }

        private void BW_Builder_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Progress_Builder.Value = e.ProgressPercentage;
        }

        private void BW_Builder_RunWorkCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                Clear(_resizer, _slicer, _analyzer, _builder);
                return;
            }

            Progress_Builder.Value = Progress_Builder.Maximum;
            Label_Progress.Text = "Finished";

            Clear(_resizer, _slicer, _analyzer, _builder);

            Utility.SetEnabled(Btn_OK, true);
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BW_Builder.CancelAsync();
        }

        #endregion

        #region Background

        private void ResizeImages(DoWorkEventArgs e)
        {
            _resizer = new ImageResizer(_paths, _data.ElementSize,
                _data.LoadedImage);
            _resizer.ResizeAll();

            CheckCancel(e);
        }

        private void SliceLoadedImage(DoWorkEventArgs e)
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage,
                _resizer.ElementSize);
            _slicer.SliceImage();

            CheckCancel(e);
        }

        private void AnalyzeColors(DoWorkEventArgs e)
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImage);
            _analyzer.AnalyzeColors();

            CheckCancel(e);
        }

        private void BuildFinalImage(DoWorkEventArgs e)
        {
            // Test
            _builder = new ImageBuilder(_resizer.ResizedImage,
                _slicer.SlicedImage, _analyzer.IndexErrors, _analyzer.Errors);
            _builder.BuildImage();

            MosaicImage = _resizer.Resize(_builder.FinalImage,
                _resizer.OrigSize);

            CheckCancel(e);
        }

        private void UpdateProgress(int val, string text)
        {
            Invoke(new Action(() =>
            {
                Label_Progress.Text = text;

                _progress = Utility.Clamp(_progress + val,
                    Progress_Builder.Minimum, Progress_Builder.Maximum);
            }));

            BW_Builder.ReportProgress(_progress);
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }

        #endregion

        private void InitBackgroundWorker()
        {
            BW_Builder.ProgressChanged +=
                new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);
        }

        private void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                if (c != null)
                    c.Clear();
        }
    }
}