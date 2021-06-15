using System.Collections.Generic;

namespace UniversityReception.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public string ShortSpecialityName { get; set; }
        public string SpecialityCode { get; set; }
        public int Open { get; set; }
        public int PassingScore { get; set; }
        public int RecievedClaims { get; set; }
        public int PrivelegesCount { get; set; }
        public int CompetitionPlaceCount { get; set; }
        public double Coefficient { get; set; }
        public List<Theme> Themes { get; set; }
        public List<Marticulant> Marticulants { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public Speciality()
        {
            Themes = new List<Theme>();
            Marticulants = new List<Marticulant>();
        }

    }
}