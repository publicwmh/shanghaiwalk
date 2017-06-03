using System;
namespace shanghaiwalk.model
{
	public class Member
	{
		public virtual int Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual string Pwd { get; set; }
		public virtual DateTime CreateDateTime { get; set; }
		public virtual int IsLock { get; set; }
		public virtual int DevType { get; set; }
		public virtual string UA { get; set; }

	}
}
