using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using shanghaiwalk.model;
using shanghaiwalk.option;
using shanghaiwalk.third;
using Aliyun.OSS;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace shanghaiwalk.Baiye
{
    public class BaiYeMapService
    {
      
        public OssClient client;
        static LocationHelper helper;
        private OssOption _ossoption;
        private BaiYeContext _baiyecontext;
        private readonly ILogger _logger;
        
        public BaiYeMapService(OssOption ossoption, 
            BaiduApiOption baiduapiOption,
            BaiYeContext baiyecontent,
            ILogger logger)
        {
            helper = new LocationHelper(baiduapiOption);
            _ossoption = ossoption;
            _baiyecontext = baiyecontent;
            _logger = logger;
            client = new OssClient(ossoption.Endpoint, ossoption.AccessKeyId, ossoption.AccessKeySecret);
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="b">The blue component.</param>
        /// <param name="StartX">Start x.</param>
        /// <param name="StartY">Start y.</param>
        /// <param name="iWidth">I width.</param>
        /// <param name="iHeight">I height.</param>
        public Bitmap Cut(System.Drawing.Image b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 加水印
        /// </summary>
        /// <param name="picture">Picture.</param>
        /// <param name="_watermarkText">Watermark text.</param>
        /// <param name="_watermarkPosition">Watermark position.</param>
        /// <param name="_width">Width.</param>
        /// <param name="_height">Height.</param>
        private void addWatermarkText(Graphics picture, string _watermarkText, string _watermarkPosition, int _width, int _height)
        {
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);
                if ((ushort)crSize.Width < (ushort)_width)
                    break;
            }
            float xpos = 0;
            float ypos = 0;
            switch (_watermarkPosition)
            {
                case "WM_TOP_LEFT":
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case "WM_TOP_RIGHT":
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case "WM_BOTTOM_RIGHT":
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
                case "WM_BOTTOM_LEFT":
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
            }
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(_watermarkText, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(_watermarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();

        }
  
        /// <summary>
        /// Calculates the distance.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        private double CalcDistance(PointF from, PointF to)
        {
            double rad = 6371; //Earth radius in Km
            //Convert to radians
            double p1X = from.X / 180 * Math.PI;
            double p1Y = from.Y / 180 * Math.PI;
            double p2X = to.X / 180 * Math.PI;
            double p2Y = to.Y / 180 * Math.PI;

            return Math.Acos(Math.Sin(p1Y) * Math.Sin(p2Y) +

                Math.Cos(p1Y) * Math.Cos(p2Y) * Math.Cos(p2X - p1X)) * rad;

        }

        public string FindMap(double x, double y, out int xxv)
        {
           
            var t=  _baiyecontext.BaiYeBookPages.Where(p => p.x1 > x && p.x2 < x && p.y1 < y && p.y2 > y).ToList();
            IList<JudeItem> ts = new List<JudeItem>();
            foreach (var item in t)
            {
                JudeItem it = new JudeItem()
                {
                    key = item.page
                };
                //center1
                var point = new PointF((float)x, (float)y);
                var Point1 = new PointF((float)(item.x1 + (item.x1 - item.x2) / 2), (float)(item.y1 + (item.y2 - item.y1) / 4));
                var Point2 = new PointF((float)(item.x1 + (item.x1 - item.x2) / 2), (float)(item.y1 + (item.y2 - item.y1) / 4 * 3));
                it.d1 = CalcDistance(point, Point1);
                it.d2 = CalcDistance(point, Point2);
                it.d = Math.Min(it.d1, it.d2);
                //log.Debug("found map" + it.key + " two side center" + it.d1 + " " + it.d2);
                ts.Add(it);
            }
            if (ts.Count != 0)
            {
                //cacl
                var j = ts.OrderBy(p => p.d).FirstOrDefault();
                if (j.d1 == j.d)
                {
                    xxv = 1;

                }
                else
                {
                    xxv = 2;

                }
                //log.Debug("map return" + j.key + " " + j.d + " use side" + xxv + "  source xy " + x + "-" + y);
                return j.key;
            }
            else
            {
                //log.Error("cannot found map" + x + "-" + y);
                xxv = 0;
                return "";

            }
        }
  

        public async Task<BaiYeMapItem> GetMapInfo(string adr, bool usehpic)
        {          
            //处理输入的文本
            adr = processinput(adr);
            //地址转化
            var gps =await helper.GeoLoc2GPS(adr);
            _logger.LogInformation($"地址转化:{gps.lat}-{gps.lng}");
            return GetMapInfo(adr, gps.lng, gps.lat, usehpic);

        }

        private static string processinput(string adr)
        {
            // 简繁转化：
             adr = OpenCC.Converter.Trad2Simple(adr);
            return adr;
        }

        public BaiYeMapItem GetMapInfo(string adr, float x, float y, bool usehpic)
        {
            int xx = 0;
            var mapname = FindMap(y, x, out xx);
            if (string.IsNullOrEmpty(mapname))
            {
                return null;
            }
            _logger.LogInformation($"找到地图:{mapname}");
            string ext = ".jpg";
            long picq = 10L;
            if (mapname.Contains("上册"))
            {
                ext = ".jpeg";
                picq = 50L;
            }
            if (usehpic)
            {
                picq = 80L;
            }

            //download file
            var fileobject = client.GetObject(_ossoption.BucketName, "MapData/" + mapname + ext);
            var image = Image.FromStream(fileobject.Content);
            Bitmap bmp = null;
            if (xx == 1)
            {
                bmp = Cut(image, 0, 0, image.Width / 2, image.Height);
            }
            else if (xx == 2)
            {
                bmp = Cut(image, image.Width / 2, 0, image.Width / 2, image.Height);
            }
            else if (xx == 3)
            {
                bmp = Cut(image, image.Width / 2, 0, image.Width / 2, image.Height);
            }
            else if (xx == 4)
            {
                bmp = Cut(image, 0, 0, image.Width / 2, image.Height);
            }
            else
            {
                return null;
            }
            _logger.LogInformation($"找到地图部分:{xx}(1左2右3左4右)");
            var key = (mapname + ':' + xx.ToString()).GetHashCode().ToString();
            var url= GenPicFromOSS(key, ext, picq, bmp);
            BaiYeMapItem item = new BaiYeMapItem();
            item.Name = adr;
            item.TmpPicUrl = url;
            item.GpsLng = x;
            item.GpsLat = y;
            _logger.LogInformation($"地图图片:{url}");
            return item;

        }

        private string GenPicFromOSS( string key,string ext, long picq, Bitmap bmp)
        {
            // 查询是否已经生成
            var list = client.ListObjects(_ossoption.BucketName, "RAND/" + key+"/");
            if (list != null && list.ObjectSummaries != null&&list.ObjectSummaries.Count()!=0)
            {
                return _ossoption.ViewPoint+"/" +list.ObjectSummaries.FirstOrDefault().Key;
            }
            // 没有 则上传OSS 暂存
            string ossfilepath = "RAND/" +key+"/"+Guid.NewGuid().ToString() + ext;

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                    ici = codec;
            }
            using (MemoryStream memory = new MemoryStream())
            {
                var myEncoderParameters = new EncoderParameters(1);
                var myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, picq);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(memory, ici, myEncoderParameters);
                memory.Seek(0, SeekOrigin.Begin);                
                var put = client.PutObject(_ossoption.BucketName, ossfilepath, memory);
                return  _ossoption.ViewPoint + "/" + ossfilepath;
                
            }
        }
    }
}
