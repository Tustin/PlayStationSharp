using Flurl.Http;
using PSN.Exceptions;
using PSN.Extensions;
using System.Threading.Tasks;

namespace PSN.Requests
{
    /// <summary>
    /// Class that builds the initial login request
    /// </summary>
    public static class LoginRequest
    {
        public static readonly string AuthenticationType = "password";
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static readonly string ClientId = "71a7beb8-f21a-47d9-a604-2e71bee24fe0";

        /// <summary>
        /// Execute the initial login request.
        /// </summary>
        /// <returns>NPSSO Id for the next auth step.</returns>
        public static string Make(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new NpssoIdNotFoundException("The username or password fields have not been set.");

            try
            {
                var result = Utilities.SendPostRequest(APIEndpoints.SSO_COOKIE_URL, new
                {
                    authentication_type = AuthenticationType,
                    username = username,
                    password = password,
                    client_id = ClientId
                }, string.Empty, new
                {
                    h1 = "Content-Type: application/json",
                }).ReceiveJson().Result;

                if (Utilities.ContainsKey(result, "error"))
                    throw new NpssoIdNotFoundException(result.error_description);


                //Dual auth is set
                if (Utilities.ContainsKey(result, "authentication_type"))
                {
                    //If the user uses SMS verification, throw exception containing the ticket UUID
                    //so you can ask the user to supply the code sent to their device.
                    if (result.challenge_method == "SMS")
                        throw new DualAuthSMSRequiredException(result.ticket_uuid);
                }

                return result.npsso;
            }
            catch (NpssoIdNotFoundException e) { throw new NpssoIdNotFoundException(e.Message); }
        }
    }
}