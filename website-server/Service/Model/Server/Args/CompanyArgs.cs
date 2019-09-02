using Model.Server.Args.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_company")]
    public class CompanyGetListRequest : PageRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_company")]
    public class CompanyGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_company")]
    public class CompanyGetCountRequest : CountRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_company")]
    public class CompanyDeleteRequest : DeleteRequestBase
    {

    }
}
