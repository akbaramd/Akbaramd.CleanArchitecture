using FastEndpoints;

namespace ACA.WebApi.Endpoints;


public class GetWeatherForecastEndpoint : EndpointWithoutRequest<GetWeatherForecastEndpointResponse[]>
{
  private readonly string[] summaries = new[]
  {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };


  public override void Configure()
  {
    
    Get("/weatherforecast2");
  }

  public override async Task HandleAsync(  CancellationToken ct)
  {
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new GetWeatherForecastEndpointResponse
        (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
        ))
      .ToArray();

    await SendAsync(forecast, cancellation: ct);
  }
}


