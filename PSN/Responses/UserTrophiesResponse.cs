using System.Collections.Generic;

namespace PSNSharp.Responses
{
    public partial class TrophyResponses
    {
        public class DefinedTrophies
        {
            public int bronze { get; set; }
            public int silver { get; set; }
            public int gold { get; set; }
            public int platinum { get; set; }
        }

        public partial class FromUser
        {
            public string onlineId { get; set; }
            public int progress { get; set; }
            public EarnedTrophies earnedTrophies { get; set; }
            public bool hiddenFlag { get; set; }
            public string lastUpdateDate { get; set; }
        }

        public partial class TrophyTitle
        {
            public string npCommunicationId { get; set; }
            public string trophyTitleName { get; set; }
            public string trophyTitleDetail { get; set; }
            public string trophyTitleIconUrl { get; set; }
            public string trophyTitlePlatfrom { get; set; }
            public bool hasTrophyGroups { get; set; }
            public DefinedTrophies definedTrophies { get; set; }
            public FromUser fromUser { get; set; }
        }

        public class UserTrophiesResponse
        {
            public int totalResults { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
            public List<TrophyTitle> trophyTitles { get; set; }
        }
    }
}