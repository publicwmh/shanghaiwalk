using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using shanghaiwalk.third;

namespace shanghaiwalk.Baiye
{
	public class BaiYeMapService
	{
		//public static IDictionary<string, IList<PointF>> Paths;

		public BaiYeMapService()
		{
		//	if (Paths == null)
			{
		//		Paths = new Dictionary<string, IList<PointF>>();
				//获取所有记录
				//              ISession session = NHibernateHelper.OpenSession();
				//              var list = session.QueryOver<baiye>().List<baiye>();
				//              foreach (BaiyeBookPage item in list)
				//              {
				//                  if (!string.IsNullOrEmpty(item.Page) && item.Ld1.HasValue)
				//                  {
				//                      IList<PointF> p = new List<PointF>();
				//
				//                      var p1 = new PointF((float)item.Lu1.Value, (float)item.Lu2.Value);
				//                      var p2 = new PointF((float)item.Ru1.Value, (float)item.Ru2.Value);
				//                      var p3 = new PointF((float)item.Rd1.Value, (float)item.Rd2.Value);
				//                      var p4 = new PointF((float)item.Ld1.Value, (float)item.Ld2.Value);
				//                      p.Add(p1);
				//                      p.Add(p2);
				//                      p.Add(p3);
				//                      p.Add(p4);
				//                      Paths.Add(new KeyValuePair<string, IList<PointF>>(item.Page, p));
				//
				//                  }
				//
				//              }
			}
		}

		///// <summary>
		///// 剪裁图片
		///// </summary>
		///// <param name="b">The blue component.</param>
		///// <param name="StartX">Start x.</param>
		///// <param name="StartY">Start y.</param>
		///// <param name="iWidth">I width.</param>
		///// <param name="iHeight">I height.</param>
		//public Bitmap Cut(System.Drawing.Image b, int StartX, int StartY, int iWidth, int iHeight)
		//{
		//	if (b == null)
		//	{
		//		return null;
		//	}
		//	int w = b.Width;
		//	int h = b.Height;
		//	if (StartX >= w || StartY >= h)
		//	{
		//		return null;
		//	}
		//	if (StartX + iWidth > w)
		//	{
		//		iWidth = w - StartX;
		//	}
		//	if (StartY + iHeight > h)
		//	{
		//		iHeight = h - StartY;
		//	}
		//	try
		//	{
		//		Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
		//		Graphics g = Graphics.FromImage(bmpOut);
		//		g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
		//		g.Dispose();
		//		return bmpOut;
		//	}
		//	catch
		//	{
		//		return null;
		//	}
		//}
		/// <summary>
		/// 加水印
		/// </summary>
		/// <param name="picture">Picture.</param>
		/// <param name="_watermarkText">Watermark text.</param>
		/// <param name="_watermarkPosition">Watermark position.</param>
		/// <param name="_width">Width.</param>
		/// <param name="_height">Height.</param>
		//private void addWatermarkText(Graphics picture, string _watermarkText, string _watermarkPosition, int _width, int _height)
		//{
		//	int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
		//	Font crFont = null;
		//	SizeF crSize = new SizeF();
		//	for (int i = 0; i < 7; i++)
		//	{
		//		crFont = new Font("arial", sizes[i], FontStyle.Bold);
		//		crSize = picture.MeasureString(_watermarkText, crFont);
		//		if ((ushort)crSize.Width < (ushort)_width)
		//			break;
		//	}
		//	float xpos = 0;
		//	float ypos = 0;
		//	switch (_watermarkPosition)
		//	{
		//		case "WM_TOP_LEFT":
		//			xpos = ((float)_width * (float).01) + (crSize.Width / 2);
		//			ypos = (float)_height * (float).01;
		//			break;
		//		case "WM_TOP_RIGHT":
		//			xpos = ((float)_width * (float).99) - (crSize.Width / 2);
		//			ypos = (float)_height * (float).01;
		//			break;
		//		case "WM_BOTTOM_RIGHT":
		//			xpos = ((float)_width * (float).99) - (crSize.Width / 2);
		//			ypos = ((float)_height * (float).99) - crSize.Height;
		//			break;
		//		case "WM_BOTTOM_LEFT":
		//			xpos = ((float)_width * (float).01) + (crSize.Width / 2);
		//			ypos = ((float)_height * (float).99) - crSize.Height;
		//			break;
		//	}
		//	StringFormat StrFormat = new StringFormat();
		//	StrFormat.Alignment = StringAlignment.Center;
		//	SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
		//	picture.DrawString(_watermarkText, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);
		//	SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
		//	picture.DrawString(_watermarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);

