using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Board.Api.Tests
{
    /*
    public class PostTests
    {
        public PostTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        private readonly BoardWebApplicationFactory _webApplicationFactory;

        [Fact]
        public Task Post_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestPostId;

            // Act
            var response = await httpClient.GetAsync($"Post/{id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<PostDto>();

            Assert.NotNull(result);

            Assert.Equal("test_advert_name", result!.Name);
            Assert.Equal("test_desc", result.Description);
            Assert.True(result.IsActive);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
    */
}
