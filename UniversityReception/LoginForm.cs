using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace UniversityReception
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            new Presenter(this, new MainForm());
        }
        public event EventHandler buttonLoginClick = null;
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            buttonLoginClick(sender, e);
        }

    }
}
