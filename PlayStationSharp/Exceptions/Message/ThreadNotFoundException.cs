using System;

namespace PlayStationSharp.Exceptions.Message
{
	[Serializable]
	public class ThreadNotFoundException : Exception
	{
		public ThreadNotFoundException()
		{
		}

		public ThreadNotFoundException(string message) : base(message)
		{
		}

		public ThreadNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
