using System.Collections.Generic;

namespace PlayStationSharp.Model
{
    public class ProfilePictureUrl
    {
        public string size { get; set; }
        public string profilePictureUrl { get; set; }
    }

    public class FriendsResponse
    {
        public List<ProfileResponse> Profiles { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        public int totalResults { get; set; }
    }
}