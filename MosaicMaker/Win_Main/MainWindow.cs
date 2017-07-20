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
        private const int _TIF = 4;

        private const string _ERROR_MSG = "An error occurred!\n\n" +
                    "Please check if the file is used by a program.";

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
                MessageBox.Show("File format is not supported!");
                return;
            }
            else if (type == ImageType.ERROR)
            {
                MessageBox.Show(_ERROR_MSG);
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
                (Bitmap)Picture_Loaded.Image);

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
                Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp|TIFF|*.tif"
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
            int errorCounter = 0;

            foreach (var p in paths)
            {
                ImageType type = Utility.GetImageType(p);

                if (type == ImageType.UNKNOWN)
                    continue;
                else if (type == ImageType.ERROR)
                {
                    ++errorCounter;
                    continue;
                }

                string name = new DirectoryInfo(p).Name;
                if (!_namePathDict.ContainsKey(name))
                    _namePathDict.Add(name, p);

                Invoke(new Action(() =>
                {
                    Checked_Elements.Items.Add(name, true);
                }));
            }

            if (errorCounter > 0)
                ShowErrorReport(errorCounter);
        }

        private void ShowErrorReport(int errorCounter)
        {
            if (errorCounter == 1)
                MessageBox.Show(_ERROR_MSG);
            else
            {
                string msg = string.Concat(errorCounter,
                    " errors occurred!\n\n",
                    "Please check if the missing files are used by a program.");
                MessageBox.Show(msg);
            }
        }

        #endregion

        private ImageFormat GetImageFormat(int filterIndex)
        {
            switch (filterIndex)
            {
                case _JPG:
                    return ImageFormat.Jpeg;

                case _PNG:
                    return ImageFormat.Png;

                case _BMP:
                    return ImageFormat.Bmp;

                case _TIF:
                    return ImageFormat.Tiff;
            }

            return null;
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