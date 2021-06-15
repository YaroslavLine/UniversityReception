using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityReception.Models;

namespace UniversityReception.Helpers
{
    public class Helper
    {
        public static void PrintWarningError(string message)
        {
            MessageBox.Show(message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void PrintCriticalError(string message)
        {
            MessageBox.Show(message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                List<Faculty> list = db.Faculties.Select(f => f).ToList();
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
                List<Theme> list = db.Themes.Select(t => t).ToList();
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
                view.DataSource = db.Specialities.Select(s => s).ToList();
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
                view.DataSource = db.EducationLevels.Select(l => l).ToList();
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
                specialities.Items.AddRange(faculty.Specialities.Select(s => s.SpecialityName).ToArray());
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                PrintCriticalError("Помилка при оновленні даних.\n" + ex.Message);
                return Task.CompletedTask;
            }
        }

        internal static void UpdateTableThemesForAttempting(ListBox.ObjectCollection specialities, DataGridView view, DbContext db)
        {
            //view.Rows.Clear();
            List<string> sList = new List<string>();
            List<Theme> tList = new List<Theme>();
            Dictionary<string, int> scores = new Dictionary<string, int>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Предмет");
            dt.Columns.Add("Оцінка");

            foreach (var item in specialities)
            {
                sList.Add(item.ToString());
            }
            var nameSet = new HashSet<string>(sList);
            var fs = db.Specialities.Include("Themes").Where(s => nameSet.Contains(s.SpecialityName)).ToList(); ;
            foreach (var s in fs)
            {
                tList.AddRange(s.Themes.Select(t => t));

            }
            //scores.Add("Biology", 15);
            var themes = tList.GroupBy(t => t.ThemeName).ToList();

            foreach (var item in themes)
            {
                dt.Rows.Add(item.Key, 0);
            }
            view.DataSource = dt;
        }

        internal static Task UpdateComboboxFaculties(ComboBox faculties, DbContext db)
        {
            List<Faculty> fList = db.Faculties.Include("Specialities").Select(f => f).ToList();

            if (fList != null)
            {
                faculties.Items.AddRange(fList.Select(f => f.FacultyName).ToArray());
                return Task.CompletedTask;
            }
            PrintCriticalError("Помилка при отриманні даних");
            return Task.CompletedTask;
        }
    }
}
