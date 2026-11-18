using System.Collections.Generic;

namespace PokeStream.Core.DTOs.Common;

/// <summary>
/// Response genérico de error
/// </summary>
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? Errors { get; set; }
}