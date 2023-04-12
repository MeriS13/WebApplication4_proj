using System.Security.Claims;

namespace Board.Contracts.Accounts 
{
    /// <summary>
    /// Информация об аккаунте. 
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Электронный адрес.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Флаг для проверки аутентификации пользователя
        /// </summary>
        public bool IsAuthenticated { get; set; }


        /// <summary>
        /// Схема авторизации
        /// </summary>
        public string Scheme { get; set; }


        /// <summary>
        /// Список клеймов
        /// </summary>
        public List<Claim> Claims { get; set; } = new List<Claim>();    
    }
}