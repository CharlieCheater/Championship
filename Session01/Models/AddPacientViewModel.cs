using System.ComponentModel.DataAnnotations;

namespace Session01.Models
{
    public class AddPacientViewModel
    {
        [Required(ErrorMessage = "Имя пациента обязательно")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Фамилия пациента обязательно")]
        public string LastName { get; set; }
        public string? Patronymic { get; set; } = null;
        [Required(ErrorMessage = "Номер телефона обязательно")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Адрес пациента обязательно")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Эл. почта пациента обязательно")]
        [EmailAddress(ErrorMessage = "Неверно указана эл. почта")]
        public string Email { get; set; }
        public int GenderId { get; set;}
        [Required(ErrorMessage = "Дата рождения пациента обязательно")]
        public DateTime? Birthday { get; set; }
        public IFormFile? Avatar { get; set; } = null;
    }
}
