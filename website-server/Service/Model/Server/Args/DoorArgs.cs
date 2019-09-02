using Model.Server.Args.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询总记录
    /// </summary>
    [Table("t_door")]
    public class DoorGetListRequest : PageRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_door")]
    public class DoorGetModelRequest : GetRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_ip")]
        public string Ip { get; set; }
        /// <summary>
        /// 类型：0.入闸、1.出闸
        /// </summary>
        [Column("c_type")]
        public int? Type { get; set; }
    }

    /// <summary>
    /// 查询记录数量
    /// </summary>
    [Table("t_door")]
    public class DoorGetCountRequest : CountRequestBase
    {

    }

    /// <summary>
    /// 删除记录
    /// </summary>
    [Table("t_door")]
    public class DoorDeleteRequest : DeleteRequestBase
    {

    }
}
