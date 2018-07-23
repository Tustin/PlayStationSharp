using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions.Activity
{
	[Serializable]
	internal class NoCommentsFoundException : Exception
	{
		public NoCommentsFoundException()
		{
		}

		public NoCommentsFoundException(string message) : base(message)
		{
		}

		public NoCommentsFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected NoCommentsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}