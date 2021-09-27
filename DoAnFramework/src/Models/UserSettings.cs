namespace DoAnFramework.src.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class UserSettings
    {
        public byte? CalcAge(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            var birthDate = date.Value;
            var now = DateTime.Now;

            var age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }

            return (byte)age;
        }
        public UserSettings()
        {
            this.DateOfBirth = null;
        }

        [Column("FirstName")]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Column("City")]
        [MinLength(GlobalConstants.CityMinLength)]
        [MaxLength(GlobalConstants.CityMaxLength)]
        public string City { get; set; }

        [Column("EducationalInstitution")]
        public string EducationalInstitution { get; set; }

        [Column("FacultyNumber")]
        [MaxLength(GlobalConstants.FacultyNumberMaxLength)]
        public string FacultyNumber { get; set; }

        [Column("DateOfBirth")]
        [DataType(DataType.Date)]
        //// TODO: [Column(TypeName = "Date")] temporally disabled because of SQL Compact database not having "date" type
        public DateTime? DateOfBirth { get; set; }

        [Column("Company")]
        [MaxLength(GlobalConstants.CompanyMaxLength)]
        [MinLength(GlobalConstants.CompanyMinLength)]
        public string Company { get; set; }

        [Column("JobTitle")]
        [MaxLength(GlobalConstants.JobTitleMaxLength)]
        [MinLength(GlobalConstants.JobTitleMinLength)]
        public string JobTitle { get; set; }

        [NotMapped]
        public byte? Age => CalcAge(this.DateOfBirth);
    }
}
