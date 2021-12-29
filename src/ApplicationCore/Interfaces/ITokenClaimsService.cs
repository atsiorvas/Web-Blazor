using System.Threading.Tasks;

namespace BlazorApp.ApplicationCore.Interfaces {

    public interface ITokenClaimsService {
        Task<string> GetTokenAsync(string userName);
    }
}