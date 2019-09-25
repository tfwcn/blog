using Model.Server.Args.Base;
using Model.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    #region Request
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

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_user")]
    public class UserCheckRequest : DeleteRequestBase
    {
        public string Token { get; set; }
    }
    #endregion

    #region Response
    /// <summary>
    /// 新增返回
    /// </summary>
    public class UserAddResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 更新返回
    /// </summary>
    public class UserUpdateResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 删除返回
    /// </summary>
    public class UserDeleteResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 分页查询返回
    /// </summary>
    public class UserGetListResponse : PageResponseBase<UserModel>
    {

    }
    #endregion
}
