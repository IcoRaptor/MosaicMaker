namespace MosaicMakerNS
{
    partial class MosaicMaker
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MosaicMaker));
            this.Btn_LoadImage = new System.Windows.Forms.Button();
            this.Btn_Generate = new System.Windows.Forms.Button();
            this.Label_Image = new System.Windows.Forms.Label();
            this.Btn_LoadFolder = new System.Windows.Forms.Button();
            this.Label_Folder = new System.Windows.Forms.Label();
            this.Checked_Elements = new System.Windows.Forms.CheckedListBox();
            this.Picture_Preview = new System.Windows.Forms.PictureBox();
            this.Picture_Loaded = new System.Windows.Forms.PictureBox();
            this.Copyright = new System.Windows.Forms.Label();
            this.Panel_Sizes = new System.Windows.Forms.GroupBox();
            this.Radio_3 = new System.Windows.Forms.RadioButton();
            this.Radio_2 = new System.Windows.Forms.RadioButton();
            this.Radio_1 = new System.Windows.Forms.RadioButton();
            this.Label_Size = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.BW_Main = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Loaded)).BeginInit();
            this.Panel_Sizes.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_LoadImage
            // 
            this.Btn_LoadImage.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_LoadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_LoadImage.FlatAppearance.BorderSize = 0;
            this.Btn_LoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_LoadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_LoadImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_LoadImage.Location = new System.Drawing.Point(50, 50);
            this.Btn_LoadImage.Name = "Btn_LoadImage";
            this.Btn_LoadImage.Size = new System.Drawing.Size(150, 30);
            this.Btn_LoadImage.TabIndex = 1;
            this.Btn_LoadImage.Text = "Load image";
            this.Btn_LoadImage.UseVisualStyleBackColor = false;
            this.Btn_LoadImage.Click += new System.EventHandler(this.Btn_LoadImage_Click);
            // 
            // Btn_Generate
            // 
            this.Btn_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Generate.BackColor = System.Drawing.Color.Crimson;
            this.Btn_Generate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generate.FlatAppearance.BorderSize = 0;
            this.Btn_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Generate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_Generate.Location = new System.Drawing.Point(754, 386);
            this.Btn_Generate.Name = "Btn_Generate";
            this.Btn_Generate.Size = new System.Drawing.Size(200, 100);
            this.Btn_Generate.TabIndex = 4;
            this.Btn_Generate.Text = "Generate\r\nMosaic";
            this.Btn_Generate.UseVisualStyleBackColor = false;
            this.Btn_Generate.Click += new System.EventHandler(this.Btn_Generate_Click);
            // 
            // Label_Image
            // 
            this.Label_Image.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Image.Location = new System.Drawing.Point(48, 83);
            this.Label_Image.MaximumSize = new System.Drawing.Size(198, 15);
            this.Label_Image.Name = "Label_Image";
            this.Label_Image.Size = new System.Drawing.Size(198, 15);
            this.Label_Image.TabIndex = 0;
            this.Label_Image.Text = "No image loaded...";
            // 
            // Btn_LoadFolder
            // 
            this.Btn_LoadFolder.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_LoadFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_LoadFolder.FlatAppearance.BorderSize = 0;
            this.Btn_LoadFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_LoadFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_LoadFolder.Location = new System.Drawing.Point(50, 145);
            this.Btn_LoadFolder.Name = "Btn_LoadFolder";
            this.Btn_LoadFolder.Size = new System.Drawing.Size(150, 30);
            this.Btn_LoadFolder.TabIndex = 2;
            this.Btn_LoadFolder.Text = "Load folder";
            this.Btn_LoadFolder.UseVisualStyleBackColor = false;
            this.Btn_LoadFolder.Click += new System.EventHandler(this.Btn_LoadFolder_Click);
            // 
            // Label_Folder
            // 
            this.Label_Folder.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Folder.Location = new System.Drawing.Point(48, 182);
            this.Label_Folder.MaximumSize = new System.Drawing.Size(198, 15);
            this.Label_Folder.Name = "Label_Folder";
            this.Label_Folder.Size = new System.Drawing.Size(198, 15);
            this.Label_Folder.TabIndex = 0;
            this.Label_Folder.Text = "No folder loaded...";
            // 
            // Checked_Elements
            // 
            this.Checked_Elements.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Checked_Elements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Checked_Elements.CheckOnClick = true;
            this.Checked_Elements.ColumnWidth = 210;
            this.Checked_Elements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Checked_Elements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Checked_Elements.FormattingEnabled = true;
            this.Checked_Elements.Location = new System.Drawing.Point(50, 261);
            this.Checked_Elements.MultiColumn = true;
            this.Checked_Elements.Name = "Checked_Elements";
            this.Checked_Elements.Size = new System.Drawing.Size(420, 225);
            this.Checked_Elements.TabIndex = 3;
            this.Checked_Elements.SelectedIndexChanged += new System.EventHandler(this.Checked_Elements_SelectedIndexChanged);
            // 
            // Picture_Preview
            // 
            this.Picture_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Picture_Preview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Picture_Preview.Location = new System.Drawing.Point(534, 50);
            this.Picture_Preview.Name = "Picture_Preview";
            this.Picture_Preview.Size = new System.Drawing.Size(420, 260);
            this.Picture_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Preview.TabIndex = 7;
            this.Picture_Preview.TabStop = false;
            // 
            // Picture_Loaded
            // 
            this.Picture_Loaded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Picture_Loaded.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Picture_Loaded.Location = new System.Drawing.Point(270, 50);
            this.Picture_Loaded.Name = "Picture_Loaded";
            this.Picture_Loaded.Size = new System.Drawing.Size(201, 125);
            this.Picture_Loaded.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Loaded.TabIndex = 8;
            this.Picture_Loaded.TabStop = false;
            // 
            // Copyright
            // 
            this.Copyright.AutoSize = true;
            this.Copyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Copyright.Location = new System.Drawing.Point(929, 515);
            this.Copyright.Name = "Copyright";
            this.Copyright.Size = new System.Drawing.Size(67, 13);
            this.Copyright.TabIndex = 9;
            this.Copyright.Text = "© 2017 - Ico";
            // 
            // Panel_Sizes
            // 
            this.Panel_Sizes.Controls.Add(this.Radio_3);
            this.Panel_Sizes.Controls.Add(this.Radio_2);
            this.Panel_Sizes.Controls.Add(this.Radio_1);
            this.Panel_Sizes.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Panel_Sizes.Location = new System.Drawing.Point(534, 381);
            this.Panel_Sizes.Name = "Panel_Sizes";
            this.Panel_Sizes.Size = new System.Drawing.Size(155, 105);
            this.Panel_Sizes.TabIndex = 0;
            this.Panel_Sizes.TabStop = false;
            this.Panel_Sizes.Text = "Mosaic Element Sizes";
            // 
            // Radio_3
            // 
            this.Radio_3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_3.Location = new System.Drawing.Point(19, 79);
            this.Radio_3.Name = "Radio_3";
            this.Radio_3.Size = new System.Drawing.Size(70, 17);
            this.Radio_3.TabIndex = 2;
            this.Radio_3.TabStop = true;
            this.Radio_3.Text = "64 x 64";
            this.Radio_3.UseVisualStyleBackColor = true;
            // 
            // Radio_2
            // 
            this.Radio_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_2.Location = new System.Drawing.Point(19, 49);
            this.Radio_2.Name = "Radio_2";
            this.Radio_2.Size = new System.Drawing.Size(70, 17);
            this.Radio_2.TabIndex = 1;
            this.Radio_2.TabStop = true;
            this.Radio_2.Text = "32 x 32";
            this.Radio_2.UseVisualStyleBackColor = true;
            // 
            // Radio_1
            // 
            this.Radio_1.Checked = true;
            this.Radio_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_1.Location = new System.Drawing.Point(19, 19);
            this.Radio_1.Name = "Radio_1";
            this.Radio_1.Size = new System.Drawing.Size(70, 17);
            this.Radio_1.TabIndex = 0;
            this.Radio_1.TabStop = true;
            this.Radio_1.Text = "16 x 16";
            this.Radio_1.UseVisualStyleBackColor = true;
            // 
            // Label_Size
            // 
            this.Label_Size.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Size.Location = new System.Drawing.Point(270, 182);
            this.Label_Size.Name = "Label_Size";
            this.Label_Size.Size = new System.Drawing.Size(201, 13);
            this.Label_Size.TabIndex = 0;
            this.Label_Size.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Save.Location = new System.Drawing.Point(644, 316);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(200, 40);
            this.Btn_Save.TabIndex = 5;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // BW_Main
            // 
            this.BW_Main.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_Main_DoWork);
            // 
            // MosaicMaker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1008, 537);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Label_Size);
            this.Controls.Add(this.Panel_Sizes);
            this.Controls.Add(this.Copyright);
            this.Controls.Add(this.Picture_Loaded);
            this.Controls.Add(this.Picture_Preview);
            this.Controls.Add(this.Checked_Elements);
            this.Controls.Add(this.Label_Folder);
            this.Controls.Add(this.Btn_LoadFolder);
            this.Controls.Add(this.Label_Image);
            this.Controls.Add(this.Btn_Generate);
            this.Controls.Add(this.Btn_LoadImage);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MosaicMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mosaic Maker - Alpha";
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Loaded)).EndInit();
            this.Panel_Sizes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_LoadImage;
        private System.Windows.Forms.Button Btn_Generate;
        private System.Windows.Forms.Label Label_Image;
        private System.Windows.Forms.Button Btn_LoadFolder;
        private System.Windows.Forms.Label Label_Folder;
        private System.Windows.Forms.CheckedListBox Checked_Elements;
        private System.Windows.Forms.PictureBox Picture_Preview;
        private System.Windows.Forms.PictureBox Picture_Loaded;
        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.GroupBox Panel_Sizes;
        private System.Windows.Forms.RadioButton Radio_3;
        private System.Windows.Forms.RadioButton Radio_2;
        private System.Windows.Forms.RadioButton Radio_1;
        private System.Windows.Forms.Label Label_Size;
        private System.Windows.Forms.Button Btn_Save;
        private System.ComponentModel.BackgroundWorker BW_Main;
    }
}

