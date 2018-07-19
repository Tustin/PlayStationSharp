using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class OAuthTokenNotFoundException : Exception
	{
		public OAuthTokenNotFoundException()
		{
		}

		public OAuthTokenNotFoundException(string message) : base(message)
		{
		}

		public OAuthTokenNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
