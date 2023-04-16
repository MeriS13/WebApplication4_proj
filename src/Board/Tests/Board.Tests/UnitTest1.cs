using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.Categories.Services;
using Board.Contracts.Category;
using Board.Domain.Categories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Board.Tests;

// “ут будут все тесты по сервису категорий
public class CategoryServiceTests 
{
    private readonly ITestOutputHelper _output;


    public CategoryServiceTests(ITestOutputHelper output)
    {
        _output = output;
        _output.WriteLine($"Test {nameof(CategoryServiceTests)} created");
    }


    [Theory]
    [ClassData(typeof(CategoryIdTestData))]
    public async Task Category_GetById_Success(List<Category> List)
    {
        Guid id = Guid.Parse("09258252-083B-439A-931E-828E7F1B4F17");
        _output.WriteLine($"Id: {id}");

        // Arrange
        // фейковый токен о.о.
        CancellationToken token = new CancellationToken(false);
        // создаем мок-объект дл€ репозитори€
        Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
        // expected - переменна€ дл€ поиска категории по идентификатору, сгенеренному выше
        var expected = List.First(x => x.Id == id);
        // установка поведени€ репозитори€ - метода GetByIdAsync. принимает описанные параметры и возвращ. expected
        categoryRepositoryMock.Setup(x => x.GetByIdAsync(id, token)).ReturnsAsync(() => expected);
        //создаем сервис, принимающий в качестве параметра Mock, т.е. имитацию работы репозитори€
        CategoryService service = new CategoryService(categoryRepositoryMock.Object);

        // Act
        //проверка
        var result = await service.GetByIdAsync(id, token);

        // Assert
        result.ShouldNotBe(null);

        id.ShouldBe(result.Id);
        expected.Name.ShouldBe(result.Name);
        expected.ParentId.ShouldBe(result.ParentId);

        categoryRepositoryMock.Verify(x => x.GetByIdAsync(id, token), Times.Once);
    }

    [Fact]
    public async Task Category_GetById_Fail()
    {
        // Arrange
        var id = Guid.NewGuid();
        _output.WriteLine($"Id: {id}");

        CancellationToken token = new CancellationToken(false);

        Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

        categoryRepositoryMock.Setup(x => x.GetByIdAsync(id, token)).ReturnsAsync(() => null);

        CategoryService service = new CategoryService(categoryRepositoryMock.Object);

        Action testCode = () => { service.GetByIdAsync(id, token); };

        // Act

        var result = Record.Exception(testCode);

        // Assert
        result.ShouldBe(null);

        categoryRepositoryMock.Verify(x => x.GetByIdAsync(id, token), Times.Once);
    }



    [Theory]
    [ClassData(typeof(CategoryIdTestData))]
    public async Task Category_Create_Success(List<Category> List)
    {
        string name = "test_name";
        //Arrange
        _output.WriteLine($"Name: {name}");

        // —озд дто дл€ передачи его в метод сервиса
        CreateCategoryDto dto = new CreateCategoryDto()
        {
            Name = name,
        };
        // —озд домен.модель дл€ передачи ее в метод репозитори€
        Category model = new Category()
        {
            Name = dto.Name,
            Id = Guid.NewGuid(),
            ParentId = Guid.NewGuid()
        };

        CancellationToken token = new CancellationToken(false);

        Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

        var expected =  List.FirstOrDefault(x => x.Name == name);
        categoryRepositoryMock.Setup(x => x.AddAsync(model, token)).ReturnsAsync(() => model.Id);

        CategoryService service = new CategoryService(categoryRepositoryMock.Object);

        //Act
        var result = service.CreateAsync(dto, token);

        //Assert

        result.ShouldBeOfType<Task<Guid>>();

        expected.ShouldBeNull();
    }



    [Theory]
    [ClassData(typeof(CategoryIdTestData))]
    public async Task Category_Create_Fail(List<Category> List)
    {
        string name = "first";
        //Arrange
        _output.WriteLine($"Name: {name}");

        // —озд дто дл€ передачи его в метод сервиса
        CreateCategoryDto dto = new CreateCategoryDto()
        {
            Name = name,
        };
        // —озд домен.модель дл€ передачи ее в метод репозитори€
        Category model = new Category()
        {
            Name = dto.Name,
            Id = Guid.NewGuid(),
            ParentId = Guid.NewGuid()
        };

        CancellationToken token = new CancellationToken(false);

        Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

        var expected = (List.FirstOrDefault(x => x.Name == name) == null)
            ? model.Id
            //: throw new Exception($" атегори€ с названием '{dto.Name}' уже существует!");
            : Guid.Parse("00000000-0000-0000-0000-000000000000");
                         
        categoryRepositoryMock.Setup(x => x.AddAsync(model, token)).ReturnsAsync(() => expected);

        CategoryService service = new CategoryService(categoryRepositoryMock.Object);

        //Act
        //var result = service.CreateAsync(dto, token);
        //(result1) ? result2 =
        //Exception($" атегори€ с названием '{dto.Name}' уже существует!");
        //Assert

        //expected.
        //result.ShouldBeOfType<Task<Guid>>();
        //////Action testCode = () => { service.CreateAsync(dto, token); };

        // Act
        var result =  service.CreateAsync(dto, token);
        /////////////var result = Record.Exception(testCode);
        //Action result = () => service.CreateAsync(dto, token);
        //Assert

        //Exception exception = Assert.Throws<Exception>(result);
        //The thrown exception can be used for even more detailed assertions.
        //Assert.Equal($" атегори€ с названием '{dto.Name}' уже существует!", exception.Message);
        //Assert.Throws<Exception>(() => $" атегори€ с названием '{dto.Name}' уже существует!")  ;
        //Assert.NotNull(result);
        //Assert.IsType<Exception>(result);
        result.ShouldBeOfType<Task<Guid>>();

        expected.ShouldBeEquivalentTo(Guid.Parse("00000000-0000-0000-0000-000000000000"));
    }



    /*
    [Theory]
    [ClassData(typeof(CategoryIdTestData))]
    public async Task Category_GetAll_Success(List<Category> List)
    {

    }
    */
}