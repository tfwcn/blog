using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Models
{
    public class PersonNumModel
    {
        /// <summary>
        /// 工种：0.来宾、1.作业人员、2.管理人员
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_num")]
        public int? Num { get; set; }
    }
}
