using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.API;

namespace PlayStationSharp.Extensions
{
	public static class MessageThreadExtensions
	{
		public static MessageThread PrivateMessageThread(this List<MessageThread> thread)
		{
			return thread.FirstOrDefault(a => a.Members.Count == 2);
		}
	}
}
