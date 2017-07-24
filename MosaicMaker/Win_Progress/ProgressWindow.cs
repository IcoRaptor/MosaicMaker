using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public partial class ProgressWindow : Form
    {
        #region Variables

        private const int _PROG_RESIZE = 20000;
        private const int _PROG_SLICE = 25000;
        private const int _PROG_ANALYZE = 5000;

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

            BW_Builder.ProgressChanged +=
                new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region GUI

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            DoTimedAction(ResizeImages, e, 1f);
            UpdateProgress(_PROG_RESIZE, "Slicing loaded image...");

            DoTimedAction(SliceLoadedImage, e, 1.5f);
            UpdateProgress(_PROG_SLICE, "Analyzing colors...");

            DoTimedAction(AnalyzeColors, e, 1f);
            UpdateProgress(_PROG_ANALYZE, "Building final image...");

            DoTimedAction(BuildFinalImage, e, 1.5f);
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

        private void DoTimedAction(TimedAction action, DoWorkEventArgs e,
            float minExecTime)
        {
            using (new ActionTimer(minExecTime))
            {
                action(e);
            }
        }

        private void ResizeImages(DoWorkEventArgs e)
        {
            _resizer = new ImageResizer(_data);
            _resizer.Execute();

            CheckCancel(e);
        }

        private void SliceLoadedImage(DoWorkEventArgs e)
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage,
                _data.ElementSize);
            _slicer.Execute();

            CheckCancel(e);
        }

        private void AnalyzeColors(DoWorkEventArgs e)
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImage);
            _analyzer.Execute();

            CheckCancel(e);
        }

        private void BuildFinalImage(DoWorkEventArgs e)
        {
            _builder = new ImageBuilder(_resizer.ResizedImage,
                _data.ElementSize, _analyzer.NewImage, this);
            _builder.Execute();

            MosaicImage = _resizer.Resize(_builder.FinalImage, _resizer.OrigSize);
        }

        public void UpdateProgress(int val, string text)
        {
            Invoke(new Action(() =>
            {
                if (text != null)
                    Label_Progress.Text = text;

                _progress = Utility.Clamp(_progress + val,
                    Progress_Builder.Minimum, Progress_Builder.Maximum);
            }));

            BW_Builder.ReportProgress(_progress);
        }

        #endregion

        private void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                if (c != null)
                    c.Clear();

            GC.Collect();
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}