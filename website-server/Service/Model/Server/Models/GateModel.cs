using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_gate")]
    public class GateModel
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
        [Column("c_ip")]
        public string Ip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_port")]
        public int? Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; }
    }
}
