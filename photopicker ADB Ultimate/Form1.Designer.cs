namespace photopicker_ADB_Ultimate
{
    partial class Form1
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
            this.lstPhotos = new System.Windows.Forms.ListBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbDirectory = new System.Windows.Forms.ComboBox();
            this.webBrowserPdf = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lstPhotos
            // 
            this.lstPhotos.FormattingEnabled = true;
            this.lstPhotos.ItemHeight = 16;
            this.lstPhotos.Location = new System.Drawing.Point(12, 60);
            this.lstPhotos.Name = "lstPhotos";
            this.lstPhotos.Size = new System.Drawing.Size(444, 724);
            this.lstPhotos.TabIndex = 0;
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(474, 60);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(1123, 724);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(216, 790);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(240, 64);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCut
            // 
            this.btnCut.Location = new System.Drawing.Point(474, 790);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(240, 64);
            this.btnCut.TabIndex = 3;
            this.btnCut.Text = "Cut";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(720, 790);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(240, 64);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(82, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 22);
            this.txtPort.TabIndex = 5;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(9, 17);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(70, 17);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "ADB Port:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(474, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(966, 790);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(240, 64);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(188, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(118, 30);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbDirectory
            // 
            this.cmbDirectory.FormattingEnabled = true;
            this.cmbDirectory.Items.AddRange(new object[] {
            "Camera",
            "Screenshots",
            "Download"});
            this.cmbDirectory.Location = new System.Drawing.Point(321, 12);
            this.cmbDirectory.Name = "cmbDirectory";
            this.cmbDirectory.Size = new System.Drawing.Size(135, 24);
            this.cmbDirectory.TabIndex = 10;
            this.cmbDirectory.Text = "Camera";
            this.cmbDirectory.SelectedIndexChanged += new System.EventHandler(this.cmbDirectory_SelectedIndexChanged);
            // 
            // webBrowserPdf
            // 
            this.webBrowserPdf.Location = new System.Drawing.Point(691, 182);
            this.webBrowserPdf.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPdf.Name = "webBrowserPdf";
            this.webBrowserPdf.Size = new System.Drawing.Size(676, 403);
            this.webBrowserPdf.TabIndex = 11;
            this.webBrowserPdf.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1609, 857);
            this.Controls.Add(this.webBrowserPdf);
            this.Controls.Add(this.cmbDirectory);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCut);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.lstPhotos);
            this.Name = "Form1";
            this.Text = "ADB Photopicker Ultimate";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListBox lstPhotos;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ComboBox cmbDirectory;
        private System.Windows.Forms.WebBrowser webBrowserPdf;
        
        
        
        
        
    }
}

