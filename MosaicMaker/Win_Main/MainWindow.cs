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

        private readonly Dictionary<string, string> _nameToPath =
            new Dictionary<string, string>();

        private List<string> _folderPaths = new List<string>();

        #endregion

        #region Properties

        private bool _Btn_Generate_Enable
        {
            get
            {
                return Picture_Loaded.Image != null &&
                    (Checked_Elements.CheckedItems.Count > 0 ||
                    Settings.Pixelate);
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
            get { return _folderPaths.Count < 5; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            InitBackgroundWorker();

            Menu_Strip.Renderer = new MenuStripRenderer();

            Utility.SetEnabled(Btn_Generate, Actions_Generate, false);
            Utility.SetEnabled(Btn_Save, Actions_Save, false);
            Utility.SetEnabled(Btn_Clear, Actions_Clear, false);
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

            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
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

                _folderPaths.Add(dialog.SelectedPath);

                string name = new DirectoryInfo(_folderPaths.GetLast()).Name;

                Label_Folder.Text = Label_Folder.Text == Strings.LabelFolder ?
                    name : string.Concat(Label_Folder.Text, "\n", name);
            }

            Utility.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            _folderPaths.Clear();
            _nameToPath.Clear();
            Checked_Elements.Items.Clear();

            Label_Folder.Text = Strings.LabelFolder;

            Utility.SetEnabled(Btn_Clear, Actions_Clear, false);
            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            Utility.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData mData = new MosaicData(
                Checked_Elements.CheckedItems, _nameToPath,
                Utility.GetElementSize(Picture_Loaded.Image.Size,
                    Radio_1, Radio_4, Radio_8, Radio_16, Radio_32, Radio_64),
                (Bitmap)Picture_Loaded.Image);

            using (ProgressDialog dialog = new ProgressDialog(mData))
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ReplaceImage(Picture_Preview, dialog.MosaicImage);
            }

            Utility.SetEnabled(Btn_Save, Actions_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = Strings.Filter;

                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ImageFormat format = GetImageFormat(dialog.FilterIndex);
                Save(dialog.FileName, format);
            }
        }

        private void Checked_Elements_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
        }

        private void Radio_1_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(0, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_4_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(1, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_8_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(2, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_16_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(3, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_32_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(4, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_64_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SingleCheck(5, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
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

        private void Actions_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit(new CancelEventArgs());
        }

        private void Size_1_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(0, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_1.Checked = true;
        }

        private void Size_4_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(1, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_4.Checked = true;
        }

        private void Size_8_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(2, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_8.Checked = true;
        }

        private void Size_16_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(3, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_16.Checked = true;
        }

        private void Size_32_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(4, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_32.Checked = true;
        }

        private void Size_64_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(5, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_64.Checked = true;
        }

        private void Mirror_Default_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(0, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Default);
        }

        private void Mirror_Horizontal_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(1, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Horizontal);
        }

        private void Mirror_Vertical_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(2, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Vertical);
        }

        private void Mirror_Full_Click(object sender, EventArgs e)
        {
            Utility.SingleCheck(3, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Full);
        }

        private void Options_Negative_Click(object sender, EventArgs e)
        {
            Options_Negative.Checked = !Options_Negative.Checked;

            Settings.ToggleNegativeImage();
        }

        private void Pixelate_Image_Click(object sender, EventArgs e)
        {
            bool check = !Pixelate_Image.Checked;
            Pixelate_Image.Checked = check;

            if (check)
                Utility.SingleCheck(0, Pixelate_Image, Pixelate_Strip);

            Settings.TogglePixelImage();
            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
        }

        private void Pixelate_Strip_Click(object sender, EventArgs e)
        {
            bool check = !Pixelate_Strip.Checked;
            Pixelate_Strip.Checked = check;

            if (check)
                Utility.SingleCheck(1, Pixelate_Image, Pixelate_Strip);

            Settings.TogglePixelStrip();
            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
        }

        private void Help_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Strings.About);
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
            Utility.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            Utility.SetEnabled(Btn_Clear, Actions_Clear, _Btn_Clear_Enable);
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
            {
                msg = string.Concat(Strings.Error, Strings.Error2,
                    Strings.TryAgain);
            }
            else
            {
                msg = string.Concat(errorCounter,
                    " errors occurred!\n\n", Strings.Error3, Strings.TryAgain);
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
                MessageBox.Show(Strings.FormatError);
                return false;
            }
            else if (type == ImageType.Error)
            {
                MessageBox.Show(string.Concat(Strings.Error, Strings.Error2,
                    Strings.TryAgain));
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

        private void InitBackgroundWorker()
        {
            BW_Main.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Main_RunWorkerCompleted);
        }

        private void LoadImage(string fileName, string safeFileName)
        {
            using (FileStream stream = Utility.GetFileStream(fileName))
            {
                Image img = Image.FromStream(stream);

                ReplaceImage(Picture_Loaded, img);

                Label_Size.Text = string.Concat(img.Size.Width, " x ", img.Size.Height);
                Label_Image.Text = safeFileName;
            }
        }

        private void Save(string fileName, ImageFormat format)
        {
            string msg = Strings.SaveSuccess;

            try
            {
                Picture_Preview.Image.Save(fileName, format);
            }
            catch (ExternalException e)
            {
                msg = string.Concat(Strings.SaveError, "\n\n",
                    e.Message);
            }
            catch
            {
                msg = Strings.SaveError;
            }

            MessageBox.Show(msg);
        }
    }
}