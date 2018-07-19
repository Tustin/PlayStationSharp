using System.Collections.Generic;

namespace PSNSharp.API
{
    public enum MessageType
    {
        MessageText,
        MessageAudio,
        MessageImage
    }

    public class Message
    {
        public MessageType MessageType { get; protected set; }
        public List<string> UsersToInvite { get; protected set; }
        public string MessageText { get; protected set; }

        public User Receiver {
            get
            {
                return Receiver;
            }
            set
            {
                //Add the person we're messaging to the list of users so it actually sends it to them.
                UsersToInvite.Add(value.Profile.onlineId);
            }
        }

        /// <summary>
        /// Creates a new Message object that adds multiple users to the group chat.
        /// </summary>
        /// <param name="usersToInvite">List of PSN onlineId's to add to the message.</param>
        /// <param name="message">Message to be sent.</param>
        public Message(List<string> usersToInvite, string message)
        {
            this.MessageType = MessageType.MessageText;
            this.UsersToInvite = usersToInvite;
            this.MessageText = message;
        }

        /// <summary>
        /// Creates a new Message object that only adds the current User object to the group chat.
        /// </summary>
        /// <param name="message">Message to be sent.</param>
        public Message(string message)
        {
            this.MessageType = MessageType.MessageText;
            this.MessageText = message;
            this.UsersToInvite = new List<string>();
        }
    }
}