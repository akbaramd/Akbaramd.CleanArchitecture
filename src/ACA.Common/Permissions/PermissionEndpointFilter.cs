using Microsoft.AspNetCore.Http;

namespace ACA.Common.Permissions;

public class PermissionEndpointFilter : IEndpointFilter
{
  public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    throw new NotImplementedException();
  }
}