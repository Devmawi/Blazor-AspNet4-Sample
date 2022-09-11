using BlazorAppLoginApi.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorAppLoginApi.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        
        public HttpClient Http { get; set; }
        public CustomAuthenticationStateProvider(HttpClient http)
        {
            Http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = new ClaimsPrincipal();
            // await Task.Delay(1500);

            try
            {
                var userInfo = await Http.GetFromJsonAsync<UserInfo>("api/account/userinfo");
                var identity = new ClaimsIdentity("Cookies");
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userInfo.Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.Role));
                claims = new ClaimsPrincipal(identity);         
            }
            catch (Exception ex)
            {

            }
            var state = new AuthenticationState(claims);

            //NotifyAuthenticationStateChanged(Task.FromResult(state));
            return await Task.FromResult(state);
        }
    }
}
