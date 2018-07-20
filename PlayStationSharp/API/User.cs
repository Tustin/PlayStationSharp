using System;
using System.Runtime.CompilerServices;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.API
{
	public class User : IPlayStation
	{
		public PlayStationClient Client { get; private set; }

		public ProfileModel Profile { get; private set; }

		public User(PlayStationClient client, string psn)
		{
			Client = client;
			Profile = GetInfo(psn).Information;
		}

		public User(PlayStationClient client, ProfileModel profile)
		{
			Client = client;
			Profile = profile;
		}

		/// <summary>
		/// Fetches profile of a user.
		/// </summary>
		/// <param name="psn">The PSN online Id of the user.</param>
		/// <returns>A profile object containing the user's info</returns>
		private Profile GetInfo(string psn)
		{
			return Request.SendGetRequest<Profile>($"{APIEndpoints.USERS_URL}{psn}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", this.Client.Tokens.Authorization);
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
		/// Fetches the user's trophies.
		/// </summary>
		/// <param name="limit">The amount of trophies to return (optional).</param>
		/// <returns></returns>
		public TrophyResponses.UserTrophiesResponse GetTrophies(int limit = 36)
		{
			return Request.SendGetRequest<TrophyResponses.UserTrophiesResponse>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset=0&limit={limit}",
				this.Client.Tokens.Authorization);
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
		//public TrophyResponses.CompareTrophiesResponse CompareTrophies(int offset = 0, int limit = 36)
		//{
		//	return Request.SendGetRequest<TrophyResponses.CompareTrophiesResponse>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset={offset}&limit={limit}&comparedUser={this.Profile.onlineId}",
		//		Auth.CurrentInstance.AccountTokens.Authorization);
		//}

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