using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public partial class MainWindow : Form
    {
        #region Variables

        private const int _PNG = 1;
        private const int _JPG = 2;
        private const int _BMP = 3;
        private const int _TIF = 4;

        private const string _FORMAT_ERROR = "File format is not supported!";

        private const string _ERROR = "An error occurred!\n\n";
        private const string _ERROR_2 = "Please check the properties of the file";
        private const string _ERROR_3 = "Please check the properties of the missing files";
        private const string _TRY_AGAIN = "\nand try again!";

        private const string _SAVE_ERROR = "Image could not be saved!";
        private const string _SAVE_SUCCESS = "Image saved successfully!";

        private const string _FILTER = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|TIFF|*.tif";

        private const string _LABEL_FOLDER = "No folder loaded...";

        private readonly Dictionary<string, string> _nameToPath =
            new Dictionary<string, string>();

        private List<string> _folderPaths = new List<string>();
        private int _folderCount;

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

        private bool _Btn_Clear_Enable
        {
            get { return Checked_Elements.Items.Count > 0; }
        }

        private bool _Btn_Folder_Enable
        {
            get { return _folderCount < 5; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            InitBackgroundWorker();

            Menu_Strip.Renderer = new MenuStripRenderer();

            Utility.SetEnabled(Btn_Generate, false, Actions_Generate);
            Utility.SetEnabled(Btn_Save, false, Actions_Save);
            Utility.SetEnabled(Btn_Clear, false, Actions_Clear);
        }

        #endregion

        #region UI

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                if (IsValidImageType(dialog.FileName))
                    LoadImage(dialog.FileName, dialog.SafeFileName);
            }

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable, Actions_Generate);
        }

        private void Btn_AddFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                if (_folderPaths.Contains(dialog.SelectedPath))
                    return;

                ++_folderCount;
                _folderPaths.Add(dialog.SelectedPath);

                string name = new DirectoryInfo(_folderPaths.GetLast()).Name;

                Label_Folder.Text = Label_Folder.Text == _LABEL_FOLDER ?
                    name : string.Concat(Label_Folder.Text, "\n", name);
            }

            Utility.SetEnabled(Btn_AddFolder, _Btn_Folder_Enable, Actions_AddFolder);

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            _folderCount = 0;
            _folderPaths.Clear();
            _nameToPath.Clear();
            Checked_Elements.Items.Clear();

            Label_Folder.Text = _LABEL_FOLDER;

            Utility.SetEnabled(Btn_Clear, false, Actions_Clear);
            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable, Actions_Generate);
            Utility.SetEnabled(Btn_AddFolder, _Btn_Folder_Enable, Actions_AddFolder);
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData mData = new MosaicData(
                Checked_Elements.CheckedItems, _nameToPath,
                Utility.GetElementSize(Radio_8, Radio_16, Radio_32, Radio_64),
                (Bitmap)Picture_Loaded.Image);

            using (ProgressDialog dialog = new ProgressDialog(mData))
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ReplaceImage(Picture_Preview, dialog.MosaicImage);
            }

            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable, Actions_Save);
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
            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable, Actions_Generate);
        }

        #endregion

        #region Menu

        private void Actions_LoadImage_Click(object sender, EventArgs e)
        {
            Btn_LoadImage_Click(sender, e);
        }

        private void Actions_AddFolder_Click(object sender, EventArgs e)
        {
            Btn_AddFolder_Click(sender, e);
        }

        private void Actions_Clear_Click(object sender, EventArgs e)
        {
            Btn_Clear_Click(sender, e);
        }

        private void Actions_Generate_Click(object sender, EventArgs e)
        {
            Btn_Generate_Click(sender, e);
        }

        private void Actions_Save_Click(object sender, EventArgs e)
        {
            Btn_Save_Click(sender, e);
        }

        #endregion

        #region Background

        private void BW_Main_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] paths = Directory.GetFiles(
                _folderPaths.GetLast(), @"*.*", SearchOption.AllDirectories);

            ProcessPaths(paths);
        }

        private void BW_Main_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable, Actions_Generate);
            Utility.SetEnabled(Btn_Clear, _Btn_Clear_Enable, Actions_Clear);
        }

        private void ProcessPaths(string[] paths)
        {
            int errors = 0;

            foreach (var path in paths)
            {
                if (!IsValidImageType(path, ref errors))
                    continue;

                string name = new DirectoryInfo(path).Name;

                if (_nameToPath.ContainsKey(name))
                    continue;

                _nameToPath.Add(name, path);

                Invoke(new Action(() =>
                {
                    Checked_Elements.Items.Add(name, true);
                }));
            }

            if (errors > 0)
                ShowErrorReport(errors);
        }

        private static void ShowErrorReport(int errorCounter)
        {
            string msg = string.Empty;

            if (errorCounter == 1)
                msg = string.Concat(_ERROR, _ERROR_2, _TRY_AGAIN);
            else
            {
                msg = string.Concat(errorCounter,
                    " errors occurred!\n\n", _ERROR_3, _TRY_AGAIN);
            }

            MessageBox.Show(msg);
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

        private static bool IsValidImageType(string path)
        {
            ImageType type = Utility.GetImageType(path);

            if (type == ImageType.Unknown)
            {
                MessageBox.Show(_FORMAT_ERROR);
                return false;
            }
            else if (type == ImageType.Error)
            {
                MessageBox.Show(string.Concat(_ERROR, _ERROR_2, _TRY_AGAIN));
                return false;
            }

            return true;
        }

        private static bool IsValidImageType(string path, ref int errorCounter)
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
            using (FileStream stream = Utility.GetFileStream(fileName))
            {
                Image img = Image.FromStream(stream);

                ReplaceImage(Picture_Loaded, img);

                Label_Size.Text = img.Size.ToString();
                Label_Image.Text = safeFileName;
            }

            Tools_Extract.Enabled = Picture_Loaded.Image != null;
        }

        private void Save(string fileName, ImageFormat format)
        {
            string msg = _SAVE_SUCCESS;

            try
            {
                Picture_Preview.Image.Save(fileName, format);
            }
            catch (ExternalException e)
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

        private void InitBackgroundWorker()
        {
            BW_Main.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Main_RunWorkerCompleted);
        }
    }
}