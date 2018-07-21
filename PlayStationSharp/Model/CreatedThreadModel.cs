using Newtonsoft.Json;

namespace PlayStationSharp.Model
{
	public class CreatedThreadModel
	{
		[JsonProperty("threadId")]
		public string ThreadId { get; set; }

		[JsonProperty("threadModifiedDate")]
		public string ThreadModifiedDate { get; set; }

		[JsonProperty("blockedByMembers")]
		public bool BlockedByMembers { get; set; }
	}
}
