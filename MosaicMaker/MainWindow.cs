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

        private volatile List<Bitmap> _elements = new List<Bitmap>();
        private volatile string _folderPath = null;
        private string[] _paths = null;

        #endregion

        #region Constructor

        public MosaicMaker()
        {
            InitializeComponent();
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
        }

        private void Btn_LoadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            _folderPath = dialog.SelectedPath;
            Label_Folder.Text = new DirectoryInfo(_folderPath).Name;

            ClearImages();

            Thread thread = new Thread(GetElements);
            thread.Start();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            // TODO: SaveFileDialog

            // TEST_START

            if (Progress_Generate.Value == 0)
            {
                Progress_Generate.Value = 100;
                Picture_Preview.Image = Picture_Loaded.Image;
            }
            else
            {
                Progress_Generate.Value = 0;

                Label_Image.Text = "No image loaded...";
                Label_Size.Text = string.Empty;

                Picture_Preview.Image.Dispose();
                Picture_Preview.Image = null;
                Picture_Loaded.Image.Dispose();
                Picture_Loaded.Image = null;
            }

            Label_Percent.Text = string.Concat(
                Progress_Generate.Value.ToString(), "%");

            // TEST_END

            Label_Folder.Text = "No folder loaded...";

            ClearImages();
        }

        #endregion

        /// <summary>
        /// ThreadStart: Gets all images from the loaded directory
        /// </summary>
        private void GetElements()
        {
            _paths = Directory.GetFiles(
                _folderPath, @"*.*", SearchOption.AllDirectories);

            foreach (string path in _paths)
            {
                ImageType type = Utility.GetImageType(path);
                if (type == ImageType.ERROR || type == ImageType.UNKNOWN)
                    continue;

                try
                {
                    _elements.Add(Image.FromFile(path) as Bitmap);
                    Checked_Elements.Items.Add(
                        new DirectoryInfo(path).Name, true);
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Too many images!");
                    return;
                }
            }
        }

        /// <summary>
        /// Frees the image resources
        /// </summary>
        private void ClearImages()
        {
            foreach (Bitmap img in _elements)
                img.Dispose();

            Checked_Elements.Items.Clear();
            _elements.Clear();
        }
    }
}