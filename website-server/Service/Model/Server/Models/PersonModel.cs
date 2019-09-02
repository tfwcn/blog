using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_person")]
    public class PersonModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("c_id")]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_update_time")]
        public DateTime? UpdateTime { get; set; }
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
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_face_path")]
        public string FacePath { get; set; }
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
        /// <summary>
        /// 0.白名单 1.黑名单
        /// </summary>
        [Column("c_permission")]
        public int? Permission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_card")]
        public string Card { get; set; }
        #region 外联字段
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_company_name")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_group_name")]
        public string GroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_in_time")]
        public DateTime? InTime { get; set; }
        /// <summary>
        /// 进场日志ID
        /// </summary>
        [Display]
        [Column("c_log_id")]
        public string LogId { get; set; }
        #endregion
    }
}
