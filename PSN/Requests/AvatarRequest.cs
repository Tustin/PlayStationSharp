namespace PSNSharp.Requests
{
    internal class AvatarRequest
    {
        public string EntitlementId { get; protected set; }

        public AvatarRequest(string contentId)
        {
            this.EntitlementId = contentId;
        }
    }
}