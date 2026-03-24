using Moq;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;
using RecipeApi.Services;

namespace RecipeApi.Tests.Services;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _mockRepo;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _mockRepo = new Mock<IRecipeRepository>();
        _service = new RecipeService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsList()
    {
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pannkakor" },
            new Recipe { Id = 2, Name = "Soppa" }
        };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(recipes);

        var result = await _service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRecipe()
    {
        var recipe = new Recipe { Id = 1, Name = "Pannkakor" };

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(recipe);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Pannkakor", result!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsRecipe()
    {
        var dto = new CreateRecipeDto
        {
            Name = "Pannkakor",
            Description = "Test",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Ingredients = new List<CreateIngredientDto>
            {
                new CreateIngredientDto { Name = "Mjöl", Quantity = 3, Unit = "dl" }
            },
            Instructions = new List<string> { "Blanda", "Stek" }
        };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Recipe>()))
            .ReturnsAsync((Recipe recipe) =>
            {
                recipe.Id = 1;
                return recipe;
            });

        var result = await _service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Pannkakor", result.Name);
    }

    [Fact]
    public async Task SearchAsync_FiltersCorrectly()
    {
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pannkakor" }
        };

        _mockRepo.Setup(r => r.SearchAsync("pann")).ReturnsAsync(recipes);

        var result = await _service.SearchAsync("pann");

        Assert.Single(result);
        Assert.Equal("Pannkakor", result[0].Name);
    }
}