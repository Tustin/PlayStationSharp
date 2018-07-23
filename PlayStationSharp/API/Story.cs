using System;
using System.Collections.Generic;
using System.Linq;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Activity;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ActivityModelJsonTypes;

namespace PlayStationSharp.API
{
	public class Story : IPlayStation
	{
		private readonly Lazy<List<Comment>> _comments;

		public PlayStationClient Client { get; protected set; }
		public Feed Information { get; }
		public string StoryId => this.Information.StoryId;
		public List<Comment> Comments => _comments.Value;

		public Story(PlayStationClient client, Feed feed)
		{
			this.Client = client;
			this.Information = feed;

			this._comments = new Lazy<List<Comment>>(() => GetComments());
		}

		/// <summary>
		/// Leaves a comment on the currently selected feed (aka story/post).
		/// </summary>
		/// <param name="comment">The comment to post on the feed.</param>
		/// <returns>True if the comment was post successfully.</returns>
		public bool Comment(string comment)
		{
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/comment/{this.StoryId}",
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
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/set/like/story/{this.StoryId}", null, this.Client.Tokens.Authorization);

			return true;
		}

		/// <summary>
		/// Unlikes the currently selected feed (aka story/post).
		/// </summary>
		/// <returns>True if the feed was successfully unliked.</returns>
		public bool Unlike()
		{
			var response = Request.SendJsonPostRequestAsync<object>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/set/dislike/story/{this.StoryId}", null, this.Client.Tokens.Authorization);

			return true;
		}

		/// <summary>
		/// Deletes the currently selected story.
		/// </summary>
		public void Delete()
		{
			Request.SendDeleteRequest<object>(
				$"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/story/{this.StoryId}",
				this.Client.Tokens.Authorization);
		}

		/// <summary>
		/// Gets all comments on the current story.
		/// </summary>
		/// <param name="start">Which comment to start from(?)</param>
		/// <param name="count">Amount of comments</param>
		/// <param name="sort">Order of the comments.</param>
		/// <returns></returns>
		private List<Comment> GetComments(int start = 0, int count = 10, string sort = "ASC")
		{
			try
			{
				var commentModels = Request.SendGetRequest<CommentModel>(
					$"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/stories/{StoryId}/comments?start={start}&count={count}&sort={sort}",
					this.Client.Tokens.Authorization);

				return commentModels.UserComments.Select(comment => new Comment(this.Client, comment, this)).ToList();
			}
			catch (PlayStationApiException ex)
			{
				switch (ex.Error.Code)
				{
					case 3157911:
						throw new NoCommentsFoundException();
					default:
						throw;
				}
			}

		}
	}
}
