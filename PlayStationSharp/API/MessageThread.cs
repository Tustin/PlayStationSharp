using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PlayStationSharp.Model;

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

		public ThreadModel Information { get; private set; }

		private readonly Lazy<List<Message>> _messages;
		private readonly Lazy<List<User>> _members;

		public List<Message> Messages => _messages.Value;
		public List<User> Members => _members.Value;

		private MessageThread()
		{
			_messages = new Lazy<List<Message>>(() => GetMessages());
			_members = new Lazy<List<User>>(GetMembers);
		}

		private List<User> GetMembers()
		{
			var members = new List<User>();

			foreach (var member in this.Information.ThreadMembers)
			{
				members.Add(new User(Client, member.OnlineId));
			}

			return members;
		}

		public MessageThread(PlayStationClient client, ThreadModel thread) : this()
		{
			Client = client;
			Information = thread;
		}

		public MessageThread(PlayStationClient client, string threadId) : this()
		{
			Client = client;
			Information = GetThread(threadId);
		}

		private ThreadModel GetThread(string threadId, int count = 200)
		{
			return Request.SendGetRequest<ThreadModel>($"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/threads/{threadId}?count={count}&fields=threadMembers,threadNameDetail,threadThumbnailDetail,threadProperty,latestTakedownEventDetail,newArrivalEventDetail,threadEvents",
				this.Client.Tokens.Authorization);
		}

		private bool Leave()
		{
			try
			{
				Request.SendDeleteRequest<object>(
					$"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/threads/{this.Information.ThreadId}/users/me",
					Client.Tokens.Authorization);
				return true;
			}
			catch (Exception)
			{
				return false; // TODO
			}

		}

		public MessageThread SendMessage(string content)
		{
			var messageModel = Request.SendMultiPartPostRequest<CreatedThreadModel>(
				$"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/threads/{this.Information.ThreadId}/messages", "gc0p4Jq0M2Yt08jU534c0p", "messageEventDetail", new
				{
					messageEventDetail = new
					{
						eventCategoryCode = 1,
						messageDetail = new
						{
							body = content
						}
					}
				}, this.Client.Tokens.Authorization);

			return this;
		}

		private List<Message> GetMessages(int count = 200)
		{
			var messages = new List<Message>();

			var threadModels = GetThread(this.Information.ThreadId, count);

			return threadModels.ThreadEvents.Select(message => new Message(Client, message)).ToList();
		}
	}
}