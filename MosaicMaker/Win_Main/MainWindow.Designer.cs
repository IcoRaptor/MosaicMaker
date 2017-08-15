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
            System.Windows.Forms.ToolStripSeparator A_Sep_1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.ToolStripSeparator A_Sep_2;
            System.Windows.Forms.ToolStripSeparator A_Sep_3;
            this.Btn_LoadImage = new System.Windows.Forms.Button();
            this.Btn_Generate = new System.Windows.Forms.Button();
            this.Label_Image = new System.Windows.Forms.Label();
            this.Btn_AddFolder = new System.Windows.Forms.Button();
            this.Label_Folder = new System.Windows.Forms.Label();
            this.Checked_Elements = new System.Windows.Forms.CheckedListBox();
            this.Picture_Preview = new System.Windows.Forms.PictureBox();
            this.Picture_Loaded = new System.Windows.Forms.PictureBox();
            this.Panel_Sizes = new System.Windows.Forms.GroupBox();
            this.Radio_1 = new System.Windows.Forms.RadioButton();
            this.Radio_4 = new System.Windows.Forms.RadioButton();
            this.Radio_8 = new System.Windows.Forms.RadioButton();
            this.Radio_64 = new System.Windows.Forms.RadioButton();
            this.Radio_32 = new System.Windows.Forms.RadioButton();
            this.Radio_16 = new System.Windows.Forms.RadioButton();
            this.Label_Size = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.BW_Main = new System.ComponentModel.BackgroundWorker();
            this.Menu_Strip = new System.Windows.Forms.MenuStrip();
            this.Menu_Actions = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_LoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_AddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_Size = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_8 = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_16 = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_32 = new System.Windows.Forms.ToolStripMenuItem();
            this.Size_64 = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_Generate = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Actions_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.Options_Pixelate = new System.Windows.Forms.ToolStripMenuItem();
            this.Pixelate_Image = new System.Windows.Forms.ToolStripMenuItem();
            this.Pixelate_Strip = new System.Windows.Forms.ToolStripMenuItem();
            this.Options_Mirror = new System.Windows.Forms.ToolStripMenuItem();
            this.Mirror_Default = new System.Windows.Forms.ToolStripMenuItem();
            this.Mirror_Horizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.Mirror_Vertical = new System.Windows.Forms.ToolStripMenuItem();
            this.Mirror_Full = new System.Windows.Forms.ToolStripMenuItem();
            this.O_Sep_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Options_Negative = new System.Windows.Forms.ToolStripMenuItem();
            this.Options_Gray = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Clear = new System.Windows.Forms.Button();
            A_Sep_1 = new System.Windows.Forms.ToolStripSeparator();
            A_Sep_2 = new System.Windows.Forms.ToolStripSeparator();
            A_Sep_3 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Loaded)).BeginInit();
            this.Panel_Sizes.SuspendLayout();
            this.Menu_Strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // A_Sep_1
            // 
            A_Sep_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_1.Name = "A_Sep_1";
            resources.ApplyResources(A_Sep_1, "A_Sep_1");
            // 
            // A_Sep_2
            // 
            A_Sep_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_2.Name = "A_Sep_2";
            resources.ApplyResources(A_Sep_2, "A_Sep_2");
            // 
            // A_Sep_3
            // 
            A_Sep_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            A_Sep_3.Name = "A_Sep_3";
            resources.ApplyResources(A_Sep_3, "A_Sep_3");
            // 
            // Btn_LoadImage
            // 
            this.Btn_LoadImage.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_LoadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_LoadImage.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_LoadImage, "Btn_LoadImage");
            this.Btn_LoadImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_LoadImage.Name = "Btn_LoadImage";
            this.Btn_LoadImage.TabStop = false;
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
            this.Btn_Generate.TabStop = false;
            this.Btn_Generate.UseVisualStyleBackColor = false;
            this.Btn_Generate.Click += new System.EventHandler(this.Btn_Generate_Click);
            // 
            // Label_Image
            // 
            resources.ApplyResources(this.Label_Image, "Label_Image");
            this.Label_Image.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Image.Name = "Label_Image";
            // 
            // Btn_AddFolder
            // 
            this.Btn_AddFolder.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_AddFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_AddFolder.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_AddFolder, "Btn_AddFolder");
            this.Btn_AddFolder.Name = "Btn_AddFolder";
            this.Btn_AddFolder.TabStop = false;
            this.Btn_AddFolder.UseVisualStyleBackColor = false;
            this.Btn_AddFolder.Click += new System.EventHandler(this.Btn_AddFolder_Click);
            // 
            // Label_Folder
            // 
            resources.ApplyResources(this.Label_Folder, "Label_Folder");
            this.Label_Folder.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Folder.Name = "Label_Folder";
            // 
            // Checked_Elements
            // 
            this.Checked_Elements.BackColor = System.Drawing.Color.LightGray;
            this.Checked_Elements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Checked_Elements.CheckOnClick = true;
            resources.ApplyResources(this.Checked_Elements, "Checked_Elements");
            this.Checked_Elements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Checked_Elements.FormattingEnabled = true;
            this.Checked_Elements.MultiColumn = true;
            this.Checked_Elements.Name = "Checked_Elements";
            this.Checked_Elements.TabStop = false;
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
            this.Panel_Sizes.Controls.Add(this.Radio_1);
            this.Panel_Sizes.Controls.Add(this.Radio_4);
            this.Panel_Sizes.Controls.Add(this.Radio_8);
            this.Panel_Sizes.Controls.Add(this.Radio_64);
            this.Panel_Sizes.Controls.Add(this.Radio_32);
            this.Panel_Sizes.Controls.Add(this.Radio_16);
            resources.ApplyResources(this.Panel_Sizes, "Panel_Sizes");
            this.Panel_Sizes.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Panel_Sizes.Name = "Panel_Sizes";
            this.Panel_Sizes.TabStop = false;
            // 
            // Radio_1
            // 
            resources.ApplyResources(this.Radio_1, "Radio_1");
            this.Radio_1.Checked = true;
            this.Radio_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_1.Name = "Radio_1";
            this.Radio_1.TabStop = true;
            this.Radio_1.UseVisualStyleBackColor = true;
            this.Radio_1.CheckedChanged += new System.EventHandler(this.Radio_1_CheckedChanged);
            // 
            // Radio_4
            // 
            resources.ApplyResources(this.Radio_4, "Radio_4");
            this.Radio_4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_4.Name = "Radio_4";
            this.Radio_4.UseVisualStyleBackColor = true;
            this.Radio_4.CheckedChanged += new System.EventHandler(this.Radio_4_CheckedChanged);
            // 
            // Radio_8
            // 
            resources.ApplyResources(this.Radio_8, "Radio_8");
            this.Radio_8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_8.Name = "Radio_8";
            this.Radio_8.UseVisualStyleBackColor = true;
            this.Radio_8.CheckedChanged += new System.EventHandler(this.Radio_8_CheckedChanged);
            // 
            // Radio_64
            // 
            resources.ApplyResources(this.Radio_64, "Radio_64");
            this.Radio_64.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_64.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_64.Name = "Radio_64";
            this.Radio_64.UseVisualStyleBackColor = true;
            this.Radio_64.CheckedChanged += new System.EventHandler(this.Radio_64_CheckedChanged);
            // 
            // Radio_32
            // 
            resources.ApplyResources(this.Radio_32, "Radio_32");
            this.Radio_32.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_32.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_32.Name = "Radio_32";
            this.Radio_32.UseVisualStyleBackColor = true;
            this.Radio_32.CheckedChanged += new System.EventHandler(this.Radio_32_CheckedChanged);
            // 
            // Radio_16
            // 
            resources.ApplyResources(this.Radio_16, "Radio_16");
            this.Radio_16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Radio_16.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Radio_16.Name = "Radio_16";
            this.Radio_16.UseVisualStyleBackColor = true;
            this.Radio_16.CheckedChanged += new System.EventHandler(this.Radio_16_CheckedChanged);
            // 
            // Label_Size
            // 
            resources.ApplyResources(this.Label_Size, "Label_Size");
            this.Label_Size.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Size.Name = "Label_Size";
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Btn_Save, "Btn_Save");
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.TabStop = false;
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // BW_Main
            // 
            this.BW_Main.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_Main_DoWork);
            // 
            // Menu_Strip
            // 
            this.Menu_Strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            resources.ApplyResources(this.Menu_Strip, "Menu_Strip");
            this.Menu_Strip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Actions,
            this.Menu_Options,
            this.Menu_Help});
            this.Menu_Strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.Menu_Strip.Name = "Menu_Strip";
            this.Menu_Strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // Menu_Actions
            // 
            this.Menu_Actions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Actions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Actions_LoadImage,
            this.Actions_AddFolder,
            this.Actions_Clear,
            A_Sep_1,
            this.Actions_Size,
            A_Sep_2,
            this.Actions_Generate,
            this.Actions_Save,
            A_Sep_3,
            this.Actions_Exit});
            resources.ApplyResources(this.Menu_Actions, "Menu_Actions");
            this.Menu_Actions.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Actions.Name = "Menu_Actions";
            // 
            // Actions_LoadImage
            // 
            this.Actions_LoadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_LoadImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_LoadImage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_LoadImage.Name = "Actions_LoadImage";
            resources.ApplyResources(this.Actions_LoadImage, "Actions_LoadImage");
            this.Actions_LoadImage.Click += new System.EventHandler(this.Actions_LoadImage_Click);
            // 
            // Actions_AddFolder
            // 
            this.Actions_AddFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_AddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_AddFolder.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_AddFolder.Name = "Actions_AddFolder";
            resources.ApplyResources(this.Actions_AddFolder, "Actions_AddFolder");
            this.Actions_AddFolder.Click += new System.EventHandler(this.Actions_AddFolder_Click);
            // 
            // Actions_Clear
            // 
            this.Actions_Clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_Clear.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_Clear.Name = "Actions_Clear";
            resources.ApplyResources(this.Actions_Clear, "Actions_Clear");
            this.Actions_Clear.Click += new System.EventHandler(this.Actions_Clear_Click);
            // 
            // Actions_Size
            // 
            this.Actions_Size.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_Size.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_Size.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Size_1,
            this.Size_4,
            this.Size_8,
            this.Size_16,
            this.Size_32,
            this.Size_64});
            this.Actions_Size.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_Size.Name = "Actions_Size";
            resources.ApplyResources(this.Actions_Size, "Actions_Size");
            // 
            // Size_1
            // 
            this.Size_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_1.Checked = true;
            this.Size_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Size_1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_1.Name = "Size_1";
            resources.ApplyResources(this.Size_1, "Size_1");
            this.Size_1.Click += new System.EventHandler(this.Size_1_Click);
            // 
            // Size_4
            // 
            this.Size_4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_4.Name = "Size_4";
            resources.ApplyResources(this.Size_4, "Size_4");
            this.Size_4.Click += new System.EventHandler(this.Size_4_Click);
            // 
            // Size_8
            // 
            this.Size_8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_8.Name = "Size_8";
            resources.ApplyResources(this.Size_8, "Size_8");
            this.Size_8.Click += new System.EventHandler(this.Size_8_Click);
            // 
            // Size_16
            // 
            this.Size_16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_16.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_16.Name = "Size_16";
            resources.ApplyResources(this.Size_16, "Size_16");
            this.Size_16.Click += new System.EventHandler(this.Size_16_Click);
            // 
            // Size_32
            // 
            this.Size_32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_32.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_32.Name = "Size_32";
            resources.ApplyResources(this.Size_32, "Size_32");
            this.Size_32.Click += new System.EventHandler(this.Size_32_Click);
            // 
            // Size_64
            // 
            this.Size_64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Size_64.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Size_64.Name = "Size_64";
            resources.ApplyResources(this.Size_64, "Size_64");
            this.Size_64.Click += new System.EventHandler(this.Size_64_Click);
            // 
            // Actions_Generate
            // 
            this.Actions_Generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_Generate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_Generate.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_Generate.Name = "Actions_Generate";
            resources.ApplyResources(this.Actions_Generate, "Actions_Generate");
            this.Actions_Generate.Click += new System.EventHandler(this.Actions_Generate_Click);
            // 
            // Actions_Save
            // 
            this.Actions_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Actions_Save.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_Save.Name = "Actions_Save";
            resources.ApplyResources(this.Actions_Save, "Actions_Save");
            this.Actions_Save.Click += new System.EventHandler(this.Actions_Save_Click);
            // 
            // Actions_Exit
            // 
            this.Actions_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Actions_Exit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Actions_Exit.Name = "Actions_Exit";
            resources.ApplyResources(this.Actions_Exit, "Actions_Exit");
            this.Actions_Exit.Click += new System.EventHandler(this.Actions_Exit_Click);
            // 
            // Menu_Options
            // 
            this.Menu_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Options_Pixelate,
            this.Options_Mirror,
            this.O_Sep_1,
            this.Options_Negative,
            this.Options_Gray});
            resources.ApplyResources(this.Menu_Options, "Menu_Options");
            this.Menu_Options.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Options.Name = "Menu_Options";
            // 
            // Options_Pixelate
            // 
            this.Options_Pixelate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Options_Pixelate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Pixelate_Image,
            this.Pixelate_Strip});
            resources.ApplyResources(this.Options_Pixelate, "Options_Pixelate");
            this.Options_Pixelate.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Options_Pixelate.Name = "Options_Pixelate";
            // 
            // Pixelate_Image
            // 
            this.Pixelate_Image.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Pixelate_Image.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Pixelate_Image.Name = "Pixelate_Image";
            resources.ApplyResources(this.Pixelate_Image, "Pixelate_Image");
            this.Pixelate_Image.Click += new System.EventHandler(this.Pixelate_Image_Click);
            // 
            // Pixelate_Strip
            // 
            this.Pixelate_Strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Pixelate_Strip.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Pixelate_Strip.Name = "Pixelate_Strip";
            resources.ApplyResources(this.Pixelate_Strip, "Pixelate_Strip");
            this.Pixelate_Strip.Click += new System.EventHandler(this.Pixelate_Strip_Click);
            // 
            // Options_Mirror
            // 
            this.Options_Mirror.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Options_Mirror.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mirror_Default,
            this.Mirror_Horizontal,
            this.Mirror_Vertical,
            this.Mirror_Full});
            this.Options_Mirror.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Options_Mirror.Name = "Options_Mirror";
            resources.ApplyResources(this.Options_Mirror, "Options_Mirror");
            // 
            // Mirror_Default
            // 
            this.Mirror_Default.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Mirror_Default.Checked = true;
            this.Mirror_Default.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Mirror_Default.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Mirror_Default.Name = "Mirror_Default";
            resources.ApplyResources(this.Mirror_Default, "Mirror_Default");
            this.Mirror_Default.Click += new System.EventHandler(this.Mirror_Default_Click);
            // 
            // Mirror_Horizontal
            // 
            this.Mirror_Horizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Mirror_Horizontal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Mirror_Horizontal.Name = "Mirror_Horizontal";
            resources.ApplyResources(this.Mirror_Horizontal, "Mirror_Horizontal");
            this.Mirror_Horizontal.Click += new System.EventHandler(this.Mirror_Horizontal_Click);
            // 
            // Mirror_Vertical
            // 
            this.Mirror_Vertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Mirror_Vertical.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Mirror_Vertical.Name = "Mirror_Vertical";
            resources.ApplyResources(this.Mirror_Vertical, "Mirror_Vertical");
            this.Mirror_Vertical.Click += new System.EventHandler(this.Mirror_Vertical_Click);
            // 
            // Mirror_Full
            // 
            this.Mirror_Full.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Mirror_Full.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Mirror_Full.Name = "Mirror_Full";
            resources.ApplyResources(this.Mirror_Full, "Mirror_Full");
            this.Mirror_Full.Click += new System.EventHandler(this.Mirror_Full_Click);
            // 
            // O_Sep_1
            // 
            this.O_Sep_1.Name = "O_Sep_1";
            resources.ApplyResources(this.O_Sep_1, "O_Sep_1");
            // 
            // Options_Negative
            // 
            this.Options_Negative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Options_Negative.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Options_Negative.Name = "Options_Negative";
            resources.ApplyResources(this.Options_Negative, "Options_Negative");
            this.Options_Negative.Click += new System.EventHandler(this.Options_Negative_Click);
            // 
            // Options_Gray
            // 
            this.Options_Gray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Options_Gray.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Options_Gray.Name = "Options_Gray";
            resources.ApplyResources(this.Options_Gray, "Options_Gray");
            this.Options_Gray.Click += new System.EventHandler(this.Options_Gray_Click);
            // 
            // Menu_Help
            // 
            this.Menu_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Help_About});
            resources.ApplyResources(this.Menu_Help, "Menu_Help");
            this.Menu_Help.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Menu_Help.Name = "Menu_Help";
            // 
            // Help_About
            // 
            this.Help_About.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Help_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Help_About.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Help_About.Name = "Help_About";
            resources.ApplyResources(this.Help_About, "Help_About");
            this.Help_About.Click += new System.EventHandler(this.Help_About_Click);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Btn_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Clear.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Btn_Clear, "Btn_Clear");
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.TabStop = false;
            this.Btn_Clear.UseVisualStyleBackColor = false;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
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
            this.Panel_Sizes.PerformLayout();
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
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help;
        private System.Windows.Forms.ToolStripMenuItem Help_About;
        private System.Windows.Forms.ToolStripMenuItem Options_Mirror;
        private System.Windows.Forms.ToolStripMenuItem Mirror_Vertical;
        private System.Windows.Forms.ToolStripMenuItem Mirror_Horizontal;
        private System.Windows.Forms.ToolStripMenuItem Options_Negative;
        private System.Windows.Forms.ToolStripMenuItem Actions_LoadImage;
        private System.Windows.Forms.ToolStripMenuItem Actions_AddFolder;
        private System.Windows.Forms.ToolStripMenuItem Actions_Clear;
        private System.Windows.Forms.ToolStripMenuItem Actions_Size;
        private System.Windows.Forms.ToolStripMenuItem Actions_Generate;
        private System.Windows.Forms.ToolStripMenuItem Actions_Save;
        private System.Windows.Forms.ToolStripMenuItem Size_8;
        private System.Windows.Forms.ToolStripMenuItem Size_16;
        private System.Windows.Forms.ToolStripMenuItem Size_32;
        private System.Windows.Forms.ToolStripMenuItem Size_64;
        private System.Windows.Forms.ToolStripMenuItem Mirror_Full;
        private System.Windows.Forms.ToolStripMenuItem Mirror_Default;
        private System.Windows.Forms.ToolStripMenuItem Actions_Exit;
        private System.Windows.Forms.ToolStripMenuItem Options_Pixelate;
        private System.Windows.Forms.ToolStripMenuItem Pixelate_Image;
        private System.Windows.Forms.ToolStripMenuItem Pixelate_Strip;
        private System.Windows.Forms.RadioButton Radio_1;
        private System.Windows.Forms.RadioButton Radio_4;
        private System.Windows.Forms.ToolStripMenuItem Size_1;
        private System.Windows.Forms.ToolStripMenuItem Size_4;
        private System.Windows.Forms.ToolStripSeparator O_Sep_1;
        private System.Windows.Forms.ToolStripMenuItem Options_Gray;
    }
}

