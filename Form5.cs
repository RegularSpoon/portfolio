using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLedgerApp
{
    public partial class Form5 : Form
    {
        /// <summary>
        ///  損益計算書の年
        /// </summary>
        public string StatementYear = "";

        // 日付の取得
        int p = 0;
        int z = 0;
        string q = "yyyyMMdd";

        public Form5()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            int y = int.Parse(StatementYear);
            p = y + 101;　//選択された年の１月１日にする
            z = y + 10101;//選択された年の翌年の１月１日にする


            DateTime day1 = DateTime.ParseExact(p.ToString(), q, null);
            DateTime day2 = DateTime.ParseExact(z.ToString(), q, null);
            
            using (var db = new AccountInfoContext())
            {
                int current;
                int earningSum;
                int currentSum;
                int taxSum;
                int tax;
                int equipmentSum;
                int depreciationSum;
                //選択された年を含む以前にBOPに存在する科目IDリストを取得
                var list1= db.BOP
                    .Where(item => item.Date < day2)
                    .Select(item => item.SubjectId)
                    .ToList();
                //選択された年のBOPに存在する科目IDリストを取得
                var list2 = db.BOP
                    .Where(item => item.Date >= day1 && item.Date < day2)
                    .Select(item => item.SubjectId)
                    .ToList();


                //選択された年の現金総額を取得（入金額）
                if (list2.Contains(2))
                {
                     current = db.BOP
                    .Where(item => (item.Date >= day1 && item.Date < day2) &&
                    (item.SubjectId == 2))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                     current = 0;
                };

                //選択された年に残っている売掛金の総額を取得　（選択された年を含む前の売上から入金額と源泉徴収額を引いたもの）

                //すべての売上
                if (list1.Contains(1))
                {
                     earningSum = db.BOP
                    .Where(item => (item.Date < day2) &&
                    (item.SubjectId == 1))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                     earningSum = 0;
                }
                //すべての入金
                if (list1.Contains(2))
                {
                     currentSum = db.BOP
                    .Where(item => (item.Date < day2) &&
                    (item.SubjectId == 2))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                     currentSum = 0;
                }
                //すべての源泉徴収
                if (list1.Contains(3))
                {
                     taxSum = db.BOP
                    .Where(item => (item.Date < day2) &&
                    (item.SubjectId == 3))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                     taxSum = 0;
                }
                

                //選択された年の売掛金
                int receivable = earningSum - currentSum - taxSum;

                //選択された年の源泉徴収の総額を取得
                if (list2.Contains(3))
                {
                    tax = db.BOP
                   .Where(item => (item.Date < day2) &&
                   (item.SubjectId == 3))
                   .Select(item => item.Price)
                   .Sum();
                }
                else
                {
                    tax = 0;
                }

                //選択された年の備品（パソコン等）の総額を取得　（選択された年を含む以前の資産から減価償却費を引いたもの）

                //すべての備品
                if (list1.Contains(4))
                {
                    equipmentSum = db.BOP
                    .Where(item => (item.Date < day2) &&
                    (item.SubjectId == 4))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                    equipmentSum = 0;
                }
                //すべての減価償却費
                if (list1.Contains(15))
                {
                    depreciationSum = db.BOP
                    .Where(item => (item.Date < day2) &&
                    (item.SubjectId == 15))
                    .Select(item => item.Price)
                    .Sum();
                }
                else
                {
                    depreciationSum = 0;
                }
                
                //選択された年の備品
                int equipment = equipmentSum - depreciationSum;

                //すべての合計額
                int allSum = current + receivable + equipment + tax;
                

                //DataGridView1に表示するデータの登録

                DataTable statement = new DataTable();
                statement.Columns.Add("科目");
                statement.Columns.Add("金額");

                //現金額の登録
                statement.Rows.Add("現金", string.Format("{0:c}", current));

                //売掛金額の登録
                statement.Rows.Add("売掛金", string.Format("{0:c}", receivable));

                //備品額の登録
                statement.Rows.Add("備品", string.Format("{0:c}", equipment));

                //事業主貸（源泉徴収額）の登録
                statement.Rows.Add("事業主貸", string.Format("{0:c}", tax));

                statement.Rows.Add("", "");

                statement.Rows.Add("合計", string.Format("{0:c}", allSum));

                for (int i = 0; i < 5; i++)
                {
                    statement.Rows.Add("", "");
                }

                statement.Rows.Add("青色申告特別控除前の所得金額", string.Format("{0:c}", allSum));

                statement.Rows.Add("", "");

                statement.Rows.Add("合計", string.Format("{0:c}", allSum));

                this.dataGridView1.DataSource = statement;
            }
        }
    }
}
