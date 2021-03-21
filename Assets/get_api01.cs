using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//JSON + HTTP
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;

public class get_api01 : MonoBehaviour
{
   // public TextAsset
    // Start is called before the first frame update
    void Start()
    {
        
    }
    static public JArray getJson(string uri)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri); //request請求
        req.Timeout = 10000; //request逾時時間
        req.Method = "GET"; //request方式
        HttpWebResponse respone = (HttpWebResponse)req.GetResponse(); //接收respone
        StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8); //讀取respone資料
        string result = streamReader.ReadToEnd(); //讀取到最後一行
        respone.Close();
        streamReader.Close();
        JObject jsondata = JsonConvert.DeserializeObject<JObject>(result); //將資料轉為json物件
        return (JArray)jsondata["records"]["location"]; //回傳json陣列

    }

    // Update is called once per frame
    void Update()
    {
        JArray jsondata = getJson("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?&Authorization=CWB-22BE69E8-CB82-48AE-9D9C-D562475BE81F");

        foreach (JObject data in jsondata)
        {
            string loactionname = (string)data["locationName"]; //地名
            string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //天氣狀況
            string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //降雨機率
            string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //最低溫度
            string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //最高溫度
            if (loactionname == "臺北市")
            {
                //GetComponent<Text>().
                this.gameObject.GetComponent<Text>().text = weathdescrible;
            }
            Debug.Log(loactionname + " 天氣:" + weathdescrible + " 溫度:" + mintemperature + "°c-" + maxtemperature + "°c 降雨機率:" + pop + "%");
           // Console.WriteLine(loactionname + " 天氣:" + weathdescrible + " 溫度:" + mintemperature + "°c-" + maxtemperature + "°c 降雨機率:" + pop + "%");
        }
       // Console.ReadLine();
    }
    /*
    static public JArray getJson(string uri)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri); //request請求
        req.Timeout = 10000; //request逾時時間
        req.Method = "GET"; //request方式
        HttpWebResponse respone = (HttpWebResponse)req.GetResponse(); //接收respone
        StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8); //讀取respone資料
        string result = streamReader.ReadToEnd(); //讀取到最後一行
        respone.Close();
        streamReader.Close();
        JObject jsondata = JsonConvert.DeserializeObject<JObject>(result); //將資料轉為json物件
        return (JArray)jsondata["records"]["location"]; //回傳json陣列

    }*/

}
