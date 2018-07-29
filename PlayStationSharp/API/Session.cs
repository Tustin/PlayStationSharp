using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Model;

namespace PlayStationSharp.API
{
	public class Session : IPlayStation
	{
		private readonly Lazy<List<User>> _players;

		public PlayStationClient Client { get; private set; }
		public SessionModel Information;
		public List<User> Players => _players.Value;


		public Session(PlayStationClient client, SessionModel sessionModel)
		{
			this.Client = client;
			this.Information = sessionModel;
			this._players = new Lazy<List<User>>(GetPlayers);
		}

		private List<User> GetPlayers()
		{
			// TODO: Check for multiple sessions??
			return this.Information.Sessions[0].Members.Select(member => new User(Client, member.OnlineId)).ToList();
		}
	}
}
