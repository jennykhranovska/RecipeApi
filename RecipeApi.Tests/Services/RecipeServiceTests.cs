using Moq;
using RecipeApi.Models;
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
}