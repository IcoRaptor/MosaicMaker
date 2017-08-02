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

        private readonly MosaicData _mData;
        private readonly ProgressData _pData;
        private readonly Size _newImageSize;
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

        public ProgressWindow(MosaicData mData)
        {
            InitializeComponent();
            InitBackgroundWorker();

            Utility.SetEnabled(Btn_OK, false);

            _newImageSize = Utility.GetNewImageSize(mData.LoadedImage.Size,
                mData.ElementSize);

            _mData = mData;
            _pData = new ProgressData(this, _newImageSize, _mData.ElementSize);

            SetMaxProgress();

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region UI

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            DoTimedAction(ResizeImages, 0.5f, e);
            UpdateProgressText(_SLICING);

            DoTimedAction(SliceLoadedImage, 0.5f, e);
            UpdateProgressText(_ANALYZING);

            DoTimedAction(AnalyzeColors, 0.5f, e);
            UpdateProgressText(_BUILDING);

            DoTimedAction(BuildFinalImage, 0.5f, e);
            UpdateProgressText(_FINISHED);
        }

        private void BW_Builder_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Progress_Builder.Value = e.ProgressPercentage;
        }

        private void BW_Builder_RunWorkCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            Clear(_resizer, _slicer, _analyzer, _builder);

            if (e.Cancelled || e.Error != null)
                return;

            //Progress_Builder.Value = Progress_Builder.Maximum;

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

        private void DoTimedAction(TimedAction action, float minExecTime,
            DoWorkEventArgs e)
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
            _resizer = new ImageResizer(_mData, _newImageSize, _pData);
            _resizer.ExecuteParallel();
        }

        private void SliceLoadedImage()
        {
            _slicer = new ImageSlicer(_resizer.ResizedImage,
                _mData.ElementSize, _pData);
            _slicer.ExecuteParallel();
        }

        private void AnalyzeColors()
        {
            _analyzer = new ColorAnalyzer(_resizer.ElementPixels,
                _slicer.SlicedImageLines, _pData);
            _analyzer.Execute();
        }

        private void BuildFinalImage()
        {
            _builder = new ImageBuilder(_resizer.ResizedImage.Size,
                _mData.ElementSize, _analyzer.NewImageLines, _pData);

            _builder.ExecuteParallel();

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

        private void InitBackgroundWorker()
        {
            BW_Builder.ProgressChanged +=
              new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);
        }

        private void SetMaxProgress()
        {
            int maxProgress = _mData.Paths.Count + _pData.NumLines * 3;
            Progress_Builder.Maximum = maxProgress;
        }

        private void CheckCancel(DoWorkEventArgs e)
        {
            if (BW_Builder.CancellationPending)
                e.Cancel = true;
        }
    }
}