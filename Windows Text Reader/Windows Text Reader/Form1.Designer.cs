namespace Windows_Text_Reader
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_Speak = new System.Windows.Forms.Button();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_Resume = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(961, 446);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Hi, Welcome to text reader";
            // 
            // btn_Speak
            // 
            this.btn_Speak.AutoSize = true;
            this.btn_Speak.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Speak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Speak.Location = new System.Drawing.Point(12, 464);
            this.btn_Speak.Name = "btn_Speak";
            this.btn_Speak.Size = new System.Drawing.Size(79, 35);
            this.btn_Speak.TabIndex = 1;
            this.btn_Speak.Text = "Speak";
            this.btn_Speak.UseVisualStyleBackColor = true;
            this.btn_Speak.Click += new System.EventHandler(this.btn_Speak_Click);
            // 
            // btn_Pause
            // 
            this.btn_Pause.AutoSize = true;
            this.btn_Pause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Pause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Pause.Location = new System.Drawing.Point(97, 464);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(78, 35);
            this.btn_Pause.TabIndex = 2;
            this.btn_Pause.Text = "Pause";
            this.btn_Pause.UseVisualStyleBackColor = true;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // btn_Resume
            // 
            this.btn_Resume.AutoSize = true;
            this.btn_Resume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Resume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Resume.Location = new System.Drawing.Point(181, 464);
            this.btn_Resume.Name = "btn_Resume";
            this.btn_Resume.Size = new System.Drawing.Size(94, 35);
            this.btn_Resume.TabIndex = 3;
            this.btn_Resume.Text = "Resume";
            this.btn_Resume.UseVisualStyleBackColor = true;
            this.btn_Resume.Click += new System.EventHandler(this.btn_Resume_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.AutoSize = true;
            this.btn_Stop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.Location = new System.Drawing.Point(281, 464);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(63, 35);
            this.btn_Stop.TabIndex = 4;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(350, 473);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(250, 24);
            this.comboBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 511);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Resume);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.btn_Speak);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Text Reader in Windows Voice";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_Speak;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Button btn_Resume;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

