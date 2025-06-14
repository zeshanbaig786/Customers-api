using FluentResults;

namespace Customers.Api.Models;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    public string TraceId { get; set; }

    public static ErrorResponse FromResult<T>(Result<T> result)
    {
        ArgumentNullException.ThrowIfNull(result, nameof(result));
        return new ErrorResponse
        {
            Type = "https://example.com/error",
            Title = "An error occurred while processing your request.",
            Status = result.IsFailed ? 500 : 200,
            Errors = result.Errors.ToDictionary(
                error => error.Message,
                error => new[]
                {
                    string.Join(",",error.Reasons.Select(s=>s.Message)) 
                    ?? "An unknown error occurred."
                }),
            TraceId = Guid.NewGuid().ToString() // Simulating a trace ID for the example
        };
    }
}
