using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityReception.Helpers;
using UniversityReception.Models;

namespace UniversityReception
{
    class Presenter
    {
        LoginForm loginForm;
        MainForm mainForm;
        DbContext db;
        EditUserForm editUserForm;
        FacultyForm facultyForm;
        SpecialityForm specialityForm;
        ThemeForm themeForm;
        MarticulantForm marticulantForm;
        DbHelper dbHelper;

        static int editElementId;
        static bool isEdit;
        public Presenter(LoginForm loginForm, MainForm mainForm)
        {
            this.loginForm = loginForm;
            this.mainForm = mainForm;
            dbHelper = new DbHelper();
            db = new DbContext();
            editUserForm = new EditUserForm();
            facultyForm = new FacultyForm();
            specialityForm = new SpecialityForm();
            themeForm = new ThemeForm();
            marticulantForm = new MarticulantForm();

            this.loginForm.buttonLoginClick += LoginForm_buttonLoginClick;
            this.mainForm.mainFormClosing += MainForm_mainFormClosing;
            this.mainForm.logoutClick += MainForm_logoutClick;
            this.mainForm.mainFormLoad += MainForm_mainFormLoad;
            this.mainForm.addUserClick += MainForm_addUserClick;
            this.mainForm.deleteUserClick += MainForm_deleteUserClick;
            this.mainForm.changeUserClick += MainForm_changeUserClick;
            this.editUserForm.saveChangesClick += EditUser_saveChangesClick;

            this.mainForm.addFacultyClick += MainForm_addFacultyClick;
            this.mainForm.editFacultyClick += MainForm_editFacultyClick;
            this.mainForm.deleteFacultyClick += MainForm_deleteFacultyClick;
            this.facultyForm.saveFacultyClick += FacultyForm_saveFacultyClick;

            this.mainForm.addSpecialityClick += MainForm_addSpecialityClick;
            this.mainForm.deleteSpecialityClick += MainForm_deleteSpecialityClick;

            this.specialityForm.selectThemeClick += SpecialityForm_selectThemeClick;
            this.specialityForm.saveSpecialityClick += SpecialityForm_saveSpecialityClick;

            this.mainForm.addThemeClick += MainForm_addThemeClick;
            this.mainForm.deleteThemeClick += MainForm_deleteThemeClick;
            this.mainForm.editThemeClick += MainForm_editThemeClick;
            this.themeForm.saveChangesClick += ThemeForm_saveChangesClick;

            this.mainForm.addEducationClick += MainForm_addEducationClick;
            this.mainForm.deleteEducationClick += MainForm_deleteEducationClick;

            this.mainForm.selectedFacultyChanged += MainForm_selectedFacultyChanged;
            this.mainForm.comboBoxSpecMartChanged += MainForm_comboBoxSpecMartChanged;

            this.mainForm.addNewMarticulantClick += MainForm_addNewMarticulantClick;
            this.mainForm.attemptMarticulantClick += MainForm_attemptMarticulantClick;
            this.mainForm.deleteMarticulantClick += MainForm_deleteMarticulantClick;

            this.mainForm.comboBoxFacultiesStudentsValueChanged += MainForm_comboBoxFacultiesStudentsValueChanged;
            this.mainForm.comboBoxSpecialitiesStudentsValueChanged += MainForm_comboBoxSpecialitiesStudentsValueChanged;
            this.mainForm.buttonDeleteStudentClick += MainForm_buttonDeleteStudentClick;

            this.marticulantForm.openFormMarticulant += MarticulantForm_openFormMarticulant;
            this.marticulantForm.selectedFacultyChangedMarticulantForm += MarticulantForm_selectedFacultyChangedMarticulantForm;
            this.marticulantForm.specialityToListClick += MarticulantForm_specialityToListClick;
            this.marticulantForm.saveNewMarticulantClick += MarticulantForm_saveNewMarticulantClick;
            this.marticulantForm.resetMarticulantClick += MarticulantForm_resetMarticulantClick;
            this.marticulantForm.closingFormMarticulant += MarticulantForm_closingFormMarticulant;
        }

        private void MarticulantForm_closingFormMarticulant(object sender, EventArgs e)
        {
            marticulantForm.comboBoxSelectFaculty.Text = "Оберіть факультет";
        }

