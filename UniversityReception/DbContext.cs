using System;
using System.Data.Entity;
using System.Linq;
using UniversityReception.Models;

namespace UniversityReception
{
    public class DbContext : System.Data.Entity.DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'UniversityReception.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public DbContext()
            : base("name=DbModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }

        public static void AddPrimaryData()
        {
            DbContext db = new DbContext();
            User user = db.Users.FirstOrDefault(u => u.UserName == "admin");
            if (user != null) return;

            Faculty faculty = new Faculty { FacultyName = "����'����� �����", FacultyCode = "001" , ShortFacultyName = "��" };

            Theme[] themes = new[]
            {
                new Theme { ThemeName = "����������" },
                new Theme { ThemeName = "���������" },
                new Theme { ThemeName = "������" },
                new Theme { ThemeName = "��������� ����" }
            };
            EducationLevel[] levels = new[]
            {
                new EducationLevel{ LevelOfEducation = "���� ��������� �����", ShortLevelOfEducation = "���" },
                new EducationLevel{ LevelOfEducation = "��������� ��������� �����", ShortLevelOfEducation = "���"  },
                new EducationLevel{ LevelOfEducation = "����� ������� �����", ShortLevelOfEducation = "�(�)��(11��)"  },
                new EducationLevel{ LevelOfEducation = "������� ��������� �����", ShortLevelOfEducation = "���"  }
            };

            db.Users.Add(new User { UserName = "Admin", UserLogin = "admin", Password = "111", UserRole = new Role { RoleName = "admin" }, DateOfCreating = DateTime.Now.Date });
            db.Roles.Add(new Role { RoleName = "���������� �����" });
            db.Themes.AddRange(themes);
            db.Faculties.Add(faculty);
            db.EducationLevels.AddRange(levels);
            db.SaveChanges();
        }
    }
}