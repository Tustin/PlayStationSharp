using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class CaptchaTokenMissingException : Exception
	{
		public CaptchaTokenMissingException()
		{
		}

		public CaptchaTokenMissingException(string message) : base(message)
		{
		}

		public CaptchaTokenMissingException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
