using Microsoft.Extensions.FileSystemGlobbing;
using Project13.Tips.Api.Constants;
using Project13.Tips.Api.Models;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;

namespace Project13.UI.Services
{
    public sealed class CouponApiService
    {
        private readonly HttpClient _http;

        public CouponApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("CouponApi");
        }

        public Task<CouponDto?> GetCouponAsync()
            => _http.GetFromJsonAsync<CouponDto>(ApiRoutes.Stryktipset);


        
    }

}
