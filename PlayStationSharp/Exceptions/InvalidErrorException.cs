using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions
{
	[Serializable]
	public class InvalidErrorException : Exception
	{
		public InvalidErrorException()
		{
		}

		public InvalidErrorException(string message) : base(message)
		{
		}

		public InvalidErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
