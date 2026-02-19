using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DreamJourney.ViewModels.User
{
    public class RegisterViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Това поле е задължително!")]
        [MinLength(4, ErrorMessage = "Минималната дължина е 4 символа.")]
        [MaxLength(20, ErrorMessage = "Максималната дължина е 20 символа.")]
        [Display(Name ="Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [EmailAddress(ErrorMessage = "Това не е валиден имейл адрес!")]
        [Display(Name = "Имейл адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [MinLength(2, ErrorMessage = "Минималната дължина е 2 символа.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [MinLength(2, ErrorMessage = "Минималната дължина е 2 символа.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително!")]
        [MinLength(6, ErrorMessage = "Минималната дължина е 6 символа.")]
        [RegularExpression(
            @"^(?=.*[A-Za-z])(?=.*\d).+$",
            ErrorMessage = "Паролата трябва да съдържа поне 1 буква и 1 цифра"
        )]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Роля")]
        public bool Role { get; set; }
    }
}
