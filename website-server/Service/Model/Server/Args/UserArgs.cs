using Model.Server.Args.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_user")]
    public class UserGetListRequest : PageRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_user")]
    public class UserGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_login_name")]
        public string LoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_user")]
    public class UserGetCountRequest : CountRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_user")]
    public class UserDeleteRequest : DeleteRequestBase
    {

    }
}
