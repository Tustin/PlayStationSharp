using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.API;
using PlayStationSharp.Extensions;

namespace PlayStationSharp
{
	public class PlayStationClient
	{
		public OAuthTokens Tokens { get; private set; }

		public Account Account { get; private set; }

		public PlayStationClient(OAuthTokens tokens, Account account)
		{
			this.Tokens = tokens;
			this.Account = account;
		}
	}
}
