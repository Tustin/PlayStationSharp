using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Activity;
using PlayStationSharp.Model.CommentModelJsonTypes;

namespace PlayStationSharp.API
{
	public class Comment : IPlayStation
	{
		private readonly UserComment Information;
		private readonly Story _story;
		private readonly Lazy<User> _submitter;

		public PlayStationClient Client { get; }
		public User Submitter => _submitter.Value;

		public Comment(PlayStationClient client, UserComment comment, Story story)
		{
			this.Client = client;
			this.Information = comment;
			this._story = story;
			this._submitter = new Lazy<User>(() => new User(Client, this.Information.OnlineId));
		}

		public bool Delete()
		{
			try
			{
				var request = Request.SendDeleteRequest<object>(
					$"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/stories/{this._story.StoryId}/comments/{this.Information.CommentId}",
					this.Client.Tokens.Authorization);
				return true;
			}
			catch (PlayStationApiException ex)
			{
				switch (ex.Error.Code)
				{
					case 3157761:
						throw new InvalidCommentException(ex.Error.Message);
					default:
						throw;
				}
			}
		}
	}
}
