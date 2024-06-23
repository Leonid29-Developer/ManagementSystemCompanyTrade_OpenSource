namespace ManagementSystem_For_PublicCatering
{
    partial class Menu
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
            this.label1 = new System.Windows.Forms.Label();
            this.Table = new System.Windows.Forms.Panel();
            this.Button_ImageShow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Button_ImageShow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(727, 75);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Table
            // 
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Table.Location = new System.Drawing.Point(3, 130);
            this.Table.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(990, 463);
            this.Table.TabIndex = 4;
            // 
            // Button_ImageShow
            // 
            this.Button_ImageShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ImageShow.BackColor = System.Drawing.Color.Transparent;
            this.Button_ImageShow.BackgroundImage = global::ManagementSystem_For_PublicCatering.Properties.Resources.DarkImageShowOn;
            this.Button_ImageShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_ImageShow.Location = new System.Drawing.Point(882, 12);
            this.Button_ImageShow.Name = "Button_ImageShow";
            this.Button_ImageShow.Size = new System.Drawing.Size(100, 100);
            this.Button_ImageShow.TabIndex = 6;
            this.Button_ImageShow.TabStop = false;
            this.Button_ImageShow.Click += new System.EventHandler(this.Button_ImageShow_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 594);
            this.ControlBox = false;
            this.Controls.Add(this.Button_ImageShow);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Button_ImageShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Table;
        private System.Windows.Forms.PictureBox Button_ImageShow;
    }
}