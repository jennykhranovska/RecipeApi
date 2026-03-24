using RecipeApi.Models;
using RecipeApi.Repositories;

namespace RecipeApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            return await _repository.CreateAsync(recipe);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _repository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return await _repository.GetAllAsync();
            }

            return await _repository.SearchAsync(term);
        }
    }
}