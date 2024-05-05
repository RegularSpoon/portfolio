using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLedgerApp
{
    internal class SubjectsDTO
    {
        ///<summary>
        ///SubjectId
        ///</summary>
        public int Id { get; set; }

        ///<summary>
        ///科目コード
        ///</summary>

        public string Code { get; set; }

        ///<summary>
        ///科目名称
        ///</summary>
        public string Name { get; set; }

        /// <summary>
        /// コンボボックスの内容
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return string.Empty;
                }
                else
                {
                    return string.Format("{0}:{1}", Code, Name);
                }

            }
        }
    }
}

