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
    public partial class MarticulantForm : Form
    {
        public MarticulantForm()
        {
            InitializeComponent();
        }
        public event EventHandler openFormMarticulant = null;
        public event EventHandler selectedFacultyChangedMarticulantForm = null;
        public event EventHandler specialityToListClick = null;
        public event EventHandler saveNewMarticulantClick = null;
        public event EventHandler resetMarticulantClick = null;

        private void MarticulantForm_Load(object sender, EventArgs e)
        {
            openFormMarticulant(sender, e);
        }

        private void comboBoxSelectFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedFacultyChangedMarticulantForm(sender, e);
        }

        private void buttonSpecialityToList_Click(object sender, EventArgs e)
        {
            specialityToListClick(sender, e);
        }

        private void buttonSaveNewMarticulant_Click(object sender, EventArgs e)
        {
            saveNewMarticulantClick(sender, e);
        }

        private void buttonClearListBoxSpecialities_Click(object sender, EventArgs e)
        {
            resetMarticulantClick(sender,e);
        }
    }
}
