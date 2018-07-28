using System;
using System.Runtime.Serialization;

namespace PlayStationSharp.API
{
	[Serializable]
	internal class EmptyImageException : Exception
	{
		public EmptyImageException()
		{
		}

		public EmptyImageException(string message) : base(message)
		{
		}

		public EmptyImageException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected EmptyImageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}