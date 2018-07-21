using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions.User
{
	[Serializable]
	internal class AlreadySharingRequestException : Exception
	{
		public AlreadySharingRequestException()
		{
		}

		public AlreadySharingRequestException(string message) : base(message)
		{
		}

		public AlreadySharingRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected AlreadySharingRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}