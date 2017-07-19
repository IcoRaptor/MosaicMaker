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

#pragma warning disable
        private ImageResizer _resizer;
        private ImageSlicer _slicer;
        private ColorAnalyzer _analyzer;
        private ImageBuilder _builder;
#pragma warning restore

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ProgressWindow(MosaicData data)
        {
            InitializeComponent();

            _data = data;
            Init();

            BW_Builder.RunWorkerAsync();
        }

        #endregion

        #region Elements

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var n in _data.Names)
                _paths.Add(_data.NamePath[(string)n]);

            Work(e);
        }

        private void BW_Builder_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Progress_Builder.Value = e.ProgressPercentage;
        }

        private void BW_Builder_RunWorkCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            Finish();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BW_Builder.CancelAsync();
        }

        #endregion

        #region Background

        private void Work(DoWorkEventArgs e)
        {
            ResizeImages();
            UpdateProgress(5, "Slicing image...");
            CheckCancel(e);

            // TODO: slice, analyze, build

            _builder = new ImageBuilder()
            {
                FinishedImage = _data.LoadedImage
            };
        }

        private void ResizeImages()
        {
            _resizer = new ImageResizer(_paths, _data.ElementSize,
                _data.LoadedImage);

            _resizer.ResizeAll();
        }

        #endregion

        private void Init()
        {
            Utility.SetEnabled(Btn_OK, false);
            _progress = 0;

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

        private void Clear(params IClearable[] args)
        {
            foreach (var c in args)
                c.Clear();
        }

        private void Finish()
        {
            Progress_Builder.Value = Progress_Builder.Maximum;
            Label_Progress.Text = "Finished";

            FinalImage = _resizer.Resize(_builder.FinishedImage, _resizer.OrigSize);

            Clear(_resizer/*, _slicer, _analyzer*/, _builder);

            Utility.SetEnabled(Btn_OK, true);
        }
    }
}