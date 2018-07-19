using System.Collections.Generic;
using PlayStationSharp.API;

namespace PlayStationSharp.Model
{
	public class Activity
	{
		public class Target
		{
			public string meta { get; set; }
			public string type { get; set; }
			public int? value { get; set; }
			public string imageUrl { get; set; }
			public string accountId { get; set; }
			public string aspectRatio { get; set; }
		}

		public class CaptionComponent
		{
			public string key { get; set; }
			public string value { get; set; }
			public bool isOfficiallyVerified { get; set; }
		}

		public class Source
		{
			public string meta { get; set; }
			public string type { get; set; }
			public string imageUrl { get; set; }
			public string accountId { get; set; }
		}

		public class StorageKey
		{
			public string primaryKey { get; set; }
			public string cacheKey { get; set; }
			public bool empty { get; set; }
		}

		public class Action
		{
			public string type { get; set; }
			public string uri { get; set; }
			public string platform { get; set; }
			public string buttonCaption { get; set; }
			public string imageUrl { get; set; }
			public string startArguments { get; set; }
		}

		public class Param
		{
			public string meta { get; set; }
			public string type { get; set; }
			public List<string> tags { get; set; }
		}

		public class Comment
		{
			public string onlineId { get; set; }
			public string realName { get; set; }
			public string displayPicUrl { get; set; }
			public string countryCode { get; set; }
			public string commentString { get; set; }
			public string accountId { get; set; }
			public bool isOfficiallyVerified { get; set; }
			public string commentId { get; set; }
			public string deleteType { get; set; }
			public string date { get; set; }
		}

		public class Feed
		{
			public string caption { get; set; }
			public List<Target> targets { get; set; }
			public string captionTemplate { get; set; }
			public List<CaptionComponent> captionComponents { get; set; }
			public string storyId { get; set; }
			public string storyType { get; set; }
			public Source source { get; set; }
			public string smallImageUrl { get; set; }
			public string smallImageAspectRatio { get; set; }
			public string largeImageUrl { get; set; }
			public string thumbnailImageUrl { get; set; }
			public string date { get; set; }
			public double relevancy { get; set; }
			public int commentCount { get; set; }
			public int likeCount { get; set; }
			public string titleId { get; set; }
			public bool isReshareable { get; set; }
			public string productId { get; set; }
			public string productUrl { get; set; }
			public bool liked { get; set; }
			public string serviceProviderName { get; set; }
			public string captionLocale { get; set; }
			public string catalogLocale { get; set; }
			public bool trophyHidden { get; set; }
			public bool followStory { get; set; }
			public int reshareCount { get; set; }
			public string reshareableStatus { get; set; }
			public bool hasReshares { get; set; }
			public string commentable { get; set; }
			public StorageKey storageKey { get; set; }
			public bool reshareable { get; set; }
			public string storyComment { get; set; }
			public List<Action> actions { get; set; }
			public List<Param> @params { get; set; }
			public Comment comment { get; set; }
			public int? subType { get; set; }

			/// <summary>
			/// Leaves a comment on the currently selected feed (aka story/post).
			/// </summary>
			/// <param name="comment">The comment to post on the feed.</param>
			/// <returns>True if the comment was post successfully.</returns>
			public bool LeaveComment(string comment)
			{
				var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/comment/{this.storyId}",
					new
					{
						commentString = comment
					}, Auth.CurrentInstance.AccountTokens.Authorization);

				return true;
			}

			/// <summary>
			/// Likes the currently selected feed (aka story/post).
			/// </summary>
			/// <returns>True if the feed was successfully liked.</returns>
			public bool Like()
			{
				var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/{Auth.CurrentInstance.Profile.onlineId}/set/like/story/{this.storyId}", null, Auth.CurrentInstance.AccountTokens.Authorization);

				return true;
			}

			/// <summary>
			/// Unlikes the currently selected feed (aka story/post).
			/// </summary>
			/// <returns>True if the feed was successfully unliked.</returns>
			public bool Unlike()
			{
				var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/{Auth.CurrentInstance.Profile.onlineId}/set/dislike/story/{storyId}", null, Auth.CurrentInstance.AccountTokens.Authorization);

				return true;
			}
		}

		/// <summary>
		/// List of feeds (aka stories/posts) returned from the GetActivity method.
		/// </summary>
		public class ActivityResponse
		{
			public List<Feed> feed { get; set; }
		}
	}
}