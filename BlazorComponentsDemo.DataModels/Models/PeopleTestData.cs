using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorComponentsDemo.DataModels.Models;

public partial class PeopleTestData
{
    [Key]
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("firstName")]
	public string FirstName { get; set; } = null!;

	[JsonPropertyName("lastName")]
	public string LastName { get; set; } = null!;

	[JsonPropertyName("birthDate")]
	public DateTime BirthDate { get; set; }

	[JsonPropertyName("email")]
	public string Email { get; set; } = null!;

	[JsonPropertyName("status")]
	public string Status { get; set; } = null!;
}
