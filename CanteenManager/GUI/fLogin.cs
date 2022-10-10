using CanteenManager.DAO;
using CanteenManager.DTO;
using System.Data;
using System.ServiceProcess;

namespace CanteenManager
{
    /// <summary>
    /// Form đăng nhập.
    /// </summary>
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        #region Methods

        /// <summary>
        /// Trả về tài khoản dựa theo tên đăng nhập và mật khẩu nhập vào.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Account Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        bool TestConnection()
        {
            this.Cursor = Cursors.AppStarting;

            lblTestConnection.Text = "Đang kết nối...";
            lblTestConnection.BackColor = Color.Gainsboro;

            string connectionStr = "Data Source = localdb.db; foreign keys=true";
            if (DataProvider.Instance is DataProvider) connectionStr = $@"Data Source=.\{txbServerName.Text};Initial Catalog={txbDatabase.Text};Integrated Security=True";
            
            if (DataProvider.Instance.TestConnection(connectionStr))
            {
                lblTestConnection.Text = "Kết nối thành công!";
                lblTestConnection.BackColor = Color.LightGreen;
                this.Cursor = Cursors.Default;
                return true;

            }
            else
            {
                lblTestConnection.Text = "Kết nối thất bại!";
                lblTestConnection.BackColor = Color.Red;
                this.Cursor = Cursors.Default;
                return false;
            }

            
        }

        #endregion

        #region Events

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbServerName.Text) || string.IsNullOrEmpty(txbDatabase.Text)) 
            {
                MessageBox.Show("Tên Server và Database không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!TestConnection()) return;

            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;

            Account loginAccount = Login(userName, passWord);


            if (loginAccount != null)
            {
                fTableManager tableManager = new fTableManager(loginAccount);
                //ẩn form login
                this.Hide();
                //hiện form quản lý
                tableManager.ShowDialog();
                //sau khi thoát form quản lý, form login mới hiện lại
                this.Show();
                //xoá thông tin nhập từ lần trước               
                txbPassWord.Text = "";
                txbUserName.Text = "";
                //đưa con trỏ quay lại ô UserName
                txbUserName.Focus();

            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void ckbShowPassWord_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShowPassWord.Checked == true)
            {
                txbPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txbPassWord.UseSystemPasswordChar = true;
            }
        }     

        private void lblAdvanceOption_Click(object sender, EventArgs e)
        {
            //Nếu đang dùng SQL Server thì mới bật chức năng này
            if (DataProvider.Instance is not DataProvider) 
	        {
		        MessageBox.Show("Chức năng này chỉ hỗ trợ SQL Server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
		        return;
	        }	

            //Co duỗi form khi click vào lblAdvanceOption
            if (plAdvanceOption.Visible == true)
            {
                plAdvanceOption.Visible = false;
                lblTestConnection.Location = new Point(0, 188);
            }
            else
            {
                plAdvanceOption.Visible = true;
                lblTestConnection.Location = new Point(0, 577);
            }
        }

        private void lblTestConnection_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        private void lblTestConnection_MouseDown(object sender, MouseEventArgs e)
        {
            //Chặn click đúp
            if (e.Clicks > 1)
            {
                return;
            }
            lblTestConnection.Text = "Đang kết nối...";
            lblTestConnection.BackColor = Color.Gainsboro;
        }

        private void btnSearchServerName_Click(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            data.Columns.Add("ServerName", typeof(string));
            data.Columns.Add("Status", typeof(string));

            //Cách 1: Tìm các service trên máy có tên bắt đầu là "MSSQL$"
            foreach (var item in ServiceController.GetServices().Where(sc => sc.ServiceName.StartsWith("MSSQL$")))
            {
                data.Rows.Add(item.ServiceName.Substring(6), item.Status);
            }

            ////Cách 2: Tìm trong registry
            //RegistryKey HKLM = Registry.LocalMachine;
            //var listInstance = (string[])HKLM.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server").GetValue("InstalledInstances", new[] { "**Không tìm thấy**" });
            //HKLM.Close();
            //foreach (var item in listInstance)
            //{
            //    data.Rows.Add(item, "???");
            //}

            dtgvShowServer.Columns.Clear();
            dtgvShowServer.DataSource = data;
            dtgvShowServer.Columns[0].HeaderText = "Tên Server";
            dtgvShowServer.Columns[1].HeaderText = "Trạng thái";
            dtgvShowServer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvShowServer.Columns.Add(new DataGridViewButtonColumn() { Text = "Show Databases", UseColumnTextForButtonValue = true });
            dtgvShowServer.AllowUserToAddRows = false;
            dtgvShowServer.RowHeadersVisible = false;
            dtgvShowServer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvShowServer.Rows[0].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dtgvShowServer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Nếu click vào nút Show Databases
            if ((sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                txbServerName.Text = dtgvShowServer.Rows[e.RowIndex].Cells[0].Value.ToString();

                string connectionStr = $@"Data Source=.\{txbServerName.Text};Initial Catalog=master;Integrated Security=True;Connect Timeout = 5";
                
                if (DataProvider.Instance.TestConnection(connectionStr))
                {
                    DataTable data = DataProvider.Instance.ExecuteQuery("select name as [Database] from sysdatabases");

                    dtgvShowServer.Columns.Clear();
                    dtgvShowServer.DataSource = data;
                    txbDatabase.Text = "master";
                }

            }
            //Nếu click vào cell chứa tên 1 database
            else if ((sender as DataGridView).Columns[e.ColumnIndex].Name == "Database" && e.RowIndex >= 0)
            {
                txbDatabase.Text = dtgvShowServer.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        #endregion


    }
}