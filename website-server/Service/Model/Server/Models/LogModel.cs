using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_log")]
    public class LogModel
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
        [Column("c_face_path")]
        public string FacePath { get; set; }
        /// <summary>
        /// 类型：0.刷脸成功、1.刷脸失败、2.远程开门(来宾)、3.远程开门(不计算人数)、4.远程开门(计算人数)
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; }
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_person_type")]
        public int? PersonType { get; set; }
        #region 外联字段
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_door_name")]
        public string DoorName { get; set; }
        /// <summary>
        /// 类型：0.入闸、1.出闸
        /// </summary>
        [Display]
        [Column("c_door_type")]
        public int? DoorType { get; set; }
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
        [Column("c_person_name")]
        public string PersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_person_card")]
        public string PersonCard{ get; set; }
        #endregion
    }
}
