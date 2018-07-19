using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	public class Trophies
	{
		/// <summary>
		/// Gets the trophies of a game.
		/// </summary>
		/// <param name="gameContentId">The content Id of the game. (ex: NPWR07466_00)</param>
		/// <returns>GameTrophiesResponse object containing a list of all the trophies.</returns>
		public static TrophyResponses.GameTrophiesResponse GetGameTrophies(string gameContentId)
		{
			return Request.SendGetRequest<TrophyResponses.GameTrophiesResponse>($"https://us-tpy.np.community.playstation.net/trophy/v1/trophyTitles/{gameContentId}/trophyGroups/all/trophies?fields=@default,trophyRare,trophyEarnedRate&npLanguage=en&sortKey=trophyId&iconSize=m",
				Auth.CurrentInstance.AccountTokens.Authorization);
		}
	}
}