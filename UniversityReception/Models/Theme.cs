using System.Collections.Generic;

namespace UniversityReception.Models
{
    public class Theme
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public List<Speciality> SpecialityNames { get; set; }
        public Theme()
        {
            SpecialityNames = new List<Speciality>();
        }
    }
}