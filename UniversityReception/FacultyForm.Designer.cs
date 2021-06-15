
namespace UniversityReception
{
    partial class FacultyForm
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
            this.nameOfFacultyTextBox = new System.Windows.Forms.TextBox();
            this.codeOfFacultyTextBox = new System.Windows.Forms.TextBox();
            this.ShortNameOfFacultyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFacultyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameOfFacultyTextBox
            // 
            this.nameOfFacultyTextBox.Location = new System.Drawing.Point(12, 25);
            this.nameOfFacultyTextBox.Name = "nameOfFacultyTextBox";
            this.nameOfFacultyTextBox.Size = new System.Drawing.Size(620, 21);
            this.nameOfFacultyTextBox.TabIndex = 0;
            // 
            // codeOfFacultyTextBox
            // 
            this.codeOfFacultyTextBox.Location = new System.Drawing.Point(12, 65);
            this.codeOfFacultyTextBox.Name = "codeOfFacultyTextBox";
            this.codeOfFacultyTextBox.Size = new System.Drawing.Size(210, 21);
            this.codeOfFacultyTextBox.TabIndex = 1;
            // 
            // ShortNameOfFacultyTextBox
            // 
            this.ShortNameOfFacultyTextBox.Location = new System.Drawing.Point(12, 105);
            this.ShortNameOfFacultyTextBox.Name = "ShortNameOfFacultyTextBox";
            this.ShortNameOfFacultyTextBox.Size = new System.Drawing.Size(210, 21);
            this.ShortNameOfFacultyTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Повна назва факультету";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Код факультету";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Скорочена назва факультету";
            // 
            // saveFacultyBtn
            // 
            this.saveFacultyBtn.Location = new System.Drawing.Point(524, 79);
            this.saveFacultyBtn.Name = "saveFacultyBtn";
            this.saveFacultyBtn.Size = new System.Drawing.Size(108, 47);
            this.saveFacultyBtn.TabIndex = 6;
            this.saveFacultyBtn.Text = "Зберегти";
            this.saveFacultyBtn.UseVisualStyleBackColor = true;
            this.saveFacultyBtn.Click += new System.EventHandler(this.saveFacultyBtn_Click);
            // 
            // AddFacultyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 155);
            this.Controls.Add(this.saveFacultyBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShortNameOfFacultyTextBox);
            this.Controls.Add(this.codeOfFacultyTextBox);
            this.Controls.Add(this.nameOfFacultyTextBox);
            this.Name = "AddFacultyForm";
            this.Text = "Редагування факультету";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox nameOfFacultyTextBox;
        public System.Windows.Forms.TextBox codeOfFacultyTextBox;
        public System.Windows.Forms.TextBox ShortNameOfFacultyTextBox;
        public System.Windows.Forms.Button saveFacultyBtn;
    }
}