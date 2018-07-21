using System.Collections.Generic;
using Newtonsoft.Json;
using PlayStationSharp.Model.ThreadModelJsonTypes;

namespace PlayStationSharp.Model
{
	public class ThreadModel
	{
		[JsonProperty("threadMembers")]
		public IList<ThreadMember> ThreadMembers { get; set; }

		[JsonProperty("threadNameDetail")]
		public ThreadNameDetail ThreadNameDetail { get; set; }

		[JsonProperty("threadThumbnailDetail")]
		public ThreadThumbnailDetail ThreadThumbnailDetail { get; set; }

		[JsonProperty("threadProperty")]
		public ThreadProperty ThreadProperty { get; set; }

		[JsonProperty("newArrivalEventDetail")]
		public NewArrivalEventDetail NewArrivalEventDetail { get; set; }

		[JsonProperty("threadEvents")]
		public IList<ThreadEvent> ThreadEvents { get; set; }

		[JsonProperty("threadId")]
		public string ThreadId { get; set; }

		[JsonProperty("threadType")]
		public int ThreadType { get; set; }

		[JsonProperty("threadModifiedDate")]
		public string ThreadModifiedDate { get; set; }

		[JsonProperty("resultsCount")]
		public int ResultsCount { get; set; }

		[JsonProperty("maxEventIndexCursor")]
		public string MaxEventIndexCursor { get; set; }

		[JsonProperty("sinceEventIndexCursor")]
		public string SinceEventIndexCursor { get; set; }

		[JsonProperty("latestEventIndex")]
		public string LatestEventIndex { get; set; }

		[JsonProperty("endOfThreadEvent")]
		public bool EndOfThreadEvent { get; set; }
	}

}
