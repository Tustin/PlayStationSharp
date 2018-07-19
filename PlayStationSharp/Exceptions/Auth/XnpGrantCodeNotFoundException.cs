using System;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class XnpGrantCodeNotFoundException : Exception
	{
		public XnpGrantCodeNotFoundException()
		{
		}

		public XnpGrantCodeNotFoundException(string message) : base(message)
		{
		}

		public XnpGrantCodeNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
