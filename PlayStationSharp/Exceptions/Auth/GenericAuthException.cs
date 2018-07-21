using System;
using PlayStationSharp.Model;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class GenericAuthException : Exception
	{
		public AuthErrorModel Error;
		public GenericAuthException(AuthErrorModel error)
		{
			Error = error;
		}
	}
}
