using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlayStationSharp.Model
{
	internal class ErrorModel
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }

		[JsonProperty("docs")]
		public string Docs { get; set; }

		[JsonProperty("parameters")]
		public IList<string> Parameters { get; set; }
	}
}
