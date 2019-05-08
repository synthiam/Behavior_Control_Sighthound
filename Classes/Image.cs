using Newtonsoft.Json;

namespace Sighthound_Face_Detection
{
	public class Image
	{
		[JsonProperty("width")]
		public int? Width
		{
			get;
			set;
		}

		[JsonProperty("height")]
		public int? Height
		{
			get;
			set;
		}

		[JsonProperty("orientation")]
		public int? Orientation
		{
			get;
			set;
		}
	}
}
