using System.Collections.Generic;

namespace PSN.Responses
{
    public partial class TrophyResponses
    {
        public partial class FromUser
        {
            public bool earned { get; set; }
            public string earnedDate { get; set; }
        }

        public class Trophy
        {
            public int trophyId { get; set; }
            public bool trophyHidden { get; set; }
            public string trophyType { get; set; }
            public string trophyName { get; set; }
            public string trophyDetail { get; set; }
            public string trophyIconUrl { get; set; }
            public int trophyRare { get; set; }
            public string trophyEarnedRate { get; set; }
            public FromUser fromUser { get; set; }
        }

        public class GameTrophiesResponse
        {
            public List<Trophy> trophies { get; set; }
        }
    }
}