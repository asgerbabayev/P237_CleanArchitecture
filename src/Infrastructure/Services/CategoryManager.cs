using AutoMapper;
using Microsoft.Extensions.Hosting;
using Nest.Application.Common.Extensions;
using Nest.Application.Dtos.Categories;
using Nest.Application.Repositories;
using Nest.Application.Services;
using Nest.Domain.Entities;
using Nest.Domain.Exceptions;

namespace Nest.Infrastructure.Services;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHostEnvironment _environment;
    private readonly IMapper _mapper;
    public CategoryManager(ICategoryRepository categoryRepository, IHostEnvironment environment, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _environment = environment;
        _mapper = mapper;
    }

    public async Task<CreateCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
    {

        var filename = await createCategoryDto.Logo.SaveFileAsync(_environment.ContentRootPath, "wwwroot/uploads");
        Category category = _mapper.Map<Category>(createCategoryDto);
        category.Logo = filename;
        await _categoryRepository.CreateAsync(category);

        return createCategoryDto;

    }

    public async Task DeleteCategory(int id, CategoryDeleteDto deleteCategoryDto)
    {
        if (id != deleteCategoryDto.Id) throw new Exception("Id is incorrect");
        await _categoryRepository.DeleteAsync(deleteCategoryDto.Id);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAll(null, "SubCategories");
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
    public async Task<IEnumerable<CategoryDto>> GetAllParentCategoriesWithInclude()
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAll(x => x.ParentId == null, "Product", "SubCategories");
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
    public async Task<CategoryDto> GetCategory(int id)
    {
        Category? category = await _categoryRepository.Get(id);
        if (category == null) throw new NotFoundException(typeof(Category), 404);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<UpdateCategoryDto> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        if (id != updateCategoryDto.Id) throw new Exception("Id is incorrect");
        var existCategory = await _categoryRepository.Get(id);
        var filename = await updateCategoryDto.Logo.SaveFileAsync(_environment.ContentRootPath, "wwwroot/uploads");
        updateCategoryDto.Logo.DeleteFile(_environment.ContentRootPath, "wwwroot/uploads", existCategory.Logo);
        Category category = _mapper.Map<Category>(updateCategoryDto);
        category.Logo = filename;
        await _categoryRepository.UpdateAsync(category);
        return updateCategoryDto;
    }
}
