using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;

namespace RecipeApi.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        return await _recipeRepository.GetAllAsync();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        return await _recipeRepository.GetByIdAsync(id);
    }

    public async Task<Recipe> CreateAsync(CreateRecipeDto dto)
    {
        var recipe = new Recipe
        {
            Name = dto.Name,
            Description = dto.Description,
            PrepTimeMinutes = dto.PrepTimeMinutes,
            CookTimeMinutes = dto.CookTimeMinutes,
            Servings = dto.Servings,
            Difficulty = dto.Difficulty,
            Ingredients = dto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            }).ToList(),
            Instructions = dto.Instructions
        };

        return await _recipeRepository.CreateAsync(recipe);
    }

    public async Task<bool> UpdateAsync(int id, CreateRecipeDto dto)
    {
        var existing = await _recipeRepository.GetByIdAsync(id);

        if (existing == null)
        {
            return false;
        }

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.PrepTimeMinutes = dto.PrepTimeMinutes;
        existing.CookTimeMinutes = dto.CookTimeMinutes;
        existing.Servings = dto.Servings;
        existing.Difficulty = dto.Difficulty;
        existing.Ingredients = dto.Ingredients.Select(i => new Ingredient
        {
            Name = i.Name,
            Quantity = i.Quantity,
            Unit = i.Unit
        }).ToList();
        existing.Instructions = dto.Instructions;

        return await _recipeRepository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _recipeRepository.DeleteAsync(id);
    }

    public async Task<List<Recipe>> SearchAsync(string term)
    {
        return await _recipeRepository.SearchAsync(term);
    }

    public async Task<List<Recipe>> GetByDifficultyAsync(string difficulty)
    {
        return await _recipeRepository.GetByDifficultyAsync(difficulty);
    }
}