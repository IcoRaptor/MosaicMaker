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

        private const int _JPG = 1;
        private const int _PNG = 2;
        private const int _BMP = 3;

        private Dictionary<string, string> _namePathDict =
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

            _namePathDict.Clear();
            Checked_Elements.Items.Clear();

            _folderPath = dialog.SelectedPath;
            Label_Folder.Text = new DirectoryInfo(_folderPath).Name;

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData data = new MosaicData(
                Checked_Elements.CheckedItems, _namePathDict,
                Utility.GetElementSize(Radio_1, Radio_2, Radio_3),
                Picture_Loaded.Image as Bitmap);

            ProgressWindow pWin = new ProgressWindow(data);
            DialogResult result = pWin.ShowDialog();

            if (result != DialogResult.OK)
                return;

            Picture_Preview.Image = pWin.FinalImage;

            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JPEG|*.jpg|PNG|*.png|Bitmap|*.bmp"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            Save(dialog.FileName, GetImageFormat(dialog.FilterIndex));
        }

        private void Checked_Elements_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        private void BW_Main_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] paths = Directory.GetFiles(
                _folderPath, @"*.*", SearchOption.AllDirectories);

            ProcessPaths(paths);

            Invoke(new Action(() =>
            {
                Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
            }));
        }

        #endregion

        #region Background

        private void ProcessPaths(string[] paths)
        {
            foreach (var p in paths)
            {
                ImageType type = Utility.GetImageType(p);
                if (type == ImageType.ERROR || type == ImageType.UNKNOWN)
                    continue;

                string name = new DirectoryInfo(p).Name;
                if (!_namePathDict.ContainsKey(name))
                    _namePathDict.Add(name, p);

                Invoke(new Action(() =>
                {
                    Checked_Elements.Items.Add(name, true);
                }));
            }
        }

        #endregion

        private ImageFormat GetImageFormat(int filterIndex)
        {
            ImageFormat format = null;

            switch (filterIndex)
            {
                case _JPG:
                    format = ImageFormat.Jpeg;
                    break;

                case _PNG:
                    format = ImageFormat.Png;
                    break;

                case _BMP:
                    format = ImageFormat.Bmp;
                    break;
            }

            return format;
        }

        private void Save(string path, ImageFormat format)
        {
            string msg = "Image saved successfully!";

            try
            {
                Picture_Preview.Image.Save(path, format);
            }
            catch (Exception e)
            {
                msg = string.Concat("Image could not be saved!\n",
                    e.Message);
            }

            MessageBox.Show(msg);
        }
    }
}