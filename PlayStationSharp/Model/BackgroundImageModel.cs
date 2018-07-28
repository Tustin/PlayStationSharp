using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlayStationSharp.Model
{
	public class BackgroundImageModel
	{
		public class Operation
		{
			[JsonProperty("op")]
			public string Op { get; set; }

			[JsonProperty("path")]
			public string Path { get; set; }

			[JsonProperty("value")]
			public string Value { get; set; }

			public Operation(string op, string path, string value)
			{
				this.Op = op;
				this.Path = path;
				this.Value = value;
			}
		}

		[JsonProperty("ops")]
		public IList<Operation> Ops { get; set; }

		public BackgroundImageModel(Operation op)
		{
			Ops = new List<Operation>
			{
				op
			};
		}
	}
}
