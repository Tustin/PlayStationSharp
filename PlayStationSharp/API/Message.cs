using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Model.ThreadModelJsonTypes;

namespace PlayStationSharp.API
{
	public class Message : IPlayStation
	{
		private readonly Lazy<User> _sender;
		private readonly ThreadEvent _message;

		public User Sender => _sender.Value;
		public string Body => _message.MessageEventDetail.MessageDetail.Body;
		public DateTime Date => DateTime.Parse(_message.MessageEventDetail.PostDate);

		public PlayStationClient Client { get; private set; }

		public override string ToString() => Body;

		public Message(PlayStationClient client, ThreadEvent message)
		{
			this.Client = client;
			this._message = message;

			_sender = new Lazy<User>(() => new User(client, _message.MessageEventDetail.Sender.OnlineId));
		}
	}
}
