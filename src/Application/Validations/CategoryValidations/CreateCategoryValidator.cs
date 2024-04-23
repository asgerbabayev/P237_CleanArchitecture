using FluentValidation;
using Microsoft.AspNetCore.Http;
using Nest.Application.Common.Extensions;
using Nest.Application.Dtos.Categories;

namespace Nest.Application.Validations.CategoryValidations;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Boşluq ola bilmez")
            .NotNull()
            .WithMessage("Null ola bilməz")
            .MaximumLength(150)
            .WithMessage("Name - 150 xarakterden boyuk ola bilmez")
            .Must(NameMustBeUnique).WithMessage("Ad boyukle baslamali");


        RuleFor(x => x.Logo)
            .NotEmpty()
            .WithMessage("Boşluq ola bilmez")
            .NotNull()
            .WithMessage("Boşluq ola bilmez")
            .Must(CheckFileAsync)
            .WithMessage("Fayl 2mbdan boyuk ola bilmez hem de shekil olmali");
    }

    private bool CheckFileAsync(IFormFile file)
    {
        return file != null ? file.CheckFileType("image") && file.CheckFileSize(2) : false;
    }

    private bool NameMustBeUnique(string name)
    {
        return !string.IsNullOrWhiteSpace(name) ? char.IsUpper(name[0]) : false;
    }
}
