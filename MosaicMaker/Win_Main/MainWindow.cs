using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace MosaicMaker
{
    public partial class MosaicMaker : Form
    {
        #region Variables

        private Dictionary<string, string> _namePath =
            new Dictionary<string, string>();

        private string _folderPath = null;

        #endregion

        #region Properties

        private bool _Btn_Generate_Enable
        {
            get
            {
                return Picture_Loaded.Image != null &&
                    Checked_Elements.CheckedItems.Count > 0;
            }
        }

        private bool _Btn_Save_Enable
        {
            get { return Picture_Preview.Image != null; }
        }

        #endregion

        #region Constructors

        public MosaicMaker()
        {
            InitializeComponent();

            Utility.SetEnabled(Btn_Generate, false);
            Utility.SetEnabled(Btn_Save, false);
        }

        #endregion

        #region Elements

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            ImageType type = Utility.GetImageType(dialog.FileName);

            if (type == ImageType.UNKNOWN)
            {
                MessageBox.Show("Filetype is not supported!");
                return;
            }
            else if (type == ImageType.ERROR)
            {
                MessageBox.Show("An error occurred!");
                return;
            }

            Image image = Image.FromFile(dialog.FileName);
            Picture_Loaded.Image = image;

            Label_Size.Text = image.Size.ToString();
            Label_Image.Text = dialog.SafeFileName;

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        private void Btn_LoadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            _namePath.Clear();
            Checked_Elements.Items.Clear();

            _folderPath = dialog.SelectedPath;
            Label_Folder.Text = new DirectoryInfo(_folderPath).Name;

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData data = new MosaicData(
                Checked_Elements.CheckedItems, _namePath,
                Utility.GetElementSize(Radio_1, Radio_2, Radio_3),
                Picture_Loaded.Image as Bitmap);

            ProgressWindow pWin = new ProgressWindow(data);
            DialogResult res = pWin.ShowDialog();

            if (res != DialogResult.OK)
                return;

            Picture_Preview.Image = pWin.FinalImage;

            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string savePath = string.Empty;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JPEG|*.jpg;*.jpeg|PNG|*.png|Bitmap|*.bmp"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            savePath = dialog.FileName;
            ImageFormat format = null;

            switch (dialog.FilterIndex)
            {
                case 1:
                    format = ImageFormat.Jpeg;
                    break;

                case 2:
                    format = ImageFormat.Png;
                    break;

                case 3:
                    format = ImageFormat.Bmp;
                    break;
            }

            try
            {
                Picture_Preview.Image.Save(savePath, format);
            }
            catch
            {
                MessageBox.Show("Image could not be saved!");
                return;
            }

            MessageBox.Show("Image saved successfully!");
        }

        private void Checked_Elements_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        private void BW_Main_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] paths = Directory.GetFiles(
                _folderPath, @"*.*", SearchOption.AllDirectories);

            foreach (var p in paths)
            {
                ImageType type = Utility.GetImageType(p);
                if (type == ImageType.ERROR || type == ImageType.UNKNOWN)
                    continue;

                string name = new DirectoryInfo(p).Name;
                if (!_namePath.ContainsKey(name))
                    _namePath.Add(name, p);

                Invoke(new Action(() =>
                {
                    Checked_Elements.Items.Add(name, true);
                }));
            }

            Invoke(new Action(() =>
            {
                Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
            }));
        }

        #endregion
    }
}