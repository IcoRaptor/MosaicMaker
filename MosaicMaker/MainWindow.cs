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

        private volatile Dictionary<string, string> _namePath =
            new Dictionary<string, string>();

        private volatile string _folderPath = null;

        #endregion

        #region Constructor

        public MosaicMaker()
        {
            InitializeComponent();

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable);
        }

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
                MessageBox.Show("Selected file is not an image!");
                return;
            }
            else if (type == ImageType.ERROR)
            {
                MessageBox.Show("Error when opening file!");
                return;
            }

            Label_Image.Text = dialog.SafeFileName;
            Image image = Image.FromFile(dialog.FileName);
            Picture_Loaded.Image = image;
            Label_Size.Text = image.Size.ToString();

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        private void Btn_LoadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            ClearItems();

            _folderPath = dialog.SelectedPath;
            Label_Folder.Text = new DirectoryInfo(_folderPath).Name;

            Thread thread = new Thread(GetElementPaths);
            thread.Start();
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            Picture_Preview.Image = Picture_Loaded.Image;

            Thread thread = new Thread(new MosaicBuilder(
                    Checked_Elements.CheckedItems, _namePath).Start);
            thread.Start();

            Utility.SetEnabled(Btn_Save, _Btn_Save_Enable);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            #region Comment

            /*
            string savePath = string.Empty;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JPG|*.jpg|Bitmap|*.bmp|PNG|*.png"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            savePath = dialog.FileName;
            */

            #endregion
        }

        private void Checked_Elements_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.SetEnabled(Btn_Generate,
                Picture_Loaded.Image != null,
                Checked_Elements.CheckedItems.Count > 0);
        }

        #endregion

        /// <summary>
        /// ThreadStart: Gets all image paths from the loaded directory
        /// </summary>
        private void GetElementPaths()
        {
            string[] paths = Directory.GetFiles(
                _folderPath, @"*.*", SearchOption.AllDirectories);

            foreach (var path in paths)
            {
                ImageType type = Utility.GetImageType(path);
                if (type == ImageType.ERROR || type == ImageType.UNKNOWN)
                    continue;

                string name = new DirectoryInfo(path).Name;
                if (!_namePath.ContainsKey(name))
                    _namePath.Add(name, path);

                Checked_Elements.Items.Add(name, true);
            }

            Utility.SetEnabled(Btn_Generate, _Btn_Generate_Enable);
        }

        /// <summary>
        /// Clears the paths and the checked items
        /// </summary>
        private void ClearItems()
        {
            _namePath.Clear();
            Checked_Elements.Items.Clear();
            Label_Folder.Text = "No folder loaded...";
        }
    }
}