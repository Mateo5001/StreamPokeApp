using System.ComponentModel.DataAnnotations;
using PokeStream.Resources.Auth;

namespace PokeStream.Core.DTOs.Auth;

/// <summary>
/// Request para registro de nuevo usuario
/// </summary>
public class RegisterRequest
{
    [Display(ResourceType = typeof(RegisterResource), Name = nameof(RegisterResource.Username))]
    [Required(ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.UsernameRequired))]
    [MinLength(3, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.UsernameMinLength))]
    [MaxLength(100, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.UsernameMaxLength))]
    public string Username { get; set; } = string.Empty;

    [Display(ResourceType = typeof(RegisterResource), Name = nameof(RegisterResource.Email))]
    [Required(ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.EmailRequired))]
    [EmailAddress(ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.EmailInvalid))]
    [MaxLength(320, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.EmailMaxLength))]
    public string Email { get; set; } = string.Empty;

    [Display(ResourceType = typeof(RegisterResource), Name = nameof(RegisterResource.Password))]
    [Required(ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.PasswordRequired))]
    [MinLength(8, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.PasswordMinLength))]
    [MaxLength(100, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.PasswordMaxLength))]
    public string Password { get; set; } = string.Empty;

    [Display(ResourceType = typeof(RegisterResource), Name = nameof(RegisterResource.DisplayName))]
    [MaxLength(200, ErrorMessageResourceType = typeof(RegisterResource), ErrorMessageResourceName = nameof(RegisterResource.DisplayNameMaxLength))]
    public string? DisplayName { get; set; }
}
