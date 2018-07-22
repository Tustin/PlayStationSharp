using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions.Activity
{
	[Serializable]
	public class InvalidCommentException : Exception
	{
		public InvalidCommentException()
		{
		}

		public InvalidCommentException(string message) : base(message)
		{
		}

		public InvalidCommentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidCommentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
