using Model.Server.Args.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_group")]
    public class GroupGetListRequest : PageRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_group")]
    public class GroupGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_group")]
    public class GroupGetCountRequest : CountRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_group")]
    public class GroupDeleteRequest : DeleteRequestBase
    {

    }
}
