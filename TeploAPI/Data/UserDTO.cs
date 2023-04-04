﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetAPI.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

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
