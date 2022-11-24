using CanteenManager.DAO;
using Models;
using System.ComponentModel;

namespace CanteenManager
{
    /// <summary>
    /// Form thông tin tài khoản.
    /// </summary>
    public partial class fAccountProfile : Form
    {
        public fAccountProfile(Account loginAccount)
        {
            InitializeComponent();

            LoginAccount = loginAccount;

            //Đăng ký sự kiện Validating
            txbDisplayName.Validating += TxbDisplayName_Validating;
            txbNewPassWord.Validating += TxbNewPassWord_Validating;
            txbConfirmPassWord.Validating += TxbConfirmPassWord_Validating;

            ShowAccount(LoginAccount);
        }
        
        private Account loginAccount;
        public Account LoginAccount
        {
            get => loginAccount;
            set => loginAccount = value;
        }

        #region Methods

        /// <summary>
        /// Truyền thông tin của tài khoản lên các TextBox của form.
        /// </summary>
        /// <param name="account"></param>
        void ShowAccount(Account account)
        {
            txbUserName.Text = account.Username;
            txbDisplayName.Text = account.DisplayName;
        }

        /// <summary>
        /// Update DisplayName và PassWord.
        /// </summary>
        void UpdateAccount()
        {
            //Validate các TextBox
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }

            if (AccountDAO.Instance.Login(LoginAccount.Username, txbPassWord.Text) != null) 
            {
                var result = AccountDAO.Instance.Update(LoginAccount.Username, txbDisplayName.Text, txbNewPassWord.Text);
                if (result.Item1 || result.Item2)
                {
                    //Load lại tài khoản sau khi update
                    //Nếu không nhập mật khẩu mới thì lấy mật khẩu cũ truyền vào tham số, ngược lại thì truyền mật khẩu mới
                    LoginAccount = AccountDAO.Instance.Login(LoginAccount.Username, string.IsNullOrEmpty(txbNewPassWord.Text) ? txbPassWord.Text : txbNewPassWord.Text);

                    //this.Refresh();

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txbPassWord.Clear();
                    txbNewPassWord.Clear();
                    txbConfirmPassWord.Clear();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
            }
            else
            {
                MessageBox.Show("Sai mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txbPassWord.Clear();
                txbNewPassWord.Clear();
                txbConfirmPassWord.Clear();
                
                
            }
        }

        #endregion

        #region Events

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private void ckbShowPassWord_CheckedChanged(object sender, EventArgs e)
        {
            txbPassWord.UseSystemPasswordChar = !txbPassWord.UseSystemPasswordChar;
            txbNewPassWord.UseSystemPasswordChar = !txbNewPassWord.UseSystemPasswordChar;
            txbConfirmPassWord.UseSystemPasswordChar = !txbConfirmPassWord.UseSystemPasswordChar;
        }

        private void TxbConfirmPassWord_Validating(object? sender, CancelEventArgs e)
        {
            if (txbNewPassWord.Text != txbConfirmPassWord.Text)
            {
                e.Cancel = true;

                errorProvider.SetError(txbConfirmPassWord, "Mật khẩu nhập lại không trùng khớp.");

                //Đặt lại focus để click được vào chỗ khác
                txbConfirmPassWord.Select();
            }
            else
            {
                e.Cancel = false;

                errorProvider.SetError(txbConfirmPassWord, "");
            }
        }

        private void TxbNewPassWord_Validating(object? sender, CancelEventArgs e)
        {
            if ((txbNewPassWord.Text != "") && (!txbNewPassWord.Text.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))))   
            {
                e.Cancel = true;

                errorProvider.SetError(txbNewPassWord, "Mật khẩu phải là các ký tự 0 - 9, a - z, A - Z.");

                txbNewPassWord.Select();
            }
            else
            {
                e.Cancel = false;

                errorProvider.SetError(txbNewPassWord, "");
            }
        }

        private void TxbDisplayName_Validating(object? sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbDisplayName.Text))
            {
                e.Cancel = true;

                errorProvider.SetError(txbDisplayName, "Tên đăng nhập không được để trống.");

                txbDisplayName.Select();
            }
            else
            {
                e.Cancel = false;

                errorProvider.SetError(txbDisplayName, "");
            }
        }

        #endregion


    }
    
}
