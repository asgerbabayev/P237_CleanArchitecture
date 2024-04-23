﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nest.Application.Dtos.Auth;

[ValidateNever]
public class LoginDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}