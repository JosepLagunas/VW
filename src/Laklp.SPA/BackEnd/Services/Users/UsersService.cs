using System.Collections.Generic;
using System.Threading.Tasks;
using Laklp.Platform.Data.Entities;

namespace Laklp.Services.Users
{
    internal class UsersService : IUsersService
    {
        //TODO: Pending to implement logic based on Auth0 users management API interaction
        
        public Task CreateUserAsync(string userName, string password, IEnumerable<string> roles)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FetchUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUserRoles(string userName, IEnumerable<string> roles)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserRoles(string userName, IEnumerable<string> roles)
        {
            throw new System.NotImplementedException();
        }

        public Task DisableUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task EnableUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUser(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}