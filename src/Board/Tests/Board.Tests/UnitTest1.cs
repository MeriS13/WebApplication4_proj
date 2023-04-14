using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.Categories.Services;
using Board.Contracts.Category;
using Board.Domain.Categories;
using Moq;
using Xunit;

namespace Board.Tests
{
    // “ут будут все тесты по сервису категорий
    public class CategoryServiceTests  
    {
        // тест на проверку успешного выполнени€ метода - "«еленый тест"
        // Arrange Act Assert - AAA !!!!!
        [Fact]
        public async Task Category_GetByIdAsync_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            CancellationToken token = new CancellationToken(false);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

            Category expected = new Category
            {
                Id = id,
                Name = "test name",
                ParentId = id
            };

            categoryRepositoryMock.Setup(x => x.GetByIdAsync(id, token)).ReturnsAsync(() => expected);

            CategoryService service = new CategoryService(categoryRepositoryMock.Object);

            // Act
            var result = await service.GetByIdAsync(id, token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.ParentId, result.ParentId);

            categoryRepositoryMock.Verify(x => x.GetByIdAsync(id, token), Times.Once);

        }
    }
}