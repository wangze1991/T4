namespace T4.Sample.Domain
{
	using System;
	public class T_Module
    {
    	public string Id { get; set; }
		public int Sort { get; set; }
		public string ParentCode { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public bool IsView { get; set; }
		public bool IsDisabled { get; set; }
		public int CreateUserId { get; set; }
		public DateTime CreateTime { get; set; }
		public int UpdateUserId { get; set; }
		public DateTime UpdateTime { get; set; }
		public string MenuCode { get; set; }
		public string Icon { get; set; }
	
    }
}

