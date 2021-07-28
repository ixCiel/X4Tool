namespace X4Tool
{
    partial class AboutMe
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
            this.label_coded = new System.Windows.Forms.Label();
            this.link_download = new System.Windows.Forms.LinkLabel();
            this.btn_close_about = new System.Windows.Forms.Button();
            this.pic_alipay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_alipay)).BeginInit();
            this.SuspendLayout();
            // 
            // label_coded
            // 
            this.label_coded.AutoSize = true;
            this.label_coded.Location = new System.Drawing.Point(12, 9);
            this.label_coded.Name = "label_coded";
            this.label_coded.Size = new System.Drawing.Size(41, 12);
            this.label_coded.TabIndex = 0;
            this.label_coded.Text = "label1";
            // 
            // link_download
            // 
            this.link_download.Location = new System.Drawing.Point(166, 9);
            this.link_download.Name = "link_download";
            this.link_download.Size = new System.Drawing.Size(130, 12);
            this.link_download.TabIndex = 1;
            this.link_download.TabStop = true;
            this.link_download.Text = "linkLabel1";
            this.link_download.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.link_download.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_download_LinkClicked);
            // 
            // btn_close_about
            // 
            this.btn_close_about.Location = new System.Drawing.Point(221, 456);
            this.btn_close_about.Name = "btn_close_about";
            this.btn_close_about.Size = new System.Drawing.Size(75, 23);
            this.btn_close_about.TabIndex = 2;
            this.btn_close_about.Text = "button1";
            this.btn_close_about.UseVisualStyleBackColor = true;
            this.btn_close_about.Click += new System.EventHandler(this.btn_close_about_Click);
            // 
            // pic_alipay
            // 
            this.pic_alipay.Image = global::X4Tool.Properties.Resources.alipay;
            this.pic_alipay.Location = new System.Drawing.Point(12, 24);
            this.pic_alipay.Name = "pic_alipay";
            this.pic_alipay.Size = new System.Drawing.Size(284, 426);
            this.pic_alipay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_alipay.TabIndex = 3;
            this.pic_alipay.TabStop = false;
            // 
            // AboutMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 486);
            this.Controls.Add(this.pic_alipay);
            this.Controls.Add(this.btn_close_about);
            this.Controls.Add(this.link_download);
            this.Controls.Add(this.label_coded);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutMe";
            this.Text = "AboutMe";
            this.Load += new System.EventHandler(this.AboutMe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_alipay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_coded;
        private System.Windows.Forms.LinkLabel link_download;
        private System.Windows.Forms.Button btn_close_about;
        private System.Windows.Forms.PictureBox pic_alipay;
    }
}