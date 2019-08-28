using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public partial class UserInfo
    {

    }
    [MetadataType(typeof(UserInfo))]
    public class userInfo2
    {   
        [Key]
        public string ID { get; set; }
        [Required(ErrorMessage ="Enter Last Name")]
        [StringLength(maximumLength:10, MinimumLength =3,ErrorMessage ="The Last Name is wrong")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "The First Name is wrong")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Your Are Docter?")]
        public bool isDoctor { get; set; }
        [Display(Name = "Sex")]
        public bool gender { get; set; }
    }
}