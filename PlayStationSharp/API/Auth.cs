using PlayStationSharp.Extensions;
using PlayStationSharp.Requests;

namespace PlayStationSharp.API
{
    public class Auth
    {
        //TODO: Possibly change this setup to allow for multiple PSN accounts. If anyone has any suggestions for implementing this, please open an Issue :D.
        /// <summary>
        /// The current instance of the logged in user.
        /// </summary>
        public static Account CurrentInstance { get; protected set; }

        /// <summary>
        /// Logs into PSN using an email and password.
        /// </summary>
        /// <param name="email">The email address for the account.</param>
        /// <param name="password">The password for the account.</param>
        /// <returns>An instance of the Account class for your account.</returns>
        public static Account Login(string email, string password)
        {
            string npsso = LoginRequest.Create(email, password);
            string grantCode = NpGrantCodeRequest.Make(npsso);
            OAuthTokens tokens = OAuthRequest.Make(grantCode);
            Account a = new Account(tokens);
            CurrentInstance = a;
            return a;
        }
        
        /// <summary>
        /// Verifies the two-step authentication code. Should be used after the initial Login() method.
        /// </summary>
        /// <param name="code">The code sent to the device.</param>
        /// <param name="ticketUuid">The UUID sent back from the Login() method.</param>
        /// <returns></returns>
        public static Account DualAuthLogin(string code, string ticketUuid)
        {
            string npsso = DualAuthLoginRequest.Make(code, ticketUuid);
            string grantCode = NpGrantCodeRequest.Make(npsso);
            OAuthTokens tokens = OAuthRequest.Make(grantCode);
            Account a = new Account(tokens);
            CurrentInstance = a;
            return a;
        }

        /// <summary>
        /// Logs into PSN using a refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token for the account.</param>
        /// <returns>An instance of the Account class for your account.</returns>
        public static Account Login(string refreshToken)
        {
            OAuthTokens tokens = OAuthRequest.MakeNewTokens(refreshToken);
            Account a = new Account(tokens);
            CurrentInstance = a;
            return a;
        }
    }
}