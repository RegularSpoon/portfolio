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
    public partial class Form1 : Form
    {
        /// <summary>
        /// 子画面1(会社情報)
        /// </summary>
        private Form2 subForm1 = new Form2();

        /// <summary>
        /// 子画面２(収支情報)
        /// </summary>
        private Form3 subForm2;

        /// <summary>
        /// 子画面３（損益計算書）
        /// </summary>
        private Form4 subForm3 = new Form4();

        /// <summary>
        /// 子画４３（貸借対照表）
        /// </summary>
        private Form5 subForm4 = new Form5();
        public Form1()
        {
            InitializeComponent();
        }
       
        public void Form1_Load(object sender, EventArgs e)
        {
      
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// フォームの初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load_2(object sender, EventArgs e)
        { 
            //コンボボックスに会社分類をセット
            using (var db = new AccountInfoContext())
            {
                //会社分類のリスト
                var list = db.Company
                    .Select(item => new SubjectsDTO
                    {
                        Id = item.CompanyId,
                        Name = item.Name
                    })
                    .ToList();

                //先頭にブランク
                list.Insert(0, new SubjectsDTO
                {
                    Id = 0,
                    Name = string.Empty
                });

                this.comboBox2.DisplayMember = "Name";
                this.comboBox2.ValueMember = "Id";
                this.comboBox2.DataSource = list;
            }
        
           
            //コンボボックスに科目分類をセット
            using (var db = new AccountInfoContext())
            {
                //科目分類のリスト
                var list = db.Subjects
                    .Select(item => new SubjectsDTO
                    {
                        Id = item.SubjectId,
                        Code = item.SubjectCode,
                        Name = item.Name
                    })
                    .ToList();

                //先頭にブランク
                list.Insert(0, new SubjectsDTO
                {
                    Id = 0,
                    Code = string.Empty,
                    Name = string.Empty
                });

                this.comboBox1.DisplayMember = "Title";
                this.comboBox1.ValueMember = "Id";
                this.comboBox1.DataSource = list;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //コンボボックスに会社分類をセット
            using (var db = new AccountInfoContext())
            {
                //会社分類のリスト
                var list = db.Company
                    .Select(item => new SubjectsDTO
                    {
                        Id = item.CompanyId,
                        Name = item.Name
                    })
                    .ToList();

                //先頭にブランク
                list.Insert(0, new SubjectsDTO
                {
                    Id = 0,
                    Name = string.Empty
                });

                this.comboBox2.DisplayMember = "Name";
                this.comboBox2.ValueMember = "Id";
                this.comboBox2.DataSource = list;
            }
        }
        //検索内容を表示
        private void button2_Click(object sender, EventArgs e)
        {
            //コンボボックスの選択内容
            string cmbtxt1 = this.comboBox1.Text;
            string cmbtxt2 = this.comboBox2.Text;

            // 日付の取得
            int p = 0;
            int z = 0;
            string q = "yyyyMMdd";
            if (checkBox1.Checked == false)
            {
                //月別でDataGridViewに表示
                string x = dateTimePicker1.Value.ToString("yyyyMM00");
                int y = int.Parse(x);
                p = y + 1;　　//選択された日にちを１日（ついたち）にする
                z = y + 101;  //選択された日にちを次の月の１日（ついたち）にする


            }
            else
            {
                //年別でDataGridViewに表示
                string x = dateTimePicker1.Value.ToString("yyyy0000");
                int y = int.Parse(x);
                p = y + 101;　//選択された年の１月１日にする
                z = y + 10101;//選択された年の翌年の１月１日にする
            }

            DateTime day1 = DateTime.ParseExact(p.ToString(),q,null);
            DateTime day2 = DateTime.ParseExact(z.ToString(),q, null);

            using (var db = new AccountInfoContext())
            {
                //仕分け情報の取得
                var list = db.BOP
                    .OrderBy(item => item.Date)
                    .Where(item => (cmbtxt1 == string.Empty || item.Subjects.SubjectCode.ToString() + ":" + item.Subjects.Name == cmbtxt1) &&
                    (cmbtxt2 == string.Empty || item.Company.Name == cmbtxt2) &&
                    (item.Date >= day1 && item.Date < day2))
                    .Select(item => new AccountDTO
                    {
                        Id = item.BOPId,
                        Subject = item.Subjects.Name,
                        Date = item.Date,
                        Company = item.Company.Name,
                        Title = item.Title,
                        price = item.Price,
                        Notes = item.Notes
                    })
                    .ToList();
             
                //DataGridViewに紐づける            
                //this.dataGridView1.AutoGenerateColumns = true;
                this.dataGridView1.DataSource = list;
            }
        }
        ///<summary>
        ///子画面２を表示
        /// </summary>
        /// <param name="BOPId"></param>
        private void ShowSubForm2(int bopId)
        {
            if (subForm2 == null) 
            {
                subForm2 = new Form3();
            }

            //収支IDをセット
            subForm2.BOPId = bopId;
            //モーダルを表示する
            subForm2 .ShowDialog(this);
        }

        /// <summary>
        /// 追加ボタンのクリックで子画面２(収支情報)を表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //子画面２を表示し、追加時の収支IDは０
            this.ShowSubForm2 (0);
        }

        ///<summary>
        ///GridViewの行をクリックで子画面２を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //選択された収支IDを取得
            int id = (int)this.dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            //子画面２を表示
            this.ShowSubForm2(id);
        }
        //会計年度IDを渡し損益計算書を表示
        private void button4_Click(object sender, EventArgs e)
        {
            string year = dateTimePicker2.Value.ToString("yyyy0000");
            this.ShowSubForm3(year);
        }

        ///<summary>
        ///子画面3を表示
        /// </summary>
        /// <param name="StatementYear"></param>
        private void ShowSubForm3(string statementYear)
        {
            if (subForm3 == null)
            {
                subForm3 = new Form4();
            }

            //選択された年をセット
            subForm3.StatementYear = statementYear;
            //モーダルを表示する
            subForm3.ShowDialog(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                dateTimePicker1.CustomFormat = "yyyy年 MM月";
            }
            else
            {
                dateTimePicker1.CustomFormat = "yyyy年";
            }
        }

        //会計年度IDを渡し貸借対照表を表示
        private void button5_Click(object sender, EventArgs e)
        {
            string year = dateTimePicker2.Value.ToString("yyyy0000");
            this.ShowSubForm4(year);
        }
        ///<summary>
        ///子画面４を表示
        /// </summary>
        /// <param name="StatementYear"></param>
        private void ShowSubForm4(string statementYear)
        {
            if (subForm4 == null)
            {
                subForm4 = new Form5();
            }

            //選択された年をセット
            subForm4.StatementYear = statementYear;
            //モーダルを表示する
            subForm4.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            subForm1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

