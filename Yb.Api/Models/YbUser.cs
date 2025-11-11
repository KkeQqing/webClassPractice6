using System;

namespace Yb.Api.Models
{
    public class YbUser
    {
        public string Id { get; set; }  // Primary Key
        public string Account { get; set; }  // Login Account
        public string Password { get; set; }  // Login Password
        public string UserNM { get; set; }  // User Name
        public string UserCD { get; set; }  // User Code
        public string Pinyin { get; set; }  // 拼音
        public int Sex { get; set; } // Gender
        public string MobileNo { get; set; }    // Mobile Number
        public string Email { get; set; }   // Email Address
        public string DepartmentCD { get; set; }    // Department Code
        public string DepartmentNM { get; set; }    // Department Name
        public string Photo { get; set; }   // Photo URL
        public int DataAuthority { get; set; }  // 数据权限
        public DateTime? LastLoginTime { get; set; } // Last Login Time
        public int IsActive { get; set; }   // Is Active
        public string RoleID { get; set; }  // roleID
        public string RoleName { get; set; }    // roleName
        public string CreateUserNM { get; set; } // Created By
        public string CreateUserCD { get; set; } // Created By Code
        public DateTime? CreateTime { get; set; }   // Creation Time
        public string ModifyUserCD { get; set; }    
        public DateTime? ModifyTime { get; set; }
        public int CheckStatus { get; set; }
        public string CheckUserNM { get; set; }
        public string CheckUserCD { get; set; }
        public DateTime? CheckTime { get; set; }
        public string CheckMemo { get; set; }
    }
}