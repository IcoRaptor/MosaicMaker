using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public partial class ProgressWindow : Form
    {
        #region Variables

        private const string _SLICING = "Slicing loaded image...";
        private const string _ANALYZING = "Analyzing colors...";
        private const string _BUILDING = "Building final image...";
        private const string _FINISHED = "Finished!";

        private MosaicData _data;
        private Size _newImageSize;
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

            CalcMaxProgress();
            InitBackgroundWorker();

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region GUI

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            DoTimedAction(ResizeImages, e, 1f);
            UpdateProgress(_SLICING);

            DoTimedAction(SliceLoadedImage, e, 1.5f);
            UpdateProgress(_ANALYZING);

            DoTimedAction(AnalyzeColors, e, 1f);
            UpdateProgress(_BUILDING);

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
            Label_Progress.Text = _FINISHED;

            Clear(_resizer, _slicer, _analyzer, _builder);

            Utility.SetEnabled(Btn_OK, true);
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BW_Builder.CancelAsync();

            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Background

        private static void DoTimedAction(TimedAction action,
            DoWorkEventArgs e, float minExecTime)
        {
            using (new ActionTimer(minExecTime))
            {
                action(e);
            }
        }

        private void ResizeImages(DoWorkEventArgs e)
        {
            _resizer = new ImageResizer(_data, _newImageSize, this);
            _resizer.Execute();

            CheckCancel(e);
        }

        private void SliceLoadedImage(DoWorkEventArgs e)
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage,
                _data.ElementSize, this);
            _slicer.Execute();

            CheckCancel(e);
        }

        private void AnalyzeColors(DoWorkEventArgs e)
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImageLines, this);
            _analyzer.Execute();

            CheckCancel(e);
        }

        private void BuildFinalImage(DoWorkEventArgs e)
        {
            _builder = new ImageBuilder(_resizer.ResizedImage.Size,
                _data.ElementSize, _analyzer.NewImageLines, this);
            _builder.Execute();

            MosaicImage = ImageResizer.Resize(_builder.FinalImage,
                _resizer.OrigSize);
            UpdateProgress(4);
        }

        public void UpdateProgress(int val)
        {
            Invoke(new Action(() =>
            {
                _progress = Utility.Clamp(_progress + val,
                    Progress_Builder.Minimum, Progress_Builder.Maximum);

                BW_Builder.ReportProgress(_progress);
            }));
        }

        private void UpdateProgress(string text)
        {
            Invoke(new Action(() =>
            {
                Label_Progress.Text = text;
            }));
        }

        #endregion

        private void CalcMaxProgress()
        {
            _newImageSize = Utility.GetNewImageSize(_data.LoadedImage,
                _data.ElementSize);

            int numLines = _newImageSize.Width / _data.ElementSize.Width;
            int maxProgress = _data.Paths.Count + 5 + numLines * 3;

            Progress_Builder.Maximum = maxProgress;
        }

        private void InitBackgroundWorker()
        {
            BW_Builder.ProgressChanged +=
              new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);
        }

        private static void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                if (c != null)
                    c.Clear();
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}