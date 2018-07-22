using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ActivityModelJsonTypes;

namespace PlayStationSharp.API
{
	public class Story : IPlayStation
	{
		public PlayStationClient Client { get; protected set; }
		public Feed Information { get; }

		public Story(PlayStationClient client, Feed feed)
		{
			this.Client = client;
			this.Information = feed;
		}

		/// <summary>
		/// Leaves a comment on the currently selected feed (aka story/post).
		/// </summary>
		/// <param name="comment">The comment to post on the feed.</param>
		/// <returns>True if the comment was post successfully.</returns>
		public bool Comment(string comment)
		{
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/comment/{this.Information.StoryId}",
				new
				{
					commentString = comment
				}, this.Client.Tokens.Authorization);

			return true;
		}

		/// <summary>
		/// Likes the currently selected feed (aka story/post).
		/// </summary>
		/// <returns>True if the feed was successfully liked.</returns>
		public bool Like()
		{
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/set/like/story/{this.Information.StoryId}", null, this.Client.Tokens.Authorization);

			return true;
		}


		/// <summary>
		/// Unlikes the currently selected feed (aka story/post).
		/// </summary>
		/// <returns>True if the feed was successfully unliked.</returns>
		public bool Unlike()
		{
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/set/dislike/story/{this.Information.StoryId}", null, this.Client.Tokens.Authorization);

			return true;
		}


		public List<Comment> GetComments(int start = 0, int count = 10, string sort = "ASC")
		{
			var commentModels = Request.SendGetRequest<CommentModel>(
				$"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/stories/{this.Information.StoryId}/comments?start={start}&count={count}&sort={sort}",
				this.Client.Tokens.Authorization);

			return commentModels.UserComments.Select(comment => new Comment(Client, comment, this)).ToList();
		}
	}
}
