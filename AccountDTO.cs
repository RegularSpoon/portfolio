using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLedgerApp
{
    internal class AccountDTO
    {
        public int Id { get; set; }

        ///<summary>
        ///科目名
        /// </summary>
        [DisplayName("科目")]
        public string Subject { get; set; }

        ///<summary>
        ///日付
        /// </summary>
       [DisplayName("日付")]
        public DateTime Date {  get; set; }

        ///<summary>
        ///会社名
        /// </summary>
        [DisplayName("会社名")]
        public string Company { get; set; }

        ///<summary>
        ///件名
        /// </summary>
        [DisplayName("件名")]
        public string Title { get; set; }


        public int price;
        
        ///<summary>
        ///金額
        /// </summary>
        [DisplayName("金額")]
        public string Price
        {
            get
            {
                return string.Format("{0:c}", price);
            }
            set 
            { 
            }
        }

        ///<summary>
        ///備考欄
        /// </summary>
        [DisplayName("備考欄")]
        public string Notes { get; set; }
    }
}
