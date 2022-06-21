using System.ComponentModel.DataAnnotations.Schema;

namespace user.domain;

public class VerifyEmail
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }
	public string Code { get; set; }
	public int ValidMinute { get; set; }
	
	public DateTimeOffset ExpirationDate { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public DateTimeOffset ModifiedDate { get; set; }

	public string UserId { get; set; }
	[ForeignKey("UserId")]
	public ApplicationUser ApplicationUser { get; set; }

	[NotMapped]
	public bool IsCodeValid
    {
        get
        {
			return ExpirationDate > DateTimeOffset.UtcNow;
        }
    }
}

