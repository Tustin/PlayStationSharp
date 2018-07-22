using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using PlayStationSharp.Exceptions;
using PlayStationSharp.Exceptions.Auth;
using PlayStationSharp.Model;
using PlayStationSharp.Model.ErrorModelJsonTypes;

namespace PlayStationSharp.Extensions
{
	/// <summary>
	/// Utilities used throughout the project
	/// </summary>
	public static class Utilities
	{

		/// <summary>
		/// Checks if a dynamic ExpandoObject contains a key and is not null.
		/// </summary>
		/// <param name="o">The ExpandoObject to check</param>
		/// <param name="key">The key to check</param>
		/// <returns>True if key exists, false if otherwise.</returns>
		public static bool ContainsKey(this ExpandoObject o, string key)
		{
			return ((o != null) && ((IDictionary<string, object>)o).ContainsKey(key));
		}

		/// <summary>
		/// Try to parse error message because Sony doesn't understand consistency.
		/// </summary>
		/// <param name="data">JSON data.</param>
		/// <returns>New Error object.</returns>
		public static Error ParseError(string data)
		{
			var result = JObject.Parse(data);

			if (result.Count == 1 && result["error"] != null)
			{
				// If there's only one property, we're going to assume it's this type of error.
				// {'error':{'code':2107649,'message':'Not in FriendList'}}

				return new Error()
				{
					Code = result["error"]["code"].ToObject<int>(),
					Message = result["error"]["message"].ToString()
				};
			}
			else
			{
				if (result["message"] != null)
				{
					// Now we're testing for this type of error
					// {'message':'invalid_request (302f01)','uuid':'4a2009d3-2b03-45b4-a1ed-01005a02ee16','data':'Comment does not exist for story','httpStatus':400,'code':3157761}

					return new Error()
					{
						Code = result["code"].ToObject<int>(),
						Message = result["data"].ToString()
					};
				}

				if (result["error"] != null)
				{
					// Lastly, test for this type of error.
					// {'error':'invalid_credentials','error_description':'Invalid two step credentials','error_code':4420,'docs':'https://auth.api.sonyentertainmentnetwork.com/docs/','parameters':[]}

					return new Error()
					{
						Code = result["error_code"].ToObject<int>(),
						Message = result["error_description"].ToString()
					};
				}
			}
			throw new InvalidErrorException($"Unable to parse error response {data}");
		}
	}
}