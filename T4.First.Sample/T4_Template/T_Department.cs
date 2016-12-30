namespace T4.Sample.Domain
{
	using System;
	public class T_Department
    {
    	public decimal Id { get; set; }
		public decimal ParentId { get; set; }
		public int Sort { get; set; }
		public string Name { get; set; }
		public string Remark { get; set; }
		public int CreateUserId { get; set; }
		public DateTime CreateTime { get; set; }
		public int UpdateUserId { get; set; }
		public DateTime UpdateTime { get; set; }
		public string Icon { get; set; }
	
    }
}

