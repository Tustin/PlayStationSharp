using Newtonsoft.Json;

namespace PlayStationSharp.Model
{
	public class UploadImageModel
	{
		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