		//	semiTransBrush2.Dispose();
		//	semiTransBrush.Dispose();

		//}
		public class JudeItem
		{
			public int xx { get; set; }
			public double judev { get; set; }
			public double d1 { get; set; }
			public double d2 { get; set; }
			public double d { get; set; }
			public string key { get; set; }
		}
		/// <summary>
		/// Calculates the distance.
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		//private double CalcDistance(PointF from, PointF to)
		//{

		//	double rad = 6371; //Earth radius in Km

		//	//Convert to radians

		//	double p1X = from.X / 180 * Math.PI;

		//	double p1Y = from.Y / 180 * Math.PI;

		//	double p2X = to.X / 180 * Math.PI;

		//	double p2Y = to.Y / 180 * Math.PI;



		//	return Math.Acos(Math.Sin(p1Y) * Math.Sin(p2Y) +

		//		Math.Cos(p1Y) * Math.Cos(p2Y) * Math.Cos(p2X - p1X)) * rad;

		//}

		//public string FindMap(double x, double y, out int xxv)
		//{
		//	var t = new List<BaiyeBookPage>();
		//	//var t=  session.QueryOver<baiye>().Where(p => p.x1 > x && p.x2 < x && p.y1 < y && p.y2 > y).List();
		//	IList<JudeItem> ts = new List<JudeItem>();
		//	foreach (var item in t)
		//	{
		//		JudeItem it = new JudeItem()
		//		{
		//			key = item.Page
		//		};
		//		//center1
		//		var point = new PointF((float)x, (float)y);
		//		var Point1 = new PointF((float)(item.x1 + (item.x1 - item.x2) / 2), (float)(item.y1 + (item.y2 - item.y1) / 4));
		//		var Point2 = new PointF((float)(item.x1 + (item.x1 - item.x2) / 2), (float)(item.y1 + (item.y2 - item.y1) / 4 * 3));
		//		it.d1 = CalcDistance(point, Point1);
		//		it.d2 = CalcDistance(point, Point2);
		//		it.d = Math.Min(it.d1, it.d2);
		//		//log.Debug("found map" + it.key + " two side center" + it.d1 + " " + it.d2);
		//		ts.Add(it);
		//	}
		//	if (ts.Count != 0)
		//	{
		//		//cacl
		//		var j = ts.OrderBy(p => p.d).FirstOrDefault();
		//		if (j.d1 == j.d)
		//		{
		//			xxv = 1;

		//		}
		//		else
		//		{
		//			xxv = 2;

		//		}
		//		//log.Debug("map return" + j.key + " " + j.d + " use side" + xxv + "  source xy " + x + "-" + y);
		//		return j.key;
		//	}
		//	else
		//	{
		//		//log.Error("cannot found map" + x + "-" + y);
		//		xxv = 0;
		//		return "";

		//	}
		//}
		/// <summary>
		/// 高斯投影中所选用的参考椭球
		/// </summary>
		public enum GaussSphere
		{
			Beijing54,
			Xian80,
			WGS84,
		}
		public static double DistanceOfTwoPoints(double lng1, double lat1, double lng2, double lat2, GaussSphere gs)
		{
			double radLat1 = Rad(lat1);
			double radLat2 = Rad(lat2);
			double a = radLat1 - radLat2;
			double b = Rad(lng1) - Rad(lng2);
			double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
				Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
			s = s * (gs == GaussSphere.WGS84 ? 6378137.0 : (gs == GaussSphere.Xian80 ? 6378140.0 : 6378245.0));
			s = Math.Round(s * 10000) / 10000;
			return s;
		}

