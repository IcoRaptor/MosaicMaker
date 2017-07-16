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

        private MosaicData _data;
        private List<string> _paths = new List<string>();
        private Size _size;
        private int _progress;

        #endregion

        #region Properties

        public Bitmap FinishedImage { get; private set; }

        #endregion

        #region Constructors

        public ProgressWindow(MosaicData data)
        {
            InitializeComponent();

            _data = data;
            Init();
        }

        #endregion

        #region Elements

        private void BW_Builder_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var n in _data.Names)
                _paths.Add(_data.NamePath[(string)n]);

            _size = _data.ElementSize;

#pragma warning disable
            ImageResizer resizer;
            ImageSlicer slicer;
            ImageBuilder builder;
#pragma warning restore

            using (resizer = new ImageResizer(_paths, _size, _data.LoadedImage))
            {
                resizer.Resize();

                UpdateProgress(5, "Computing color information...");
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
            FinishedImage = _data.LoadedImage;
            Utility.SetEnabled(Btn_OK, true);
        }

        private void Btn_Cancel_Click(object sender, System.EventArgs e)
        {
            BW_Builder.CancelAsync();
        }

        #endregion

        private void Init()
        {
            Utility.SetEnabled(Btn_OK, false);

            BW_Builder.ProgressChanged +=
                new ProgressChangedEventHandler(BW_Builder_ProgressChanged);
            BW_Builder.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Builder_RunWorkCompleted);

            BW_Builder.RunWorkerAsync();
        }

        private void UpdateProgress(int val, string text)
        {
            Invoke(new Action(() =>
            {
                _progress += val;
                BW_Builder.ReportProgress(_progress);
                Label_Progress.Text = text;
            }));
        }
    }
}