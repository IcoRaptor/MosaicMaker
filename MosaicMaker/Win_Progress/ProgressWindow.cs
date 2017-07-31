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

        private readonly MosaicData _data;
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

        #region UI

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            DoTimedAction(ResizeImages, e, 0.5f);
            UpdateProgressText(_SLICING);

            DoTimedAction(SliceLoadedImage, e, 0.5f);
            UpdateProgressText(_ANALYZING);

            DoTimedAction(AnalyzeColors, e, 0.5f);
            UpdateProgressText(_BUILDING);

            DoTimedAction(BuildFinalImage, e, 0.5f);
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

        private void DoTimedAction(TimedAction action,
            DoWorkEventArgs e, float minExecTime)
        {
            if (Settings.PowerMode)
                action();
            else
            {
                using (new ActionTimer(minExecTime))
                {
                    action();
                }
            }

            CheckCancel(e);
        }

        private void ResizeImages()
        {
            _resizer = new ImageResizer(_data, _newImageSize, this);
            _resizer.Execute();
        }

        private void SliceLoadedImage()
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage,
                _data.ElementSize, this);
            _slicer.Execute();
        }

        private void AnalyzeColors()
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImageColumns, this);
            _analyzer.Execute();
        }

        private void BuildFinalImage()
        {
            _builder = new ImageBuilder(_resizer.ResizedImage.Size,
                _data.ElementSize, _analyzer.NewImageColumns, this);
            _builder.Execute();

            MosaicImage = ImageResizer.Resize(_builder.FinalImage,
                _resizer.OriginalSize);
        }

        #endregion

        private static void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                if (c != null)
                    c.Clear();
        }

        private void CalcMaxProgress()
        {
            _newImageSize = Utility.GetNewImageSize(_data.LoadedImage.Size,
                _data.ElementSize);

            int numLines = _newImageSize.Width / _data.ElementSize.Width;
            int maxProgress = _data.Paths.Count + numLines * 3;

            Progress_Builder.Maximum = maxProgress;
        }

        private void InitBackgroundWorker()
        {
            BW_Builder.ProgressChanged +=
              new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}