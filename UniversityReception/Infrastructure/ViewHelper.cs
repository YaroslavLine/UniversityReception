using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityReception.Models;

namespace UniversityReception.Helpers
{
    public class ViewHelper
    {
        public static void PrintWarningError(string message)
        {
            MessageBox.Show(message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void PrintCriticalError(string message)
        {
            MessageBox.Show(message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void PrintInfoMesage(string message)
        {
            MessageBox.Show(message, "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static Task UpdateTableOfUsersAsync(DataGridView view, DbContext db)
        {
            try
            {
                var users = db.Users.Select(u => new { u.UserId, u.UserName, u.UserLogin, u.Password, u.DateOfCreating, u.UserRole.RoleName }).OrderBy(u => u.DateOfCreating).ToList();
                view.DataSource = users;
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні таблиці.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }
        public static Task UpdateTableFacultiesAsync(DataGridView view, DbContext db)
        {
            try
            {
                List<Faculty> list = db.Faculties.ToList();
                view.DataSource = list;
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні таблиці.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }
        public static Task UpdateTableThemesAsync(DataGridView view, DbContext db)
        {
            try
            {
                List<Theme> list = db.Themes.ToList();
                view.DataSource = list;
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні таблиці.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static Task UpdateTableSpecialitiesAsync(DataGridView view, DbContext db)
        {
            try
            {
                view.DataSource = db.Specialities.ToList();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при виведенні спеціальностей\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static Task UpdateTableEducationAsync(DataGridView view, DbContext db)
        {
            try
            {
                view.DataSource = db.EducationLevels.ToList();
                view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                view.Columns[0].Visible = false;
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні даних.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static Task UpdateComboboxSpecialities(string itemName, ComboBox specialities, DbContext db)
        {
            try
            {
                specialities.Items.Clear();
                specialities.Text = "Оберіть спеціальність";
                Faculty faculty = db.Faculties.FirstOrDefault(f => f.FacultyName == itemName);
                specialities.Items.AddRange(faculty.Specialities.Select(s => s.SpecialityName).ToArray() ?? null);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні даних.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static Task UpdateTableStudentsAsync(MainForm form, DbContext db)
        {
            string specialityName = form.comboBoxSelectSpecialitiesStudents.SelectedItem.ToString();
            try
            {
                Speciality sp = db.Specialities.Include("Marticulants").FirstOrDefault(s => s.SpecialityName == specialityName);
                if (sp != null)
                {
                    form.dataGridViewStudents.DataSource = sp.Marticulants.Where(m => m.AdmittedToLearning == true).ToList();
                    SetupTableStudents(form.dataGridViewStudents);

                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при отриманні даних.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        private static void SetupTableStudents(DataGridView view)
        {
            view.Columns[0].Visible = false;
            view.Columns[1].HeaderText = "ПІБ";
            view.Columns[2].HeaderText = "Дата зарахування";
            view.Columns[3].HeaderText = "Сума балів ЗНО";
            view.Columns[4].HeaderText = "Денна форма навчання";
            view.Columns[5].HeaderText = "Бюджетна форма оплати";
            view.Columns[6].HeaderText = "Наявність пільг";

            for (int i = 7; i < view.ColumnCount; i++) view.Columns[i].Visible = false;
        }

        internal static Task UpdateDataMarticulantsAsync(MainForm form, DbContext db)
        {
            if (form.comboBoxSelectSpecialityMart.SelectedItem == null) return Task.CompletedTask;
            string speciality = form.comboBoxSelectSpecialityMart.SelectedItem.ToString();
            Speciality sp = db.Specialities.Include("Marticulants").FirstOrDefault(s => s.SpecialityName == speciality);
            if (sp != null)
            {
                form.labelScore.Text = sp.PassingScore.ToString();
                form.labelCountOfStudents.Text = sp.Open.ToString();
                form.labelPrivilegies.Text = sp.PrivelegesCount.ToString();
                form.labelAccepted.Text = sp.RecievedClaims.ToString();
                form.labelCompetition.Text = sp.CompetitionPlaceCount.ToString();
                var marticulants = sp.Marticulants.Select(m => new { m.MarticulantId, m.FullNameOfMarticulant, m.DateOfClaim, m.SumScore, m.Privileges, m.AdmittedToLearning }).Where(m => m.AdmittedToLearning == false);
                if (marticulants != null)
                {
                    form.dataGridViewMarticulants.DataSource = marticulants.ToList();
                    form.dataGridViewMarticulants.Columns[0].Visible = false;
                    form.dataGridViewMarticulants.Columns[1].HeaderText = "ПІБ";
                    form.dataGridViewMarticulants.Columns[2].HeaderText = "Дата подачі документів";
                    form.dataGridViewMarticulants.Columns[3].HeaderText = "Сума балів ЗНО";
                    form.dataGridViewMarticulants.Columns[4].HeaderText = "Наявність пільг";
                    form.dataGridViewMarticulants.Columns[5].Visible = false;
                }
                return Task.CompletedTask;
            }
            PrintWarningError("Додайте спеціальності");
            return Task.CompletedTask;
        }

        internal static void UpdateTableThemesForAttempting(ListBox.ObjectCollection specialities, DataGridView view, DbContext db)
        {
            HashSet<string> selectedSpecialities = new HashSet<string>(specialities.Cast<string>());
            List<Theme> tList = new List<Theme>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Предмет");
            dt.Columns.Add("Оцінка");
            dt.Columns[0].ReadOnly = true;
            List<Speciality> fs = db.Specialities.Include("Themes").Where(s => selectedSpecialities.Contains(s.SpecialityName)).ToList();
            foreach (Speciality s in fs)
            {
                tList.AddRange(s.Themes);
            }

            var themes = tList.GroupBy(t => t.ThemeName);

            foreach (var item in themes)
            {
                dt.Rows.Add(item.Key, 0);
            }
            view.DataSource = dt;
        }

        internal static void InsertDataOfMarticulant(MarticulantForm form, Marticulant marticulant)
        {
            marticulant.FullNameOfMarticulant = form.textBoxFullNameOfMarticulant.Text;
            marticulant.PassportCode = form.textBoxCodeOfPassport.Text;
            marticulant.BirthDay = form.dateTimePickerBirthday.Value;
            marticulant.Male = form.comboBoxSex.SelectedItem.ToString() == "Чоловіча" ? true : false;
            marticulant.PlaceOfbirth = form.textBoxPlaceOfBirth.Text;
            marticulant.Nationality = form.textBoxNationality.Text;
            marticulant.RegistrationAddress = form.textBoxRegistrationAddress.Text;
            marticulant.AccommodationAddress = form.textBoxAccommodationAddress.Text;
            marticulant.IsDayForm = form.radioButtonIsDayForm.Checked;
            marticulant.OnBudget = form.radioButtonOnBudget.Checked;
            marticulant.IsNeedHostel = form.checkBoxIsNeedHostel.Checked;
            marticulant.IsResident = form.checkBoxIsResident.Checked;
            marticulant.Privileges = form.checkBoxPrivileges.Checked;
            marticulant.FullNameOfFather = form.textBoxFullNameFather.Text;
            marticulant.PhoneNumberOfFather = form.textBoxPhoneFather.Text;
            marticulant.FullNameOfMother = form.textBoxFullNameMother.Text;
            marticulant.PhoneNumberOfMother = form.textBoxPhoneMother.Text;
            marticulant.FullNameOfClosePerson = form.textBoxFullNameOfClosePerson.Text;
            marticulant.PhoneNumberOfFather = form.textBoxPhoneOfClosePerson.Text;
            marticulant.PhoneNumber = form.textBoxPhoneNumber.Text;
            try
            {
                marticulant.MiddleScore = Convert.ToInt32(form.domainUpDownMiddleScore.Text);
                marticulant.YearOfEndingEducation = new DateTime(int.Parse(form.domainUpDownYearOfEndingEducation.Text), 7, 1);
                marticulant.LevelOfEducation = form.comboBoxLevelOfEducation.SelectedItem.ToString();
            }
            catch (FormatException ex)
            {
                PrintWarningError("Невірний формат даних\n" + ex.Message);
                return;
            }
        }

        internal static Task UpdateComboboxLevelsOfEduAsync(ComboBox cb, DbContext db)
        {
            try
            {
                cb.Items.Clear();
                cb.Items.AddRange(db.EducationLevels.Select(l => l.LevelOfEducation).ToArray());
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintWarningError("Помилка при отриманні рівнів освіти.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static Task UpdateComboboxFacultiesAsync(ComboBox faculties, DbContext db)
        {
            faculties.Items.Clear();
            List<Faculty> fList = db.Faculties.Include("Specialities").Select(f => f).ToList();

            if (fList != null)
            {
                faculties.Items.AddRange(fList.Select(f => f.FacultyName).ToArray());
                return Task.CompletedTask;
            }
            PrintCriticalError("Помилка при отриманні даних");
            return Task.CompletedTask;
        }
        internal static bool CheckControls(MarticulantForm form)
        {
            foreach (var tb in form.groupBoxMainData.Controls)
            {
                if (tb is TextBox)
                {
                    TextBox t = tb as TextBox;
                    if (string.IsNullOrEmpty(t.Text) || string.IsNullOrWhiteSpace(t.Text))
                    {
                        return false;
                    }
                }
            }
            if (form.checkedListBoxDocs.CheckedItems.Count != form.checkedListBoxDocs.Items.Count || form.listBoxSpecialities.Items.Count == 0)
            {
                return false;
            }
            if (form.comboBoxSex.SelectedItem == null || form.comboBoxLevelOfEducation.SelectedItem == null || form.textBoxPhoneNumber.Text.Contains("_"))
            {
                return false;
            }
            return true;
        }
        internal static bool CheckControls(SpecialityForm form)
        {
            foreach (var tb in form.groupBoxSpecialityFields.Controls)
            {
                if (tb is TextBox)
                {
                    TextBox t = tb as TextBox;
                    if (string.IsNullOrEmpty(t.Text) || string.IsNullOrWhiteSpace(t.Text))
                    {
                        return false;
                    }
                }
            }
            if (form.listBoxThemes.Items.Count == 0)
            {
                return false;
            }

            try
            {
                Convert.ToInt32(form.textBoxAvailableCount.Text);
                Convert.ToInt32(form.textBoxPassingScore.Text);
                Convert.ToInt32(form.textBoxCompetitionCount.Text);
                Convert.ToInt32(form.textBoxCountOfPrivileges.Text);
            }
            catch (Exception ex)
            {
                form.textBoxCompetitionCount.Clear();
                form.textBoxCountOfPrivileges.Clear();
                form.textBoxAvailableCount.Clear();
                form.textBoxPassingScore.Clear();
                ViewHelper.PrintWarningError("Невірно введене число.\n" + ex.Message);
                return false;
            }
            return true;
        }
        internal static void UpdateViews(MainForm form, DbContext db)
        {
            form.rolesCombobox.DataSource = db.Roles.Select(r => r.RoleName).ToList();

            form.dataGridViewUsers.Columns[0].Visible = false;
            form.dataGridViewUsers.Columns[1].HeaderText = "ПІБ";
            form.dataGridViewUsers.Columns[2].HeaderText = "Логін";
            form.dataGridViewUsers.Columns[3].HeaderText = "Пароль";
            form.dataGridViewUsers.Columns[4].HeaderText = "Дата додавання";
            form.dataGridViewUsers.Columns[5].HeaderText = "Роль";

            form.dataGridViewFaculties.Columns[0].Visible = false;
            form.dataGridViewFaculties.Columns[1].HeaderText = "Назва";
            form.dataGridViewFaculties.Columns[2].HeaderText = "Код";
            form.dataGridViewFaculties.Columns[3].HeaderText = "Скорочено";

            form.dataGridViewThemes.Columns[0].Visible = false;
            form.dataGridViewThemes.Columns[1].HeaderText = "Назва предмету";

            form.dataGridViewSpecialities.Columns[0].Visible = false;
            form.dataGridViewSpecialities.Columns[1].HeaderText = "Найменування";
            form.dataGridViewSpecialities.Columns[1].Width = 500;
            form.dataGridViewSpecialities.Columns[2].HeaderText = "Скорочення";
            form.dataGridViewSpecialities.Columns[3].HeaderText = "Код";
            form.dataGridViewSpecialities.Columns[4].HeaderText = "Кількість місць";
            form.dataGridViewSpecialities.Columns[5].HeaderText = "Прохідний бал";
            form.dataGridViewSpecialities.Columns[6].Visible = false;
            form.dataGridViewSpecialities.Columns[7].Visible = false;
            form.dataGridViewSpecialities.Columns[8].Visible = false;
            form.dataGridViewSpecialities.Columns[9].HeaderText = "Коефіцієнт";
            form.dataGridViewSpecialities.Columns[10].Visible = false;
            form.dataGridViewSpecialities.Columns[11].Visible = false;

            form.dataGridViewEduLevels.Columns[1].HeaderText = "Рівень освіти";
            form.dataGridViewEduLevels.Columns[2].HeaderText = "Скорочено";

            form.dataGridViewMarticulants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridViewSpecialities.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridViewFaculties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridViewThemes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridViewStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
