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
		public PlayStationClient Client { get; }
		private UserComment Information;
		private readonly Story Story;

		public Comment(PlayStationClient client, UserComment comment, Story story)
		{
			this.Client = client;
			this.Information = comment;
			this.Story = story;
		}

		public bool Delete()
		{
			try
			{
				var request = Request.SendDeleteRequest<object>(
					$"https://activity.api.np.km.playstation.net/activity/api/v1/users/me/stories/{this.Story.Information.StoryId}/comments/c971d550-8d54-11e8-9071-02a1d0b8a704",
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
