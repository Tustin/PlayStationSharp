using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSN.APIResponses;
using PSN.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSN
{
    /// <summary>
    /// Contains information for the currently logged in account.
    /// </summary>
    public class Account
    {
        public OAuthTokens AccountTokens { get; set; }
        public Profile Profile { get; protected set; }

        public Account(OAuthTokens tokens) {
            AccountTokens = tokens;
            Profile = Task.Run(GetInfo).Result;
        }

        private async Task<Profile> GetInfo() {
            var response = await Utilities.SendGetRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", this.AccountTokens.Authorization).ReceiveString();
            //Remove root "profile" element
            Profile p = JsonConvert.DeserializeObject<Profile>(JObject.Parse(response).SelectToken("profile").ToString());
            return p;
        }

        /// <summary>
        /// Adds a friend to your friends list.
        /// </summary>
        /// <param name="psn">The PSN online ID of the friend to add.</param>
        /// <param name="requestMessage">A message to send with the friend request. Optional.</param>
        /// <returns>True if the user was added successfully.</returns>
        public async Task<bool> AddFriend(string psn, string requestMessage = "") {
            object message = (string.IsNullOrEmpty(requestMessage)) ? new object() : new
            {
                requestMessage = requestMessage
            };
            var response = await Utilities.SendJsonPostRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/users/{this.Profile.onlineId}/friendList/{psn}", message, this.AccountTokens.Authorization).ReceiveJson();

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);

            return true;
        }

        /// <summary>
        /// Removes a friend from your friends list.
        /// </summary>
        /// <param name="psn">The PSN online ID to remove.</param>
        /// <returns>True if the friend was removed successfully.</returns>
        public async Task<bool> RemoveFriend(string psn) {
            var response = await Utilities.SendDeleteRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/users/{this.Profile.onlineId}/friendList/{psn}", this.AccountTokens.Authorization).ReceiveJson();

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);

            return true;
        }

        /// <summary>
        /// Fetches each friend of the logged in account.
        /// </summary>
        /// <param name="filter">A filter to grab online, offline or all friends. Defaults to online.</param>
        /// <param name="limit">The amount of friends to return. By default, the PSN app uses 36.</param>
        /// <returns>A list of User objects for each friend.</returns>
        public async Task<List<User>> GetFriends(string filter = "online", int limit = 36) {
            List<User> friends = new List<User>();
            Friends response = await Utilities.SendGetRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/friends/profiles2?fields=onlineId,avatarUrls,plus,trophySummary(@default),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),primaryOnlineStatus,presences(@titleInfo,hasBroadcastData)&sort=name-onlineId&userFilter={filter}&avatarSizes=m&profilePictureSizes=m&offset=0&limit={limit}", this.AccountTokens.Authorization).ReceiveJson<Friends>();
            foreach (Profile friend in response.Profiles)
            {
                friends.Add(new User(friend));
            }
            return friends;
        }

        /// <summary>
        /// Blocks a user.
        /// </summary>
        /// <param name="psn">The PSN online ID to block.</param>
        /// <returns>True if the user was blocked.</returns>
        public async Task<bool> BlockUser(string psn) {
            //This service requires the request to be a JSON POST request... not sure why it doesn't accept GET or PUT. So I'll just pass null for the data.
            var response = await Utilities.SendJsonPostRequest($"{APIEndpoints.USERS_URL}{this.Profile.onlineId}/blockList/{psn}", null, this.AccountTokens.Authorization).ReceiveJson();

            //Since we pass null for the JSON POST data, the response likes to be null too...
            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);

            return true;
        }

        /// <summary>
        /// Unblocks a user.
        /// </summary>
        /// <param name="psn">The PSN online ID to unblock.</param>
        /// <returns>True if user was unblocked.</returns>
        public async Task<bool> UnblockUser(string psn) {
            var response = await Utilities.SendDeleteRequest($"{APIEndpoints.USERS_URL}{this.Profile.onlineId}/blockList/{psn}", this.AccountTokens.Authorization).ReceiveJson();

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);

            return true;
        }

        /// <summary>
        /// Sets your current avatar. You must own the avatar for this to set.
        /// </summary>
        /// <param name="contentId">The content ID/entitlement ID of the avatar. (ex: "UP4478-CUSA01744_00-AV00000000000059")</param>
        /// <returns>True if avatar is set successfully. Throws an exception otherwise.</returns>
        public void SetAvatar(string contentId) {
            //TODO: Find out why this endpoint constantly gives the "Not Authorized" error...
            throw new NotImplementedException();
            //var response = await Utilities.SendPutRequest($"{APIEndpoints.USERS_URL}me/avatar", new AvatarRequest(contentId), this.AccountTokens.Authorization).ReceiveJson();

            //if (Utilities.ContainsKey(response, "error"))
            //    throw new Exception(response.error.message);

            //return true;
        }

        /// <summary>
        /// Gets all avatars for the user.
        /// </summary>
        /// <param name="category">Category to get the avatars from.</param>
        /// <param name="offset">The amount of avatars to skip (optional).</param>
        /// <param name="limit">The amount of avatars to show (optional).</param>
        /// <param name="avatarSizes">The size of the avatar to show (optional).</param>
        /// <returns></returns>
        public void GetAvatars(AvatarCategoryId category, int offset = 0, int limit = 48, char avatarSizes = 'm') {
            //TODO: Find out why this endpoint constantly gives the "Not Authorized" error...
            throw new NotImplementedException();

            //var response = await Utilities.SendGetRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/avatars/categories/{ (int)category }?offset={offset}&limit={limit}&avatarSizes={avatarSizes}", this.AccountTokens.Authorization).ReceiveString();
        }

        /// <summary>
        /// Fetches each category of avatars (Premium, Action, etc).
        /// </summary>
        /// <param name="offset">The amount of categories to skip (optional).</param>
        /// <param name="limit">The amount of categories to show (optional).</param>
        /// <returns>An AvatarCategories object containing a list of all categories.</returns>
        public async Task<AvatarCategories> GetAvatarCategories(int offset = 0, int limit = 64) {
            //This response shouldn't ever throw an error even if the auth token is invalid. It just won't return then 'Premium Avatars' category.
            var response = await Utilities.SendGetRequest($"https://us-prof.np.community.playstation.net/userProfile/v1/avatars/categories?offset={offset}&limit={limit}", this.AccountTokens.Authorization).ReceiveJson<AvatarCategories>();

            return response;
        }

        /// <summary>
        /// Fetches the user's trophies.
        /// </summary>
        /// <param name="limit">The amount of trophies to return (optional).</param>
        /// <returns></returns>
        public async Task<TrophyResponses.UserTrophiesResponse> GetTrophies(int limit = 36) {
            var response = await Utilities.SendGetRequest($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset=0&limit={limit}", Auth.CurrentInstance.AccountTokens.Authorization).ReceiveJson<TrophyResponses.UserTrophiesResponse>();

            return response;
        }

        /// <summary>
        /// Deletes the game from showing up under the user's trophies list (only works if the game has 0% of trophies obtained).
        /// </summary>
        /// <param name="gameContentId">The content Id of the game. (ex: NPWR07466_00)</param>
        /// <returns>True if the trophy was successfully deleted.</returns>
        public async Task<bool> DeleteTrophy(string gameContentId) {
            var response = await Utilities.SendDeleteRequest($"https://us-tpy.np.community.playstation.net/trophy/v1/users/{ this.Profile.onlineId }/trophyTitles/{ gameContentId }", this.AccountTokens.Authorization).ReceiveJson();

            if (Utilities.ContainsKey(response, "error"))
                throw new Exception(response.error.message);

            return true;
        }
    }
}