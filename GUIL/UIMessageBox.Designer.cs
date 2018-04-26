namespace FEMProject.GUIL
{
    partial class UIMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIMessageBox));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new Janus.Windows.EditControls.UIButton();
            this.lblErrorText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Panel;
            this.uiGroupBox1.Controls.Add(this.lblErrorText);
            this.uiGroupBox1.Controls.Add(this.btnOK);
            this.uiGroupBox1.Controls.Add(this.pictureBox1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.FrameStyle = Janus.Windows.EditControls.FrameStyle.None;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(407, 134);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(320, 99);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // lblErrorText
            // 
            this.lblErrorText.AutoSize = true;
            this.lblErrorText.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorText.Location = new System.Drawing.Point(107, 13);
            this.lblErrorText.Name = "lblErrorText";
            this.lblErrorText.Size = new System.Drawing.Size(55, 13);
            this.lblErrorText.TabIndex = 3;
            this.lblErrorText.Text = "something";
            // 
            // UIMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 134);
            this.Controls.Add(this.uiGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UIMessageBox";
            this.Text = "Message";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Janus.Windows.EditControls.UIButton btnOK;
        private System.Windows.Forms.Label lblErrorText;
    }
}