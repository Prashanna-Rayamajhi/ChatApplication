using ChatAppClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Repository
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetAppUserByEmail(string email);

        Task PostAppUser(AppUser appUser);
    }
}
