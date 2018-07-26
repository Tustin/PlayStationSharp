using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Exceptions.Activity;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.API
{
	public abstract class AbstractUser : IPlayStation
	{
		private readonly Lazy<List<User>> _friends;
		private readonly Lazy<List<Trophy>> _trophies;
		private readonly Lazy<List<Story>> _activityFeed;

		private string UserParameter =>
			(this.OnlineId == this.Client.Account.OnlineId) ? "me" : this.OnlineId;

		public List<User> Friends => _friends.Value;
		public List<Trophy> Trophies => _trophies.Value;
		public List<Story> ActivityFeed => _activityFeed.Value;

		public ProfileModel Profile { get; protected set; }
		public PlayStationClient Client { get; protected set; }

		public string OnlineId => this.Profile.OnlineId;

		public override string ToString() => this.OnlineId;


		[Flags]
		public enum StatusFilter
		{
			Online,
			All
		}

		protected AbstractUser()
		{
			_friends = new Lazy<List<User>>(() => GetFriends());
			_trophies = new Lazy<List<Trophy>>(() => GetTrophies());
			_activityFeed = new Lazy<List<Story>>(() => GetActivityFeed());
		}

		protected void Init(PlayStationClient client)
		{
			this.Client = client;
			this.Profile = this.GetInfo().Information;
		}

		protected void Init(PlayStationClient client, ProfileModel profile)
		{
			this.Client = client;
			this.Profile = profile;
		}

		protected void Init(PlayStationClient client, string onlineId)
		{
			this.Client = client;
			this.Profile = this.GetInfo(onlineId).Information;
		}

		/// <summary>
		/// Fetches profile of a user.
		/// </summary>
		/// <param name="onlineId">The PSN online Id of the user.</param>
		/// <returns>A profile object containing the user's info</returns>
		protected Profile GetInfo(string onlineId = null)
		{
			return Request.SendGetRequest<Profile>($"{APIEndpoints.USERS_URL}{onlineId ?? "me"}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", this.Client.Tokens.Authorization);
		}

		/// <summary>
		/// Gets friends for the current account.
		/// </summary>
		/// <param name="filter">Filter friends by status.</param>
		/// <param name="limit">The amount of friends to return.</param>
		/// <returns>A list of User objects for each friend.</returns>
		protected List<User> GetFriends(StatusFilter filter = StatusFilter.All, int limit = 500)
		{
			var response = Request.SendGetRequest<FriendModel>($"https://us-prof.np.community.playstation.net/userProfile/v1/users/{this.UserParameter}/friends/profiles2?fields=onlineId,accountId,avatarUrls,aboutMe,plus,trophySummary(@default),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),presences(@titleInfo,hasBroadcastData),presences(@titleInfo),friendRelation,consoleAvailability&profilePictureSizes=m&avatarSizes=m&sort=onlineStatus&titleIconSize=s&extendPersonalDetailTarget=true&offset=0&limit={limit}",
				this.Client.Tokens.Authorization);

			return response.Profiles.Select(friend => new User(Client, friend)).ToList();
		}

		/// <summary>
		/// Gets trophies for the current account.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit">The amount of trophies to return.</param>
		/// <returns></returns>
		public List<Trophy> GetTrophies(int offset = 0, int limit = 36)
		{
			var url =
				$"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles?fields=@default&npLanguage=en&iconSize=m&platform=PS3,PSVITA,PS4&offset={offset}&limit={limit}&comparedUser={this.Profile.OnlineId}";

			//if (this.Profile.OnlineId != null)
			//	url += $"&comparedUser={compare}";

			var trophyModels = Request.SendGetRequest<TrophyModel>(url,
				this.Client.Tokens.Authorization);

			return trophyModels.TrophyTitles.Select(trophy => new Trophy(Client, trophy)).ToList();
		}

		/// <summary>
		/// Gets activity feed.
		/// </summary>
		/// <returns></returns>
		protected List<Story> GetActivityFeed(bool includeComments = true, int offset = 0, int blockSize = 10)
		{
			var activityModels = Request.SendGetRequest<ActivityModel>(
				$"https://activity.api.np.km.playstation.net/activity/api/v2/users/{UserParameter}/feed/0?includeComments={includeComments}&offset={offset}&blockSize={blockSize}",
				this.Client.Tokens.Authorization);

			if (activityModels.Feed.Count == 0) throw new EmptyActivityFeedException();

			return activityModels.Feed.Select(feed => new Story(Client, feed)).ToList();
		}
	}
}
