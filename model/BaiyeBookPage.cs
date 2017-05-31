using System;
namespace shanghaiwalk.model
{
	public class BaiyeBookPage
	{
        public virtual int id { get; set; }

		public virtual string page { get; set; }
        public virtual System.Nullable<double> lu1 { get; set; }
        public virtual System.Nullable<double> lu2 { get; set; }
        public virtual System.Nullable<double> rd1 { get; set; }
        public virtual System.Nullable<double> rd2 { get; set; }
		public virtual System.Nullable<double> ld1 { get; set; }
        public virtual System.Nullable<double> ld2 { get; set; }
        public virtual System.Nullable<double> ru1 { get; set; }
        public virtual System.Nullable<double> ru2 { get; set; }
		public virtual System.Nullable<double> x1 { get; set; }
		public virtual System.Nullable<double> x2 { get; set; }
		public virtual System.Nullable<double> y1 { get; set; }
		public virtual System.Nullable<double> y2 { get; set; }
        public virtual string note { get; set; }
	}
}
