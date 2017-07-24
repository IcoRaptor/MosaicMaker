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

        private const string _SAVE_ERROR = "Image could not be saved!";
        private const string _SAVE_SUCCESS = "Image saved successfully!";

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

        #region GUI

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ImageType type = Utility.GetImageType(dialog.FileName);

                if (type == ImageType.Unknown)
                {
                    MessageBox.Show(_FILE_ERROR);
                    return;
                }
                else if (type == ImageType.Error)
                {
                    MessageBox.Show(string.Concat(_ERROR, _ERROR_2));
                    return;
                }

                using (FileStream stream = Utility.TryGetFileStream(dialog.FileName))
                {
                    Image image = Image.FromStream(stream);

                    ReplaceImage(Picture_Loaded, image);

                    Label_Size.Text = image.Size.ToString();
                    Label_Image.Text = dialog.SafeFileName;
                }
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

                _namePath.Clear();
                Checked_Elements.Items.Clear();

                _folderPath = dialog.SelectedPath;
                Label_Folder.Text = new DirectoryInfo(_folderPath).Name;
            }

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData data = new MosaicData(
                Checked_Elements.CheckedItems, _namePath,
                Utility.GetElementSize(Radio_1, Radio_2, Radio_3),
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
            using (SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|TIFF|*.tif"
            })
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                Save(dialog.FileName, GetImageFormat(dialog.FilterIndex));
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

            foreach (var p in paths)
            {
                ImageType type = Utility.GetImageType(p);

                if (type == ImageType.Unknown)
                    continue;
                else if (type == ImageType.Error)
                {
                    ++errorCounter;
                    continue;
                }

                string name = new DirectoryInfo(p).Name;
                _namePath.Add(name, p);

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
                MessageBox.Show(string.Concat(_ERROR, _ERROR_2));
            else
            {
                string msg = string.Concat(errorCounter,
                    " errors occurred!\n\n", _ERROR_2);
                MessageBox.Show(msg);
            }
        }

        #endregion

        /// <summary>
        /// Default: PNG
        /// </summary>
        private ImageFormat GetImageFormat(int filterIndex)
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

        private void ReplaceImage(PictureBox box, Image image)
        {
            if (box.Image != null)
                box.Image.Dispose();

            box.Image = image;
        }
    }
}