using System.Collections.Generic;
using Newtonsoft.Json;
using PlayStationSharp.Model.ActivityModelJsonTypes;

namespace PlayStationSharp.Model.ActivityModelJsonTypes
{
	public class Target
	{
		[JsonProperty("meta")]
		public string Meta { get; set; }

		[JsonProperty("value")]
		public object Value { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonProperty("accountId")]
		public string AccountId { get; set; }
	}

	public class CaptionComponent
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("isOfficiallyVerified")]
		public bool IsOfficiallyVerified { get; set; }
	}

	public class Source
	{
		[JsonProperty("meta")]
		public string Meta { get; set; }

		[JsonProperty("value")]
		public object Value { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonProperty("realName")]
		public string RealName { get; set; }

		[JsonProperty("accountId")]
		public string AccountId { get; set; }
	}

	public class StorageKey
	{
		[JsonProperty("primaryKey")]
		public string PrimaryKey { get; set; }

		[JsonProperty("cacheKey")]
		public string CacheKey { get; set; }

		[JsonProperty("empty")]
		public bool Empty { get; set; }
	}

	public class Feed
	{
		[JsonProperty("targets")]
		public IList<Target> Targets { get; set; }

		[JsonProperty("captionTemplate")]
		public string CaptionTemplate { get; set; }

		[JsonProperty("captionComponents")]
		public IList<CaptionComponent> CaptionComponents { get; set; }

		[JsonProperty("storyId")]
		public string StoryId { get; set; }

		[JsonProperty("storyType")]
		public string StoryType { get; set; }

		[JsonProperty("source")]
		public Source Source { get; set; }

		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("commentCount")]
		public int CommentCount { get; set; }

		[JsonProperty("likeCount")]
		public int LikeCount { get; set; }

		[JsonProperty("liked")]
		public bool Liked { get; set; }

		[JsonProperty("trophyHidden")]
		public bool TrophyHidden { get; set; }

		[JsonProperty("reshareCount")]
		public int ReshareCount { get; set; }

		[JsonProperty("reshareableStatus")]
		public string ReshareableStatus { get; set; }

		[JsonProperty("commentable")]
		public string Commentable { get; set; }

		[JsonProperty("storageKey")]
		public StorageKey StorageKey { get; set; }
	}
}

namespace PlayStationSharp.Model
{
	public class ActivityModel
	{
		[JsonProperty("feed")]
		public IList<Feed> Feed { get; set; }

		[JsonProperty("lastBlock")]
		public bool LastBlock { get; set; }

		[JsonProperty("privateFeed")]
		public bool PrivateFeed { get; set; }

		[JsonProperty("offset")]
		public int Offset { get; set; }
	}
}
