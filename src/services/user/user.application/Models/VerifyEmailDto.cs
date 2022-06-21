using System;
namespace user.application.Models;

public class VerifyEmailDto
{
	public string Code { get; set; }
	public bool IsCodeValid { get; set; }
}

