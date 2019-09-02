using Model.Server.Args.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询总记录
    /// </summary>
    [Table("t_log")]
    public class LogGetListRequest : PageRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_person_id")]
        public string PersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_door_id")]
        public string DoorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string PersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string PersonCard { get; set; }
        /// <summary>
        /// 类型：0.入闸、1.出闸
        /// </summary>
        [NotMapped]
        public int? DoorType { get; set; }
        /// <summary>
        /// 类型：0.刷脸成功、1.刷脸失败、2.远程开门(来宾)、3.远程开门(不计算人数)、4.远程开门(计算人数)
        /// </summary>
        [NotMapped]
        public string Types { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public bool? IsDel { get; set; }
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_log")]
    public class LogGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_person_id")]
        public string PersonId { get; set; }
    }

    /// <summary>
    /// 查询记录数量
    /// </summary>
    [Table("t_log")]
    public class LogGetCountRequest : CountRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_person_id")]
        public string PersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_door_id")]
        public string DoorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string PersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string PersonCard { get; set; }
        /// <summary>
        /// 类型：0.入闸、1.出闸
        /// </summary>
        [NotMapped]
        public int? DoorType { get; set; }
        /// <summary>
        /// 类型：0.刷脸成功、1.刷脸失败、2.远程开门(来宾)、3.远程开门(不计算人数)、4.远程开门(计算人数)
        /// </summary>
        [NotMapped]
        public string Types { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public bool? IsDel { get; set; }
    }

    /// <summary>
    /// 删除单条记录
    /// </summary>
    [Table("t_log")]
    public class LogDeleteRequest : DeleteRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_log")]
    public class LogDeleteInRequest : DeleteRequestBase
    {
    }
}
