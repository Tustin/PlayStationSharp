using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class DualAuthSMSRequiredException : Exception
	{
		public DualAuthSMSRequiredException()
		{
		}

		public DualAuthSMSRequiredException(string message) : base(message)
		{
		}

		public DualAuthSMSRequiredException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
