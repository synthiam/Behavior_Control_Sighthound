using Newtonsoft.Json;

namespace Sighthound_Face_Detection
{
	public static class Serialize
	{
		public static string ToJson(this SightHoundRespCls self)
		{
			return JsonConvert.SerializeObject((object)self);
		}
	}
}
