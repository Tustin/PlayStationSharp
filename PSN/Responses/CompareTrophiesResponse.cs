using System.Collections.Generic;

namespace PSNSharp.Responses
{
    public partial class TrophyResponses
    {
        public class ComparedUser
        {
            public string onlineId { get; set; }
            public int progress { get; set; }
            public EarnedTrophies earnedTrophies { get; set; }
            public string lastUpdateDate { get; set; }
        }

        /// <summary>
        /// This class is an extension of TrophyTitle from UserTrophiesResponse. The only difference with this response is it contains the compared user info.
        /// </summary>
        public partial class TrophyTitle
        {
            public ComparedUser comparedUser { get; set; }
        }

        public class CompareTrophiesResponse
        {
            public int totalResults { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
            public List<TrophyTitle> trophyTitles { get; set; }
        }
    }
}