using Model.Server.Args.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args
{
    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_gate")]
    public class GateGetListRequest : PageRequestBase
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_gate")]
    public class GateGetModelRequest : GetRequestBase
    {
    
    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_gate")]
    public class GateGetCountRequest
    {

    }

    /// <summary>
    /// 查询单条记录
    /// </summary>
    [Table("t_gate")]
    public class GateDeleteRequest : DeleteRequestBase
    {

    }
}
