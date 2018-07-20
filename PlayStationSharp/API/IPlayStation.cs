using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayStationSharp.Extensions;

namespace PlayStationSharp.API
{
	// TODO: Come up with a better name for this.
	interface IPlayStation
	{
		PlayStationClient Client { get;  }
	}
}
