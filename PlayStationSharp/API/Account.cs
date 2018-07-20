using System;
using System.Collections.Generic;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	/// <summary>
	/// Contains information for the currently logged in account.
	/// </summary>
	public class Account : IPlayStation
	{
		public Profile Profile { get; protected set; }
		public List<User> Friends { get; protected set; }

		public PlayStationClient Client { get; private set; }

		public Account(OAuthTokens tokens)
		{
			Client = new PlayStationClient(tokens, this);
			Profile = GetInfo();
			Friends = GetFriends(StatusFilter.All);
		}

		[Flags]
		public enum StatusFilter
		{
			Online,
			All
		}

		private Profile GetInfo()
		{
			return Request.SendGetRequest<Profile>($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s",
				this.Client.Tokens.Authorization);
		}

		/// <summary>
		/// Fetches each friend of the logged in account.
		/// </summary>
		/// <param name="filter">A filter to grab online or all friends. Defaults to online.</param>
		/// <param name="limit">The amount of friends to return. By default, the PSN app uses 36.</param>
		/// <returns>A list of User objects for each friend.</returns>
		private List<User> GetFriends(StatusFilter filter = StatusFilter.Online, int limit = 36)
		{
			var friends = new List<User>();

			var filterQuery = "";

			if (filter == StatusFilter.Online)
			{
				filterQuery = "&userFilter=online";
			}

			var response = Request.SendGetRequest<FriendsModel>($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/friends/profiles2?fields=onlineId,avatarUrls,plus,trophySummary(@default),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),primaryOnlineStatus,presences(@titleInfo,hasBroadcastData)&sort=name-onlineId{filterQuery}&avatarSizes=m&profilePictureSizes=m&offset=0&limit={limit}",
				this.Client.Tokens.Authorization);

			foreach (var friend in response.Profiles)
			{
				friends.Add(new User(Client, friend));
			}

			return friends;
		}

		// TODO: Move into Trophy class
		/// <summary>
		/// Deletes the game from showing up under the user's trophies list (only works if the game has 0% of trophies obtained).
		/// </summary>
		/// <param name="gameContentId">The content Id of the game. (ex: NPWR07466_00)</param>
		/// <returns>True if the trophy was successfully deleted.</returns>
		//public bool DeleteTrophy(string gameContentId)
		//{
		//	Request.SendDeleteRequest<object>($"https://us-tpy.np.community.playstation.net/trophy/v1/users/{ this.Profile.onlineId }/trophyTitles/{ gameContentId }",
		//		this.AccountTokens.Authorization);

		//	return true;
		//}
	}
}