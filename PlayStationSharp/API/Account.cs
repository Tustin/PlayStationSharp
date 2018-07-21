using System;
using System.Collections.Generic;
using System.Linq;
using Flurl.Http;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Exceptions.User;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	/// <summary>
	/// Contains information for the currently logged in account.
	/// </summary>
	public class Account : AbstractUser
	{
		public Profile Profile { get; protected set; }

		private readonly Lazy<List<User>> _friends;
		private readonly Lazy<List<Trophy>> _trophies;
		private readonly Lazy<List<MessageThread>> _messageThreads;

		public List<User> Friends => _friends.Value;
		public List<Trophy> Trophies => _trophies.Value;
		public List<MessageThread> MessageThreads => _messageThreads.Value;

		public Account(OAuthTokens tokens)
		{
			Client = new PlayStationClient(tokens, this);
			Profile = GetInfo();

			_friends = new Lazy<List<User>>(() => GetFriends());
			_trophies = new Lazy<List<Trophy>>(() => GetTrophies());
			_messageThreads = new Lazy<List<MessageThread>>(() => GetMessageThreads());
		}

		[Flags]
		public enum StatusFilter
		{
			Online,
			All
		}

		public User FindUser(string psn)
		{
			try
			{
				var profile = Request.SendGetRequest<Profile>(
					$"{APIEndpoints.USERS_URL}{psn}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s",
					this.Client.Tokens.Authorization);

				return new User(Client, profile.Information);
			}
			catch (FlurlHttpException ex)
			{
				var error = ex.GetResponseJsonAsync<ErrorModel>().Result;

				switch (error.ErrorCode)
				{
					case 2105356:
						throw new UserNotFoundException();
					default:
						throw new GenericAuthException(error.ErrorDescription);
				}
			}

		}

		public List<MessageThread> FindMessageThreads(string onlineId)
		{
			return this.Client.Account.MessageThreads.Where(a => a.Members.Any(b => b.OnlineId.Equals(onlineId)))
				.ToList();
		}

		/// <summary>
		/// Fetches each friend of the logged in account.
		/// </summary>
		/// <param name="filter">A filter to grab online or all friends. Defaults to online.</param>
		/// <param name="limit">The amount of friends to return. By default, the PSN app uses 36.</param>
		/// <returns>A list of User objects for each friend.</returns>
		private List<User> GetFriends(StatusFilter filter = StatusFilter.Online, int limit = 36)
		{
			var filterQuery = (filter == StatusFilter.Online) ? "&userFilter=online" : "";

			var response = Request.SendGetRequest<FriendModel>($"https://us-prof.np.community.playstation.net/userProfile/v1/users/me/friends/profiles2?fields=onlineId,avatarUrls,plus,trophySummary(@default),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),primaryOnlineStatus,presences(@titleInfo,hasBroadcastData)&sort=name-onlineId{filterQuery}&avatarSizes=m&profilePictureSizes=m&offset=0&limit={limit}",
				this.Client.Tokens.Authorization);

			return response.Profiles.Select(friend => new User(Client, friend)).ToList();
		}

		/// <summary>
		/// Fetches trophies for the logged in account.
		/// </summary>
		/// <param name="limit">The amount of trophies to return (optional).</param>
		/// <returns></returns>
		public List<Trophy> GetTrophies(int limit = 36)
		{
			var trophyModels = Request.SendGetRequest<TrophyModel>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset=0&limit={limit}",
				this.Client.Tokens.Authorization);

			return trophyModels.TrophyTitles.Select(trophy => new Trophy(Client, trophy)).ToList();
		}

		public List<MessageThread> GetMessageThreads(int offset = 0, int limit = 20)
		{
			var threadModels = Request.SendGetRequest<ThreadsModel>($"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/threads?fields=threadMembers,threadNameDetail,threadThumbnailDetail,threadProperty,latestMessageEventDetail,latestTakedownEventDetail,newArrivalEventDetail&limit={limit}&offset={offset}&sinceReceivedDate=1970-01-01T00:00:00Z",
				this.Client.Tokens.Authorization);

			return threadModels.Threads.Select(thread => new MessageThread(Client, thread)).ToList();
		}
	}
}