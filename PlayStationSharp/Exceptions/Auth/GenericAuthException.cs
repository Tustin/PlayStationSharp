using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class GenericAuthException : Exception
	{
		public GenericAuthException()
		{
		}

		public GenericAuthException(string message) : base(message)
		{
		}

		public GenericAuthException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
