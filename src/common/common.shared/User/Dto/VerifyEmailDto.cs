using System;
namespace common.shared.User.Dto;

public class VerifyEmailDto
{
	public string Code { get; set; }
	public bool IsCodeValid { get; set; }
}

