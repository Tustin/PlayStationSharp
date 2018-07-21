using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Flurl.Util;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.API
{
	public class User : AbstractUser
	{
		public ProfileModel Profile { get; private set; }

		private readonly Lazy<List<Trophy>> _trophies;
		private readonly Lazy<List<MessageThread>> _messageThreads;

		public List<Trophy> Trophies => _trophies.Value;
		public List<MessageThread> MessageThreads => _messageThreads.Value;

		public string OnlineId => this.Profile.OnlineId;

		private User(PlayStationClient client)
		{
			Client = client;
			_trophies = new Lazy<List<Trophy>>(() => this.GetTrophies());
			_messageThreads = new Lazy<List<MessageThread>>(this.GetMessageThreads);
		}


		public User(PlayStationClient client, string psn) : this(client)
		{
			Profile = this.GetInfo(psn).Information;
		}

		public User(PlayStationClient client, ProfileModel profile) : this(client)
		{
			Profile = profile;
		}

		/// <summary>
		/// Adds the current user as a friend.
		/// </summary>
		/// <param name="requestMessage">The message to send with the request.</param>
		public void AddFriend(string requestMessage = "")
		{
			object message = (string.IsNullOrEmpty(requestMessage)) ? new object() : new
			{
				requestMessage
			};

			var response = Request.SendJsonPostRequestAsync<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/friendList/{this.Profile.OnlineId}",
				message, this.Client.Tokens.Authorization);

			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}

		/// <summary>
		/// Removes the current user from your friends list,
		/// </summary>
		public void RemoveFriend()
		{
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/friendList/{this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);

			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}

		/// <summary>
		/// Blocks the current user.
		/// </summary>
		public void Block()
		{
			var response = Request.SendJsonPostRequestAsync<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/blockList/{this.Profile.OnlineId}",
				null, this.Client.Tokens.Authorization);

			//Since we pass null for the JSON POST data, the response likes to be null too...
			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}

		/// <summary>
		/// Unblocks the current user.
		/// </summary>
		/// <returns>True if the user was unblocked successfully.</returns>
		public void Unblock()
		{
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/blockList/{this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);

			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}

		// TODO: Move anonymous object into proper class
		/// <summary>
		/// Sends a message.
		/// </summary>
		/// <param name="content">Body of the message.</param>
		/// <returns>New instance of the message thread.</returns>
		public MessageThread SendMessage(string content)
		{
			MessageThread messageThread = null;

			var exists = this.Client.Account.FindMessageThreads(this.OnlineId).PrivateMessageThread();
			if (exists != null)
			{
				messageThread = new MessageThread(Client, exists.Information);
			}
			else
			{
				var newThreadModel = Request.SendMultiPartPostRequest<CreatedThreadModel>(
					"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/messageGroups", "gc0p4Jq0M2Yt08jU534c0p", "threadDetail", new
					{
						threadDetail = new
						{
							threadMembers = new[]
							{
								new {
									onlineId = this.Profile.OnlineId
								},
								new
								{
									onlineId = this.Client.Account.Profile.Information.OnlineId
								}
							}
						}
					}, this.Client.Tokens.Authorization);

				messageThread = new MessageThread(Client, newThreadModel.ThreadId);
			}

			return messageThread.SendMessage(content);
		}

		/// <summary>
		/// Gets all message threads with both you and the user.
		/// </summary>
		/// <returns>List of each message thread.</returns>
		public List<MessageThread> GetMessageThreads()
		{
			return this.Client.Account.FindMessageThreads(this.OnlineId);
		}

		// TODO: Remove redudancy of GetTrophies
		/// <summary>
		/// Compares the current User's trophies with the current logged in account.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		public List<Trophy> GetTrophies(int offset = 0, int limit = 36)
		{
			var trophyModels = Request.SendGetRequest<TrophyModel>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset={offset}&limit={limit}&comparedUser={this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);

			return trophyModels.TrophyTitles.Select(trophy => new Trophy(Client, trophy)).ToList();
		}

		/// <summary>
		/// Gets the activity of the current User.
		/// </summary>
		/// <returns>An ActivityResponse object containing a list of all the posts.</returns>
		//public Activity.ActivityResponse GetActivity()
		//{
		//	return Request.SendGetRequest<Activity.ActivityResponse>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/{this.Profile.onlineId}/feed/0?includeComments=true&includeTaggedItems=true&filters=PURCHASED&filters=RATED&filters=VIDEO_UPLOAD&filters=SCREENSHOT_UPLOAD&filters=PLAYED_GAME&filters=WATCHED_VIDEO&filters=TROPHY&filters=BROADCASTING&filters=LIKED&filters=PROFILE_PIC&filters=FRIENDED&filters=CONTENT_SHARE&filters=IN_GAME_POST&filters=RENTED&filters=SUBSCRIBED&filters=FIRST_PLAYED_GAME&filters=IN_APP_POST&filters=APP_WATCHED_VIDEO&filters=SHARE_PLAYED_GAME&filters=VIDEO_UPLOAD_VERIFIED&filters=SCREENSHOT_UPLOAD_VERIFIED&filters=SHARED_EVENT&filters=JOIN_EVENT&filters=TROPHY_UPLOAD&filters=FOLLOWING&filters=RESHARE",
		//		 Auth.CurrentInstance.AccountTokens.Authorization);
		//}
	}
}