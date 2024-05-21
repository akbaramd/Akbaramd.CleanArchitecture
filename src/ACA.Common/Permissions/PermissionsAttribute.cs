namespace ACA.Common.Permissions;

public class PermissionsAttribute(params string[] permissions) : Attribute
{
  public string[] Permissions { get; set; } = permissions;
}
