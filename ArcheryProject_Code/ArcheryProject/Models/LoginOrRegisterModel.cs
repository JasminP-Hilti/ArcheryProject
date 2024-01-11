using System.ComponentModel.DataAnnotations;

namespace ArcheryProject.Models
{
    public class LoginOrRegisterModel
    {

        //Login 
        ////////////////////////////////////////////////////

        [Display(Name = "Username / Email:")]
        [Required(ErrorMessage = "Required")]
        public string? loginNameOrMail { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Required")]
        public string? loginPassword { get; set; }

        //Register 
        ////////////////////////////////////////////////////

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Required")]
        public string? RegisterFirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Required")]
        public string? registerLastName { get; set; }

        [Display(Name = "Username:")]
        [Required(ErrorMessage = "Required")]
        public string? registerUsername { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Required")]
        public string? registerEmail { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Required")]
        public string? registerPassword { get; set; }

        [Display(Name = "Repeat Password:")]
        [Required(ErrorMessage = "Required")]
        public string? registerRepeatPassword { get; set; }


    }
}



