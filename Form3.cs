using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SimpleLedgerApp
{
    public partial class Form3 : Form
    {
        /// <summary>
        ///  収支ID
        /// </summary>
        public int BOPId = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを '_desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet2.Company' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.companyTableAdapter.Fill(this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet2.Company);
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

                this.cmbSubject.DisplayMember = "Title";
                this.cmbSubject.ValueMember = "Id";
                this.cmbSubject.DataSource = list;
            }
            //ラベルの初期化
            this.lblId.Text = string.Empty;

            //GridViewからの表示の場合
            if(this.BOPId != 0)
            {
                //収支情報の表示
                this.SetBOP();
             
                this.btnAdd.Enabled = true;
                this.btnDel.Enabled = true;              
            }
            else
            {
                this.btnAdd.Enabled = true;
                this.btnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 親フォームからの収支情報を画面に表示
        /// </summary>
        private void SetBOP()
        {
            using (var db = new AccountInfoContext())
            {
                var bop = db.BOP
                    .Where(item => item.BOPId == this.BOPId)
                    .FirstOrDefault();
          
                if (bop != null)
                {
                    this.lblId.Text = this.BOPId.ToString();
                    this.cmbSubject.SelectedValue = bop.SubjectId;
                    this.dateTimePicker1.Value = bop.Date;
                    this.cmbCompany.SelectedValue = bop.CompanyId;
                    this.txtTitle.Text = bop.Title;
                    this.txtPrice.Text = bop.Price.ToString();
                    this.txtNotes.Text = bop.Notes;
                }
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 登録ボタンクリックで追加・更新処理を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool ret = false;

            // 入力チェック
            if (this.validate())
            {
                if (this.BOPId == 0)
                {
                    // 追加
                    ret = this.AddBOP();
                }
                else
                {
                    // 更新
                    ret = this.UpdateBOP();
                }

                if (ret)
                {
                    // 登録成功
                    MessageBox.Show("登録が完了しました。");
                    this.Close();
                }
                else
                {
                    // 登録失敗
                    MessageBox.Show("登録に失敗しました。");
                }
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            string msg = "登録に失敗しました。\n{0}";
            bool valid = true;

            if((int)this.cmbSubject.SelectedValue == 0)
            {
                //科目名必須
                msg = string.Format(msg, "科目名は必須です。");
            }

            if (this.dateTimePicker1.Value == null)
            {
                //日付必須
                msg = string.Format(msg, "日付は必須です。");
            }

            if ((int)this.cmbCompany.SelectedValue == 0)
            {
                //会社名必須
                msg = string.Format(msg, "会社名は必須です。");
            }

            if (string.IsNullOrEmpty(this.txtPrice.Text))
            {
                //金額必須
                msg = string.Format(msg, "金額は必須です。");

            }
            if (!valid)
            {
                // エラーメッセージを表示する
                MessageBox.Show(msg);
            }

            return valid;
        }

        ///<summary>
        ///収支情報の追加処理
        /// </summary>
        private bool AddBOP()
        {
            try
            {
                using(var db = new AccountInfoContext())
                {
                    //フォームの入力値を取得
                    var bop = GetFormValue();
                    //収支情報を追加
                    db.BOP.Add(bop);
                    //保存
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        ///<summary>
        ///収支情報の更新処理
        /// </summary>
        private bool UpdateBOP()
        {
            try
            {
                using (var db = new AccountInfoContext())
                {
                    //更新対象の収支情報を取得
                    var bop1 = db.BOP
                        .Where(item => item.BOPId == this.BOPId)
                        .FirstOrDefault();

                   //取得できなかった場合
                    if (bop1 == null)
                    {
                        return false;
                    }

                    //フォームの値を取得
                    var bop2 = this.GetFormValue();

                    //プロパティの更新
                    bop1.SubjectId = bop2.SubjectId;
                    bop1.Date = bop2.Date;
                    bop1.CompanyId = bop2.CompanyId;
                    bop1.Title = bop2.Title;
                    bop1.Price = bop2.Price;
                    bop1.Notes = bop2.Notes;

                    //保存
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
　　　　　　
            return true;
        }
        ///<summary>
        ///フォームの入力値を取得
        /// </summary>
        /// <returns></returns>
        private BOP GetFormValue()
        {
            int price = int.Parse(this.txtPrice.Text);

            var bop = new BOP
            {
                BOPId = this.BOPId,
                SubjectId = (int)this.cmbSubject.SelectedValue,
                Date = this.dateTimePicker1.Value,
                CompanyId = (int)this.cmbCompany.SelectedValue,
                Title = this.txtTitle.Text,
                Price = price,
                Notes = this.txtNotes.Text,
            };

            return bop;
        }

        /// <summary>
        /// 削除ボタンのクリックで、BOPIdに該当する収支情報を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.BOPId != 0)
            {
                if (this.DeleteBOP())
                {
                    MessageBox.Show("収支情報を削除しました。");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("収支情報の削除に失敗しました。");
                }
            }
        }

        ///<summary>
        ///収支情報の削除処理
        /// </summary>
        private bool DeleteBOP()
        {
            try
            {
                using (var db = new AccountInfoContext())
                {
                    //削除対象の収支情報を取得
                    var bop = db.BOP
                        .Where(item => item.BOPId == this.BOPId)
                        .FirstOrDefault();
                    //取得できなかった場合
                    if(bop == null)
                    {
                        return false;
                    }

                    //収支情報の削除
                    db.BOP.Remove((BOP)bop);
                    //保存
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
    }
}
