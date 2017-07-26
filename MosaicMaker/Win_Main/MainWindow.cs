using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public partial class MosaicMaker : Form
    {
        #region Variables

        private const int _PNG = 1;
        private const int _JPG = 2;
        private const int _BMP = 3;
        private const int _TIF = 4;

        private const string _FILE_ERROR = "File format is not supported!";

        private const string _ERROR = "An error occurred!\n\n";
        private const string _ERROR_2 = "Please check the properties of the file!";
        private const string _ERROR_3 = "Please check the properties of the missing files!";

        private const string _SAVE_ERROR = "Image could not be saved!";
        private const string _SAVE_SUCCESS = "Image saved successfully!";

        private const string _FILTER = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|TIFF|*.tif";

        private Dictionary<string, string> _nameToPath =
            new Dictionary<string, string>();

        private string _folderPath;

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

        #region GUI

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                if (!CheckValidImageType(dialog.FileName))
                    return;

                LoadImage(dialog.FileName, dialog.SafeFileName);
            }

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        private void Btn_LoadFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                _nameToPath.Clear();
                Checked_Elements.Items.Clear();

                _folderPath = dialog.SelectedPath;
                Label_Folder.Text = new DirectoryInfo(_folderPath).Name;
            }

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData data = new MosaicData(
                Checked_Elements.CheckedItems, _nameToPath,
                Utility.GetElementSize(Radio_8, Radio_16, Radio_32, Radio_64),
                (Bitmap)Picture_Loaded.Image);

            using (ProgressWindow pWin = new ProgressWindow(data))
            {
                DialogResult result = pWin.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ReplaceImage(Picture_Preview, pWin.MosaicImage);
            }

            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = _FILTER;

                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ImageFormat format = GetImageFormat(dialog.FilterIndex);
                Save(dialog.FileName, format);
            }
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

            foreach (var path in paths)
            {
                if (!CheckValidImageType(path, ref errorCounter))
                    continue;

                string name = new DirectoryInfo(path).Name;
                _nameToPath.Add(name, path);

                Invoke(new Action(() =>
                {
                    Checked_Elements.Items.Add(name, true);
                }));
            }

            if (errorCounter > 0)
                ShowErrorReport(errorCounter);
        }

        private static void ShowErrorReport(int errorCounter)
        {
            if (errorCounter == 1)
                MessageBox.Show(string.Concat(_ERROR, _ERROR_2));
            else
            {
                string msg = string.Concat(errorCounter,
                    " errors occurred!\n\n", _ERROR_3);
                MessageBox.Show(msg);
            }
        }

        #endregion

        /// <summary>
        /// Default: PNG
        /// </summary>
        private static ImageFormat GetImageFormat(int filterIndex)
        {
            switch (filterIndex)
            {
                case _PNG:
                    return ImageFormat.Png;

                case _JPG:
                    return ImageFormat.Jpeg;

                case _BMP:
                    return ImageFormat.Bmp;

                case _TIF:
                    return ImageFormat.Tiff;

                default:
                    return ImageFormat.Png;
            }
        }

        private static bool CheckValidImageType(string path)
        {
            ImageType type = Utility.GetImageType(path);

            if (type == ImageType.Unknown)
            {
                MessageBox.Show(_FILE_ERROR);
                return false;
            }
            else if (type == ImageType.Error)
            {
                MessageBox.Show(string.Concat(_ERROR, _ERROR_2));
                return false;
            }

            return true;
        }

        private static bool CheckValidImageType(string path, ref int errorCounter)
        {
            ImageType type = Utility.GetImageType(path);

            if (type == ImageType.Unknown)
                return false;
            else if (type == ImageType.Error)
            {
                ++errorCounter;
                return false;
            }

            return true;
        }

        private static void ReplaceImage(PictureBox box, Image img)
        {
            if (box.Image != null)
                box.Image.Dispose();

            box.Image = img;
        }

        private void LoadImage(string fileName, string safeFileName)
        {
            using (FileStream stream = Utility.TryGetFileStream(fileName))
            {
                Image img = Image.FromStream(stream);

                ReplaceImage(Picture_Loaded, img);

                Label_Size.Text = img.Size.ToString();
                Label_Image.Text = safeFileName;
            }
        }

        private void Save(string path, ImageFormat format)
        {
            string msg = _SAVE_SUCCESS;

            try
            {
                Picture_Preview.Image.Save(path, format);
            }
            catch (System.Runtime.InteropServices.ExternalException e)
            {
                msg = string.Concat(_SAVE_ERROR, "\n\n",
                    e.Message);
            }
            catch
            {
                msg = _SAVE_ERROR;
            }

            MessageBox.Show(msg);
        }
    }
}