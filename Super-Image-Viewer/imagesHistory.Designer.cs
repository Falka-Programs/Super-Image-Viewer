
namespace Super_Image_Viewer
{
    partial class imagesHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(imagesHistory));
            this.history_View = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.close_button = new System.Windows.Forms.Button();
            this.clean_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file_path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.watch_date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // history_View
            // 
            this.history_View.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.file_name,
            this.file_path,
            this.watch_date});
            this.history_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.history_View.FullRowSelect = true;
            this.history_View.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.history_View.HideSelection = false;
            this.history_View.Location = new System.Drawing.Point(3, 27);
            this.history_View.Name = "history_View";
            this.history_View.Size = new System.Drawing.Size(697, 250);
            this.history_View.TabIndex = 0;
            this.history_View.UseCompatibleStateImageBehavior = false;
            this.history_View.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.close_button);
            this.groupBox1.Controls.Add(this.clean_button);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // close_button
            // 
            this.close_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.close_button.Location = new System.Drawing.Point(599, 27);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(101, 40);
            this.close_button.TabIndex = 2;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // clean_button
            // 
            this.clean_button.Dock = System.Windows.Forms.DockStyle.Left;
            this.clean_button.Location = new System.Drawing.Point(3, 27);
            this.clean_button.Name = "clean_button";
            this.clean_button.Size = new System.Drawing.Size(101, 40);
            this.clean_button.TabIndex = 1;
            this.clean_button.Text = "Clean";
            this.clean_button.UseVisualStyleBackColor = true;
            this.clean_button.Click += new System.EventHandler(this.clean_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.history_View);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(703, 280);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // file_name
            // 
            this.file_name.Text = "File name";
            this.file_name.Width = 120;
            // 
            // file_path
            // 
            this.file_path.Text = "File path";
            this.file_path.Width = 360;
            // 
            // watch_date
            // 
            this.watch_date.Text = "Watch date";
            this.watch_date.Width = 150;
            // 
            // imagesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 350);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "imagesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImagesHistory";
            this.Load += new System.EventHandler(this.imagesHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView history_View;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button clean_button;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader file_name;
        private System.Windows.Forms.ColumnHeader file_path;
        private System.Windows.Forms.ColumnHeader watch_date;
    }
}