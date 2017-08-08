namespace MosaicMakerNS
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.Btn_LoadImage = new System.Windows.Forms.Button();
            this.Btn_Generate = new System.Windows.Forms.Button();
            this.Label_Image = new System.Windows.Forms.Label();
            this.Btn_AddFolder = new System.Windows.Forms.Button();
            this.Label_Folder = new System.Windows.Forms.Label();
            this.Checked_Elements = new System.Windows.Forms.CheckedListBox();
            this.Picture_Preview = new System.Windows.Forms.PictureBox();
            this.Picture_Loaded = new System.Windows.Forms.PictureBox();
            this.Panel_Sizes = new System.Windows.Forms.GroupBox();
            this.Radio_8 = new System.Windows.Forms.RadioButton();
            this.Radio_64 = new System.Windows.Forms.RadioButton();
            this.Radio_32 = new System.Windows.Forms.RadioButton();
            this.Radio_16 = new System.Windows.Forms.RadioButton();
            this.Label_Size = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.BW_Main = new System.ComponentModel.BackgroundWorker();
            this.Menu_Strip = new System.Windows.Forms.MenuStrip();
            this.Menu_Actions = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools_Extract = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Loaded)).BeginInit();
            this.Panel_Sizes.SuspendLayout();
            this.Menu_Strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_LoadImage
            // 
            this.Btn_LoadImage.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_LoadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_LoadImage.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_LoadImage, "Btn_LoadImage");
            this.Btn_LoadImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_LoadImage.Name = "Btn_LoadImage";
            this.Btn_LoadImage.UseVisualStyleBackColor = false;
            this.Btn_LoadImage.Click += new System.EventHandler(this.Btn_LoadImage_Click);
            // 
            // Btn_Generate
            // 
            resources.ApplyResources(this.Btn_Generate, "Btn_Generate");
            this.Btn_Generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(57)))), ((int)(((byte)(70)))));
            this.Btn_Generate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generate.FlatAppearance.BorderSize = 0;
            this.Btn_Generate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_Generate.Name = "Btn_Generate";
            this.Btn_Generate.UseVisualStyleBackColor = false;
            this.Btn_Generate.Click += new System.EventHandler(this.Btn_Generate_Click);
            // 
            // Label_Image
            // 
            this.Label_Image.ForeColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.Label_Image, "Label_Image");
            this.Label_Image.Name = "Label_Image";
            // 
            // Btn_AddFolder
            // 
            this.Btn_AddFolder.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_AddFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_AddFolder.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_AddFolder, "Btn_AddFolder");
            this.Btn_AddFolder.Name = "Btn_AddFolder";
            this.Btn_AddFolder.UseVisualStyleBackColor = false;
            this.Btn_AddFolder.Click += new System.EventHandler(this.Btn_AddFolder_Click);
            // 
            // Label_Folder
            // 
            this.Label_Folder.ForeColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.Label_Folder, "Label_Folder");
            this.Label_Folder.Name = "Label_Folder";
            // 
            // Checked_Elements
            // 
            this.Checked_Elements.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Checked_Elements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Checked_Elements.CheckOnClick = true;
            resources.ApplyResources(this.Checked_Elements, "Checked_Elements");
            this.Checked_Elements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Checked_Elements.FormattingEnabled = true;
            this.Checked_Elements.MultiColumn = true;
            this.Checked_Elements.Name = "Checked_Elements";
            this.Checked_Elements.SelectedIndexChanged += new System.EventHandler(this.Checked_Elements_SelectedIndexChanged);
            // 
            // Picture_Preview
            // 
            resources.ApplyResources(this.Picture_Preview, "Picture_Preview");
            this.Picture_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Picture_Preview.Cursor = System.Windows.Forms.Cursors.Default;
            this.Picture_Preview.Name = "Picture_Preview";
            this.Picture_Preview.TabStop = false;
            // 
            // Picture_Loaded
            // 
            this.Picture_Loaded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Picture_Loaded.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Picture_Loaded, "Picture_Loaded");
            this.Picture_Loaded.Name = "Picture_Loaded";
            this.Picture_Loaded.TabStop = false;
            // 
            // Panel_Sizes
            // 
            this.Panel_Sizes.Controls.Add(this.Radio_8);
            this.Panel_Sizes.Controls.Add(this.Radio_64);
            this.Panel_Sizes.Controls.Add(this.Radio_32);
            this.Panel_Sizes.Controls.Add(this.Radio_16);
            this.Panel_Sizes.ForeColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.Panel_Sizes, "Panel_Sizes");
            this.Panel_Sizes.Name = "Panel_Sizes";
            this.Panel_Sizes.TabStop = false;
            // 
            // Radio_8
            // 
            this.Radio_8.Checked = true;
            this.Radio_8.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Radio_8, "Radio_8");
            this.Radio_8.Name = "Radio_8";
            this.Radio_8.TabStop = true;
            this.Radio_8.UseVisualStyleBackColor = true;
            // 
            // Radio_64
            // 
            this.Radio_64.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_64.ForeColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.Radio_64, "Radio_64");
            this.Radio_64.Name = "Radio_64";
            this.Radio_64.UseVisualStyleBackColor = true;
            // 
            // Radio_32
            // 
            this.Radio_32.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_32.ForeColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.Radio_32, "Radio_32");
            this.Radio_32.Name = "Radio_32";
            this.Radio_32.UseVisualStyleBackColor = true;
            // 
            // Radio_16
            // 
            this.Radio_16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_16.ForeColor = System.Drawing.Color.DeepSkyBlue;
            resources.ApplyResources(this.Radio_16, "Radio_16");
            this.Radio_16.Name = "Radio_16";
            this.Radio_16.UseVisualStyleBackColor = true;
            // 
            // Label_Size
            // 
            this.Label_Size.ForeColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.Label_Size, "Label_Size");
            this.Label_Size.Name = "Label_Size";
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Btn_Save, "Btn_Save");
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // BW_Main
            // 
            this.BW_Main.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_Main_DoWork);
            // 
            // Menu_Strip
            // 
            this.Menu_Strip.AllowMerge = false;
            this.Menu_Strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            resources.ApplyResources(this.Menu_Strip, "Menu_Strip");
            this.Menu_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Actions,
            this.Menu_Options,
            this.Menu_Tools,
            this.Menu_Help});
            this.Menu_Strip.Name = "Menu_Strip";
            this.Menu_Strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // Menu_Actions
            // 
            this.Menu_Actions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Actions.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Actions.Name = "Menu_Actions";
            resources.ApplyResources(this.Menu_Actions, "Menu_Actions");
            // 
            // Menu_Options
            // 
            this.Menu_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Options.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Options.Name = "Menu_Options";
            resources.ApplyResources(this.Menu_Options, "Menu_Options");
            // 
            // Menu_Tools
            // 
            this.Menu_Tools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tools_Extract});
            this.Menu_Tools.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Tools.Name = "Menu_Tools";
            resources.ApplyResources(this.Menu_Tools, "Menu_Tools");
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Clear.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_Clear, "Btn_Clear");
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.UseVisualStyleBackColor = false;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // Menu_Help
            // 
            this.Menu_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Help_About});
            this.Menu_Help.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Help.Name = "Menu_Help";
            resources.ApplyResources(this.Menu_Help, "Menu_Help");
            // 
            // Help_About
            // 
            this.Help_About.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Help_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Help_About.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Help_About.Name = "Help_About";
            resources.ApplyResources(this.Help_About, "Help_About");
            // 
            // Tools_Extract
            // 
            this.Tools_Extract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Tools_Extract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Tools_Extract.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Tools_Extract.Name = "Tools_Extract";
            resources.ApplyResources(this.Tools_Extract, "Tools_Extract");
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Label_Size);
            this.Controls.Add(this.Panel_Sizes);
            this.Controls.Add(this.Picture_Loaded);
            this.Controls.Add(this.Picture_Preview);
            this.Controls.Add(this.Checked_Elements);
            this.Controls.Add(this.Label_Folder);
            this.Controls.Add(this.Btn_AddFolder);
            this.Controls.Add(this.Label_Image);
            this.Controls.Add(this.Btn_Generate);
            this.Controls.Add(this.Btn_LoadImage);
            this.Controls.Add(this.Menu_Strip);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.Menu_Strip;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Loaded)).EndInit();
            this.Panel_Sizes.ResumeLayout(false);
            this.Menu_Strip.ResumeLayout(false);
            this.Menu_Strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_LoadImage;
        private System.Windows.Forms.Button Btn_Generate;
        private System.Windows.Forms.Label Label_Image;
        private System.Windows.Forms.Button Btn_AddFolder;
        private System.Windows.Forms.Label Label_Folder;
        private System.Windows.Forms.CheckedListBox Checked_Elements;
        private System.Windows.Forms.PictureBox Picture_Preview;
        private System.Windows.Forms.PictureBox Picture_Loaded;
        private System.Windows.Forms.GroupBox Panel_Sizes;
        private System.Windows.Forms.RadioButton Radio_64;
        private System.Windows.Forms.RadioButton Radio_32;
        private System.Windows.Forms.RadioButton Radio_16;
        private System.Windows.Forms.Label Label_Size;
        private System.Windows.Forms.Button Btn_Save;
        private System.ComponentModel.BackgroundWorker BW_Main;
        private System.Windows.Forms.RadioButton Radio_8;
        private System.Windows.Forms.MenuStrip Menu_Strip;
        private System.Windows.Forms.ToolStripMenuItem Menu_Actions;
        private System.Windows.Forms.ToolStripMenuItem Menu_Options;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help;
        private System.Windows.Forms.ToolStripMenuItem Help_About;
        private System.Windows.Forms.ToolStripMenuItem Tools_Extract;
    }
}

