namespace VeritabanıProje
{
    partial class Organizasyon
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblGarson = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1034, 681);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblGarson
            // 
            this.lblGarson.AutoSize = true;
            this.lblGarson.Location = new System.Drawing.Point(1074, 35);
            this.lblGarson.Name = "lblGarson";
            this.lblGarson.Size = new System.Drawing.Size(35, 13);
            this.lblGarson.TabIndex = 1;
            this.lblGarson.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1040, 575);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 94);
            this.button1.TabIndex = 2;
            this.button1.Text = "Çıkış";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Organizasyon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblGarson);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Organizasyon";
            this.Text = "Organizasyon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblGarson;
        private System.Windows.Forms.Button button1;
    }
}