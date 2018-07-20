using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	public abstract class AbstractUser : IPlayStation
	{
		public PlayStationClient Client { get; protected set; }

		/// <summary>
		/// Fetches profile of a user.
		/// </summary>
		/// <param name="psn">The PSN online Id of the user.</param>
		/// <returns>A profile object containing the user's info</returns>
		protected Profile GetInfo(string psn = null)
		{
			return Request.SendGetRequest<Profile>($"{APIEndpoints.USERS_URL}{psn ?? "me"}/profile2?fields=npId,onlineId,avatarUrls,plus,aboutMe,languagesUsed,trophySummary(@default,progress,earnedTrophies),isOfficiallyVerified,personalDetail(@default,profilePictureUrls),personalDetailSharing,personalDetailSharingRequestMessageFlag,primaryOnlineStatus,presences(@titleInfo,hasBroadcastData),friendRelation,requestMessageFlag,blocking,mutualFriendsCount,following,followerCount,friendsCount,followingUsersCount&avatarSizes=m,xl&profilePictureSizes=m,xl&languagesUsedLanguageSet=set3&psVitaTitleIcon=circled&titleIconSize=s", this.Client.Tokens.Authorization);
		}
	}
}
