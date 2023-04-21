using Board.Domain.Answers;
using Board.Domain.Comments;
using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Accounts 
{
    /// <summary>
    /// Акаунт
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Электронный адрес.
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Список постов
        /// </summary>
        public virtual List<Post> Posts { get; set; }

        /// <summary>
        /// Список комментов
        /// </summary>
        public virtual List<Comment> Comments { get; set; }

        /// <summary>
        /// Список ответов
        /// </summary>
        public virtual List<Answer> Answers { get; set; }
    }
}
