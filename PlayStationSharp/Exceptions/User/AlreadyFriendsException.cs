using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions.User
{
	[Serializable]
	internal class AlreadyFriendsException : Exception
	{
		public AlreadyFriendsException()
		{
		}

		public AlreadyFriendsException(string message) : base(message)
		{
		}

		public AlreadyFriendsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected AlreadyFriendsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}