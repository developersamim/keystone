using System;
using System.ComponentModel.DataAnnotations;

namespace identity_server.Controllers.Account;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
}
