namespace 启动项管理
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.voice = new System.Windows.Forms.CheckBox();
            this.wifiOpen = new System.Windows.Forms.ComboBox();
            this.wifiStop = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contain = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.voiceNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "多少秒执行";
            // 
            // sCount
            // 
            this.sCount.Location = new System.Drawing.Point(106, 19);
            this.sCount.Name = "sCount";
            this.sCount.Size = new System.Drawing.Size(100, 21);
            this.sCount.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "wifi下必执行";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "wifi下必不执行";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "执行前开关声音";
            // 
            // voice
            // 
            this.voice.AutoSize = true;
            this.voice.Location = new System.Drawing.Point(139, 177);
            this.voice.Name = "voice";
            this.voice.Size = new System.Drawing.Size(15, 14);
            this.voice.TabIndex = 5;
            this.voice.UseVisualStyleBackColor = true;
            this.voice.CheckedChanged += new System.EventHandler(this.voice_CheckedChanged);
            // 
            // wifiOpen
            // 
            this.wifiOpen.FormattingEnabled = true;
            this.wifiOpen.Location = new System.Drawing.Point(119, 52);
            this.wifiOpen.Name = "wifiOpen";
            this.wifiOpen.Size = new System.Drawing.Size(121, 20);
            this.wifiOpen.TabIndex = 6;
            // 
            // wifiStop
            // 
            this.wifiStop.FormattingEnabled = true;
            this.wifiStop.Location = new System.Drawing.Point(130, 83);
            this.wifiStop.Name = "wifiStop";
            this.wifiStop.Size = new System.Drawing.Size(121, 20);
            this.wifiStop.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 36);
            this.label5.TabIndex = 4;
            this.label5.Text = "无线列表有该名\r\n称wifi则操作/连\r\n接该名称wifi时操作";
            // 
            // contain
            // 
            this.contain.AutoSize = true;
            this.contain.Location = new System.Drawing.Point(154, 128);
            this.contain.Name = "contain";
            this.contain.Size = new System.Drawing.Size(15, 14);
            this.contain.TabIndex = 5;
            this.contain.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "音量大小";
            // 
            // voiceNum
            // 
            this.voiceNum.Location = new System.Drawing.Point(106, 200);
            this.voiceNum.Name = "voiceNum";
            this.voiceNum.Size = new System.Drawing.Size(81, 21);
            this.voiceNum.TabIndex = 2;
            this.voiceNum.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "自动关闭";
            // 
            // Close
            // 
            this.Close.AutoSize = true;
            this.Close.Location = new System.Drawing.Point(139, 236);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(15, 14);
            this.Close.TabIndex = 7;
            this.Close.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 314);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.wifiStop);
            this.Controls.Add(this.wifiOpen);
            this.Controls.Add(this.contain);
            this.Controls.Add(this.voice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.voiceNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox voice;
        private System.Windows.Forms.ComboBox wifiOpen;
        private System.Windows.Forms.ComboBox wifiStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox contain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox voiceNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox Close;
    }
}