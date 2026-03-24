using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Services;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetAll()
    {
        var recipes = await _recipeService.GetAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetById(int id)
    {
        var recipe = await _recipeService.GetByIdAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> Create(CreateRecipeDto dto)
    {
        var createdRecipe = await _recipeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateRecipeDto dto)
    {
        var success = await _recipeService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _recipeService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpGet("search")]
    public async Task<ActionResult<List<Recipe>>> Search([FromQuery] string q)
    {
        var results = await _recipeService.SearchAsync(q);
        return Ok(results);
    }
    [HttpGet("difficulty/{level}")]
    public async Task<ActionResult<List<Recipe>>> GetByDifficulty(string level)
    {
        var results = await _recipeService.GetByDifficultyAsync(level);
        return Ok(results);
    }
}