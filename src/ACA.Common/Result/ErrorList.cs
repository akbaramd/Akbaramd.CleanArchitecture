namespace ACA.Common.Result;

public record ErrorList(IEnumerable<string> ErrorMessages, string? CorrelationId = null);