using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationSharp.API
{
	public class Message : IPlayStation
	{
		public PlayStationClient Client { get; }
	}
}
