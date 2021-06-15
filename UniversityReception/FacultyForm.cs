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
    public partial class FacultyForm : Form
    {
        public FacultyForm()
        {
            InitializeComponent();
        }
        public event EventHandler saveFacultyClick = null;
        private void saveFacultyBtn_Click(object sender, EventArgs e)
        {
            saveFacultyClick(sender,e);
        }
    }
}
