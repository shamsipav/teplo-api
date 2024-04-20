namespace TeploAPI.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// IP-адрес последнего входа
        /// </summary>
        public string? LastLoginIP { get; set; }
        /// <summary>
        /// Дата последнего входа
        /// </summary>
        public DateTime LastLoginDate { get; set; }
    }
}
