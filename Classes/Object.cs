using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sighthound_Face_Detection
{
	public class Object
	{
		[JsonProperty("type")]
		public string Type
		{
			get;
			set;
		}

		[JsonProperty("boundingBox")]
		public BoundingBox BoundingBox
		{
			get;
			set;
		}

		[JsonProperty("landmarks")]
		public Dictionary<string, int[][]> Landmarks
		{
			get;
			set;
		}

		[JsonProperty("attributes")]
		public Attributes Attributes
		{
			get;
			set;
		}
	}
}
