using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace Utilities.Framework
{
    public static class Claims
    {
        public static string Sub = "sub";
        public static string Role = "role";
    }

    public interface IClaimHelper
    {
        int GetUserId();
        List<string> GetAllRoleIds();
        string GetToken();
        List<string> GetRoleCode();
        IPAddress GetUserIP();
        string GetRoleType();
    }

    public class ClaimHelper : IClaimHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal GetUserInfo()
        {
            return _httpContextAccessor.HttpContext?.User;
        }

        public int GetUserId()
        {
            if (_httpContextAccessor?.HttpContext != null)
            {
                var userId = _httpContextAccessor.HttpContext.User.Identity.FindFirstValue(ClaimTypes.NameIdentifier);
                return Convert.ToInt32(userId);
            }
            return 0;
        }

        private List<Claim> GetUserClaims()
        {
            return _httpContextAccessor.HttpContext.User.Claims.ToList();
        }

        public List<string> GetAllRoleIds()
        {
            var claims = GetUserClaims();
            var jsonStirngRoleId = claims.Where(x => x.Type == "client_RoleIds").Select(x => x.Value).FirstOrDefault();
            var roleIds = jsonStirngRoleId.HasValue() ? JsonConvert.DeserializeObject<List<string>>(jsonStirngRoleId) : null;
            return roleIds;
        }

        public string GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            return token;
        }

        public List<string> GetRoleCode()
        {
            var claims = GetUserClaims();
            var jsonStringCode = claims.FirstOrDefault(x => x.Type == "client_RoleCodes").Value;
            var code = JsonConvert.DeserializeObject<List<string>>(jsonStringCode).ToList();
            return code;
        }
        public string GetRoleType()
        {
            var claims = GetUserClaims();
            var jsonStringCode = claims.FirstOrDefault(x => x.Type == "roleType").Value;
            //var code = JsonConvert.DeserializeObject<string>(jsonStringCode);
            return jsonStringCode;
        }

        public IPAddress GetUserIP()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            return ip;
        }
    }
}
