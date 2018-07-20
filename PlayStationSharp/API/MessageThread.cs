using System;
using System.Collections.Generic;
using PlayStationSharp.Model.ThreadModelJsonTypes;

namespace PlayStationSharp.API
{
	public enum MessageType
	{
		MessageText,
		MessageAudio,
		MessageImage
	}

	public class MessageThread : IPlayStation
	{
		public PlayStationClient Client { get; private set; }

		public Thread Information { get; private set; }

		private readonly Lazy<List<Message>> _messages;

		public List<Message> Messages => _messages.Value;

		public MessageThread(PlayStationClient client, Thread thread)
		{
			Client = client;
			Information = thread;

			_messages = new Lazy<List<Message>>(GetMessages);
		}

		private List<Message> GetMessages()
		{
			var messages = new List<Message>();


			return messages;
		}
	}
}