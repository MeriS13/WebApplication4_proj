namespace Board.Contracts.Accounts 
{
    /// <summary>
    /// Результат входа в аккаунт.
    /// </summary>
    public class LoginResultDto
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public string Token { get; set; }
    }
}
