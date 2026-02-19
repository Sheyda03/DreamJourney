using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.ViewModels.User
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Това поле е задължително!")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [Display(Name = "Парола")]
        public string Password { get; set; }
    }
}
