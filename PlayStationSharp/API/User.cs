using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.User;
using PlayStationSharp.Extensions;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ProfileJsonTypes;

namespace PlayStationSharp.API
{
	public class User : AbstractUser
	{
		private readonly Lazy<List<MessageThread>> _messageThreads;

		public List<MessageThread> MessageThreads => _messageThreads.Value;

		[Flags]
		public enum RequestType
		{
			Normal,
			Close,
		}

		private User()
		{
			_messageThreads = new Lazy<List<MessageThread>>(this.GetMessageThreads);
		}

		public User(PlayStationClient client, ProfileModel profile) : this()
		{
			Init(client, profile);
		}

		public User(PlayStationClient client, string onlineId) : this()
		{
			Init(client, onlineId);
		}

		/// <summary>
		/// Adds the current user as a friend.
		/// </summary>
		/// <param name="requestType">Type of friend request to send.</param>
		/// <param name="requestMessage">The message to send with the request.</param>
		public bool AddFriend(RequestType requestType = RequestType.Normal, string requestMessage = "")
		{
			try
			{
				var endpoint =
					$"{APIEndpoints.USERS_URL}{this.Client.Account.OnlineId}/friendList/{this.Profile.OnlineId}{(requestType == RequestType.Close ? "/personalDetailSharingWithBody" : "")}";

				var message = (string.IsNullOrEmpty(requestMessage))
					? new object()
					: new
					{
						requestMessage
					};

				Request.SendJsonPostRequestAsync<object>(endpoint, message, this.Client.Tokens.Authorization);

				return true;
			}
			catch (PlayStationApiException ex)
			{
				switch (ex.Error.Code)
				{
					case 2107654:
						throw new AlreadyFriendsException();
					case 2107657:
						throw new AlreadySharingRequestException();
					default:
						throw;
				}
			}
		}

		/// <summary>
		/// Removes the current user from your friends list,
		/// </summary>
		public void RemoveFriend()
		{
			try
			{
				Request.SendDeleteRequest<object>(
					$"{APIEndpoints.USERS_URL}{this.Client.Account.OnlineId}/friendList/{this.Profile.OnlineId}",
					this.Client.Tokens.Authorization);
			}
			catch (PlayStationApiException ex)
			{
				switch (ex.Error.Code)
				{
					case 2107649:
						throw new NotInFriendsListException();
					default:
						throw;
				}
			}

		}

		/// <summary>
		/// Blocks the current user.
		/// </summary>
		public void Block()
		{
			var response = Request.SendJsonPostRequestAsync<object>($"{APIEndpoints.USERS_URL}{this.Client.Account.OnlineId}/blockList/{this.Profile.OnlineId}",
				null, this.Client.Tokens.Authorization);
		}

		/// <summary>
		/// Unblocks the current user.
		/// </summary>
		public void Unblock()
		{
			var response = Request.SendDeleteRequest<object>($"{APIEndpoints.USERS_URL}{this.Client.Account.OnlineId}/blockList/{this.Profile.OnlineId}",
				this.Client.Tokens.Authorization);
		}

		/// <summary>
		/// Follow the current user.
		/// </summary>
		public void Follow()
		{
			// No point in returning anything here because the endpoint doesn't throw an error if you try to follow someone while already following them.
			Request.SendPutRequest<object>(
				$"https://us-fllw.np.community.playstation.net/follow/v1/users/me/followings/users/{this.OnlineId}",
				oAuthToken: this.Client.Tokens.Authorization);
		}

		public void Unfollow()
		{
			// Same thing as Follow() method.
			Request.SendDeleteRequest<object>(
				$"https://us-fllw.np.community.playstation.net/follow/v1/users/me/followings/users/{this.OnlineId}", this.Client.Tokens.Authorization);
		}

		// TODO: Move anonymous object into proper class
		/// <summary>
		/// Sends a message.
		/// </summary>
		/// <param name="content">Body of the message.</param>
		/// <returns>New instance of the message thread.</returns>
		public MessageThread SendMessage(string content)
		{
			MessageThread messageThread = null;

			var exists = this.Client.Account.FindMessageThreads(this.OnlineId).PrivateMessageThread();
			if (exists != null)
			{
				messageThread = new MessageThread(Client, exists.Information);
			}
			else
			{
				var data = new
				{
					threadDetail = new
					{
						threadMembers = new[]
						{
							new
							{
								onlineId = this.Profile.OnlineId
							},
							new
							{
								onlineId = this.Client.Account.OnlineId
							}
						}
					}
				};

				var requestBody = new StringBuilder();

				requestBody.Append("--gc0p4Jq0M2Yt08jU534c0p");
				requestBody.AppendLine();
				requestBody.Append("Content-Type: application/json; charset=utf-8");
				requestBody.AppendLine();
				requestBody.Append("Content-Disposition: form-data; name=\"threadDetail\"");
				requestBody.AppendLine();
				requestBody.AppendLine();
				requestBody.Append(JsonConvert.SerializeObject(data, Formatting.Indented));
				requestBody.AppendLine();
				requestBody.Append("--gc0p4Jq0M2Yt08jU534c0p--");

				var newThreadModel = Request.SendMultiPartPostRequest<CreatedThreadModel>(
					"https://us-gmsg.np.community.playstation.net/groupMessaging/v1/messageGroups", requestBody, "gc0p4Jq0M2Yt08jU534c0p", this.Client.Tokens.Authorization);

				messageThread = new MessageThread(Client, newThreadModel.ThreadId);
			}

			return messageThread.SendMessage(content);
		}

		/// <summary>
		/// Gets all message threads with both you and the user.
		/// </summary>
		/// <returns>List of each message thread.</returns>
		public List<MessageThread> GetMessageThreads()
		{
			return this.Client.Account.FindMessageThreads(this.OnlineId);
		}
	}
}