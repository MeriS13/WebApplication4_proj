namespace Board.Contracts.Account
{
    /// <summary>
    /// Модель для входа в аккаунт. (***скопировано***)
    /// </summary>
    public class LoginAccountDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}