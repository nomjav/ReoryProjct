using System;
using System.Data.Spatial;

namespace AcademyLockSmith.Data.Helpers
{
    public static class DistanceAlgorithm
    {
        /// <summary>
        /// This class cannot be instantiated.
        /// </summary>

        private const double PIx = 3.141592653589793;
        private const double Radius = 6378.16;

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="x">Degrees</param>
        /// <returns>The equivalent in radians</returns>
        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        /// <summary>
        /// Calculate the distance between two places.
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
			double _eQuatorialEarthRadius = 6378.1370D;
			double _d2r = (Math.PI / 180D);

			double dlong = (lon2 - lon1) * _d2r;
			double dlat = (lat2 - lat1) * _d2r;
			double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
			double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
			double d = _eQuatorialEarthRadius * c;
			return d * 0.621371 ; // Convert to Miles



			//var jobLocation  = DbGeography.PointFromText(string.Format("POINT({0} {1})", lon1, lat1), 4326);
			//var contractorLocation = DbGeography.PointFromText(string.Format("POINT({0} {1})", lon2, lat2), 4326);

			//return MetersToMiles(jobLocation.Distance(contractorLocation)); 


			//var sCoord = new GeoCoordinate(sLatitude, sLongitude);
			//var eCoord = new GeoCoordinate(eLatitude, eLongitude);
			//return sCoord.GetDistanceTo(eCoord);
        }

        public static double MetersToMiles(double? meters)
        {
            if (meters == null)
                return 0F;

            return meters.Value * 0.000621371192;
        }

        public static Double ToRadian(this Double number)
        {
            return (number * Math.PI / 180);
        }
    }
}


