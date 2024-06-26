using System.Text.Json.Serialization;

namespace ACA.Common.Result;

public class PagedResult<T> : Result<T>
{
  public PagedResult(PagedInfo pagedInfo, T value) : base(value)
  {
    PagedInfo = pagedInfo;
  }

  [JsonInclude] 
  public PagedInfo PagedInfo { get; init; }
}