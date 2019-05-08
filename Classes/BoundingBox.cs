using Newtonsoft.Json;

namespace Sighthound_Face_Detection
{
	public class BoundingBox
	{
		[JsonProperty("x")]
		public int X
		{
			get;
			set;
		}

		[JsonProperty("y")]
		public int Y
		{
			get;
			set;
		}

		[JsonProperty("height")]
		public int Height
		{
			get;
			set;
		}

		[JsonProperty("width")]
		public int Width
		{
			get;
			set;
		}
	}
}
