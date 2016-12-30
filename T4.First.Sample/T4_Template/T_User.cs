namespace T4.Sample.Domain
{
	using System;
	public class T_User
    {
    	public int Id { get; set; }
		public string UserName { get; set; }
		public string Pwd { get; set; }
		public string Remark { get; set; }
		public bool IsOpen { get; set; }
		public int LoginTime { get; set; }
		public DateTime LastLoginTime { get; set; }
		public int CreateUserId { get; set; }
		public DateTime CreateTime { get; set; }
		public int UpdateUser { get; set; }
		public DateTime UpdateTime { get; set; }
		public string Sex { get; set; }
		public string Mobile { get; set; }
		public string QQ { get; set; }
	
    }
}

