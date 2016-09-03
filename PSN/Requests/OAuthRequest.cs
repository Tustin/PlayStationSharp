using System.Threading.Tasks;
using Flurl.Http;
using PSN.Exceptions;
using PSN.Extensions;

namespace PSN.Requests
{
    /// <summary>
    /// Builds the requests for the OAuth tokens
    /// </summary>
    public class OAuthRequest
    {
        public class AuthorizationBearer
        {
            public readonly string app_context = "inapp_ios";
            public readonly string client_id = "b7cbf451-6bb6-4a5a-8913-71e61f462787";
            public readonly string client_secret = "zsISsjmCx85zgCJg";
            public string code { get; set; }
            public readonly string duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
            public readonly string grant_type = "authorization_code";
            public readonly string scope = "capone:report_submission,psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes";
        }
        class Refresh : AuthorizationBearer
        {
            public new string grant_type = "refresh";
        }

        public async Task<OAuthTokens> Make(string grant)
        {
            if (string.IsNullOrEmpty(grant))
                throw new OAuthTokenNotFoundException("No grant code was supplied");

            AuthorizationBearer ab = new AuthorizationBearer();

            dynamic result = await Utilities.SendPOSTRequest("https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token", 
            new
            {
                app_context = ab.app_context,
                client_id = ab.client_id,
                client_secret = ab.client_secret,
                code = grant,
                duid = ab.duid,
                grant_type = ab.grant_type,
                scope = ab.scope
            }).ReceiveJson();

            if (Utilities.ContainsKey(result, "error"))
                throw new NPSSOIdNotFoundException(result.error_description);

            return new OAuthTokens()
            {
                Authorization = result.access_token,
                Refresh = result.refresh_token
            };
        }
    }
}
