using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Web;

namespace AcademyLockSmith.Data.Helpers
{
    public static class GeoCodeHelper
    {
        private static int _sleepinterval = 200;
        public static GeoResponse CallGeoWs(string address, string zipCode, string country)
        {
            string url = string.Format("https://maps.google.com/maps/api/geocode/json?address={0}&postal_code={1}&country={2}",
                 HttpUtility.UrlEncode(address), zipCode, country);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
            var res = (GeoResponse)serializer.ReadObject(request.GetResponse().GetResponseStream());
            return res;
        }
        public static GeoResponse CallWsCount(string address, string zipCode, string country, int badtries)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    // no throttling, go a little bit faster
                    _sleepinterval = _sleepinterval > 10000 ? 200 : Math.Max(_sleepinterval / 2, 50);

                    Thread.Sleep(_sleepinterval);

                    GeoResponse res = CallGeoWs(address, zipCode, country);

                    if (res == null || res.Status == "OVER_QUERY_LIMIT")
                    {
                        Console.WriteLine("OVER_QUERY_LIMIT ############################");
                        _sleepinterval = Math.Min(_sleepinterval + ++badtries * 500, 60000);
                    }
                    else
                    {
                        _sleepinterval = 200; // If success then reset sleepinterval
                        return res;
                    }
                }
            }
#pragma warning disable
            catch (Exception e)
            {
                //Logger.Logger.LogException(e.Message, e);
                return null;
            }

            //If no Lat Long found after 10 tries return null
            _sleepinterval = 200;
            return null;
        }

        //public static GeoResponse CallGeoWsN(string address, string zipCode, string country)
        //{
        //    string url = string.Format("https://maps.google.com/maps/api/geocode/json?address={0}&region=dk&sensor=false", HttpUtility.UrlEncode(address));
        //    var request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
        //    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
        //    var res = (GeoResponse)serializer.ReadObject(request.GetResponse().GetResponseStream());
        //    return res;
        //}

        //public static GeoResponse CallWsCountN(string address, string zipCode, string country, int badtries)
        //{
        //    try
        //    {
        //        for (int i = 0; i < 5; i++)
        //        {
        //            // no throttling, go a little bit faster
        //            _sleepinterval = _sleepinterval > 10000 ? 200 : Math.Max(_sleepinterval / 2, 50);

        //            Thread.Sleep(_sleepinterval);

        //            GeoResponse res = CallGeoWsN(address, zipCode, country);

        //            if (res == null || res.Status == "OVER_QUERY_LIMIT")
        //            {
        //                _sleepinterval = Math.Min(_sleepinterval + ++badtries * 500, 60000);
        //            }
        //            else
        //            {
        //                _sleepinterval = 200; // If success then reset sleepinterval
        //                return res;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Logger.LogException(e.Message, e);
        //        return null;
        //    }

        //    //If no Lat Long found after 10 tries return null
        //    _sleepinterval = 200;
        //    return null;
        //}

    }

    [DataContract]
    public class GeoResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "results")]
        public CResult[] Results { get; set; }

        [DataContract]
        public class CResult
        {
            [DataMember(Name = "geometry")]
            public CGeometry Geometry { get; set; }

            [DataContract]
            public class CGeometry
            {
                [DataMember(Name = "location")]
                public CLocation Location { get; set; }

                [DataContract]
                public class CLocation
                {
                    [DataMember(Name = "lat")]
                    public double Lat { get; set; }
                    [DataMember(Name = "lng")]
                    public double Lng { get; set; }
                }
            }
        }
    }
}
