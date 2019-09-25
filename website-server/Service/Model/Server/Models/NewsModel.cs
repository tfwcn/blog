using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    [Table("t_news")]
    public class NewsModel
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
        [Column("c_title")]
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_content")]
        public string Content { get; set; }
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
        [Column("c_state")]
        public int? State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_link")]
        public string Link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_remark")]
        public string Remark { get; set; }
    }
}
