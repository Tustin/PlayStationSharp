using System.Collections.Generic;
using Newtonsoft.Json;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.Model
{
    public class FriendsModel
    {
	    [JsonProperty("profiles")]
	    public IList<ProfileModel> Profiles { get; set; }

	    [JsonProperty("start")]
	    public int Start { get; set; }

	    [JsonProperty("size")]
	    public int Size { get; set; }

	    [JsonProperty("totalResults")]
	    public int TotalResults { get; set; }
	}
}