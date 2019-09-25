using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args.Base
{
    public class CountRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; } = 0;
    }
}
