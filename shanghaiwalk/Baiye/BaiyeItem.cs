using System;
namespace shanghaiwalk.Baiye
{
	public class BaiYeMapItem
	{
		public string Name { get; set; }
		public string TmpPicUrl { get; set; }
		public double GpsLat { get; set; }
		public double GpsLng { get; set; }
		public int Good { get; set; }
		public int Bad { get; set; }
		public string Content { get; set; }
		public long POIKey { get; set; }
	}
}
