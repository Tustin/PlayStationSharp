using System.Threading.Tasks;
using Flurl.Http;
using PSN.Exceptions;
using PSN.Extensions;

namespace PSN.Requests
{
    /// <summary>
    /// Class that builds the initial login request
    /// </summary>
    public class LoginRequest
    {
        public readonly string authentication_type = "password";
        public string username { get; set; }
        public string password { get; set; }
        public readonly string client_id = "71a7beb8-f21a-47d9-a604-2e71bee24fe0";

        /// <summary>
        /// Execute the initial login request.
        /// </summary>
        /// <returns>NPSSO Id for the next auth step.</returns>
        public async Task<string> Make()
        {
            if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password))
                throw new NPSSOIdNotFoundException("The username or password fields have not been set.");

            try
            {
                dynamic result = await Utilities.SendPOSTRequest("https://auth.api.sonyentertainmentnetwork.com/2.0/ssocookie", 
                new
                {
                    authentication_type = authentication_type,
                    username = username,
                    password = password,
                    client_id = client_id
                }, 
                new
                {
                    h1 = "Content-Type: application/json",
                }).ReceiveJson();

                if (Utilities.ContainsKey(result, "error"))
                    throw new NPSSOIdNotFoundException(result.error_description);

                return result.npsso;
            }
            catch(NPSSOIdNotFoundException e) { throw new NPSSOIdNotFoundException(e.Message); }
        }
    }
}
