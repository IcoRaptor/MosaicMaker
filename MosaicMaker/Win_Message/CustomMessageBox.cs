using System.Windows.Forms;

namespace MosaicMakerNS
{
    public partial class CustomMessageBox : Form
    {
        #region Variables

        private static CustomMessageBox box;

        #endregion

        #region Constructors

        private CustomMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        public static void Show(string msg)
        {
            using (box = new CustomMessageBox())
            {
                box.ShowDialog();
            }
        }
    }
}