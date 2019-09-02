using Model.Server.Args.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询总记录
    /// </summary>
    [Table("t_person")]
    public class PersonGetListRequest : PageRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_group_id")]
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        [NotMapped]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_card")]
        public string Card { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_permission")]
        public int? Permission { get; set; }
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_person")]
    public class PersonGetModelRequest : GetRequestBase
    {

    }

    /// <summary>
    /// 查询数量
    /// </summary>
    [Table("t_person")]
    public class PersonGetCountRequest : CountRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_group_id")]
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        [NotMapped]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_card")]
        public string Card { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_permission")]
        public string Permission { get; set; }
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
    }

    /// <summary>
    /// 删除记录
    /// </summary>
    [Table("t_person")]
    public class PersonDeleteRequest : DeleteRequestBase
    {

    }
    /// <summary>
    /// 
    /// </summary>
    [Table("t_person")]
    public class PersonGetInListRequest : PageRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_group_id")]
        public string GroupId { get; set; }
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Table("t_person")]
    public class PersonUpdatePermissionRequest
    {
        /// <summary>
        /// 人员Id列表(更新条件)
        /// </summary>
        [NotMapped]
        public List<string> Ids { get; set; }
        /// <summary>
        /// 0.白名单 1.黑名单(更新字段)
        /// </summary>
        [Column("c_permission")]
        [NotMapped]
        public int? Permission { get; set; }
        /// <summary>
        /// 公司Id(更新条件)
        /// </summary>
        [Column("c_company_id")]
        [NotMapped]
        public string CompanyId { get; set; }
        /// <summary>
        /// 班组Id(更新条件)
        /// </summary>
        [Column("c_group_id")]
        [NotMapped]
        public string GroupId { get; set; }
    }
}
