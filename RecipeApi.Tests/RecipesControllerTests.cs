using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeApi.Controllers;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Services;

namespace RecipeApi.Tests.Controllers;

public class RecipesControllerTests
{
    private readonly Mock<IRecipeService> _mockService;
    private readonly RecipesController _controller;

    public RecipesControllerTests()
    {
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipesController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pannkakor" }
        };

        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(recipes);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRecipes = Assert.IsType<List<Recipe>>(okResult.Value);
        Assert.Single(returnedRecipes);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

        var result = await _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
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

        var createdRecipe = new Recipe
        {
            Id = 1,
            Name = "Pannkakor",
            Description = "Test",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Mjöl", Quantity = 3, Unit = "dl" }
            },
            Instructions = new List<string> { "Blanda", "Stek" }
        };

        _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(createdRecipe);

        var result = await _controller.Create(dto);

        var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(RecipesController.GetById), createdAtResult.ActionName);
    }
}