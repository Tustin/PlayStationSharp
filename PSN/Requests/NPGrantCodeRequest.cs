using Flurl;
using Flurl.Http;
using PSN.Exceptions;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PSN.Requests
{
    /// <summary>
    /// Builds and sends the request for the X-NP-GRANT-CODE
    /// </summary>
    public static class NpGrantCodeRequest
    {
        public static readonly string State = "06d7AuZpOmJAwYYOWmVU63OMY";
        public static readonly string Duid = "0000000d000400808F4B3AA3301B4945B2E3636E38C0DDFC";
        public static readonly string AppContext = "inapp_ios";
        public static readonly string ClientId = "b7cbf451-6bb6-4a5a-8913-71e61f462787";
        public static readonly string Scope = "capone:report_submission,psn:sceapp,user:account.get,user:account.settings.privacy.get,user:account.settings.privacy.update,user:account.realName.get,user:account.realName.update,kamaji:get_account_hash,kamaji:ugc:distributor,oauth:manage_device_usercodes";
        public static readonly string ResponseType = "code";

        public static async Task<string> Make(string npsso) {
            if (string.IsNullOrEmpty(npsso))
                throw new XnpGrantCodeNotFoundException("The NPSSO Id field has not been set.");
            try
            {
                //I'm going to do the request here because this is a special case.
                //Need to hardcode this object because for some reason SetQueryParams doesn't take in the fields above automatically.
                var response = await APIEndpoints.NP_GRANT_URL.SetQueryParams(new
                {
                    state = State,
                    duid = Duid,
                    app_context = AppContext,
                    client_id = ClientId,
                    scope = Scope,
                    response_type = ResponseType
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
            catch { throw new XnpGrantCodeNotFoundException(); }
        }
    }
}