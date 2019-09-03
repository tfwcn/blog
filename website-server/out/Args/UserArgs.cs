using Model.Server.Args.Base;
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
    #endregion
}
