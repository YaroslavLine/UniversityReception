
namespace UniversityReception
{
    partial class EditUserForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editRolesCombobox = new System.Windows.Forms.ComboBox();
            this.editPassRepeatTextBox = new System.Windows.Forms.TextBox();
            this.editPassTextBox = new System.Windows.Forms.TextBox();
            this.editNewLoginTextBox = new System.Windows.Forms.TextBox();
            this.editUserNameTextBox = new System.Windows.Forms.TextBox();
            this.saveChangesUserBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Оберіть роль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Повторіть пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Новий пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Введіть логін";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Введіть ім\'я";
            // 
            // editRolesCombobox
            // 
            this.editRolesCombobox.FormattingEnabled = true;
            this.editRolesCombobox.Location = new System.Drawing.Point(77, 286);
            this.editRolesCombobox.Name = "editRolesCombobox";
            this.editRolesCombobox.Size = new System.Drawing.Size(198, 21);
            this.editRolesCombobox.TabIndex = 4;
            // 
            // editPassRepeatTextBox
            // 
            this.editPassRepeatTextBox.Location = new System.Drawing.Point(76, 232);
            this.editPassRepeatTextBox.Name = "editPassRepeatTextBox";
            this.editPassRepeatTextBox.PasswordChar = '*';
            this.editPassRepeatTextBox.Size = new System.Drawing.Size(199, 20);
            this.editPassRepeatTextBox.TabIndex = 3;
            // 
            // editPassTextBox
            // 
            this.editPassTextBox.Location = new System.Drawing.Point(76, 173);
            this.editPassTextBox.Name = "editPassTextBox";
            this.editPassTextBox.PasswordChar = '*';
            this.editPassTextBox.Size = new System.Drawing.Size(199, 20);
            this.editPassTextBox.TabIndex = 2;
            // 
            // editNewLoginTextBox
            // 
            this.editNewLoginTextBox.Location = new System.Drawing.Point(76, 114);
            this.editNewLoginTextBox.Name = "editNewLoginTextBox";
            this.editNewLoginTextBox.Size = new System.Drawing.Size(199, 20);
            this.editNewLoginTextBox.TabIndex = 1;
            // 
            // editUserNameTextBox
            // 
            this.editUserNameTextBox.Location = new System.Drawing.Point(76, 62);
            this.editUserNameTextBox.Name = "editUserNameTextBox";
            this.editUserNameTextBox.Size = new System.Drawing.Size(199, 20);
            this.editUserNameTextBox.TabIndex = 0;
            // 
            // saveChangesUserBtn
            // 
            this.saveChangesUserBtn.Location = new System.Drawing.Point(200, 325);
            this.saveChangesUserBtn.Name = "saveChangesUserBtn";
            this.saveChangesUserBtn.Size = new System.Drawing.Size(75, 23);
            this.saveChangesUserBtn.TabIndex = 5;
            this.saveChangesUserBtn.Text = "Зберегти";
            this.saveChangesUserBtn.UseVisualStyleBackColor = true;
            this.saveChangesUserBtn.Click += new System.EventHandler(this.saveChangesUserBtn_Click);
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 404);
            this.Controls.Add(this.saveChangesUserBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editRolesCombobox);
            this.Controls.Add(this.editPassRepeatTextBox);
            this.Controls.Add(this.editPassTextBox);
            this.Controls.Add(this.editNewLoginTextBox);
            this.Controls.Add(this.editUserNameTextBox);
            this.Name = "EditUserForm";
            this.Text = "Редагування користувача";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox editRolesCombobox;
        public System.Windows.Forms.TextBox editPassRepeatTextBox;
        public System.Windows.Forms.TextBox editPassTextBox;
        public System.Windows.Forms.TextBox editNewLoginTextBox;
        public System.Windows.Forms.TextBox editUserNameTextBox;
        public System.Windows.Forms.Button saveChangesUserBtn;
    }
}