using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityReception.Helpers;

namespace UniversityReception.Models
{
    public class M
    {
        public void InsertFacultyIntoDb(Faculty data, DbContext db)
        {
            try
            {
                db.Faculties.Add(data);
                db.SaveChangesAsync();
                Helper.PrintInfoMesage("Дані збережено");
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при додаванні запису. " + ex.Message);
            }
        }
        public void UpdateFacultyIntoDb(Faculty faculty, DbContext db)
        {
            try
            {
                db.SaveChangesAsync();
                Helper.PrintInfoMesage("Дані змінено");
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при редагуванні запису. " + ex.Message);
            }
        }
        public void DeleteFaculty(int? id, DbContext db)
        {
            if (id.HasValue)
            {
                try
                {
                    db.Faculties.Remove(db.Faculties.FirstOrDefault(f => f.FacultyId == id));
                    db.SaveChangesAsync();
                    Helper.PrintInfoMesage("Запис видалено");
                }
                catch (Exception ex)
                {
                    Helper.PrintCriticalError("Помилка при видаленні об'єкта\n" + ex.Message);
                }

            }
        }
        public int GetSelectedId(DataGridView view, string columnName)
        {
            if (view.RowCount <= 0)
            {
                Helper.PrintWarningError("Недостатньо даних в таблиці");
                return -1;
            }
            var rowId = view.SelectedCells[0].RowIndex;
            DataGridViewRow row = view.Rows[rowId];
            return Convert.ToInt32(row.Cells[columnName].Value);
        }
        public void InsertThemeIntoDb(Theme theme, DbContext db)
        {
            try
            {
                var v = db.Themes.FirstOrDefault(t => t.ThemeName.Equals(theme.ThemeName, StringComparison.OrdinalIgnoreCase));
                if (v != null)
                {
                    Helper.PrintWarningError("Предмет вже існує");
                    return;
                }

                db.Themes.Add(theme);
                db.SaveChangesAsync();
                Helper.PrintInfoMesage("Дані збережено");
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при додаванні предмету\n" + ex.Message);
            }
        }
        internal void UpdateThemeIntoDb(int id, string newName, DbContext db)
        {
            try
            {
                Theme theme = db.Themes.FirstOrDefault(t => t.ThemeId == id);
                theme.ThemeName = newName;
                db.SaveChangesAsync();
                Helper.PrintInfoMesage("Дані змінено");
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при редагуванні запису. " + ex.Message);
            }
        }
        internal void DeleteEduLevel(int? id, DbContext db)
        {
            if (id.HasValue)
            {
                try
                {
                    EducationLevel level = db.EducationLevels.FirstOrDefault(l => l.EducationLevelId == id.Value);
                    if (level != null)
                    {
                        db.EducationLevels.Remove(level);
                        db.SaveChangesAsync();
                    }
                    Helper.PrintInfoMesage("Об'єкт видалено");
                }
                catch (Exception ex)
                {
                    Helper.PrintCriticalError("Помилка при видаленні об'екта.\n" + ex.Message);
                }
            }
        }
        internal void InsertLevelOfEducationIntoDbAsync(EducationLevel newLevel, DbContext db)
        {
            try
            {
                EducationLevel lev = db.EducationLevels.FirstOrDefault(l => l.LevelOfEducation == newLevel.LevelOfEducation);
                if (lev == null)
                {
                    db.EducationLevels.Add(newLevel);
                    db.SaveChangesAsync();
                    Helper.PrintInfoMesage("Об'єкт додано");
                    return;
                }
                Helper.PrintWarningError("Об'єкт вже існує");
                return;
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при додаванні запису.\n" + ex.Message);
            }
        }
        internal void DeleteTheme(int? id, DbContext db)
        {
            try
            {
                Theme theme = db.Themes.FirstOrDefault(t => t.ThemeId == id.Value);
                if (theme != null)
                {
                    db.Themes.Remove(theme);
                    db.SaveChangesAsync();
                    Helper.PrintInfoMesage("Предмет видалено");
                }
            }
            catch (Exception ex)
            {
                Helper.PrintCriticalError("Помилка при видаленні об'єкта\n" + ex.Message);
            }
        }
    }
}
