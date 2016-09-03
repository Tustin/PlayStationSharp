using System.Threading.Tasks;
using PSN.Requests;

namespace PSN
{
    public class Auth
    {
        private LoginRequest _request = new LoginRequest();
        private NPGrantCodeRequest _npgrant = new NPGrantCodeRequest();
        private OAuthRequest _oaRequest = new OAuthRequest();
        private OAuthTokens _tokens = new OAuthTokens();

        /// <summary>
        /// Login to PSN with an email and password.
        /// </summary>
        /// <param name="email">The e-mail for the PSN account.</param>
        /// <param name="password">The password for the PSN account.</param>
        public Auth(string email, string password)
        {
            _request.username = email;
            _request.password = password;
        }

        public async Task<OAuthTokens> Make()
        {
            string npsso = await _request.Make();
            string grant_code = await _npgrant.Make(npsso);
            return await _oaRequest.Make(grant_code);
        }
        public OAuthTokens GetTokens()
        {
            return _tokens;
        }
    }
}
