using System;
using System.Collections.Generic;
using System.Linq;
using Flurl.Http;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Activity;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Exceptions.User;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.API
{
	/// <summary>
	/// Contains information for the currently logged in account.
	/// </summary>
	public class Account : AbstractUser
	{
		private readonly Lazy<List<MessageThread>> _messageThreads;

		public List<MessageThread> MessageThreads => _messageThreads.Value;

		public Account(OAuthTokens tokens)
		{
			Init(new PlayStationClient(tokens, this));

			_messageThreads = new Lazy<List<MessageThread>>(() => GetMessageThreads());
		}

		/// <summary>
		/// Finds a user.
		/// </summary>
		/// <param name="onlineId">Online name of the user.</param>
		/// <returns>New instance of User for the selected player.</returns>
		public User FindUser(string onlineId)
		{
			try
			{
				var profile = Request.SendGetRequest<Profile>(
					$"{APIEndpoints.USERS_URL}{onlineId}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s",
					this.Client.Tokens.Authorization);

				return new User(Client, profile.Information);
			}
			catch (PlayStationApiException ex)
			{
				switch (ex.Error.Code)
				{
					case 2105356:
						throw new UserNotFoundException(ex.Error.Message);
					default:
						throw;
				}
			}

		}

		/// <summary>
		/// Finds all message threads (aka groups) you are in with the given onlineId.
		/// </summary>
		/// <param name="onlineId"></param>
		/// <returns></returns>
		public List<MessageThread> FindMessageThreads(string onlineId)
		{
			return this.Client.Account.MessageThreads.Where(a => a.Members.Any(b => b.OnlineId.Equals(onlineId)))
				.ToList();
		}

		/// <summary>
		/// Gets message threads (aka groups) for the logged in account.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		private List<MessageThread> GetMessageThreads(int offset = 0, int limit = 20)
		{
			var threadModels = Request.SendGetRequest<ThreadsModel>($"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/threads?fields=threadMembers,threadNameDetail,threadThumbnailDetail,threadProperty,latestMessageEventDetail,latestTakedownEventDetail,newArrivalEventDetail&limit={limit}&offset={offset}&sinceReceivedDate=1970-01-01T00:00:00Z",
				this.Client.Tokens.Authorization);

			return threadModels.Threads.Select(thread => new MessageThread(Client, thread)).ToList();
		}
	}
}