        private async void MainForm_buttonDeleteStudentClick(object sender, EventArgs e)
        {
            int id = dbHelper.GetSelectedId(mainForm.dataGridViewStudents, "MarticulantId");
            if (id == -1) return;
            string spName = mainForm.comboBoxSelectSpecialitiesStudents.SelectedItem.ToString() ?? "Оберіть";
            if (!spName.Contains("Оберіть"))
            {
                await dbHelper.DeleteStudentAsync(id, spName, db);
                await ViewHelper.UpdateTableStudentsAsync(mainForm, db);
                ViewHelper.PrintInfoMesage($"Студент видалений");
            }
        }

        private async void MainForm_comboBoxSpecialitiesStudentsValueChanged(object sender, EventArgs e)
        {
            await ViewHelper.UpdateTableStudentsAsync(mainForm, db);
        }

        private void MainForm_comboBoxFacultiesStudentsValueChanged(object sender, EventArgs e)
        {
            ViewHelper.UpdateComboboxSpecialities(mainForm.comboBoxSelectFacultyStudents.SelectedItem.ToString(), mainForm.comboBoxSelectSpecialitiesStudents, db);
        }

        private async void MainForm_deleteMarticulantClick(object sender, EventArgs e)
        {
            int id = dbHelper.GetSelectedId(mainForm.dataGridViewMarticulants, "MarticulantId");
            string spName = mainForm.comboBoxSelectSpecialityMart.Text;
            await dbHelper.DeleteMarticulantAsync(id, spName, db);
            await ViewHelper.UpdateDataMarticulantsAsync(mainForm, db);
            ViewHelper.PrintInfoMesage("Абітурієнта видалено");
        }

