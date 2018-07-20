using System.Collections.Generic;
using Newtonsoft.Json;
using PlayStationSharp.API;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.Model.ProfileJsonTypes
{
	public class AvatarUrlModel
	{

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("avatarUrl")]
		public string AvatarUrl { get; set; }
	}

	public class EarnedTrophiesModel
	{

		[JsonProperty("platinum")]
		public int Platinum { get; set; }

		[JsonProperty("gold")]
		public int Gold { get; set; }

		[JsonProperty("silver")]
		public int Silver { get; set; }

		[JsonProperty("bronze")]
		public int Bronze { get; set; }
	}

	public class TrophySummaryModel
	{

		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonProperty("progress")]
		public int Progress { get; set; }

		[JsonProperty("earnedTrophies")]
		public EarnedTrophiesModel EarnedTrophies { get; set; }
	}

	public class PersonalDetailModel
	{

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }
	}

	public class PresenceModel
	{

		[JsonProperty("onlineStatus")]
		public string OnlineStatus { get; set; }

		[JsonProperty("hasBroadcastData")]
		public bool HasBroadcastData { get; set; }
	}

	public class ProfileModel
	{

		[JsonProperty("onlineId")]
		public string OnlineId { get; set; }

		[JsonProperty("npId")]
		public string NpId { get; set; }

		[JsonProperty("avatarUrls")]
		public IList<AvatarUrlModel> AvatarUrls { get; set; }

		[JsonProperty("plus")]
		public int Plus { get; set; }

		[JsonProperty("aboutMe")]
		public string AboutMe { get; set; }

		[JsonProperty("languagesUsed")]
		public IList<string> LanguagesUsed { get; set; }

		[JsonProperty("trophySummary")]
		public TrophySummaryModel TrophySummary { get; set; }

		[JsonProperty("isOfficiallyVerified")]
		public bool IsOfficiallyVerified { get; set; }

		[JsonProperty("personalDetail")]
		public PersonalDetailModel PersonalDetail { get; set; }

		[JsonProperty("personalDetailSharing")]
		public string PersonalDetailSharing { get; set; }

		[JsonProperty("personalDetailSharingRequestMessageFlag")]
		public bool PersonalDetailSharingRequestMessageFlag { get; set; }

		[JsonProperty("primaryOnlineStatus")]
		public string PrimaryOnlineStatus { get; set; }

		[JsonProperty("presences")]
		public IList<PresenceModel> Presences { get; set; }

		[JsonProperty("friendRelation")]
		public string FriendRelation { get; set; }

		[JsonProperty("requestMessageFlag")]
		public bool RequestMessageFlag { get; set; }

		[JsonProperty("blocking")]
		public bool Blocking { get; set; }

		[JsonProperty("friendsCount")]
		public int FriendsCount { get; set; }

		[JsonProperty("mutualFriendsCount")]
		public int MutualFriendsCount { get; set; }

		[JsonProperty("following")]
		public bool Following { get; set; }

		[JsonProperty("followingUsersCount")]
		public int FollowingUsersCount { get; set; }

		[JsonProperty("followerCount")]
		public int FollowerCount { get; set; }
	}

}

namespace PlayStationSharp.Model
{
	public class Profile
	{
		[JsonProperty("profile")]
		public ProfileModel Information { get; set; }

	}

}
