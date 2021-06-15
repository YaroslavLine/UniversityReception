using System;
using System.Windows.Forms;

namespace UniversityReception
{
    public partial class SpecialityForm : Form
    {
        public SpecialityForm()
        {
            InitializeComponent();
        }
        public event EventHandler selectThemeClick = null;
        public event EventHandler saveSpecialityClick = null;
        private void saveFacultyBtn_Click(object sender, EventArgs e)
        {
            saveSpecialityClick(sender,e);
        }
        private void selectThemesBtn_Click(object sender, EventArgs e)
        {
            selectThemeClick(sender,e);
        }
    }
}
