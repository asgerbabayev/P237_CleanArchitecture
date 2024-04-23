using Microsoft.AspNetCore.Http;

namespace Nest.Application.Dtos.Categories;
public class CreateCategoryDto
{
    public string Name { get; set; } = null!;
    public IFormFile Logo { get; set; } = null!;
    public int? ParentId { get; set; }
}
