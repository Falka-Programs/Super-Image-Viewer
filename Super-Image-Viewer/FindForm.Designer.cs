
namespace Super_Image_Viewer
{
    partial class FindForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
            this.mainDirectory_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.finded_files_listBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fileName_textBox = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDirectory_textBox
            // 
            this.mainDirectory_textBox.Location = new System.Drawing.Point(180, 47);
            this.mainDirectory_textBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.mainDirectory_textBox.Name = "mainDirectory_textBox";
            this.mainDirectory_textBox.Size = new System.Drawing.Size(545, 31);
            this.mainDirectory_textBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main directory:";
            // 
            // finded_files_listBox
            // 
            this.finded_files_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finded_files_listBox.FormattingEnabled = true;
            this.finded_files_listBox.ItemHeight = 25;
            this.finded_files_listBox.Location = new System.Drawing.Point(3, 27);
            this.finded_files_listBox.Name = "finded_files_listBox";
            this.finded_files_listBox.Size = new System.Drawing.Size(734, 491);
            this.finded_files_listBox.TabIndex = 2;
            this.finded_files_listBox.DoubleClick += new System.EventHandler(this.finded_files_listBox_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.finded_files_listBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 521);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Finded files";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "File name:";
            // 
            // fileName_textBox
            // 
            this.fileName_textBox.Location = new System.Drawing.Point(180, 91);
            this.fileName_textBox.Margin = new System.Windows.Forms.Padding(6);
            this.fileName_textBox.Name = "fileName_textBox";
            this.fileName_textBox.Size = new System.Drawing.Size(545, 31);
            this.fileName_textBox.TabIndex = 5;
            // 
            // start_button
            // 
            this.start_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.start_button.Location = new System.Drawing.Point(3, 137);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(734, 37);
            this.start_button.TabIndex = 7;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.start_button);
            this.groupBox2.Controls.Add(this.fileName_textBox);
            this.groupBox2.Controls.Add(this.mainDirectory_textBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 177);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 698);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FindForm";
            this.Text = "FindForm";
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox mainDirectory_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox finded_files_listBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileName_textBox;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}