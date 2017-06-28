using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MosaicMaker
{
    public partial class MosaicMaker : Form
    {
        #region Variables

        private volatile Dictionary<string, string> _pathDict =
            new Dictionary<string, string>();

        private volatile string _folderPath = null;

        #endregion

        #region Constructor

        public MosaicMaker()
        {
            InitializeComponent();

            CheckSetEnabled(Btn_Generate,
                Picture_Loaded.Image != null,
                Checked_Elements.Items.Count > 0);
        }

        #endregion

        #region Buttons

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            ImageType type = Utility.GetImageType(dialog.FileName);

            if (type == ImageType.UNKNOWN)
            {
                MessageBox.Show("Selected file is not an image!");
                return;
            }
            else if (type == ImageType.ERROR)
            {
                MessageBox.Show("Error when opening file!");
                return;
            }

            Label_Image.Text = dialog.SafeFileName;
            Image image = Image.FromFile(dialog.FileName, true);
            Picture_Loaded.Image = image;
            Label_Size.Text = image.Size.ToString();

            CheckSetEnabled(Btn_Generate,
                Picture_Loaded.Image != null,
                Checked_Elements.Items.Count > 0);
        }

        private void Btn_LoadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            ClearImages();

            _folderPath = dialog.SelectedPath;
            Label_Folder.Text = new DirectoryInfo(_folderPath).Name;

            Thread thread = new Thread(GetElements);
            thread.Start();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            string savePath = string.Empty;

            // TEST_START

            if (Progress_Generate.Value == 0)
            {
                /*
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "JPG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png"
                };

                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                savePath = dialog.FileName;
                */

                Progress_Generate.Value = 100;
                Picture_Preview.Image = Picture_Loaded.Image;
            }
            else
            {
                Progress_Generate.Value = 0;
                Label_Size.Text = string.Empty;

                if (Picture_Preview.Image != null)
                {
                    Picture_Preview.Image.Dispose();
                    Picture_Preview.Image = null;
                }

                if (Picture_Loaded.Image != null)
                {
                    Picture_Loaded.Image.Dispose();
                    Picture_Loaded.Image = null;
                }
            }

            Label_Percent.Text = string.Concat(
                Progress_Generate.Value.ToString(), "%");

            // TEST_END

            ClearImages();
            CheckSetEnabled(Btn_Generate,
                Picture_Loaded.Image != null,
                Checked_Elements.Items.Count > 0);
        }

        #endregion

        /// <summary>
        /// ThreadStart: Gets all images from the loaded directory
        /// </summary>
        private void GetElements()
        {
            string[] paths = Directory.GetFiles(
                _folderPath, @"*.*", SearchOption.AllDirectories);

            foreach (var path in paths)
            {
                ImageType type = Utility.GetImageType(path);
                if (type == ImageType.ERROR || type == ImageType.UNKNOWN)
                    continue;

                try
                {
                    string name = new DirectoryInfo(path).Name;
                    _pathDict.Add(name, path);
                    Checked_Elements.Items.Add(name, true);
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Too many images!\nSearch stopped!");
                    return;
                }
            }

            CheckSetEnabled(Btn_Generate,
                Picture_Loaded.Image != null,
                Checked_Elements.Items.Count > 0);
        }

        /// <summary>
        /// Frees the image resources
        /// </summary>
        private void ClearImages()
        {
            Checked_Elements.Items.Clear();
            Label_Folder.Text = "No folder loaded...";
        }

        /// <summary>
        /// Sets the buttons enabled property according to the given conditions
        /// </summary>
        private void CheckSetEnabled(Button btn, params bool[] conditons)
        {
            bool enabled = true;
            foreach (var con in conditons)
                enabled = enabled && con;

            btn.Enabled = enabled;
            btn.BackColor = enabled ?
                Color.FromArgb(255, btn.BackColor) :
                Color.FromArgb(155, btn.BackColor);
        }
    }
}