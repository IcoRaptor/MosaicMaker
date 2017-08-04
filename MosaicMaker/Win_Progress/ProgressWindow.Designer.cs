namespace MosaicMakerNS
{
    partial class ProgressWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Progress_Builder = new System.Windows.Forms.ProgressBar();
            this.Label_Progress = new System.Windows.Forms.Label();
            this.BW_Builder = new System.ComponentModel.BackgroundWorker();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Progress_Builder
            // 
            this.Progress_Builder.Location = new System.Drawing.Point(12, 54);
            this.Progress_Builder.Name = "Progress_Builder";
            this.Progress_Builder.Size = new System.Drawing.Size(460, 23);
            this.Progress_Builder.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Progress_Builder.TabIndex = 0;
            // 
            // Label_Progress
            // 
            this.Label_Progress.Location = new System.Drawing.Point(9, 84);
            this.Label_Progress.Name = "Label_Progress";
            this.Label_Progress.Size = new System.Drawing.Size(460, 23);
            this.Label_Progress.TabIndex = 0;
            this.Label_Progress.Text = "Resizing images...";
            this.Label_Progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BW_Builder
            // 
            this.BW_Builder.WorkerReportsProgress = true;
            this.BW_Builder.WorkerSupportsCancellation = true;
            this.BW_Builder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_Builder_DoWork);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(57)))), ((int)(((byte)(70)))));
            this.Btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Cancel.FlatAppearance.BorderSize = 0;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_Cancel.Location = new System.Drawing.Point(397, 126);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 2;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_OK.FlatAppearance.BorderSize = 0;
            this.Btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Btn_OK.Location = new System.Drawing.Point(316, 126);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 22);
            this.Btn_OK.TabIndex = 1;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = false;
            // 
            // ProgressWindow
            // 
            this.AcceptButton = this.Btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(484, 161);
            this.ControlBox = false;
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Label_Progress);
            this.Controls.Add(this.Progress_Builder);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(500, 200);
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "ProgressWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Building Mosaic...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar Progress_Builder;
        private System.Windows.Forms.Label Label_Progress;
        private System.ComponentModel.BackgroundWorker BW_Builder;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_OK;
    }
}