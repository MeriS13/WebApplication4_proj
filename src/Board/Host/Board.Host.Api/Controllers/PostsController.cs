using Microsoft.AspNetCore.Mvc;

namespace Board.Host.Api.Controllers
{
    ///<summary/>
    ///Api-контроллер для работы с постами вроде
    /// имеет логгер
    ///</summary>

    [ApiController]
    [Route(template: "controller")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }


        //тип запроса, маршрут по которому обращается
        [HttpGet(template: "get-posts")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(message: $"Запрос объявления");
            return await Task.Run(Ok);
        }

        [HttpPost]
        
    }

}
