using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ToolsLib
{
    public class WeatherTools
    {
        const int TIME_OUT = 10000;
        public class AddressItem
        {
            public string name { get; set; }
            public string id { get; set; }
        }
        public class Address
        {
            public AddressItem country { get; set; }
            public AddressItem province { get; set; }
            public AddressItem city { get; set; }
            public AddressItem county { get; set; }
            public Address()
            {
                country = new AddressItem();
                province = new AddressItem();
                city = new AddressItem();
                county = new AddressItem();
            }
        }
        static public Address GetIpLookUp()
        {
            Address address = new Address();
            string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=c#";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = TIME_OUT;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.Default);
                string result = readStream.ReadToEnd();
                var resultList = result.Split('\t');
                address.country.name = resultList[3];
                address.province.name = resultList[4];
                address.city.name = resultList[5];
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
            //HttpRequestOpt.SendRequestByGetMethod(url, System.Text.Encoding.UTF8);
            return address;
        }
        static public List<AddressItem> ParseItemListJson(string json)
        {
            List<AddressItem> itemList = new List<AddressItem>();
            if(json == null)
            {
                return itemList;
            }
            json = json.Trim();
            json = json.Substring(1, json.Length - 2);
            var pairStringList = json.Split(',');
            foreach (string pairString in pairStringList)
            {
                var pair = pairString.Trim().Split(':');
                AddressItem addressItem = new AddressItem();
                addressItem.id = pair[0].Substring(1, pair[0].Length - 2);
                addressItem.name = pair[1].Substring(1, pair[1].Length - 2);
                itemList.Add(addressItem);
            }
            return itemList;
        }
        static public string GetJsonByUrl(string url)
        {
            string result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = TIME_OUT;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                result = readStream.ReadToEnd();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
            //HttpRequestOpt.SendRequestByGetMethod(url, System.Text.Encoding.UTF8);
            return result;
        }
        static public Address GetIdForPCC(Address address)
        {
            try
            {
                string json = GetProvinceListJson(address);
                List<AddressItem> itemList = ParseItemListJson(json);
                bool flag = FetchIdForProvince(address, itemList);
                json = GetCityListJson(address);
                itemList = ParseItemListJson(json);
                flag = FetchIdForCity(address, itemList);
                json = GetCountyListJson(address);
                itemList = ParseItemListJson(json);
                flag = FetchIdForCounty(address, itemList);
                return address;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
        }

        static public string GetProvinceListJson()
        {
            string url = "http://www.weather.com.cn/data/citydata/china.html";
            return GetJsonByUrl(url);
        }
        static public string GetProvinceListJson(Address address)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return null;
            }
            return GetProvinceListJson();
        }
        static public bool FetchIdForProvince(Address address, List<AddressItem> itemList)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return false;
            }
            AddressItem addressItem = itemList.Find(e => e.name == address.province.name);
            if (addressItem != null && addressItem.name == address.province.name)
            {
                address.province.id = addressItem.id;
                return true;
            }
            else
            {
                return false;
            }
        }

        static public string GetCityListJson(string provinceId)
        {
            if(provinceId == null)
            {
                return null;
            }
            string url = string.Format("http://www.weather.com.cn/data/citydata/district/{0}.html", provinceId);
            return GetJsonByUrl(url);
        }
        static public string GetCityListJson(Address address)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return null;
            }
            return GetCityListJson(address.province.id);
        }
        static public bool FetchIdForCity(Address address, List<AddressItem> itemList)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return false;
            }
            AddressItem addressItem = itemList.Find(e => e.name == address.city.name);
            if (addressItem != null && addressItem.name == address.city.name)
            {
                address.city.id = addressItem.id;
                return true;
            }
            else
            {
                address.city.id = itemList[0].id;
                address.city.name = itemList[0].name;
                return false;
            }
        }

        static public string GetCountyListJson(string provinceId, string cityId)
        {
            if (provinceId == null || cityId == null)
            {
                return null;
            }
            string url = string.Format("http://www.weather.com.cn/data/citydata/city/{0}{1}.html", provinceId, cityId);
            return GetJsonByUrl(url);
        }
        static public string GetCountyListJson(Address address)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return null;
            }
            return GetCountyListJson(address.province.id, address.city.id);
        }
        static public bool FetchIdForCounty(Address address, List<AddressItem> itemList)
        {
            if (address == null || address.country == null || "中国" != address.country.name)
            {
                return false;
            }
            AddressItem addressItem = itemList.Find(e => e.name == address.county.name);
            if (addressItem != null && addressItem.name == address.county.name)
            {
                address.county.id = addressItem.id;
                return true;
            }
            else
            {
                address.county.id = itemList[0].id;
                address.county.name = itemList[0].name;
                return false;
            }
        }

        static public string GetFullWeather(string provinceId, string cityId, string countyId)
        {
            if (provinceId == null || cityId == null || countyId == null)
            {
                return null;
            }
            if(cityId == "00")
            {
                string temp = cityId;
                cityId = countyId;
                countyId = temp;
            }
            string url = string.Format("http://www.weather.com.cn/data/cityinfo/{0}{1}{2}.html", provinceId, cityId, countyId);
            return GetJsonByUrl(url);
        }
        static public string GetFullWeather(Address address)
        {
            return GetFullWeather(address.province.id, address.city.id, address.county.id);
        }
        static public string SpideWeather()
        {
            string defaultoutput = "NetworkError";
            try
            {
                string url = @"http://www.weather.com.cn/weather1d/101020100.shtml";
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                var div = document.GetElementbyId("hidden_title");
                return div.GetAttributeValue("value", defaultoutput);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return defaultoutput;
            }
        }
    }
}