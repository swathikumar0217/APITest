using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Helpers;

namespace SampleAPIAutomation
{
    [TestClass]
    public class SwathiTestCase1
    {
        [TestMethod]        
        public void TestMethodReadData()
        {
            string readJsonData;
            //Sample online RestAPI to call data from Web.
            string url = "https://reqres.in/api/users/5";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                readJsonData = reader.ReadToEnd();
            }
            var resp = JsonConvert.DeserializeObject<SingleUserResponseObject>(readJsonData);
            Assert.IsTrue(resp.data.id.Equals(5));
        }

        [TestMethod]
        public void TestMethodReadData2()
        {

            string readJsonData;

            var reqObj = new UsersRequestObject();
            reqObj.name = "charles";
            reqObj.job = "leader";
            string request = JsonConvert.SerializeObject(reqObj);
            //Sample online RestAPI to call data from Web.
            string url = "https://reqres.in/api/users";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(request);
            }
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                readJsonData = reader.ReadToEnd();
            }

            var apiResponse = JsonConvert.DeserializeObject<UsersResponseObject>(readJsonData);
            Assert.IsTrue(apiResponse.name.Equals("charles") && apiResponse.job.Equals("leader"));
        }
      
       
    }

    public class SingleUserResponseObject
    {
        public Data data { get; set; }
        public Ad ad { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }

    public class Ad
    {
        public string company { get; set; }
        public string url { get; set; }
        public string text { get; set; }
    }

    public class UsersRequestObject
    {
        public string name { get; set; }
        public string job { get; set; }
    }
    
 


    public class UsersResponseObject
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
    }


}
