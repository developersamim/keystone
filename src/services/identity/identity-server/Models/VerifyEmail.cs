using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace identity_server.Models;

public class VerifyEmail
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }
	public string Code { get; set; }
	public int ValidMinute { get; set; }
	public bool IsCodeValid { get; set; }
	public DateTimeOffset ExpirationDate { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public DateTimeOffset ModifiedDate { get; set; }

	public string UserId { get; set; }
	[ForeignKey("UserId")]
	public ApplicationUser ApplicationUser { get; set; }

	public VerifyEmail(string userId)
    {
		Random random = new Random();
		Code = random.Next(1, 1000000).ToString("00000");

		ValidMinute = 10;

		ExpirationDate = DateTimeOffset.UtcNow.AddMinutes(10);
		CreatedDate = DateTimeOffset.UtcNow;

		IsCodeValid = true;

		UserId = userId;
    }
}

