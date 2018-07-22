using System.Runtime.CompilerServices;
using PlayStationSharp.API;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Model;

namespace PlayStationSharp.Extensions
{
	public class OAuthTokens
	{
		private OAuthTokenModel _model;

		public string Authorization => _model.AccessToken;
		public string Refresh => _model.RefreshToken;
		public int ExpiresIn => _model.ExpiresIn;

		public OAuthTokens(OAuthTokenModel model)
		{
			this._model = model;
		}

		public OAuthTokens(string refreshToken)
		{
			this._model = new OAuthTokenModel
			{
				RefreshToken = refreshToken
			};
			this.RefreshTokens();
		}

		public OAuthTokens RefreshTokens()
		{
			try
			{
				var result = Request.SendPostRequest<OAuthTokenModel>(APIEndpoints.OAUTH_URL,
					new
					{
						app_context = Auth.AuthorizationBearer.AppContext,
						client_id = Auth.AuthorizationBearer.ClientId,
						client_secret = Auth.AuthorizationBearer.ClientSecret,
						refresh_token = Refresh,
						duid = Auth.AuthorizationBearer.Duid,
						grant_type = "refresh_token",
						scope = Auth.AuthorizationBearer.Scope
					});

				_model = result;
				return this;
			}
			catch (GenericAuthException ex)
			{
				switch (ex.Error.ErrorCode)
				{
					case 4159:
						throw new InvalidRefreshTokenException(ex.Error.ErrorDescription);
					default:
						throw;
				}
			}
		}
	}
}