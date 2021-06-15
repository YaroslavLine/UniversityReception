using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityReception
{
    public partial class EditUserForm : Form
    {
        public EditUserForm()
        {
            InitializeComponent();
        }
        public event EventHandler saveChangesClick = null;

        private void saveChangesUserBtn_Click(object sender, EventArgs e)
        {
            saveChangesClick(sender,e);
        }
    }
}
