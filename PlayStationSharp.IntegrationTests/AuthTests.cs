using System;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayStationSharp.API;
using PlayStationSharp.Exceptions.Auth;

namespace PlayStationSharp.IntegrationTests
{
	[TestClass]
	public class AuthTests
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidRefreshTokenException), "An invalid or expired refresh token was passed.")]
		public void Login_WithInvalidRefreshToken_InvalidRefreshTokenException()
		{
			Auth.Login("invalid-token");
		}

		[TestMethod]
		public void Login_WithValidRefreshToken_NotNull()
		{
			var token = Environment.GetEnvironmentVariable("PSSHARP_REFRESH", EnvironmentVariableTarget.User);
			var account = Auth.Login(token);

			Assert.IsNotNull(token);
		}
	}
}
