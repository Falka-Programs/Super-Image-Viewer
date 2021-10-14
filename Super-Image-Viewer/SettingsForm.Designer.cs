
namespace Super_Image_Viewer
{
    partial class SettingsForm
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
            this.imagePreview_checkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IntelegentPreview_comboBox = new System.Windows.Forms.CheckBox();
            this.save_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imagePreview_checkBox
            // 
            this.imagePreview_checkBox.AutoSize = true;
            this.imagePreview_checkBox.Location = new System.Drawing.Point(15, 70);
            this.imagePreview_checkBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.imagePreview_checkBox.Name = "imagePreview_checkBox";
            this.imagePreview_checkBox.Size = new System.Drawing.Size(165, 29);
            this.imagePreview_checkBox.TabIndex = 0;
            this.imagePreview_checkBox.Text = "ImagePreview";
            this.imagePreview_checkBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(141, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings";
            // 
            // IntelegentPreview_comboBox
            // 
            this.IntelegentPreview_comboBox.AutoSize = true;
            this.IntelegentPreview_comboBox.Location = new System.Drawing.Point(192, 70);
            this.IntelegentPreview_comboBox.Margin = new System.Windows.Forms.Padding(6);
            this.IntelegentPreview_comboBox.Name = "IntelegentPreview_comboBox";
            this.IntelegentPreview_comboBox.Size = new System.Drawing.Size(269, 29);
            this.IntelegentPreview_comboBox.TabIndex = 2;
            this.IntelegentPreview_comboBox.Text = "Intelegent image preview";
            this.IntelegentPreview_comboBox.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(81, 108);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(99, 38);
            this.save_button.TabIndex = 3;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(272, 108);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(99, 38);
            this.close_button.TabIndex = 4;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(459, 161);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.IntelegentPreview_comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagePreview_checkBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox imagePreview_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox IntelegentPreview_comboBox;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button close_button;
    }
}