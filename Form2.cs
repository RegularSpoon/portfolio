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
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを '_desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.Company' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.companyTableAdapter.Fill(this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.Company);

        }

        ///<summary>
        ///キャンセルボタンのクリックでForm2を閉じる
        ///</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void btnCancelCompany_Click_1(object sender, EventArgs e)
         { 
            this.Hide();         
    　　 }

        //コンボボックスに値が選択されているとき削除ボタンが使用可能
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnDelCompany.Enabled = true;
        }

        ///<summary>
        ///削除ボタンのクリックでコンボボックスで選択された会社を削除
        ///</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelCompany_Click_1(object sender, EventArgs e)
        {
            int id = (int)this.comboBox1.SelectedValue;

            if (id != 0)
            {
                if (this.DeleteCompany())
                {
                    MessageBox.Show("会社を削除しました");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("会社の削除に失敗しました");
                }
            }
        }

        /// <summary>
        /// 薬品を削除する
        /// </summary>
        /// <returns></returns>
        private bool DeleteCompany()
        {
            try
            {
                using (var db = new AccountInfoContext())
                {
                    //コンボボックスから会社IDを取得
                    int id = (int)this.comboBox1.SelectedValue;
                    // 対象の薬品を取得
                    var company = db.Company
                        .Where(item => item.CompanyId == id)
                        .FirstOrDefault();

                    if (company == null)
                    {
                        return false;
                    }

                    // 会社を削除する
                    db.Company.Remove(company);

                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 登録ボタンクリックで追加処理を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCompany_Click_1(object sender, EventArgs e)
        {
            bool ret = false;

            if (this.validate())
            {
                ret = this.AddCompany();

                if (ret)
                {
                    // 登録成功
                    MessageBox.Show("登録が完了しました。");
                    this.Hide();
                }
                else
                {
                    // 登録失敗
                    MessageBox.Show("登録に失敗しました。");
                }
            }
        }

        ///<summary>
        ///入力チェック
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {

            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("登録に失敗しました。会社名を入力していません。");
            }

            return true;
        }

        ///<summary>
        ///会社名の追加処理
        /// </summary>
        private bool AddCompany()
        {
            try
            {
                using (var db = new AccountInfoContext())
                {
                    var company = new Company
                    {
                        Name = this.textBox1.Text
                    };
                    //会社を追加する
                    db.Company.Add(company);
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
                return false;
            }
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを '_desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.Company' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.companyTableAdapter.Fill(this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.Company);
        }
    }

}
