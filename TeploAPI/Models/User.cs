using System;
using TeploAPI.Models;

namespace SweetAPI.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
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
        /// Пароль
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// IP-адрес последнего входа
        /// </summary>
        public string? LastLoginIp { get; set; }
        /// <summary>
        /// Дата последнего входа
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        public User()
        {
            LastLoginDate = DateTime.Now;
        }
    }
}
