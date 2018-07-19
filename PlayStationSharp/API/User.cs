using System;
using PlayStationSharp.Extensions;
using PlayStationSharp.Responses;

namespace PlayStationSharp.API
{
    public class User
    {
        /// <summary>
        /// Contains profile information for the user.
        /// </summary>
        public ProfileResponse Profile { get; protected set; }

        /// <summary>
        /// Instantiates a new User object and fetches the profile of the PSN.
        /// </summary>
        /// <param name="psn">The PSN onlineId of the user.</param>
        public User(string psn)
        {
            Profile = GetInfo(psn);
        }

        /// <summary>
        /// Instantiates a new User object using a Profile object.
        /// </summary>
        /// <param name="profile">The Profile of the PSN.</param>
        public User(ProfileResponse profile)
        {
            Profile = profile;
        }

        /// <summary>
        /// Fetches profile of a user.
        /// </summary>
        /// <param name="psn">The PSN online Id of the user.</param>
        /// <returns>A profile object containing the user's info</returns>
        private static ProfileResponse GetInfo(string psn)
        {
           return Request.SendGetRequest<ProfileResponse>($"{APIEndpoints.USERS_URL}{psn}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s",
                Auth.CurrentInstance.AccountTokens.Authorization);
        }

        /// <summary>
        /// Adds the current user as a friend. (Same as Account.AddFriend, but adds the current User object as a friend)
        /// </summary>
        /// <param name="requestMessage">The message to send with the request (optional).</param>
        public void AddFriend(string requestMessage = "")
        {
            object message = (string.IsNullOrEmpty(requestMessage)) ? new object() : new
            {
				requestMessage
			};

			var response = Request.SendJsonPostRequestAsync<dynamic>($"{APIEndpoints.USERS_URL}{Auth.CurrentInstance.Profile.onlineId}/friendList/{this.Profile.onlineId}",
				message, Auth.CurrentInstance.AccountTokens.Authorization);

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);
        }

        /// <summary>
        /// Removes the current user from your friends list. (Same as Account.RemoveFriend, but removes the current User object from your friends list.)
        /// </summary>
        public void RemoveFriend()
        {
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{Auth.CurrentInstance.Profile.onlineId}/friendList/{this.Profile.onlineId}",
				Auth.CurrentInstance.AccountTokens.Authorization);

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);
        }

        /// <summary>
        /// Blocks the current user. (Same as Account.BlockUser, but blocks the current user object.)
        /// </summary>
        public void Block()
        {
			//This service requires the request to be a JSON POST request... not sure why it doesn't accept GET or PUT. So I'll just pass null for the data.
			var response = Request.SendJsonPostRequestAsync<dynamic>($"{APIEndpoints.USERS_URL}{Auth.CurrentInstance.Profile.onlineId}/blockList/{this.Profile.onlineId}",
				null, Auth.CurrentInstance.AccountTokens.Authorization);

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
			var response = Request.SendDeleteRequest<dynamic>($"{APIEndpoints.USERS_URL}{Auth.CurrentInstance.Profile.onlineId}/blockList/{this.Profile.onlineId}",
				Auth.CurrentInstance.AccountTokens.Authorization);

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
        public TrophyResponses.CompareTrophiesResponse CompareTrophies(int offset = 0, int limit = 36)
        {
			return Request.SendGetRequest<TrophyResponses.CompareTrophiesResponse>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset={offset}&limit={limit}&comparedUser={this.Profile.onlineId}",
				Auth.CurrentInstance.AccountTokens.Authorization);
        }

        /// <summary>
        /// Gets the activity of the current User.
        /// </summary>
        /// <returns>An ActivityResponse object containing a list of all the posts.</returns>
        public Activity.ActivityResponse GetActivity()
        {
			return Request.SendGetRequest<Activity.ActivityResponse>($"https://activity.api.np.km.playstation.net/activity/api/v1/users/{this.Profile.onlineId}/feed/0?includeComments=true&includeTaggedItems=true&filters=PURCHASED&filters=RATED&filters=VIDEO_UPLOAD&filters=SCREENSHOT_UPLOAD&filters=PLAYED_GAME&filters=WATCHED_VIDEO&filters=TROPHY&filters=BROADCASTING&filters=LIKED&filters=PROFILE_PIC&filters=FRIENDED&filters=CONTENT_SHARE&filters=IN_GAME_POST&filters=RENTED&filters=SUBSCRIBED&filters=FIRST_PLAYED_GAME&filters=IN_APP_POST&filters=APP_WATCHED_VIDEO&filters=SHARE_PLAYED_GAME&filters=VIDEO_UPLOAD_VERIFIED&filters=SCREENSHOT_UPLOAD_VERIFIED&filters=SHARED_EVENT&filters=JOIN_EVENT&filters=TROPHY_UPLOAD&filters=FOLLOWING&filters=RESHARE",
				 Auth.CurrentInstance.AccountTokens.Authorization);
        }
    }
}