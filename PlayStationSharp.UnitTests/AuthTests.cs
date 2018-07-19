using System;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayStationSharp.API;
using PlayStationSharp.Exceptions.Auth;

namespace PlayStationSharp.UnitTests
{
	[TestClass]
	public class AuthTests
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException),
			"Invalid credentials were passed to the Login method.")]
		public void Login_WithInvalidCredentials_NotNull()
		{
			var login = Auth.Login("me@email.com", "p@55w0rd");
		}

		[TestMethod]
		[ExpectedException(typeof(CaptchaTokenMissingException),
			"Invalid credentials were passed to the Login method.")]
		public void Login_WithMissingCaptcha_CaptchaTokenMissingException()
		{
			var login = Auth.Login("me@email.com", "p@55w0rd");
		}
	}
}
