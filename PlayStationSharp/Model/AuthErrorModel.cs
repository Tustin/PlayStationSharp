using Newtonsoft.Json;

namespace PlayStationSharp.Model
{
	public class AuthErrorModel
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }

		[JsonProperty("docs")]
		public string Docs { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }
	}
}
