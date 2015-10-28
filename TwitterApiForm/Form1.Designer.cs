namespace TwitterApiForm
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
            this.btnGo = new System.Windows.Forms.Button();
            this.txtTweet = new System.Windows.Forms.TextBox();
            this.btnGetUser = new System.Windows.Forms.Button();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnGo.Location = new System.Drawing.Point(868, 447);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(204, 76);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Tweet";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTweet
            // 
            this.txtTweet.Location = new System.Drawing.Point(39, 47);
            this.txtTweet.Multiline = true;
            this.txtTweet.Name = "txtTweet";
            this.txtTweet.Size = new System.Drawing.Size(1033, 364);
            this.txtTweet.TabIndex = 1;
            this.txtTweet.Text = "mojitocoder";
            // 
            // btnGetUser
            // 
            this.btnGetUser.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnGetUser.Location = new System.Drawing.Point(639, 447);
            this.btnGetUser.Name = "btnGetUser";
            this.btnGetUser.Size = new System.Drawing.Size(204, 76);
            this.btnGetUser.TabIndex = 2;
            this.btnGetUser.Text = "GetUser";
            this.btnGetUser.UseVisualStyleBackColor = true;
            this.btnGetUser.Click += new System.EventHandler(this.btnGetUser_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.ImageLocation = "";
            this.picAvatar.Location = new System.Drawing.Point(98, 618);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(635, 639);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picAvatar.TabIndex = 3;
            this.picAvatar.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1114, 1649);
            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.btnGetUser);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtTweet);
            this.Name = "Form1";
            this.Text = "Twitter API Test";
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtTweet;
        private System.Windows.Forms.Button btnGetUser;
        private System.Windows.Forms.PictureBox picAvatar;
    }
}

