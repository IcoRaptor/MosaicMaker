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
    /// <summary>
    /// The main window
    /// </summary>
    public partial class MainWindow : Form
    {
        #region Variables

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
                    Settings.PixelMode);
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
            get
            {
                return _folderPaths.Count < 5 && !Settings.PixelMode;
            }
        }

        private static bool _Checked_Elements_Enable
        {
            get { return !Settings.PixelMode; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            InitBackgroundWorker();

            Menu_Strip.Renderer = new MenuStripRenderer();

            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, false);
            UIUtil.SetEnabled(Btn_Save, Actions_Save, false);
            UIUtil.SetEnabled(Btn_Clear, Actions_Clear, false);

            SetTitle();
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

            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
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

            UIUtil.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);

            BW_Main.RunWorkerAsync();
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            _folderPaths.Clear();
            _nameToPath.Clear();
            Checked_Elements.Items.Clear();

            Label_Folder.Text = Strings.LabelFolder;

            UIUtil.SetEnabled(Btn_Clear, Actions_Clear, false);
            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            UIUtil.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            MosaicData mData = new MosaicData(
                Checked_Elements.CheckedItems, _nameToPath,
                SizeUtil.GetElementSize(Picture_Loaded.Image.Size,
                    Radio_1, Radio_4, Radio_8, Radio_16, Radio_32, Radio_64),
                (Bitmap)Picture_Loaded.Image);

            using (ProgressDialog dialog = new ProgressDialog(mData))
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                ReplaceImage(Picture_Preview, dialog.MosaicImage);
            }

            UIUtil.SetEnabled(Btn_Save, Actions_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = Strings.Filter;

                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                FormatResolver resolver = new FormatResolver(dialog.FilterIndex);
                Save(dialog.FileName, resolver.Format);
            }
        }

        private void Checked_Elements_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
        }

        private void Radio_1_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(0, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_4_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(1, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_8_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(2, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_16_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(3, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_32_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(4, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
        }

        private void Radio_64_CheckedChanged(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(5, Size_1, Size_4, Size_8,
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
            UIUtil.SingleCheck(0, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_1.Checked = true;
        }

        private void Size_4_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(1, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_4.Checked = true;
        }

        private void Size_8_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(2, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_8.Checked = true;
        }

        private void Size_16_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(3, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_16.Checked = true;
        }

        private void Size_32_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(4, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_32.Checked = true;
        }

        private void Size_64_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(5, Size_1, Size_4, Size_8,
                Size_16, Size_32, Size_64);
            Radio_64.Checked = true;
        }

        private void Mirror_Default_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(0, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Default);
        }

        private void Mirror_Horizontal_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(1, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Horizontal);
        }

        private void Mirror_Vertical_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(2, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Vertical);
        }

        private void Mirror_Full_Click(object sender, EventArgs e)
        {
            UIUtil.SingleCheck(3, Mirror_Default, Mirror_Horizontal,
                Mirror_Vertical, Mirror_Full);

            Settings.SetMirrorMode(MirrorMode.Full);
        }

        private void Pixelate_Image_Click(object sender, EventArgs e)
        {
            Pixelate_Image.Checked = !Pixelate_Image.Checked;

            if (Pixelate_Image.Checked)
                UIUtil.SingleCheck(0, Pixelate_Image, Pixelate_Strip);

            Settings.TogglePixelImage();
            UIUtil.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);
            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            UIUtil.SetEnabled(Checked_Elements, _Checked_Elements_Enable);

            SetTitle();
        }

        private void Pixelate_Strip_Click(object sender, EventArgs e)
        {
            Pixelate_Strip.Checked = !Pixelate_Strip.Checked;

            if (Pixelate_Strip.Checked)
                UIUtil.SingleCheck(1, Pixelate_Image, Pixelate_Strip);

            Settings.TogglePixelStrip();
            UIUtil.SetEnabled(Btn_AddFolder, Actions_AddFolder, _Btn_Folder_Enable);
            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            UIUtil.SetEnabled(Checked_Elements, _Checked_Elements_Enable);

            SetTitle();
        }

        private void Options_Negative_Click(object sender, EventArgs e)
        {
            Options_Negative.Checked = !Options_Negative.Checked;

            Settings.ToggleNegativeImage();
        }

        private void Options_Gray_Click(object sender, EventArgs e)
        {
            Options_Gray.Checked = !Options_Gray.Checked;

            Settings.ToggleGrayscaleImage();
        }

        private void Help_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Strings.About);
        }

        #endregion

        #region Background

        /// <summary>
        /// Retrieves all files from the most recently added path
        /// </summary>
        private void BW_Main_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] paths = Directory.GetFiles(
                _folderPaths.GetLast(), @"*.*", SearchOption.AllDirectories);

            ProcessPaths(paths);
        }

        private void BW_Main_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UIUtil.SetEnabled(Btn_Generate, Actions_Generate, _Btn_Generate_Enable);
            UIUtil.SetEnabled(Btn_Clear, Actions_Clear, _Btn_Clear_Enable);
        }

        /// <summary>
        /// Checks if the given files are images and adds them to the dictionary
        ///  and the CheckedListBox
        /// </summary>
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

            ShowErrorReport(errors);
        }

        /// <summary>
        /// Shows a MessageBox if errors happend during ProcessPaths
        /// </summary>
        private static void ShowErrorReport(int errors)
        {
            if (errors == 0)
                return;

            string msg = string.Empty;

            if (errors == 1)
            {
                msg = string.Concat(Strings.Error, Strings.Error2,
                    Strings.TryAgain);
            }
            else
            {
                msg = string.Concat(errors, " errors occurred!\n\n",
                    Strings.Error3, Strings.TryAgain);
            }

            MessageBox.Show(msg);
        }

        #endregion

        /// <summary>
        /// Checks the ImageTyoe of a file.
        ///  Shows a MessageBox if the type is Error or Unknown
        /// </summary>
        private static bool IsValidImageType(string path)
        {
            ImageType type = ImageTypeEvaluator.GetImageType(path);

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

        /// <summary>
        /// Checks the ImageType of a file.
        ///  Increases the errorCounter if the type is Error
        /// </summary>
        private static bool IsValidImageType(string path, ref int errorCounter)
        {
            ImageType type = ImageTypeEvaluator.GetImageType(path);

            if (type == ImageType.Unknown)
                return false;

            if (type == ImageType.Error)
            {
                ++errorCounter;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Disposes the old image in the PictureBox
        ///  and sets the new one
        /// </summary>
        private static void ReplaceImage(PictureBox box, Image img)
        {
            if (box.Image != null)
                box.Image.Dispose();

            box.Image = img;
        }

        /// <summary>
        /// Adds the handlers to the BackgroundWorker
        /// </summary>
        private void InitBackgroundWorker()
        {
            BW_Main.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BW_Main_RunWorkerCompleted);
        }

        /// <summary>
        /// Sets the title of the window
        /// </summary>
        private void SetTitle()
        {
            string title = Settings.PixelMode ?
                string.Concat(Strings.Title, Strings.PixelMode) :
                string.Concat(Strings.Title, Strings.MosaicMode);

            Text = title;
        }

        /// <summary>
        /// Gets the image from the path, loads it into Picture_Loaded
        ///  and sets the labels accordingly
        /// </summary>
        private void LoadImage(string fileName, string safeFileName)
        {
            using (FileStream stream = StreamUtil.GetFileStream(fileName))
            {
                Image img = Image.FromStream(stream);

                ReplaceImage(Picture_Loaded, img);

                Label_Size.Text = string.Concat(img.Width, " x ", img.Height);
                Label_Image.Text = safeFileName;
            }
        }

        /// <summary>
        /// Saves the image in Picture_Preview to the given path
        /// </summary>
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