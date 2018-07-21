using Newtonsoft.Json;

namespace PlayStationSharp.Model
{
	public class OAuthTokenModel
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty("scope")]
		public string Scope { get; set; }
	}
}
