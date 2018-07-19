using System;

namespace PlayStationSharp.Exceptions
{
	[Serializable]
	public class PlayStationApiException : Exception
	{
		public PlayStationApiException()
		{
		}

		public PlayStationApiException(string message) : base(message)
		{
		}

		public PlayStationApiException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
