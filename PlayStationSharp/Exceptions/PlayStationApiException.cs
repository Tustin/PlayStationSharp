using System;
using PlayStationSharp.Model.ErrorModelJsonTypes;

namespace PlayStationSharp.Exceptions
{
	[Serializable]
	public class PlayStationApiException : Exception
	{
		public Error Error;

		public PlayStationApiException(Error error) : base(error.Message)
        {
            Error = error;
        }
	}
}
