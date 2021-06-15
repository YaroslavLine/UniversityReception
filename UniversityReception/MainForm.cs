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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }
        public event EventHandler mainFormClosing = null;
        public event EventHandler logoutClick = null;
        public event EventHandler mainFormLoad = null;
        public event EventHandler addUserClick = null;
        public event EventHandler deleteUserClick = null;
        public event EventHandler changeUserClick = null;
        public event EventHandler addFacultyClick = null;
        public event EventHandler editFacultyClick = null;
        public event EventHandler deleteFacultyClick = null;

        public event EventHandler addSpecialityClick = null;
        public event EventHandler deleteSpecialityClick = null;

        public event EventHandler addThemeClick = null;
        public event EventHandler deleteThemeClick = null;
        public event EventHandler editThemeClick = null;

        public event EventHandler addEducationClick = null;
        public event EventHandler deleteEducationClick = null;

        public event EventHandler selectedFacultyChanged = null;

        public event EventHandler addNewMarticulantClick = null;
        public event EventHandler attemptMarticulantClick = null;
        public event EventHandler deleteMarticulantClick = null;

        public event EventHandler comboBoxFacultiesStudentsValueChanged = null;
        public event EventHandler comboBoxSpecialitiesStudentsValueChanged = null;
        public event EventHandler buttonDeleteStudentClick = null;

        public event EventHandler comboBoxSpecMartChanged = null;


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainFormClosing(sender, e);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            mainFormLoad(sender, e);
        }

        #region FirstTab
        private void logOutButton_Click(object sender, EventArgs e)
        {
            logoutClick(sender, e);
        }


        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            addUserClick(sender, e);
        }

        private void deleteUserBtn_Click(object sender, EventArgs e)
        {
            deleteUserClick(sender, e);
        }

        private void editUserBtn_Click(object sender, EventArgs e)
        {
            changeUserClick(sender, e);
        }
        #endregion

        private void addFacultyBtn_Click(object sender, EventArgs e)
        {
            addFacultyClick(sender, e);
        }

        private void editFacultyBtn_Click(object sender, EventArgs e)
        {
            editFacultyClick(sender, e);
        }

        private void deleteFacultyBtn_Click(object sender, EventArgs e)
        {
            deleteFacultyClick(sender, e);
        }

        private void addSpecialityBtn_Click(object sender, EventArgs e)
        {
            addSpecialityClick(sender, e);
        }

        private void addThemeBtn_Click(object sender, EventArgs e)
        {
            addThemeClick(sender, e);
        }

        private void deleteThemeBtn_Click(object sender, EventArgs e)
        {
            deleteThemeClick(sender, e);
        }

        private void editThemeBtn_Click(object sender, EventArgs e)
        {
            editThemeClick(sender, e);
        }

        private void deleteSpecialityBtn_Click(object sender, EventArgs e)
        {
            deleteSpecialityClick(sender, e);
        }

        private void addEducationBtn_Click(object sender, EventArgs e)
        {
            addEducationClick(sender, e);
        }

        private void deleteEducationBtn_Click(object sender, EventArgs e)
        {
            deleteEducationClick(sender, e);
        }

        private void comboBoxSelectFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedFacultyChanged(sender, e);
        }

        private void buttonAddNewMarticulant_Click(object sender, EventArgs e)
        {
            addNewMarticulantClick(sender, e);
        }

        private void comboBoxSelectSpecialityMart_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxSpecMartChanged(sender, e);
        }

        private void attemptBtn_Click(object sender, EventArgs e)
        {
            attemptMarticulantClick(sender, e);
        }

        private void deleteMarticulantBtn_Click(object sender, EventArgs e)
        {
            deleteMarticulantClick(sender, e);
        }

        private void comboBoxSelectFacultyStudents_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxFacultiesStudentsValueChanged(sender, e);
        }

        private void comboBoxSelectSpecialitiesStudents_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxSpecialitiesStudentsValueChanged(sender, e);
        }

        private void buttonDeleteStudent_Click(object sender, EventArgs e)
        {
            buttonDeleteStudentClick(sender, e);
        }
    }
}
