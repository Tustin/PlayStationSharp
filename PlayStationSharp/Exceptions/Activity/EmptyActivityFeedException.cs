using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.Exceptions.Activity
{
	[Serializable]
	internal class EmptyActivityFeedException : Exception
	{
		public EmptyActivityFeedException()
		{
		}

		public EmptyActivityFeedException(string message) : base(message)
		{
		}

		public EmptyActivityFeedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected EmptyActivityFeedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}