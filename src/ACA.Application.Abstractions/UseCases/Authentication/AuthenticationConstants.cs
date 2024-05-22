using ACA.Domain.PermissionAggregate;

namespace ACA.Application.Abstractions.UseCases.Authentication;

public class AuthenticationConstants
{
  private const string PermissionGroup = "اهراز هویت";
  private const string PermissionNamePrefix = "auth";
  

  public static readonly Permission GetProfilePermission = 
    new Permission($"{PermissionNamePrefix}.read.profile","نمایش حساب کابری",PermissionGroup);
  
  public static readonly Permission UpdateProfilePermission = 
    new Permission($"{PermissionNamePrefix}.update.profile","ویرایش حساب کابری",PermissionGroup);
}
