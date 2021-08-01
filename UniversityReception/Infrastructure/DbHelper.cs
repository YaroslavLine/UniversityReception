using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityReception.Helpers;

namespace UniversityReception.Models
{
    public class DbHelper
    {
        public void InsertFacultyIntoDb(Faculty data, DbContext db)
        {
            try
            {
                db.Faculties.Add(data);
                db.SaveChanges();
                ViewHelper.PrintInfoMesage("Дані збережено");
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при додаванні запису. " + ex.Message);
            }
        }
        public void UpdateFacultyIntoDb(Faculty faculty, DbContext db)
        {
            try
            {
                db.SaveChanges();
                ViewHelper.PrintInfoMesage("Дані змінено");
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при редагуванні запису. " + ex.Message);
            }
        }
        public Task DeleteFacultyAsync(int? id, DbContext db)
        {
            if (id.HasValue)
            {
                try
                {
                    Faculty faculty = db.Faculties.FirstOrDefault(f => f.FacultyId == id);
                    db.Faculties.Remove(faculty);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewHelper.PrintCriticalError("Помилка при видаленні об'єкта\n" + ex.Message);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
        public int GetSelectedId(DataGridView view, string columnName)
        {
            if (view.RowCount <= 0)
            {
                ViewHelper.PrintWarningError("Недостатньо даних в таблиці");
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
                    ViewHelper.PrintWarningError("Предмет вже існує");
                    return;
                }

                db.Themes.Add(theme);
                db.SaveChangesAsync();
                ViewHelper.PrintInfoMesage("Дані збережено");
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при додаванні предмету\n" + ex.Message);
            }
        }
        internal void UpdateThemeIntoDb(int id, string newName, DbContext db)
        {
            try
            {
                Theme theme = db.Themes.FirstOrDefault(t => t.ThemeId == id);
                theme.ThemeName = newName;
                db.SaveChangesAsync();
                ViewHelper.PrintInfoMesage("Дані змінено");
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при редагуванні запису. " + ex.Message);
            }
        }
        internal Task DeleteEduLevelAsync(int? id, DbContext db)
        {
            if (id.HasValue)
            {
                try
                {
                    EducationLevel level = db.EducationLevels.FirstOrDefault(l => l.EducationLevelId == id.Value);
                    if (level != null)
                    {
                        db.EducationLevels.Remove(level);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewHelper.PrintCriticalError("Помилка при видаленні об'екта.\n" + ex.Message);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }

        internal Task DeleteMarticulantAsync(int id, string speciality, DbContext db)
        {
            try
            {
                if (id >= 0 && !speciality.Contains("Оберіть"))
                {
                    Speciality sp = db.Specialities.FirstOrDefault(s => s.SpecialityName == speciality);
                    sp.RecievedClaims--;
                    Marticulant m = db.Set<Marticulant>().Find(id);
                    db.Set<Marticulant>().Remove(m);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при видаленні даних\n" + ex.Message);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        internal Task DeleteStudentAsync(int id, string speciality, DbContext db)
        {
            if (id == -1)
            {
                ViewHelper.PrintCriticalError("Помилка отримання id студента");
                return Task.CompletedTask;
            }
            try
            {
                Marticulant m = db.Set<Marticulant>().Find(id);
                db.Set<Marticulant>().Remove(m);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка отримання id студента.\n" + ex.Message);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        internal void InsertLevelOfEducationIntoDb(EducationLevel newLevel, DbContext db)
        {
            try
            {
                EducationLevel lev = db.EducationLevels.FirstOrDefault(l => l.LevelOfEducation == newLevel.LevelOfEducation);
                if (lev == null)
                {
                    db.EducationLevels.Add(newLevel);
                    db.SaveChanges();
                    ViewHelper.PrintInfoMesage("Об'єкт додано");
                    return;
                }
                ViewHelper.PrintWarningError("Об'єкт вже існує");
                return;
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при додаванні запису.\n" + ex.Message);
            }
        }
        internal Task DeleteThemeAsync(int? id, DbContext db)
        {
            try
            {
                Theme theme = db.Themes.FirstOrDefault(t => t.ThemeId == id.Value);
                if (theme != null)
                {
                    db.Themes.Remove(theme);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при видаленні об'єкта\n" + ex.Message);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
        internal int CalculateScores(Speciality speciality, Dictionary<string, int> themesValues, int middleScore)
        {
            double coef = speciality.Coefficient;
            double result = 0;
            foreach (Theme t in speciality.Themes)
            {
                if (themesValues.Keys.Contains(t.ThemeName))
                {
                    result += Convert.ToDouble(themesValues[t.ThemeName] * coef);
                }
            }
            result += Convert.ToDouble(middleScore) * coef;
            return Convert.ToInt32(Math.Round(result));
        }

        internal Task DeleteSpecialityAsync(int? id, DbContext db)
        {
            try
            {
                Speciality speciality = db.Specialities.FirstOrDefault(s => s.SpecialityId == id.Value);
                if (speciality != null)
                {
                    db.Specialities.Remove(speciality);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewHelper.PrintCriticalError("Помилка при видаленні об'єкта\n" + ex.Message);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
