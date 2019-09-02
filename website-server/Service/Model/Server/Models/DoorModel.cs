using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_door")]
    public class DoorModel
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
        /// 门禁服务地址
        /// </summary>
        [Column("c_ip")]
        public string Ip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
        /// <summary>
        /// 类型：0.入闸、1.出闸
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_threshold")]
        public decimal? Threshold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        [Column("c_server_url")]
        public string ServerUrl { get; set; }
        ///// <summary>
        ///// 暂时屏蔽闸机方案
        ///// </summary>
        //[Column("c_gate_id")]
        //public string GateId { get; set; }
        ///// <summary>
        ///// 暂时屏蔽闸机方案
        ///// </summary>
        //[Display]
        //[Column("c_gate_name")]
        //public string GateName { get; set; }
    }
}
