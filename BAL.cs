using System;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RPS
{
    static class Smatch
    {
        public static bool IsValid(string value)
        {
            return Regex.IsMatch(value, @"(MF|JS)([0-9]{2})(MSC|PHD)([0-9]{4})", RegexOptions.Singleline);
        }
    }

    [Table("TableA")]
    class BAL
    {
        [Key]
        [MaxLength(20)]
        [Required(ErrorMessage = "Cannot be left Empty")]
        public String Userid { get; set; }
        [MinLength(5)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Cannot be left Empty")]
        public string Pass { get; set; }
    }
    [Table("TableB")]
    class AddReviewerBAL : IDisposable
    {
        [Key]
        public int Reviewerid { get; set; }
        [DataType(DataType.Date)]
        public DateTime DTAppoint { get; set; }
        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(15, ErrorMessage = "Initials cannot exceed 15 characters")]
        public string Initials { get; set; }
        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Full Name cannot exceed 50 characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Cannot be left Empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The entry must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(10, ErrorMessage = "Qualificaton cannot exceed 10 characters")]
        public string Qualification { get; set; }

        [MaxLength(10, ErrorMessage = "Channel cannot exceed 10 characters")]
        public string Channel { get; set; }//internal / external
        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(25, ErrorMessage = "Type of Reviewer cannot exceed 25 characters")]//editorial board member, independent reviewer
        public string Type { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(25, ErrorMessage = "Designation cannot exceed 25 characters")]
        public string Designation { get; set; }
        //[Required(ErrorMessage = "Cannot be left Empty")]
        //[MaxLength(250, ErrorMessage = "Expertise cannot exceed 250 characters")]
        //public List<string> Expertise { get; set; }
        //public bool HaveAdd { get; set; }

        public virtual RevOfficeAdd RevOfficeAdd { get; set; }
        public virtual RevHomeAdd RevHomeAdd { get; set; }
        public ICollection<Expertise> Expertise { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("TableC")]
    class RevOfficeAdd : IDisposable
    {
        [ForeignKey("AddReviewerBAL")]
        public int RevOfficeAddid { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Institute Name cannot exceed 50 characters")]
        public string Institute { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Street Address cannot exceed 50 characters")]
        public string StreetAdd { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "City Name cannot exceed 50 characters")]

        public string City { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "State / Province Name cannot exceed 50 characters")]

        public string StateProvince { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Country Name cannot exceed 50 characters")]
        public string Country { get; set; }


        [MaxLength(15, ErrorMessage = "Landline cannot exceed 15 characters")]
        public string OffLandline { get; set; }

        [MaxLength(15, ErrorMessage = "Cell Number cannot exceed 15 characters")]
        public string OffCell { get; set; }
        public virtual AddReviewerBAL AddReviewerBAL { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("TableD")]
    class RevHomeAdd : IDisposable
    {
        [ForeignKey("AddReviewerBAL")]
        public int RevHomeAddid { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Street Address cannot exceed 50 characters")]
        public string StreetAdd { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "City Name cannot exceed 50 characters")]

        public string City { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "State / Province Name cannot exceed 50 characters")]

        public string StateProvince { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(50, ErrorMessage = "Country Name cannot exceed 50 characters")]
        public string Country { get; set; }


        [MaxLength(15, ErrorMessage = "Landline cannot exceed 15 characters")]
        public string HomeLandline { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(15, ErrorMessage = "Cell Number cannot exceed 15 characters")]
        public string Cell { get; set; }
        public virtual AddReviewerBAL AddReviewerBAL { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("TableE")]
    class Expertise : IDisposable
    {
        [Key]
        public int Expertiseid { get; set; }

        [Required(ErrorMessage = "Cannot be left Empty")]
        [MaxLength(75, ErrorMessage = "Experty cannot exceed 75 characters")]
        public string Expertin { get; set; }

        //[ForeignKey("AddReviewerBAL")]
        public int Reviewerid { get; set; }
        public AddReviewerBAL AddReviewerBAL { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("TableF")]
    class PAuthor : IDisposable
    {
        [Key]
        public int Authorid { get; set; }
        [Required, MaxLength(50)]
        public string PauthorName { get; set; }
        [Required, MaxLength(150)]
        public string PauthorAff { get; set; }
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Pauthoremail { get; set; }
        [Required, MaxLength(15)]
        public string Pauthorcell { get; set; }

        public ICollection<Article> Article
        {
            get; set;
        }

        public void Dispose()
        {

        }
    }
    [Table("TableG")]
    class CAuthor // Not Implemented
    {
        [Key]
        public int CAuthorid { get; set; }
        [Required, MaxLength(50)]
        public string CauthorName { get; set; }
        [MaxLength(150)]
        public string CauthorAff { get; set; }
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Cauthoremail { get; set; }
        [MaxLength(15)]
        public string Cauthorcell { get; set; }
        public string ArticleID { get; set; }
        public Article Article { get; set; }
    }
    public enum Articlestatus : Byte
    {
        Inplagiarism = 0,
        Consentletter = 1,
        Inreview = 2,
        AuthorRevision = 4,
        ReReview = 8,
        Reject = 16,
        Accept = 32,
        Published = 64
    }
    [Table("TableH")]
    class Article : IDisposable
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ArticleID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTsubmission { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }
        [Required, EnumDataType(typeof(Articlestatus))]
        public Articlestatus Articlestatus
        {
            get; set;
        }
        public int Authorid { get; set; }
        public PAuthor PAuthor { get; set; }
        public ICollection<CAuthor> CAuthor { get; set; }

        public void Dispose()
        {

        }
    }
    //[Table("TableI")]
    //class Payment
    //{ }
    class Conn : DbContext
    {
        public Conn() : base("cs")
        {

        }


        public DbSet<BAL> LoginBals
        {
            get; set;
        }
        public DbSet<AddReviewerBAL> AddReviewerBALs { get; set; }
        public DbSet<RevOfficeAdd> RevOfficeAdds { get; set; }
        public DbSet<RevHomeAdd> RevHomeAdds { get; set; }
        public DbSet<Expertise> Expertises { get; set; }

        public DbSet<PAuthor> PAuthors { get; set; }

        public DbSet<CAuthor> CAuthors { get; set; }
        public DbSet<Article> Articles { get; set; }

    }
}
