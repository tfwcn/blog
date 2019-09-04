using Model.Server.Args.Base;
using Model.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    #region Request
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_news")]
    public class NewsGetListRequest : PageRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_news")]
    public class NewsGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_link")]
        public string Link { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_news")]
    public class NewsGetCountRequest : CountRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_news")]
    public class NewsDeleteRequest : DeleteRequestBase
    {

    }
    #endregion

    #region Response
    /// <summary>
    /// 新增返回
    /// </summary>
    public class NewsAddResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 更新返回
    /// </summary>
    public class NewsUpdateResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 删除返回
    /// </summary>
    public class NewsDeleteResponse : NumResponseBase
    {

    }
    /// <summary>
    /// 分页查询返回
    /// </summary>
    public class NewsGetListResponse : PageResponseBase<NewsModel>
    {

    }
    #endregion
}
