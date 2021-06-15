using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityReception.Models
{
    public class Marticulant
    {
        public int MarticulantId { get; set; }
        public string FullNameOfMarticulant { get; set; }
        public DateTime DateOfAdmittingToLearning { get; set; } = new DateTime(1985, 1, 1);
        public double SumScore { get; internal set; }
        public bool IsDayForm { get; set; }
        public bool OnBudget { get; set; }
        public bool Privileges { get; set; }
        public string PassportCode { get; set; }
        public DateTime BirthDay { get; set; }
        public bool Male { get; set; }
        public string PlaceOfbirth { get; set; }
        public string Nationality { get; set; }
        public string RegistrationAddress { get; set; }
        public string AccommodationAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string LevelOfEducation { get; set; }
        public DateTime YearOfEndingEducation { get; set; }
        public DateTime DateOfClaim { get; internal set; }
        public bool IsNeedHostel { get; set; }
        public bool IsResident { get; set; }
        public string FullNameOfMother { get; set; }
        public string PhoneNumberOfMother { get; set; }
        public string FullNameOfFather { get; set; }
        public string PhoneNumberOfFather { get; set; }
        public string FullNameOfClosePerson { get; set; }
        public string PhoneNumberOfClosePerson { get; set; }
        public bool AdmittedToLearning { get; set; }
        public List<Speciality> Specialities { get; set; }
        public int MiddleScore { get; internal set; }
        public string SelectedSpeciality { get; internal set; }

        public Marticulant()
        {
            Specialities = new List<Speciality>();
        }
    }
}
