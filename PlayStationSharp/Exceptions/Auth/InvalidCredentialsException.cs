using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException()
		{
		}

		public InvalidCredentialsException(string message) : base(message)
		{
		}

		public InvalidCredentialsException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
