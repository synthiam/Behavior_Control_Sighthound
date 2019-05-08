using Newtonsoft.Json;

namespace Sighthound_Face_Detection
{
	public class Attributes
	{
		[JsonProperty("gender")]
		public string Gender
		{
			get;
			set;
		}

		[JsonProperty("genderConfidence")]
		public double? GenderConfidence
		{
			get;
			set;
		}

		[JsonProperty("frontal")]
		public bool? Frontal
		{
			get;
			set;
		}
	}
}