        private async void MainForm_attemptMarticulantClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewMarticulants, "MarticulantId");
            string spName = mainForm.comboBoxSelectSpecialityMart.Text;
            if (id != null && id.Value > 0)
            {
                Speciality sp = db.Specialities.FirstOrDefault(s => s.SpecialityName == spName);
                sp.RecievedClaims--;
                List<Speciality> anotherSp = db.Specialities.Include("Marticulants").Where(s => s.SpecialityName != spName).ToList();
                Marticulant mr = sp.Marticulants.Find(m => m.MarticulantId == id.Value);
                mr.SelectedSpeciality = spName;
                mr.AdmittedToLearning = true;
                mr.DateOfAdmittingToLearning = DateTime.Now.Date;

                foreach (Speciality spc in anotherSp)
                {
                    Marticulant mrtc = spc.Marticulants.Find(m => m.MarticulantId == id);
                    if (mrtc != null)
                    {
                        spc.RecievedClaims--;
                        spc.Marticulants.Remove(mrtc);
                    }
                }
                db.SaveChanges();
                await ViewHelper.UpdateDataMarticulantsAsync(mainForm, db);
                ViewHelper.PrintInfoMesage($"Абітурієнта {mr.FullNameOfMarticulant} зараховано.");
            }
        }

        private async void MainForm_comboBoxSpecMartChanged(object sender, EventArgs e)
        {
            await ViewHelper.UpdateDataMarticulantsAsync(mainForm, db);
        }

        private void MarticulantForm_resetMarticulantClick(object sender, EventArgs e)
        {
            marticulantForm.listBoxSpecialities.Items.Clear();
            marticulantForm.dataGridViewThemesForMarticulant.DataSource = null;
        }
        #region Formula
        //        Формула має наступний вигляд:
        //КБ = (K1* П1 + K2* П2 + K3* П3 + K4* П4 + K5*А), де:
        //-  КБ – конкурсний бал абітурієнта;
        //·  П1, П2, П3, П4 - результати Сертифікатів ЗНО з предмету 1, з предмету 2, з предмету 3 та з предмету 4
        //(предмет 4 може буде замінений на конкурс творчих або фізичних здібностей);
        //·  А - середній бал абітурієнта в документі про здобуття повної загальної середньої освіти(звичайно атестату); 
        #endregion
        private async void MarticulantForm_saveNewMarticulantClick(object sender, EventArgs e)
        {
            var resultsTable = marticulantForm.dataGridViewThemesForMarticulant.DataSource as DataTable;
            if (resultsTable == null || !ViewHelper.CheckControls(marticulantForm))
            {
                ViewHelper.PrintWarningError("Не всі дані отримано!");
                return;
            }

            Marticulant marticulant = new Marticulant();
            Dictionary<string, int> themesValues = new Dictionary<string, int>();
            ViewHelper.InsertDataOfMarticulant(marticulantForm, marticulant);
            HashSet<string> spFromListBox = marticulantForm.listBoxSpecialities.Items.Cast<string>().ToHashSet();
            IEnumerable<Speciality> selectedSpecialities = db.Specialities.Include("Themes").Where(s => spFromListBox.Contains(s.SpecialityName));

            foreach (DataRow item in resultsTable.Rows)
            {
                try
                {
                    themesValues.Add(item["Предмет"].ToString(), Convert.ToInt32(item["Оцінка"]));
                }
                catch (FormatException ex)
                {
                    ViewHelper.PrintWarningError("Невірний формат даних.\n" + ex.Message);
                    themesValues.Clear();
                    return;
                }
            }

            marticulant.SumScore = themesValues.Values.Sum() + marticulant.MiddleScore;

            foreach (Speciality s in selectedSpecialities)
            {
                int result = dbHelper.CalculateScores(s, themesValues, marticulant.MiddleScore);
                if (result >= s.PassingScore)
                {
                    Speciality ss = db.Specialities.Find(s.SpecialityId);
                    marticulant.DateOfClaim = DateTime.Now.Date;
                    ss.Marticulants.Add(marticulant);
                    ss.RecievedClaims++;
                    ViewHelper.PrintInfoMesage($"Прийнята заявка за спеціальністю {s.ShortSpecialityName}");
                }
                else
                {
                    ViewHelper.PrintInfoMesage($"Нажаль недостатньо балів для спеціальності {s.ShortSpecialityName}.\nАбітурієнта направлено на конкурс");
                }
            }
            db.SaveChanges();
            await ViewHelper.UpdateDataMarticulantsAsync(mainForm, db);
        }

        private void MarticulantForm_specialityToListClick(object sender, EventArgs e)
        {
            if (marticulantForm.comboBoxSelectSpeciality.SelectedItem != null &&
                !marticulantForm.listBoxSpecialities.Items.Contains(marticulantForm.comboBoxSelectSpeciality.SelectedItem.ToString()))
            {
                marticulantForm.listBoxSpecialities.Items.Add(marticulantForm.comboBoxSelectSpeciality.SelectedItem.ToString());
                ViewHelper.UpdateTableThemesForAttempting(marticulantForm.listBoxSpecialities.Items, marticulantForm.dataGridViewThemesForMarticulant, db);
            }
        }

        private void MarticulantForm_selectedFacultyChangedMarticulantForm(object sender, EventArgs e)
        {
            ViewHelper.UpdateComboboxSpecialities(marticulantForm.comboBoxSelectFaculty.SelectedItem.ToString(), marticulantForm.comboBoxSelectSpeciality, db);
        }

        private void MainForm_addNewMarticulantClick(object sender, EventArgs e)
        {
            marticulantForm.ShowDialog();
        }

        private async void MarticulantForm_openFormMarticulant(object sender, EventArgs e)
        {
            await ViewHelper.UpdateComboboxFacultiesAsync(marticulantForm.comboBoxSelectFaculty, db);
            await ViewHelper.UpdateComboboxLevelsOfEduAsync(marticulantForm.comboBoxLevelOfEducation, db);
        }

        private async void MainForm_selectedFacultyChanged(object sender, EventArgs e)
        {
            await ViewHelper.UpdateComboboxSpecialities(mainForm.comboBoxSelectFacultyMart.SelectedItem.ToString(), mainForm.comboBoxSelectSpecialityMart, db);
        }

        private async void MainForm_deleteEducationClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewEduLevels, "EducationLevelId");
            await dbHelper.DeleteEduLevelAsync(id, db);
            await ViewHelper.UpdateTableEducationAsync(mainForm.dataGridViewEduLevels, db);
            ViewHelper.PrintInfoMesage("Об'єкт видалено");

        }

        private async void MainForm_addEducationClick(object sender, EventArgs e)
        {
            string l = mainForm.textBoxLevelOfEducation.Text;
            string sL = mainForm.textBoxShortLevelOfEducation.Text;
            if (!string.IsNullOrEmpty(l) && !string.IsNullOrEmpty(sL) && !string.IsNullOrWhiteSpace(l) && !string.IsNullOrWhiteSpace(sL))
            {
                EducationLevel level = new EducationLevel { LevelOfEducation = l, ShortLevelOfEducation = sL };
                dbHelper.InsertLevelOfEducationIntoDb(level, db);
                await ViewHelper.UpdateTableEducationAsync(mainForm.dataGridViewEduLevels, db);

                return;
            }
            ViewHelper.PrintWarningError("Завовніть всі поля");
        }

        private async void MainForm_deleteSpecialityClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewSpecialities, "SpecialityId");
            if (id.HasValue)
            {
                await dbHelper.DeleteSpecialityAsync(id, db);
                await ViewHelper.UpdateTableSpecialitiesAsync(mainForm.dataGridViewSpecialities, db);
                ViewHelper.PrintInfoMesage("Запис видалено");
            }
        }

        private async void SpecialityForm_saveSpecialityClick(object sender, EventArgs e)
        {
            if (!ViewHelper.CheckControls(specialityForm))
            {
                ViewHelper.PrintWarningError("Заповніть всі поля");
                return;
            }

            List<Theme> list = new List<Theme>();
            foreach (var item in specialityForm.listBoxThemes.Items)
            {
                list.Add(db.Themes.FirstOrDefault(t => t.ThemeName == item.ToString()));
            }
            Faculty faculty = db.Faculties.FirstOrDefault(f => f.FacultyName == specialityForm.comboBoxFaculties.SelectedItem.ToString());
            if (faculty != null && list != null)
            {
                Speciality speciality = new Speciality();
                speciality.PrivelegesCount = Convert.ToInt32(specialityForm.textBoxCountOfPrivileges.Text);
                speciality.Open = Convert.ToInt32(specialityForm.textBoxAvailableCount.Text);
                speciality.PassingScore = Convert.ToInt32(specialityForm.textBoxPassingScore.Text);
                speciality.CompetitionPlaceCount = Convert.ToInt32(specialityForm.textBoxCompetitionCount.Text);
                speciality.SpecialityName = specialityForm.textBoxNameOfSpeciality.Text;
                speciality.SpecialityCode = specialityForm.textBoxCodeOfSpeciality.Text;
                speciality.ShortSpecialityName = specialityForm.textBoxShortNameOfSpeciality.Text;
                speciality.Themes.AddRange(list);
                speciality.Coefficient = Math.Round(1 / (Convert.ToDouble(list.Count) + 1), 2);
                faculty.Specialities.Add(speciality);
                //speciality.FacultyId = faculty.FacultyId;
                //db.Specialities.Add(speciality);
                db.SaveChanges();
                await ViewHelper.UpdateTableSpecialitiesAsync(mainForm.dataGridViewSpecialities, db);
                ViewHelper.PrintInfoMesage("Дані збережено");
                specialityForm.Hide();
                return;
            }
            ViewHelper.PrintCriticalError("Помилка при додаванні спеціальності");
        }

        private void SpecialityForm_selectThemeClick(object sender, EventArgs e)
        {
            var selected = specialityForm.comboBoxThemes.SelectedItem;
            if (!specialityForm.listBoxThemes.Items.Contains(selected))
            {
                specialityForm.listBoxThemes.Items.Add(selected);
            }
        }
        private void MainForm_addSpecialityClick(object sender, EventArgs e)
        {
            specialityForm.listBoxThemes.Items.Clear();
            List<Faculty> faculties = db.Faculties.Select(f => f).ToList();
            List<Theme> themes = db.Themes.Select(t => t).ToList();
            if (themes.Count > 0 && faculties.Count > 0 && themes != null && faculties != null)
            {
                specialityForm.comboBoxFaculties.DataSource = faculties.Select(f => f.FacultyName).ToList();
                specialityForm.comboBoxThemes.DataSource = themes.Select(t => t.ThemeName).ToList();
                specialityForm.ShowDialog();
                return;
            }
            ViewHelper.PrintWarningError("Предмети відсутні");
        }
        private async void ThemeForm_saveChangesClick(object sender, EventArgs e)
        {
            string name = themeForm.textBoxEditThemeName.Text;
            if (!string.IsNullOrEmpty(name))
            {
                dbHelper.UpdateThemeIntoDb(editElementId, name, db);
                await ViewHelper.UpdateTableThemesAsync(mainForm.dataGridViewThemes, db);
                themeForm.Hide();
                editElementId = -1;
                return;
            }
            ViewHelper.PrintWarningError("Заповніть поле");
        }

        private void MainForm_editThemeClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewThemes, "ThemeId");
            if (id.HasValue)
            {
                editElementId = id.Value;
                Theme theme = db.Themes.FirstOrDefault(t => t.ThemeId == id.Value);
                if (theme != null)
                {
                    themeForm.textBoxEditThemeName.Text = theme.ThemeName;
                    themeForm.ShowDialog();
                    return;
                }
            }
            ViewHelper.PrintCriticalError("Помилка при виконанні операції");
        }

        private async void MainForm_deleteThemeClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewThemes, "ThemeId");
            if (id.HasValue)
            {
                await dbHelper.DeleteThemeAsync(id, db);
                await ViewHelper.UpdateTableThemesAsync(mainForm.dataGridViewThemes, db);
                ViewHelper.PrintInfoMesage("Предмет видалено");
            }
        }

        private async void MainForm_addThemeClick(object sender, EventArgs e)
        {
            string theme = mainForm.textBoxNewTheme.Text;
            if (!string.IsNullOrEmpty(theme))
            {
                dbHelper.InsertThemeIntoDb(new Theme { ThemeName = theme }, db);
                await ViewHelper.UpdateTableThemesAsync(mainForm.dataGridViewThemes, db);
                return;
            }
            ViewHelper.PrintWarningError("Заповніть всі поля");
        }

        #region Faculties
        private async void MainForm_deleteFacultyClick(object sender, EventArgs e)
        {
            int? id = dbHelper.GetSelectedId(mainForm.dataGridViewFaculties, "FacultyId");
            await dbHelper.DeleteFacultyAsync(id, db);
            await ViewHelper.UpdateTableFacultiesAsync(mainForm.dataGridViewFaculties, db);
            await ViewHelper.UpdateTableSpecialitiesAsync(mainForm.dataGridViewSpecialities, db);
            ViewHelper.PrintInfoMesage("Запис видалено");
        }

        private void MainForm_editFacultyClick(object sender, EventArgs e)
        {
            int selectedRow = dbHelper.GetSelectedId(mainForm.dataGridViewFaculties, "FacultyId");
            Faculty f = db.Faculties.FirstOrDefault(ff => ff.FacultyId == selectedRow);
            if (f != null)
            {
                facultyForm.nameOfFacultyTextBox.Text = f.FacultyName;
                facultyForm.codeOfFacultyTextBox.Text = f.FacultyCode;
                facultyForm.ShortNameOfFacultyTextBox.Text = f.ShortFacultyName;
                editElementId = f.FacultyId;
                isEdit = true;
                facultyForm.ShowDialog();
                return;
            }
            ViewHelper.PrintCriticalError("Об'єкт не знайдено");
        }

        private async void FacultyForm_saveFacultyClick(object sender, EventArgs e)
        {
            foreach (var c in facultyForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (string.IsNullOrEmpty(t.Text))
                    {
                        ViewHelper.PrintWarningError("Не всі поля заповнені");
                        return;
                    }
                }
            }
            if (!isEdit)
            {
                Faculty faculty = new Faculty
                {
                    FacultyName = facultyForm.nameOfFacultyTextBox.Text,
                    FacultyCode = facultyForm.codeOfFacultyTextBox.Text,
                    ShortFacultyName = facultyForm.ShortNameOfFacultyTextBox.Text
                };
                dbHelper.InsertFacultyIntoDb(faculty, db);
            }
            else
            {
                Faculty faculty = db.Faculties.FirstOrDefault(f => f.FacultyId == editElementId);

                if (faculty != null)
                {
                    faculty.FacultyName = facultyForm.nameOfFacultyTextBox.Text;
                    faculty.FacultyCode = facultyForm.codeOfFacultyTextBox.Text;
                    faculty.ShortFacultyName = facultyForm.ShortNameOfFacultyTextBox.Text;
                    dbHelper.UpdateFacultyIntoDb(faculty, db);
                    editElementId = 0;
                    isEdit = false;
                }
            }

            await ViewHelper.UpdateTableFacultiesAsync(mainForm.dataGridViewFaculties, db);
            await ViewHelper.UpdateComboboxFacultiesAsync(mainForm.comboBoxSelectFacultyMart, db);
            await ViewHelper.UpdateComboboxFacultiesAsync(mainForm.comboBoxSelectFacultyStudents, db);
            facultyForm.Hide();
        }

        private void MainForm_addFacultyClick(object sender, EventArgs e)
        {
            facultyForm.ShowDialog();
        }
        #endregion
        #region Users
        private void MainForm_changeUserClick(object sender, EventArgs e)
        {
            int id = dbHelper.GetSelectedId(mainForm.dataGridViewUsers, "UserId");
            editElementId = id;
            User user = db.Users.Include("UserRole").Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                editUserForm.editUserNameTextBox.Text = user.UserName;
                editUserForm.editNewLoginTextBox.Text = user.UserLogin;
                editUserForm.editPassTextBox.Text = string.Empty;
                editUserForm.editPassRepeatTextBox.Text = string.Empty;
                editUserForm.editRolesCombobox.DataSource = db.Roles.Select(r => r.RoleName).ToList();
                editUserForm.editRolesCombobox.SelectedIndex = editUserForm.editRolesCombobox.FindStringExact(user.UserRole.RoleName);
                editUserForm.ShowDialog();
            }
            else
            {
                ViewHelper.PrintCriticalError("Користувача не знайдено");
            }
        }

        private async void EditUser_saveChangesClick(object sender, EventArgs e)
        {
            foreach (var c in editUserForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = c as TextBox;
                    if (string.IsNullOrEmpty(tb.Text))
                    {
                        ViewHelper.PrintWarningError("Не всі поля заповнено");
                        return;
                    }
                }
            }
            User user = db.Users.Include("UserRole").FirstOrDefault(u => u.UserId == editElementId);
            Role role = db.Roles.FirstOrDefault(r => r.RoleName == editUserForm.editRolesCombobox.Text);
            if (role == null)
            {
                ViewHelper.PrintCriticalError("Роль не знайдено");
                return;
            }
            if (!editUserForm.editPassTextBox.Text.Equals(editUserForm.editPassRepeatTextBox.Text, StringComparison.Ordinal))
            {
                ViewHelper.PrintWarningError("Паролі не співпадають");
                return;
            }
            if (user != null)
            {
                user.UserName = editUserForm.editUserNameTextBox.Text;
                user.UserLogin = editUserForm.editNewLoginTextBox.Text;
                user.Password = editUserForm.editPassRepeatTextBox.Text;
                user.UserRole = role;
                await db.SaveChangesAsync();
                await ViewHelper.UpdateTableOfUsersAsync(mainForm.dataGridViewUsers, db);
                ViewHelper.PrintInfoMesage("Дані збережено");
                editUserForm.Hide();
            }
            else
            {
                ViewHelper.PrintCriticalError("Не вдалось змінити дані користувача");
            }
        }

        private async void MainForm_deleteUserClick(object sender, EventArgs e)
        {
            var rowId = mainForm.dataGridViewUsers.SelectedCells[0].RowIndex;
            DataGridViewRow row = mainForm.dataGridViewUsers.Rows[rowId];
            int id = Convert.ToInt32(row.Cells["UserId"].Value);
            User user = db.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                await ViewHelper.UpdateTableOfUsersAsync(mainForm.dataGridViewUsers, db);
                ViewHelper.PrintInfoMesage("Користувача видалено.");
            }
            else
            {
                ViewHelper.PrintCriticalError("Помилка при видаленні користувача");
            }
        }
        private async void MainForm_addUserClick(object sender, EventArgs e)
        {
            string name = mainForm.userNameTextBox.Text;
            string login = mainForm.newLoginTextBox.Text;
            string pass = mainForm.passTextBox.Text;
            string pass2 = mainForm.passRepeatTextBox.Text;
            string role = mainForm.rolesCombobox.SelectedItem.ToString();
            Role userRole = db.Roles.FirstOrDefault(r => r.RoleName == role);
            if (userRole == null)
            {
                ViewHelper.PrintCriticalError("Роль не знайдена");
                return;
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass) &&
                !string.IsNullOrEmpty(pass2) && !string.IsNullOrEmpty(role))
            {
                if (pass.Equals(pass2, StringComparison.Ordinal))
                {
                    User user = new User();
                    user.UserName = name;
                    user.UserLogin = login;
                    user.Password = pass;
                    user.DateOfCreating = DateTime.Now.Date;
                    user.UserRole = userRole;
                    try
                    {
                        db.Users.Add(user);
                        await db.SaveChangesAsync();
                        await ViewHelper.UpdateTableOfUsersAsync(mainForm.dataGridViewUsers, db);
                        ViewHelper.PrintInfoMesage("Нового користувача додано");
                    }
                    catch (Exception ex)
                    {
                        ViewHelper.PrintCriticalError("Помилка при додаванні нового користувача. " + ex.Message);
                    }
                }
                else
                {
                    ViewHelper.PrintWarningError("Паролі не співпадають");
                }
            }
            else
            {
                ViewHelper.PrintWarningError("Заповніть всі поля");
            }
        }
        #endregion
        #region Authorization
        private void MainForm_logoutClick(object sender, EventArgs e)
        {
            loginForm.Show();
            mainForm.Hide();
            mainForm.labelUser.Text = "Доброго дня, ";
            loginForm.textBoxLogin.Text = string.Empty;
            loginForm.textBoxPassword.Text = string.Empty;
        }

        private void LoginForm_buttonLoginClick(object sender, EventArgs e)
        {
            string userName = loginForm.textBoxLogin.Text;
            string pass = loginForm.textBoxPassword.Text;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
            {
                try
                {
                    User user = db.Users.FirstOrDefault(u => u.UserName == userName);
                    if (user != null && pass == user.Password)
                    {
                        mainForm.Show();
                        mainForm.labelUser.Text += user.UserName;
                        loginForm.Hide();
                    }
                    else
                    {
                        ViewHelper.PrintCriticalError("Невірно введені логін або пароль");
                        loginForm.textBoxLogin.Text = string.Empty;
                        loginForm.textBoxPassword.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {

                    ViewHelper.PrintCriticalError("Не вдалося з'єднатися з БД. " + ex.Message);
                }
            }
            else
            {
                ViewHelper.PrintWarningError("Заповніть всі поля");
            }
        }
        #endregion
        private async void MainForm_mainFormLoad(object sender, EventArgs e)
        {
            await ViewHelper.UpdateTableOfUsersAsync(mainForm.dataGridViewUsers, db);
            await ViewHelper.UpdateTableFacultiesAsync(mainForm.dataGridViewFaculties, db);
            await ViewHelper.UpdateTableThemesAsync(mainForm.dataGridViewThemes, db);
            await ViewHelper.UpdateTableSpecialitiesAsync(mainForm.dataGridViewSpecialities, db);
            await ViewHelper.UpdateTableEducationAsync(mainForm.dataGridViewEduLevels, db);
            await ViewHelper.UpdateComboboxFacultiesAsync(mainForm.comboBoxSelectFacultyMart, db);
            await ViewHelper.UpdateComboboxFacultiesAsync(mainForm.comboBoxSelectFacultyStudents, db);

            ViewHelper.UpdateViews(mainForm, db);

            marticulantForm.dataGridViewThemesForMarticulant.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void MainForm_mainFormClosing(object sender, EventArgs e)
        {
            loginForm.Close();
            facultyForm.Close();
            editUserForm.Close();
            themeForm.Close();
            specialityForm.Close();
            marticulantForm.Close();
        }
    }
}
