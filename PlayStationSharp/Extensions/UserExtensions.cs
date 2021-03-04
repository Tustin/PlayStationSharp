using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.API;

namespace PlayStationSharp.Extensions
{
	public static class UserExtensions
	{
		public static List<User> Online(this List<User> users)
		{
			return users.Where(a => a.IsOnline).ToList();
		}
	}
}
