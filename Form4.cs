using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLedgerApp
{
    public partial class Form4 : Form
    {
        /// <summary>
        ///  損益計算書の年
        /// </summary>
        public string StatementYear = "";

        // 日付の取得
        int p = 0;
        int z = 0;
        string q = "yyyyMMdd";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            int y = int.Parse(StatementYear);
            p = y + 101;　//選択された年の１月１日にする
            z = y + 10101;//選択された年の翌年の１月１日にする


            DateTime day1 = DateTime.ParseExact(p.ToString(), q, null);
            DateTime day2 = DateTime.ParseExact(z.ToString(), q, null);

            using (var db = new AccountInfoContext())
            {
                //表示する科目の数
                int count = db.Subjects
                    .Select(item => item.SubjectId)
                    .Count();
                //科目ごとの合計
                List<int> sum = new List<int>(count);
                //表示する科目ID
                List<int> subjectId = new List<int>(count);
                //科目のうちの費用名だけ
                List<string> subjectName = new List<string>();
                //BPOに仕分けが存在する科目のID
                List<int> bopId = new List<int>();

                  

                //リストに値を格納
                subjectId = db.Subjects
                    .Select(item => item.SubjectId)
                    .ToList();

                subjectName = db.Subjects
                    .Select(item => item.Name)
                    .ToList();

                bopId = db.BOP
                    .Where(item => item.Date >= day1 && item.Date < day2)
                    .Select(item => item.SubjectId)
                    .ToList();

                 for (int i = 0; i < count; i ++)
                 {
                    if (bopId.Contains(subjectId[i])) 
                    {
                        int id = subjectId[i];

                        sum.Add(db.BOP
                        .Where(item => (item.SubjectId == id) && (item.Date >= day1 && item.Date < day2))
                        .Select(item => item.Price)
                        .Sum());
                    }
                    else
                    {
                        sum.Add(0);
                    }
                 }

                //DataGridView1に表示するデータの登録

                DataTable statement = new DataTable ();

                //売り上げの登録
                statement.Columns.Add("科目");
                statement.Columns.Add("金額");

                statement.Rows.Add ("売上", string.Format("{0:c}", sum[0]));
                statement.Rows.Add("売上原価", string.Format("{0:c}", 0));
                statement.Rows.Add("差引金額", string.Format("{0:c}", sum[0]));
               
                statement.Rows.Add("", "");

                //費用項目の登録
                for (int i = 4; i < count; i++)
                {
                    statement.Rows.Add(subjectName[i], string.Format("{0:c}", sum[i]));
                }

                statement.Rows.Add("", "");

                //費用科目の合計
                int[] costSum = new int[count];
                for(int i = 4; i < count ; i++)
                {
                    costSum[i-4] = sum[i];                 
                }
                
                statement.Rows.Add("費用合計", string.Format("{0:c}", costSum.Sum()));
                statement.Rows.Add("", "");

                //引当金、準備金等
                statement.Rows.Add("貸倒引当金繰戻", string.Format("{0:c}", 0));
                statement.Rows.Add("", "");
                statement.Rows.Add("合計", string.Format("{0:c}", 0));
                statement.Rows.Add("", "");
                statement.Rows.Add("専従者給与", string.Format("{0:c}", 0));
                statement.Rows.Add("貸倒引当金繰入", string.Format("{0:c}", 0));
                statement.Rows.Add("", "");
                statement.Rows.Add("合計", string.Format("{0:c}", 0));
                statement.Rows.Add("", "");

                //青色申告特別控除前の所得金額
                int income = sum[0] - costSum.Sum();
                if (income < 0)
                {
                    statement.Rows.Add("青色申告特別控除前の所得金額","-" + string.Format("{0:c}", costSum.Sum() - sum[0]));
                }
                else
                {
                    statement.Rows.Add("青色申告特別控除前の所得金額", string.Format("{0:c}",sum[0] - costSum.Sum()));
                }
              

                this.dataGridView1.DataSource = statement;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
