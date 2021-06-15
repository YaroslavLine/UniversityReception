using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityReception.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string FacultyCode { get; set; }
        public string ShortFacultyName { get; set; }
        public List<Speciality> Specialities { get; set; }
        public Faculty()
        {
            Specialities = new List<Speciality>();
        }
    }
}
