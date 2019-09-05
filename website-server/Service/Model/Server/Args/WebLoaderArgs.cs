using Model.Server.Args.Base;
using Model.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    #region Request
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_web_loader")]
    public class WebLoaderGetListRequest : PageRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_web_loader")]
    public class WebLoaderGetModelRequest : GetRequestBase
    {
    
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_web_loader")]
    public class WebLoaderGetCountRequest : CountRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_web_loader")]
    public class WebLoaderDeleteRequest : DeleteRequestBase
    {

    }
    #endregion

    #region Response
    /// <summary>
    /// 新增返回
    /// </summary>
    public class WebLoaderAddResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 更新返回
    /// </summary>
    public class WebLoaderUpdateResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 删除返回
    /// </summary>
    public class WebLoaderDeleteResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 分页查询返回
    /// </summary>
    public class WebLoaderGetListResponse : PageResponseBase<WebLoaderModel>
    {

    }
    #endregion
}
