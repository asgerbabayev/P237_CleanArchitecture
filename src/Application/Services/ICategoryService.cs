using Nest.Application.Dtos.Categories;

namespace Nest.Application.Services;

public interface ICategoryService
{
    Task<CreateCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
    Task<UpdateCategoryDto> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategory(int id, CategoryDeleteDto deleteCategoryDto);
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<CategoryDto> GetCategory(int id);
}

