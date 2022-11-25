namespace StatechStudyProject
{
    partial class MainForm
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
            this.btnViewDataTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewDataTable
            // 
            this.btnViewDataTable.Location = new System.Drawing.Point(654, 12);
            this.btnViewDataTable.Name = "btnViewDataTable";
            this.btnViewDataTable.Size = new System.Drawing.Size(134, 23);
            this.btnViewDataTable.TabIndex = 0;
            this.btnViewDataTable.Text = "View Data Table";
            this.btnViewDataTable.UseVisualStyleBackColor = true;
            this.btnViewDataTable.Click += new System.EventHandler(this.btnViewDataTable_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnViewDataTable);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnViewDataTable;
    }
}