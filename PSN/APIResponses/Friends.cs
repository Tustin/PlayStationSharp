using System.Collections.Generic;

namespace PSN.APIResponses
{
    public class ProfilePictureUrl
    {
        public string size { get; set; }
        public string profilePictureUrl { get; set; }
    }

    public class Friends
    {
        public List<Profile> Profiles { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        public int totalResults { get; set; }
    }
}