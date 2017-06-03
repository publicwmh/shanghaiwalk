using System;
namespace shanghaiwalk.model
{
	public class BaiyeBookPage
	{
		public virtual int Id { get; set; }

		public virtual string Page { get; set; }
		public virtual System.Nullable<double> Lu1 { get; set; }
		public virtual System.Nullable<double> Lu2 { get; set; }
		public virtual System.Nullable<double> Rd1 { get; set; }
		public virtual System.Nullable<double> Rd2 { get; set; }
		public virtual System.Nullable<double> Ld1 { get; set; }
		public virtual System.Nullable<double> Ld2 { get; set; }
		public virtual System.Nullable<double> Ru1 { get; set; }
		public virtual System.Nullable<double> Ru2 { get; set; }
		public virtual System.Nullable<double> x1 { get; set; }
		public virtual System.Nullable<double> x2 { get; set; }
		public virtual System.Nullable<double> y1 { get; set; }
		public virtual System.Nullable<double> y2 { get; set; }
		public virtual string Note { get; set; }
	}
}
