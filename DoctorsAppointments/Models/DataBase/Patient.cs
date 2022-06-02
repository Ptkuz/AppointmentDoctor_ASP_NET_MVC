using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointments.Models.DataBase
{
    public enum Genders 
    {
        [Display(Name = "Мужчина")]
        male,
        [Display(Name = "Женщина")]
        female
    }

    public class Patient
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Имя пациента не задано")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Имя:")]
        public string? FirstName { get; set; }


        
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное отчество")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Отчество:")]
        public string? Patronymic { get; set; }


        [Required(ErrorMessage = "Фамилия пациента не задана")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректная фамилия")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Фамилия:")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Дата рождения не задана")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения:")]
        public DateTime DateAge { get; set; }


        [Required(ErrorMessage = "Пол не выбран")]
        [Display(Name = "Профиль:")]
        public Genders? Genders { get; set; }


        [Required(ErrorMessage = "Паспорт не указан")]
        [RegularExpression(@"^[0-9]{4}\s[0-9]{6}$", ErrorMessage = "Некорректная серия номер паспорта")]
        [Display(Name = "Серия и номер паспорта")]
        public string? Passport { get; set; }


        [Required(ErrorMessage = "Полис не указан")]
        [RegularExpression(@"^[1-9][0-9]{16}$", ErrorMessage = "Некорректная номер страхового полиса")]
        [Display(Name = "Страховой полис")]
        public string? Polis { get; set; }


        [RegularExpression(@"^[1-9][0-9]{11}$", ErrorMessage = "Некорректная номер СНИЛС")]
        [Display(Name = "СНИЛС")]
        public string? Snils { get; set; }

        public Guid UserKey { get; set; }
        public User? User { get; set; }

        public List<Appointment> Appointments { get; set; } = new();

    }
}
