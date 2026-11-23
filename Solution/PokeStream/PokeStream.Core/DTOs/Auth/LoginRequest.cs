using System.ComponentModel.DataAnnotations;
using PokeStream.Resources.Auth;

namespace PokeStream.Core.DTOs.Auth;

/// <summary>
/// Request para login
/// </summary>
public class LoginRequest
{
    [Display(ResourceType = typeof(LoginResource), Name = nameof(LoginResource.UsernameOrEmail))]
    [Required(ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.UsernameOrEmailRequired))]
    [MaxLength(100, ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.UsernameOrEmailMaxLength))]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Display(ResourceType = typeof(LoginResource), Name = nameof(LoginResource.Password))]
    [Required(ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.PasswordRequired))]
    [MaxLength(100, ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.PasswordMaxLength))]
    public string Password { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.DeviceInfoMaxLength))]
    public string? DeviceInfo { get; set; }

    [MaxLength(500, ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.UserAgentMaxLength))]
    public string? UserAgent { get; set; }

    [MaxLength(100, ErrorMessageResourceType = typeof(LoginResource), ErrorMessageResourceName = nameof(LoginResource.IpAddressMaxLength))]
    public string? IpAddress { get; set; }
}
