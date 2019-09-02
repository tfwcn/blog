using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_group")]
    public class GroupModel
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
        [Column("c_name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_company_id")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; }
        #region 外联字段
        /// <summary>
        /// 
        /// </summary>
        [Display]
        [Column("c_company_name")]
        public string CompanyName { get; set; }
        #endregion
    }
}
