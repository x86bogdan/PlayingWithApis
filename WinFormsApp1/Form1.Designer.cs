namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbDogImage = new System.Windows.Forms.PictureBox();
            this.btnFetchDog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbDogImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbDogImage
            // 
            this.pbDogImage.Location = new System.Drawing.Point(125, 113);
            this.pbDogImage.Name = "pbDogImage";
            this.pbDogImage.Size = new System.Drawing.Size(598, 562);
            this.pbDogImage.TabIndex = 0;
            this.pbDogImage.TabStop = false;
            // 
            // btnFetchDog
            // 
            this.btnFetchDog.Location = new System.Drawing.Point(826, 133);
            this.btnFetchDog.Name = "btnFetchDog";
            this.btnFetchDog.Size = new System.Drawing.Size(150, 46);
            this.btnFetchDog.TabIndex = 1;
            this.btnFetchDog.Text = "button1";
            this.btnFetchDog.UseVisualStyleBackColor = true;
            this.btnFetchDog.Click += new System.EventHandler(this.btnFetchDog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 944);
            this.Controls.Add(this.btnFetchDog);
            this.Controls.Add(this.pbDogImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbDogImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pbDogImage;
        private Button btnFetchDog;
    }
}