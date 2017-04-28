namespace TexasHoldem.UI
{
    partial class FormTitleScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTitleScreen));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbTitleImage = new System.Windows.Forms.PictureBox();
            this.pbSuits = new System.Windows.Forms.PictureBox();
            this.nudBuyIn = new System.Windows.Forms.NumericUpDown();
            this.lblBuyIn = new System.Windows.Forms.Label();
            this.lblPlayerAmount = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtYourName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSuits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyIn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(20, 456);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 25);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(144, 456);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(144, 25);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.Text = "Start Game";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.pbTitleImage);
            this.panel1.Location = new System.Drawing.Point(64, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 212);
            this.panel1.TabIndex = 12;
            // 
            // pbTitleImage
            // 
            this.pbTitleImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbTitleImage.BackgroundImage")));
            this.pbTitleImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTitleImage.Location = new System.Drawing.Point(4, 3);
            this.pbTitleImage.Name = "pbTitleImage";
            this.pbTitleImage.Size = new System.Drawing.Size(201, 206);
            this.pbTitleImage.TabIndex = 8;
            this.pbTitleImage.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pbSuits.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pbSuits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSuits.Location = new System.Drawing.Point(12, 13);
            this.pbSuits.Name = "pictureBox1";
            this.pbSuits.Size = new System.Drawing.Size(306, 70);
            this.pbSuits.TabIndex = 13;
            this.pbSuits.TabStop = false;
            // 
            // nudBuyIn
            // 
            this.nudBuyIn.Increment = 10000m;
            this.nudBuyIn.Location = new System.Drawing.Point(144, 365);
            this.nudBuyIn.Maximum = 500000m;
            this.nudBuyIn.Minimum = 50000m;
            this.nudBuyIn.Name = "nudBuyIn";
            this.nudBuyIn.Size = new System.Drawing.Size(144, 20);
            this.nudBuyIn.TabIndex = 2;
            this.nudBuyIn.Value = 50000m;
            // 
            // lblBuyIn
            // 
            this.lblBuyIn.AutoSize = true;
            this.lblBuyIn.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBuyIn.Location = new System.Drawing.Point(24, 368);
            this.lblBuyIn.Name = "lblBuyIn";
            this.lblBuyIn.Size = new System.Drawing.Size(85, 13);
            this.lblBuyIn.TabIndex = 1;
            this.lblBuyIn.Text = "Buy In Amount : ";
            // 
            // lblPlayerAmount
            // 
            this.lblPlayerAmount.AutoSize = true;
            this.lblPlayerAmount.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPlayerAmount.Location = new System.Drawing.Point(24, 418);
            this.lblPlayerAmount.Name = "lblPlayerAmount";
            this.lblPlayerAmount.Size = new System.Drawing.Size(104, 13);
            this.lblPlayerAmount.TabIndex = 2;
            this.lblPlayerAmount.Text = "Number Of Players : ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblName.Location = new System.Drawing.Point(24, 324);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(69, 13);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Your Name : ";
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.White;
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn2.FlatAppearance.BorderSize = 2;
            this.btn2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Location = new System.Drawing.Point(144, 412);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(31, 25);
            this.btn2.TabIndex = 3;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.Color.White;
            this.btn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn3.FlatAppearance.BorderSize = 2;
            this.btn3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3.Location = new System.Drawing.Point(200, 412);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(31, 25);
            this.btn3.TabIndex = 4;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.Color.White;
            this.btn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn4.FlatAppearance.BorderSize = 2;
            this.btn4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4.Location = new System.Drawing.Point(257, 412);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(31, 25);
            this.btn4.TabIndex = 5;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // txtYourName
            // 
            this.txtYourName.Location = new System.Drawing.Point(144, 321);
            this.txtYourName.Name = "txtYourName";
            this.txtYourName.Size = new System.Drawing.Size(144, 20);
            this.txtYourName.TabIndex = 1;
            // 
            // FormTitleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(331, 496);
            this.Controls.Add(this.pbSuits);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtYourName);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.lblPlayerAmount);
            this.Controls.Add(this.lblBuyIn);
            this.Controls.Add(this.nudBuyIn);
            this.Name = "FormTitleScreen";
            this.Text = "Texas Hold\'em Poker";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSuits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbSuits;
        private System.Windows.Forms.NumericUpDown nudBuyIn;
        private System.Windows.Forms.Label lblBuyIn;
        private System.Windows.Forms.Label lblPlayerAmount;
        private System.Windows.Forms.TextBox txtYourName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbTitleImage;
    }
}