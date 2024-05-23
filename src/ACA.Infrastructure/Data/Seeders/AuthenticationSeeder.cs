using ACA.Application.Abstractions.UseCases.Authentication;
using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ACA.Domain.PermissionAggregate;

namespace ACA.Infrastructure.Data.Seeders
{
    public class AuthenticationSeeder : BackgroundService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User?> _userRepository;
        private static readonly List<Role> _roles = new()
        {
            new Role("administrator", "مدیریت", false)
            {
                Permissions = new List<Permission>
                {
                    AuthenticationConstants.UpdateProfilePermission,
                    AuthenticationConstants.GetProfilePermission,
                }
            },
            new Role("user", "کاربر عادی", false)
            {
                Permissions = new List<Permission>
                {
                    AuthenticationConstants.UpdateProfilePermission,
                    AuthenticationConstants.GetProfilePermission,
                }
            }
        };

        private readonly List<User?> _users = new()
        {
            User.Create("Akbar", "Ahmadi", "98", "09371770774")
        };

        public AuthenticationSeeder(IServiceProvider sp)
        {
            using var scope = sp.CreateScope();
            _roleRepository = scope.ServiceProvider.GetRequiredService<IRepository<Role>>();
            _userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var role in _roles)
            {
                if (await _roleRepository.ExistAsync(x => x.Name.Equals(role.Name), stoppingToken))
                {
                    continue;
                }
                await _roleRepository.InsertAsync(role, cancellationToken: stoppingToken);
            }

            foreach (var user in _users)
            {
                if (await _userRepository.ExistAsync(x => x.PhoneNumber.Equals(user.PhoneNumber), stoppingToken))
                {
                    continue;
                }
                await _userRepository.InsertAsync(user, cancellationToken: stoppingToken);
            }
        }
    }
}
