using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationSharp.Exceptions.Auth
{
	[Serializable]
	public class NpssoIdNotFoundException : Exception
	{
		public NpssoIdNotFoundException()
		{
		}

		public NpssoIdNotFoundException(string message) : base(message)
		{
		}

		public NpssoIdNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
