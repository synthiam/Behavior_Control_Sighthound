using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sighthound_Face_Detection {

  public class InsightHoundUtrils {

    public static SightHoundRespCls DetectImage(string apiKey, byte[] imgData) {

      Dictionary<string, byte[]> dict = new Dictionary<string, byte[]>();
      dict.Add("image", imgData);
      string json = JsonConvert.SerializeObject((object)dict);
      byte[] body = Encoding.UTF8.GetBytes(json);

      WebRequest request = WebRequest.Create("https://dev.sighthoundapi.com/v1/detections?type=face,person&faceOption=landmark,gender");
      request.Method = "POST";
      request.ContentType = "application/json";
      request.ContentLength = json.Length;
      request.Headers["X-Access-Token"] = apiKey;

      using (Stream requestStream = request.GetRequestStream()) {

        requestStream.Write(body, 0, body.Length);
        requestStream.Close();
      }

      using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {

        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {

          json = reader.ReadToEnd();
          reader.Close();
        }
      }

      return SightHoundRespCls.FromJson(json);
    }
  }
}
