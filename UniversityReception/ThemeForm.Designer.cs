
namespace UniversityReception
{
    partial class ThemeForm
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
            this.textBoxEditThemeName = new System.Windows.Forms.TextBox();
            this.saveChangesThemeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxEditThemeName
            // 
            this.textBoxEditThemeName.Location = new System.Drawing.Point(12, 29);
            this.textBoxEditThemeName.Name = "textBoxEditThemeName";
            this.textBoxEditThemeName.Size = new System.Drawing.Size(235, 21);
            this.textBoxEditThemeName.TabIndex = 1;
            // 
            // saveChangesThemeBtn
            // 
            this.saveChangesThemeBtn.Location = new System.Drawing.Point(172, 56);
            this.saveChangesThemeBtn.Name = "saveChangesThemeBtn";
            this.saveChangesThemeBtn.Size = new System.Drawing.Size(75, 23);
            this.saveChangesThemeBtn.TabIndex = 2;
            this.saveChangesThemeBtn.Text = "Зберегти";
            this.saveChangesThemeBtn.UseVisualStyleBackColor = true;
            this.saveChangesThemeBtn.Click += new System.EventHandler(this.saveChangesThemeBtn_Click);
            // 
            // ThemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 125);
            this.Controls.Add(this.saveChangesThemeBtn);
            this.Controls.Add(this.textBoxEditThemeName);
            this.Name = "ThemeForm";
            this.Text = "Нове ім\'я предмету";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox textBoxEditThemeName;
        public System.Windows.Forms.Button saveChangesThemeBtn;
    }
}