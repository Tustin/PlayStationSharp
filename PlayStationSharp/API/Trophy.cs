using PlayStationSharp.Model;
using PlayStationSharp.Model.TrophyJsonTypes;

namespace PlayStationSharp.API
{
	public class Trophy : IPlayStation
	{
		public PlayStationClient Client { get; private set; }

		public TrophyTitle Information { get; private set; }

		public Trophy(PlayStationClient client, TrophyTitle trophy)
		{
			Client = client;
			Information = trophy;
		}

		/// <summary>
		/// Deletes the game from showing up under the user's trophies list (only works if the game has 0% of trophies obtained).
		/// </summary>
		/// <param name="gameContentId">The content Id of the game. (ex: NPWR07466_00)</param>
		/// <returns>True if the trophy was successfully deleted.</returns>
		public bool DeleteTrophy(string gameContentId)
		{
			Request.SendDeleteRequest<object>($"https://us-tpy.np.community.playstation.net/trophy/v1/users/{ this.Client.Account.OnlineId}/trophyTitles/{ gameContentId }",
				this.Client.Tokens.Authorization);

			return true;
		}

		/// <summary>
		/// Gets the trophies of a game.
		/// </summary>
		/// <param name="gameContentId">The content Id of the game. (ex: NPWR07466_00)</param>
		/// <returns>GameTrophiesResponse object containing a list of all the trophies.</returns>
		//public static TrophyResponses.GameTrophiesResponse GetGameTrophies(string gameContentId)
		//{
		//	return Request.SendGetRequest<TrophyResponses.GameTrophiesResponse>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles/{gameContentId}/trophyGroups/all/trophies?fields=@default,trophyRare,trophyEarnedRate&npLanguage=en&sortKey=trophyId&iconSize=m",
		//		Auth.CurrentInstance.AccountTokens.Authorization);
		//}
	}
}