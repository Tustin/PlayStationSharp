using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using System.Net;
using System.Web;
using PSN.Exceptions;

namespace PSN.Requests
{
    /// <summary>
    /// Builds and sends the request for the X-NP-GRANT-CODE
    /// </summary>
    public class NPGrantCodeRequest
    {
        public readonly string state = "06d7AuZpOmJAwYYOWmVU63OMY";
        public readonly string duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
        public readonly string app_context = "inapp_ios";
        public readonly string client_id = "b7cbf451-6bb6-4a5a-8913-71e61f462787";
        public readonly string scope = "capone:report_submission,psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes";
        public readonly string response_type = "code";

        public async Task<string> Make(string npsso)
        {
            if (string.IsNullOrEmpty(npsso))
                throw new XNPGrantCodeNotFoundException("The NPSSO Id field has not been set.");
            try
            {
                //I'm going to do the request here because this is a special case.
                //Need to hardcode this object because for some reason SetQueryParams doesn't take in the fields above automatically.
                var response = await "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorize".SetQueryParams(new
                {
                    state = state,
                    duid = duid,
                    app_context = app_context,
                    client_id = client_id,
                    scope = scope,
                    response_type = response_type
                }).WithCookie(new Cookie("npsso", npsso)).GetAsync();

                //TODO: Add exception throwing if we cannot find the code
                //For now, the try catch will just throw an empty exception.

                //Decode the encoded URI
                string uri = HttpUtility.UrlDecode(response.RequestMessage.RequestUri.AbsoluteUri);
                //Grab the code from the URL
                string code = uri.Remove(0, uri.IndexOf("code=") + "code=".Length);
                code = code.Substring(0, code.IndexOf("&"));

                return code;
            }
            catch { throw new XNPGrantCodeNotFoundException(); }
        }
    }
}
