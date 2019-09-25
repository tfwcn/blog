using System.Collections.Generic;

namespace Model.Server.Args.Base
{
    public class PageResponseBase<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> DataList { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }
    }
}
