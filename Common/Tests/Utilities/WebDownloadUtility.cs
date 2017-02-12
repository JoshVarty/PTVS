using System;
using System.IO;
using System.Net;

namespace TestUtilities {
    public static class WebDownloadUtility {
        public static string GetString(Uri siteUri) {
            string text;
            var req = HttpWebRequest.CreateHttp(siteUri);
            
            using (var resp = req.GetResponse())
            using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {
                text = reader.ReadToEnd();
            }

            return text;
        }
    }
}
