using Newtonsoft.Json;

namespace Sighthound_Face_Detection
{
	public class SightHoundRespCls
	{
		[JsonProperty("image")]
		public Image Image
		{
			get;
			set;
		}

		[JsonProperty("objects")]
		public Object[] Objects
		{
			get;
			set;
		}

		[JsonProperty("requestId")]
		public string RequestId
		{
			get;
			set;
		}

		public static SightHoundRespCls FromJson(string json)
		{
			return JsonConvert.DeserializeObject<SightHoundRespCls>(json);
		}
	}
}