		private static double Rad(double d)
		{
			return d * Math.PI / 180.0;
		}
		//private bool PointInFences(PointF pnt1, PointF[] fencePnts)
		//{
		//	int j = 0, cnt = 0;
		//	for (int i = 0; i < fencePnts.Length; i++)
		//	{
		//		j = (i == fencePnts.Length - 1) ? 0 : j + 1;
		//		if ((fencePnts[i].Y != fencePnts[j].Y) && (((pnt1.Y >= fencePnts[i].Y) && (pnt1.Y < fencePnts[j].Y)) || ((pnt1.Y >= fencePnts[j].Y) && (pnt1.Y < fencePnts[i].Y))) && (pnt1.X < (fencePnts[j].X - fencePnts[i].X) * (pnt1.Y - fencePnts[i].Y) / (fencePnts[j].Y - fencePnts[i].Y) + fencePnts[i].X)) cnt++;
		//	}
		//	return (cnt % 2 > 0) ? true : false;
		//}

		static LocationHelper helper = new LocationHelper();

		//public BaiYeMapItem GetMapInfo(string adr, bool usehpic)
		//{
		//	//query 是否属于专有名词
		//	//var getname = helper.FindName(adr);
		//	var getname = "";
		//	if (!string.IsNullOrEmpty(getname))
		//	{
		//		adr = getname;
		//	}
		//	if (!adr.Contains("上海"))
		//	{
		//		adr = "上海市" + adr;
		//	}
		//	//地址转化
		//	var gps = helper.GetGPS(adr);
		//	//og.Debug("地址转化" + adr + " " + gps.lat + " " + gps.lng);
		//	return GetMapInfo(adr, gps.lng, gps.lat, usehpic);

		//}

		//public BaiYeMapItem GetMapInfo(string adr, float x, float y, bool usehpic)
		//{
		//	int xx = 0;
		//	var mapname = FindMap(y, x, out xx);

		//	if (string.IsNullOrEmpty(mapname))
		//	{
		//		return null;
		//	}

		//	string ext = ".jpg";
		//	long picq = 10l;
		//	if (mapname.Contains("上册"))
		//	{
		//		ext = ".jpeg";
		//		picq = 50l;
		//	}
		//	if (usehpic)
		//	{
		//		picq = 80l;
		//	}
		//	var image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/Map/" + mapname + ext));
		//	Bitmap bmp = null;
		//	if (xx == 1)
		//	{
		//		bmp = Cut(image, 0, 0, image.Width / 2, image.Height);

		//	}
		//	else if (xx == 2)
		//	{
		//		bmp = Cut(image, image.Width / 2, 0, image.Width / 2, image.Height);
		//	}
		//	else if (xx == 3)
		//	{
		//		bmp = Cut(image, image.Width / 2, 0, image.Width / 2, image.Height);
		//	}
		//	else if (xx == 4)
		//	{
		//		bmp = Cut(image, 0, 0, image.Width / 2, image.Height);
		//	}
		//	else
		//	{
		//		return null;
		//	}
		//	string fn = "tmppic/" + Guid.NewGuid().ToString() + ext;
		//	string filen = HttpContext.Current.Server.MapPath("~/" + fn);
		//	ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
		//	ImageCodecInfo ici = null;
		//	foreach (ImageCodecInfo codec in codecs)
		//	{
		//		if (codec.MimeType == "image/jpeg")
		//			ici = codec;
		//	}
		//	using (MemoryStream memory = new MemoryStream())
		//	{
		//		using (FileStream fs = new FileStream(filen, FileMode.Create, FileAccess.ReadWrite))
		//		{
		//			var myEncoderParameters = new EncoderParameters(1);

		//			var myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, picq);
		//			myEncoderParameters.Param[0] = myEncoderParameter;
		//			bmp.Save(memory, ici, myEncoderParameters);
		//			byte[] bytes = memory.ToArray();
		//			fs.Write(bytes, 0, bytes.Length);
		//		}
		//	}
		//	string url = "";
		//	//string url = ConfigurationManager.AppSettings["webdir"] + fn;
		//	BaiYeMapItem item = new BaiYeMapItem();
		//	item.Name = adr;
		//	item.TmpPicUrl = url;
		//	item.GpsLng = x;
		//	item.GpsLat = y;
		//	return item;

		//}
	}
}
