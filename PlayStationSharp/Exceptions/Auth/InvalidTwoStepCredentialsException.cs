using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	internal class InvalidTwoStepCredentialsException : Exception
	{
		public InvalidTwoStepCredentialsException()
		{
		}

		public InvalidTwoStepCredentialsException(string message) : base(message)
		{
		}

		public InvalidTwoStepCredentialsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}