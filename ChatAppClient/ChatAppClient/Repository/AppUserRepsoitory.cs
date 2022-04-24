using ChatAppClient.Model;
using ChatAppClient.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Repository
{
    public class AppUserRepsoitory : IAppUserRepository
    {
        private readonly HttpClient client = new HttpClient();
        public AppUserRepsoitory()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<AppUser> GetAppUserByEmail(string email)
        {
            var response = await client.GetAsync($"/api/AppUsers/{email}");
            if (response.IsSuccessStatusCode)
            {
                AppUser appUser = await response.Content.ReadAsAsync<AppUser>();
                return appUser;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task PostAppUser(AppUser appUser)
        {
            var response = await client.PostAsJsonAsync("/api/AppUsers", appUser);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
            
        }
    }
}
