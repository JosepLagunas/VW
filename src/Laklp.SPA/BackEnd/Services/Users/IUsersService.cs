using System.Collections.Generic;
using System.Threading.Tasks;
using Laklp.Platform.Data.Entities;

namespace Laklp.Services.Users
{
    internal interface IUsersService
    {
        Task CreateUserAsync(string userName, string password, IEnumerable<string> roles);
        Task<User> FetchUser(string userName);
        Task AddUserRoles(string userName, IEnumerable<string> roles);

        Task RemoveUserRoles(string userName, IEnumerable<string> roles);
        Task DisableUser(string userName);
        Task EnableUser(string userName);
        Task RemoveUser(string userName);
    }
}