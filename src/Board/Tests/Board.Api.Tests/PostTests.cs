using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Board.Api.Tests
{
    
    public class PostTests : IClassFixture<BoardWebApplicationFactory>
    {
        public PostTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        private readonly BoardWebApplicationFactory _webApplicationFactory;


        /// <summary>
        /// Проверка что при введенном идентификаторе все ок
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Post_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestPostId;

            // Act
            var response = await httpClient.GetAsync($"posts-controller/{id}");

            var result = await response.Content.ReadFromJsonAsync<PostDto>();

            // Assert

            Assert.NotNull(response);

            

            Assert.NotNull(result);
            Assert.Equal("test_post_name1", result!.Name);
            Assert.Equal("test_desc1", result.Description);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Проверка что при неверном идентификаторе генерируется исключение
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Post_GetById_Fail()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = Guid.Parse("d5d57071-5a64-4461-af51-de55676ea625");

            var fordelete  = 0;
            // Act
            var response = await httpClient.GetAsync($"posts-controller/{id}");

            // Assert

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }


    }
    
}
