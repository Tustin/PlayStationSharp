using PSN.Requests;
using System.Threading.Tasks;

namespace PSN
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
        public static async Task<Account> Login(string email, string password) {
            string npsso = await LoginRequest.Make(email, password);
            string grantCode = await NpGrantCodeRequest.Make(npsso);
            OAuthTokens tokens = await OAuthRequest.Make(grantCode);
            Account a = new Account(tokens);
            CurrentInstance = a;
            return a;
        }

        /// <summary>
        /// Logs into PSN using a refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token for the account.</param>
        /// <returns>An instance of the Account class for your account.</returns>
        public static async Task<Account> Login(string refreshToken) {
            OAuthTokens tokens = await OAuthRequest.MakeNewTokens(refreshToken);
            Account a = new Account(tokens);
            CurrentInstance = a;
            return a;
        }
    }
}