using System.ComponentModel.DataAnnotations;

namespace clientapp.Models;

public class UserUpdateProfileDto
{
	public string GivenName { get; set; }
	public string FamilyName { get; set; }
    [Required(ErrorMessage = "Birth date is required")]
	public DateTime? BirthDate { get; set; }
}
