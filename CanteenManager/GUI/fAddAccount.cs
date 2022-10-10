using CanteenManager.DAO;

namespace CanteenManager
{
    /// <summary>
    /// Hiện cửa sổ thêm tài khoản.
    /// </summary>
    public partial class fAddAccount : Form
    {
        public fAddAccount()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txbUserName.Text == "" || (!txbUserName.Text.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))) 
            {
                MessageBox.Show("Tên tài khoản phải là các ký tự 0 - 9, a - z, A - Z.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txbUserName.Select();
            }
            else
            {
                if (AccountDAO.Instance.InsertAccount(txbUserName.Text))
                    MessageBox.Show($"Tài khoản [{txbUserName.Text}] đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Thêm tài khoản thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                Close();
            }
        }
    }
}
