using System;
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
		
		public TrophyModel Trophies { get; private set; }

		public User(PlayStationClient client, string psn)
		{
			Client = client;
			Profile = this.GetInfo(psn).Information;
			Trophies = this.CompareTrophies();
		}

		public User(PlayStationClient client, ProfileModel profile)
		{
			Client = client;
			Profile = profile;
			Trophies = this.CompareTrophies();
		}

		/// <summary>
		/// Adds the current user as a friend.
		/// </summary>
		/// <param name="requestMessage">The message to send with the request (optional).</param>
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
		/// Removes the current user from your friends list. (Same as Account.RemoveFriend, but removes the current User object from your friends list.)
		/// </summary>
		public void RemoveFriend()
		{
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/friendList/{this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);

			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}

		/// <summary>
		/// Blocks the current user. (Same as Account.BlockUser, but blocks the current user object.)
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
		/// Unblocks the current user. (Same as Account.UnblockUser, but unblocks the current user object.)
		/// </summary>
		/// <returns>True if the user was unblocked successfully.</returns>
		public void Unblock()
		{
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{this.Client.Account.Profile.Information.OnlineId}/blockList/{this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);

			if (Utilities.ContainsKey(response, "error"))
				throw new Exception(response.error.message);
		}



		/// <summary>
		/// Sends a message to the current User instance.
		/// </summary>
		/// <param name="messageContent">Message object containing the details for the message.</param>
		/// <returns>True if the message gets sent successfully.</returns>
		//     public bool SendMessage(Message messageContent)
		//     {
		//if (messageContent == null) return false;

		//         //Set the receiver of the message to the current user.
		//         messageContent.Receiver = this;

		//         if (messageContent.MessageType == MessageType.MessageText)
		//         {
		//             MessageRequest mr = new MessageRequest(messageContent.UsersToInvite, new MessageData(messageContent.MessageText, 1));
		//             var message = mr.BuildTextMessage();
		//             IFlurlClient fc = new FlurlClient("https://us-gmsg.np.community.playstation.net/groupMessaging/v1/messageGroups")
		//                 .WithHeader("Authorization", new AuthenticationHeaderValue("Bearer", Auth.CurrentInstance.AccountTokens.Authorization));
		//             var response = Request.SendRequest(HttpMethod.Post, fc, message).ReceiveJson().Result;

		//             if (Utilities.ContainsKey(response, "error"))
		//                 throw new Exception(response.error.message);

		//             return true;
		//         }
		//         //TODO: Add other messageTypes (audio and images)
		//         throw new NotImplementedException();
		//     }

		/// <summary>
		/// Compares the current User's trophies with the current logged in account.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		public TrophyModel CompareTrophies(int offset = 0, int limit = 36)
		{
			return Request.SendGetRequest<TrophyModel>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset={offset}&limit={limit}&comparedUser={this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);
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