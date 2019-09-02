using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Server.Args.Base
{
    public class PageRequestBase
    {
        [Display]
        public int? Page { get; set; }
        [Display]
        public int? Row { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("c_state")]
        public int? State { get; set; } = 0;
    }
}
