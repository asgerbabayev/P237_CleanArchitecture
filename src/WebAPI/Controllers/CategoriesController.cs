using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest.Application.Dtos.Categories;
using Nest.Application.Services;

namespace Nest.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryService.CreateCategory(createCategoryDto);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto updateCategoryDto)
    {
        var result = await _categoryService.UpdateCategory(id, updateCategoryDto);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CategoryDeleteDto categoryDeleteDto)
    {
        await _categoryService.DeleteCategory(id, categoryDeleteDto);
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _categoryService.GetCategory(id));
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _categoryService.GetAllCategories());
    }
}
