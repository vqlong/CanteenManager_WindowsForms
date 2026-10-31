using System.Data;
using CanteenManager.DAO;
using Microsoft.Reporting.WinForms;
using Models;

namespace CanteenManager
{
    /// <summary>
    /// Form quản lý của Admin.
    /// </summary>
    public partial class fAdmin : Form
    {
        public fAdmin(Account admin)
        {
            InitializeComponent();

            LoginAdmin = admin;

            LoadData();

            LoadBinding();
             
        }

        #region Fields
        
        private Account loginAdmin;
        public Account LoginAdmin
        {
            get => loginAdmin;
            set => loginAdmin = value;
        }

        BindingSource billBindingSource = new BindingSource();

        /// <summary>
        /// Chứa data cho dtgvFood.
        /// </summary>
        BindingSource foodBindingSource = new BindingSource();

        /// <summary>
        /// Chứa data cho dtgvCategory.
        /// </summary>
        BindingSource categoryBindingSource = new BindingSource();

        /// <summary>
        /// Chứa data cho dtgvTable.
        /// </summary>
        BindingSource tableBindingSource = new BindingSource();

        /// <summary>
        /// Chứa data cho dtgvAccount.
        /// </summary>
        BindingSource accountBindingSource = new BindingSource();

        /// <summary>
        /// Xảy ra khi dữ liệu Food thay đổi.
        /// </summary>
        event EventHandler foodChange;

        /// <summary>
        /// Xảy ra khi dữ liệu Food thay đổi.
        /// </summary>
        public event EventHandler FoodChange
        {
            add { foodChange += value; }
            remove { foodChange -= value; }
        }

        /// <summary>
        /// Xảy ra khi dữ liệu Category thay đổi.
        /// </summary>
        event EventHandler categoryChange;

        /// <summary>
        /// Xảy ra khi dữ liệu Category thay đổi.
        /// </summary>
        public event EventHandler CategoryChange
        {
            add { categoryChange += value; }
            remove { categoryChange -= value; }
        }

        /// <summary>
        /// Xảy ra khi dữ liệu Table thay đổi.
        /// </summary>
        event EventHandler tableChange;

        /// <summary>
        /// Xảy ra khi dữ liệu Table thay đổi.
        /// </summary>
        public event EventHandler TableChange
        {
            add { tableChange += value; }
            remove { tableChange -= value; }
        }

        /// <summary>
        /// Xảy ra khi dữ liệu của 1 Admin đang đăng nhập thay đổi.
        /// </summary>
        event EventHandler adminChange;

        /// <summary>
        /// Xảy ra khi dữ liệu của 1 Admin đang đăng nhập thay đổi.
        /// </summary>
        public event EventHandler AdminChange
        {
            add { adminChange += value; }
            remove { adminChange -= value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load dữ liệu lên các DataGridView.
        /// </summary>
        void LoadData()
        {
            dtgvBill.DataSource = billBindingSource;

            //Tạo 1 danh sách về tình trạng sử dụng để làm data cho các ComboBox
            var listState = new List<Tuple<string, UsingState>>()
            {
                new Tuple<string,UsingState>( "Đang được phục vụ", UsingState.Serving ),
                new Tuple<string,UsingState>( "Đã dừng phục vụ", UsingState.StopServing )
            };

            //Tab thức ăn
            //Khởi tạo các thuộc tính cho dtgvFood, load data cho cbFoodStatus...
            dtgvFood.DataSource = foodBindingSource;
            dtgvFood.AllowUserToAddRows = false;
            dtgvFood.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgvFood.AllowUserToResizeRows = false;
            dtgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvFood.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cbFoodStatus.DataSource = listState;
            cbFoodStatus.DisplayMember = "item1";
            cbFoodStatus.ValueMember = "item2";

            LoadFood();

            dtgvFood.Columns[0].HeaderText = "ID";
            dtgvFood.Columns[1].HeaderText = "Tên món";
            dtgvFood.Columns[2].HeaderText = "Danh mục";
            dtgvFood.Columns[3].HeaderText = "Giá";
            dtgvFood.Columns[4].HeaderText = "Trạng thái";
            dtgvFood.Columns[5].Visible = false;
            dtgvFood.Columns[6].Visible = false;

            dtgvFood.Columns[0].FillWeight = 40;
            dtgvFood.Columns[1].FillWeight = 160;

            //Tab danh mục
            dtgvCategory.DataSource = categoryBindingSource;
            dtgvCategory.AllowUserToAddRows = false;
            dtgvCategory.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgvCategory.AllowUserToResizeRows = false;

            cbCategoryStatus.DataSource = listState;
            cbCategoryStatus.DisplayMember = "item1";
            cbCategoryStatus.ValueMember = "item2";

            LoadCategory();

            dtgvCategory.Columns[0].HeaderText = "ID";
            dtgvCategory.Columns[1].HeaderText = "Tên danh mục";
            dtgvCategory.Columns[2].HeaderText = "Trạng thái";
            dtgvCategory.Columns[3].Visible = false;

            //Tab tài khoản
            dtgvAccount.DataSource = accountBindingSource;
            dtgvAccount.AllowUserToAddRows = false;
            dtgvAccount.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgvAccount.AllowUserToResizeRows = false;

            cbAccountType.DataSource = new List<Tuple<string, AccountType>>()
            {
                new Tuple<string,AccountType>( "Quản lý", AccountType.Admin ),
                new Tuple<string,AccountType>( "Nhân viên", AccountType.Staff )
            };
            cbAccountType.DisplayMember = "item1";
            cbAccountType.ValueMember = "item2";

            LoadAccount();

            dtgvAccount.Columns[0].HeaderText = "Tên đăng nhập";
            dtgvAccount.Columns[1].HeaderText = "Tên hiển thị";
            dtgvAccount.Columns[3].HeaderText = "Loại tài khoản";
            //Ẩn cột mật khẩu
            dtgvAccount.Columns[2].Visible = false; 

            //Tab bàn ăn
            dtgvTable.DataSource = tableBindingSource;
            dtgvTable.AllowUserToAddRows = false;
            dtgvTable.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgvTable.AllowUserToResizeRows = false;
            dtgvTable.RowHeadersVisible = false;

            cbTableUsingState.DataSource = listState;
            cbTableUsingState.DisplayMember = "item1";
            cbTableUsingState.ValueMember = "item2";

            LoadTable();

            dtgvTable.Columns[0].HeaderText = "ID";
            dtgvTable.Columns[1].HeaderText = "Tên bàn";
            dtgvTable.Columns[2].HeaderText = "Tình trạng";
            dtgvTable.Columns[3].HeaderText = "Trạng thái";
            dtgvTable.Columns[4].Visible = false;

            //Tab doanh thu
            //Từ đầu giờ sáng ngày 1, 12:00:00 AM của tháng hiện tại
            DateTime fromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            //Đến cuối ngày cuối cùng, 11:59:59 PM của tháng hiện tại
            DateTime toDate = fromDate.AddHours(23.9999).AddMonths(1).AddDays(-1);

            SetValueDateTimePicker(fromDate, toDate);
            
            //Tab báo cáo
            //
            //LoadReport(dtpkFromDate.Value, dtpkToDate.Value);
            //Có thể resize form khi đang ở tab báo cáo
            tpReport.Enter += delegate { FormBorderStyle = FormBorderStyle.Sizable; };
            tpReport.Leave += delegate { FormBorderStyle = FormBorderStyle.FixedSingle; };
            //Giữ nút btnReport luôn ở giữa form
            SizeChanged += delegate { btnReport.Left = (ClientSize.Width - btnReport.Width) / 2; };

            //Tạo 1 nút bấm để thoát form khi nhấn ESC
            var btnExit = new Button();
            btnExit.Click += delegate { Close(); };
            CancelButton = btnExit;
            //AcceptButton = btnSearchFood;

            //Thêm chức năng show hoá đơn khi click đúp lên dtgvBill
            dtgvBill.CellDoubleClick += delegate { new fShowBill(dtgvBill.SelectedCells[0].OwningRow.Cells["ID"].Value).ShowDialog(); };

            //Subcribe cho mấy nút chuyển trang
            SetMaxPageNumber();
            btnFirst.Click += delegate { nmPageNumber.Value = 1; };
            btnPrevious.Click += delegate { if (nmPageNumber.Value > 1) nmPageNumber.Value--; };
            btnNext.Click += delegate { if (nmPageNumber.Value < nmPageNumber.Maximum) nmPageNumber.Value++; };
            btnLast.Click += delegate { nmPageNumber.Value = nmPageNumber.Maximum; };
            nmPageRow.ValueChanged += delegate { SetMaxPageNumber(); LoadDataBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, (int)nmPageNumber.Value, (int)nmPageRow.Value); };
            dtpkFromDate.ValueChanged+= delegate { SetMaxPageNumber(); };
            dtpkToDate.ValueChanged += delegate { SetMaxPageNumber(); };
        }

        /// <summary>
        /// Load binding.
        /// </summary>
        void LoadBinding()
        {
            LoadFoodBinding();

            LoadCategoryBinding();

            LoadTableBinding();

            LoadAccountBinding();
        }

        /// <summary>
        /// Đặt giá trị Maximum cho nmPageNumber
        /// </summary>
        void SetMaxPageNumber()
        {
            var totalRow = BillDAO.Instance.GetNumberBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            //Trong trường hợp người dùng chọn ngày lung tung, totalRow trả về kết quả 0 => thoát
            if (totalRow <= 0) return;

            var lastPage = totalRow / (int)nmPageRow.Value;

            if (totalRow % (int)nmPageRow.Value != 0) lastPage++;

            nmPageNumber.Maximum = lastPage;
        }

        /// <summary>
        /// Đặt giá trị cho dtpkFromDate, dtpkFromDate2, dtpkToDate và dtpkToDate2.
        /// </summary>
        /// <param name="fromDate">Giá trị cho dtpkFromDate.</param>
        /// <param name="toDate">Giá trị cho dtpkToDate.</param>
        void SetValueDateTimePicker(DateTime fromDate, DateTime toDate)
        {
            dtpkFromDate.Value = fromDate;
            dtpkToDate.Value = toDate;

            dtpkFromDate2.DataBindings.Add("Value", dtpkFromDate, "Value");
            dtpkToDate2.DataBindings.Add("Value", dtpkToDate, "Value");
        }

        /// <summary>
        /// Lấy danh sách tài khoản cho dtgvAccount.
        /// </summary>
        void LoadAccount() => accountBindingSource.DataSource = AccountDAO.Instance.GetListAccount();

        void LoadAccountBinding()
        {
            txbUserName.DataBindings.Add("Text", dtgvAccount.DataSource, "Username", false, DataSourceUpdateMode.Never);
            txbDisplayName.DataBindings.Add("Text", dtgvAccount.DataSource, "DisplayName", false, DataSourceUpdateMode.Never);
            cbAccountType.DataBindings.Add("SelectedValue", dtgvAccount.DataSource, "Type", false, DataSourceUpdateMode.Never);
        }

        [Obsolete("Hàm này không dùng nữa", false)]
        bool InsertAccount() => true;

        (bool, bool, bool) UpdateAccount(string username, string displayname, string password, int? type) => AccountDAO.Instance.Update(username, displayname, password, type);

        bool DeleteAccount(string userName) => AccountDAO.Instance.DeleteAccount(userName);

        /// <summary>
        /// Lấy danh sách bàn ăn cho dtgvTable.
        /// </summary>
        void LoadTable() => tableBindingSource.DataSource = TableDAO.Instance.GetListTable(); 

        void LoadTableBinding()
        {
            txbTableName.DataBindings.Add("Text", dtgvTable.DataSource, "Name", false, DataSourceUpdateMode.Never);
            txbTableID.DataBindings.Add("Text", dtgvTable.DataSource, "Id", false, DataSourceUpdateMode.Never);
            cbTableUsingState.DataBindings.Add("SelectedValue", dtgvTable.DataSource, "UsingState", false, DataSourceUpdateMode.Never);
            txbTableStatus.DataBindings.Add("Text", dtgvTable.DataSource, "Status", false, DataSourceUpdateMode.Never);
        }

        bool InsertTable() => TableDAO.Instance.InsertTable();

        bool UpdateTable(int id, string name, int usingState) => TableDAO.Instance.UpdateTable(id, name, usingState);

        /// <summary>
        /// Load danh sách các hoá đơn lên dtgvBill dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate">Từ ngày này.</param>
        /// <param name="toDate">Tới ngày này.</param>
        void LoadDataBillByDate(DateTime fromDate, DateTime toDate)
        {
            billBindingSource.DataSource = BillDAO.Instance.GetDataBillByDate(fromDate, toDate);

            lbTotal.Visible = false;
            lbTotalPrice.Visible = false;
            FormatBill();
        }

        /// <summary>
        /// Load danh sách các hoá đơn lên dtgvBill dựa theo ngày và số trang truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageNumber">Page muốn lấy về.</param>
        /// <param name="pageRow">Số dòng 1 page.</param>
        void LoadDataBillByDateAndPage(DateTime fromDate, DateTime toDate, int pageNumber = 1, int pageRow = 12)
        {
            billBindingSource.DataSource = BillDAO.Instance.GetDataBillByDateAndPage(fromDate, toDate, pageNumber, pageRow);

            lbTotal.Visible = true;
            lbTotalPrice.Visible = true;
            FormatBill();
        }

        void FormatBill()
        {
            if (dtgvBill.RowCount <= 0) return;

            //Trải đều, căn giữa
            dtgvBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvBill.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvBill.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvBill.RowHeadersVisible = false;
            dtgvBill.AllowUserToAddRows = false;
            dtgvBill.AllowUserToResizeRows = false;
            dtgvBill.AllowUserToResizeColumns = false;
            dtgvBill.EditMode = DataGridViewEditMode.EditProgrammatically;

            //Chỉnh độ rộng các cột
            dtgvBill.Columns[0].Width = 40;
            dtgvBill.Columns[3].Width = 120;
            dtgvBill.Columns[4].Width = 120;

            //Chỉnh tiêu đề của 3 cột này cho nó vào chính giữa
            dtgvBill.Columns[3].HeaderCell.Style.Padding = new Padding(15, 0, 0, 0);
            dtgvBill.Columns[4].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);

            dtgvBill.Columns[0].HeaderText = "Id";
            dtgvBill.Columns[1].HeaderText = "Ngày phát sinh";
            dtgvBill.Columns[2].HeaderText = "Ngày thanh toán";
            dtgvBill.Columns[3].HeaderText = "Tên bàn";
            dtgvBill.Columns[4].HeaderText = "Giảm giá (%)";
            dtgvBill.Columns[5].HeaderText = "Tiền thanh toán (Vnđ)";

            var font = new Font("Arial", 12, FontStyle.Bold);
            dtgvBill.Columns[0].HeaderCell.Style.Font = font;
            dtgvBill.Columns[1].HeaderCell.Style.Font = font;
            dtgvBill.Columns[2].HeaderCell.Style.Font = font;
            dtgvBill.Columns[3].HeaderCell.Style.Font = font;
            dtgvBill.Columns[4].HeaderCell.Style.Font = font;
            dtgvBill.Columns[5].HeaderCell.Style.Font = font;

            //Hiển thị tiền theo kiểu Việt Nam
            var culture = new System.Globalization.CultureInfo("vi-vn");
            dtgvBill.Columns[5].DefaultCellStyle.FormatProvider = culture;
            dtgvBill.Columns[5].DefaultCellStyle.Format = "c0";

            //Cộng tổng tiền các hoá đơn hiện ra trên dtgvBill
            double totalPrice = 0;
            foreach (DataGridViewRow row in dtgvBill.Rows)
            {
                totalPrice += (double)row.Cells[5].Value;
            }

            lbTotalPrice.Text = totalPrice.ToString("c0", culture);
        }

        /// <summary>
        /// Lấy danh sách thức ăn cho dtgvFood.
        /// </summary>
        void LoadFood() => foodBindingSource.DataSource = FoodDAO.Instance.GetListFood();  

        /// <summary>
        /// Thêm các binding với dtgvFood.
        /// </summary>
        void LoadFoodBinding()
        {
            txbFoodName.DataBindings.Add("Text", dtgvFood.DataSource, "Name", false, DataSourceUpdateMode.Never);
            txbFoodID.DataBindings.Add("Text", dtgvFood.DataSource, "Id", false, DataSourceUpdateMode.Never);
            nmFoodPrice.DataBindings.Add("Value", dtgvFood.DataSource, "Price", false, DataSourceUpdateMode.Never);
            cbFoodCategory.DataBindings.Add("SelectedValue", dtgvFood.DataSource, "CategoryId", false, DataSourceUpdateMode.Never);
            cbFoodStatus.DataBindings.Add("SelectedValue", dtgvFood.DataSource, "FoodStatus", false, DataSourceUpdateMode.Never);
        }      

        /// <summary>
        /// Thêm món ăn cho danh sách quản lý.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categorID"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        bool InsertFood(string name, int categoryID, double price) => FoodDAO.Instance.InsertFood(name, categoryID, price);

        bool UpdateFood(int id, string name, int categoryID, double price, int foodStatus) => FoodDAO.Instance.UpdateFood(id, name, categoryID, price, foodStatus);

        /// <summary>
        /// Tìm kiếm trong database (có thể bỏ qua phân biệt Unicode), trả về list kết quả.
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="option">True để bỏ qua phân biệt Unicode, ngược lại, False.</param>
        /// <returns></returns>
        List<Food> SearchFood(string foodName, bool option = true) => FoodDAO.Instance.SearchFood(foodName, option);

        /// <summary>
        /// Tìm kiếm ngay trên dtgvFood, highlight các kết quả.
        /// </summary>
        void SearchFood(string foodName)
        {
            //Bỏ chọn những cell đang được highlight
            dtgvFood.ClearSelection();

            //Highlight và active những cell có chứa nội dung trong txbSearchFood
            var listHighlightCell = new List<DataGridViewCell>();

            for (int i = 0; i < dtgvFood.Rows.Count; i++)
            {
                if (dtgvFood.Rows[i].Cells["Name"].Value.ToString().ToLower().Contains(foodName.ToLower()))
                {
                    //Active cell có chứa nội dung trong txbSearchFood
                    dtgvFood.CurrentCell = dtgvFood.Rows[i].Cells[1];

                    //Cho những cell này vào list để tí nữa highlight hết cả bọn
                    listHighlightCell.Add(dtgvFood.CurrentCell);
                }
            }

            listHighlightCell.ForEach(cell => cell.Selected = true);
        }

        /// <summary>
        /// Lấy danh sách Category cho dtgvCategory và cbFoodCategory.
        /// </summary>
        void LoadCategory()
        {
            var listCategory = CategoryDAO.Instance.GetListCategory();

            //Load data cho dtgvCategory
            categoryBindingSource.DataSource = listCategory;

            //Load data cho cbFoodCategory
            cbFoodCategory.DataSource = listCategory;
            cbFoodCategory.DisplayMember = "Name";
            //Đặt giá trị cho ValueMember để sử dụng được SelectedValue cho binding
            cbFoodCategory.ValueMember = "ID";
        }

        void LoadCategoryBinding()
        {
            txbCategoryName.DataBindings.Add("Text", dtgvCategory.DataSource, "Name", false, DataSourceUpdateMode.Never);
            txbCategoryID.DataBindings.Add("Text", dtgvCategory.DataSource, "Id", false, DataSourceUpdateMode.Never);
            cbCategoryStatus.DataBindings.Add("SelectedValue", dtgvCategory.DataSource, "CategoryStatus", false, DataSourceUpdateMode.Never);
        }

        bool InsertCategory(string name) => CategoryDAO.Instance.InsertCategory(name);

        bool UpdateCategory(int id, string name, int categoryStatus) => CategoryDAO.Instance.UpdateCategory(id, name, categoryStatus);

        void LoadReport(DateTime fromDate, DateTime toDate)
        {
            var data = FoodDAO.Instance.GetRevenueByFoodAndDate(fromDate, toDate);
            ReportDataSource rds = new ReportDataSource("dsRevenueByFoodAndDate", data);

            var data2 = BillDAO.Instance.GetRevenueByMonth(fromDate, toDate);
            ReportDataSource rds2 = new ReportDataSource("dsRevenueByMonth", data2);

            rpvRevenue.LocalReport.ReportEmbeddedResource = "CanteenManager.Report.rpRevenue.rdlc";
            rpvRevenue.LocalReport.DataSources.Clear();
            rpvRevenue.LocalReport.DataSources.Add(rds);
            rpvRevenue.LocalReport.DataSources.Add(rds2);
            rpvRevenue.RefreshReport();

        }

        #endregion

        #region Events

        private void fAdmin_Load(object sender, EventArgs e)
        {
            if (ckbShowByPage.Checked)
                LoadDataBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, (int)nmPageNumber.Value, (int)nmPageRow.Value);
            else
                LoadDataBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            nmPageNumber.Value = 1;

            if (ckbShowByPage.Checked)
                LoadDataBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, (int)nmPageNumber.Value, (int)nmPageRow.Value);
            else
                LoadDataBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadFood();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (int)cbFoodCategory.SelectedValue;
            double price = (double)nmFoodPrice.Value;

            if (InsertFood(name, categoryID, price)) 
            {
                MessageBox.Show($"Thêm món [{name}] thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFood();

                foodChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm món thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbFoodID.Text);
            string name = txbFoodName.Text;
            int categoryID = (int)cbFoodCategory.SelectedValue;
            double price = (double)nmFoodPrice.Value;
            var foodStatus = (int)cbFoodStatus.SelectedValue;

            if (UpdateFood(id, name, categoryID, price, foodStatus))
            {
                MessageBox.Show($"Cập nhật món [{name}] thành công!\nMón [{name}] {cbFoodStatus.Text.ToLower()}!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFood();

                foodChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật món thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbFoodID.Text);
            string name = txbFoodName.Text;
            int categoryID = (int)cbFoodCategory.SelectedValue;
            double price = (double)nmFoodPrice.Value;

            if (UpdateFood(id, name, categoryID, price, 0))
            {
                MessageBox.Show($"Món [{name}] đã dừng phục vụ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFood();

                foodChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật món thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodBindingSource.DataSource = SearchFood(txbSearchFood.Text, ckbSearch.Checked);           
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (InsertCategory(name))
            {
                MessageBox.Show($"Thêm danh mục [{name}] thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCategory();

                categoryChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm danh mục thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);
            string name = txbCategoryName.Text;
            var categoryStatus = (int)cbCategoryStatus.SelectedValue;

            if (UpdateCategory(id, name, categoryStatus))
            {
                MessageBox.Show($"Cập nhật danh mục [{name}] thành công!\nDanh mục [{name}] {cbCategoryStatus.Text.ToLower()}!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCategory();

                categoryChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật danh mục thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);
            string name = txbCategoryName.Text;

            if (UpdateCategory(id, name, 0))
            {
                MessageBox.Show($"Danh mục [{name}] đã dừng phục vụ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCategory();

                categoryChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật danh mục thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (InsertTable())
            {
                MessageBox.Show($"Thêm bàn mới thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTable();

                tableChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            if (txbTableStatus.Text == "Có người")
            {
                MessageBox.Show("Không thể chỉnh sửa bàn đang có người.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            int id = int.Parse(txbTableID.Text);
            string name = txbTableName.Text;
            var usingState = (int)cbTableUsingState.SelectedValue;

            if (UpdateTable(id, name, usingState))
            {
                MessageBox.Show($"Cập nhật [{name}] thành công!\n[{name}] {cbTableUsingState.Text.ToLower()}!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTable();

                tableChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật bàn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (txbTableStatus.Text == "Có người")
            {
                MessageBox.Show("Không thể xoá bàn đang có người.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            int id = int.Parse(txbTableID.Text);
            string name = txbTableName.Text;

            if (UpdateTable(id, name, 0))
            {
                MessageBox.Show($"[{name}] không còn được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTable();

                tableChange?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật bàn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            fAddAccount addAccount = new fAddAccount();
            addAccount.ShowDialog();

            LoadAccount();
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbDisplayName.Text) || string.IsNullOrWhiteSpace(txbDisplayName.Text))
            {
                MessageBox.Show("Tên hiển thị không được để trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if ((accountBindingSource.Current as Account).Type == AccountType.Admin && txbUserName.Text != LoginAdmin.Username && LoginAdmin.Username != "admin")
            {
                MessageBox.Show("Bạn không thể chỉnh sửa tài khoản Admin khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }           

            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int? type = (int)cbAccountType.SelectedValue;

            if (LoginAdmin.Username != "admin" && (accountBindingSource.Current as Account).Type == AccountType.Staff && type == 1)
            {
                MessageBox.Show("Bạn không được tạo thêm Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (txbUserName.Text == "admin" && type == 0)
            {
                MessageBox.Show("Bạn phải làm quản lý, không cho làm nhân viên O.o", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var result = UpdateAccount(username, displayname, null, type);
            if (result.Item1 && result.Item3) 
            {
                MessageBox.Show($"Cập nhật tài khoản [{username}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadAccount();

                //Nếu Admin đang đăng nhập tự sửa chính mình
                if (txbUserName.Text == LoginAdmin.Username)
                {
                    //Load lại loginAdmin
                    foreach (var item in accountBindingSource.List)
                    {
                        if ((item as Account).Username == loginAdmin.Username) loginAdmin = (Account)item;                       
                    } 

                    //Nếu không còn là Admin nữa
                    if (LoginAdmin.Type != AccountType.Admin) tcAdmin.Enabled = false;

                    //publish event
                    adminChange?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            
            if(txbUserName.Text == LoginAdmin.Username)
            {
                MessageBox.Show("Bạn không thể xoá chính mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if((accountBindingSource.Current as Account).Type == AccountType.Admin && LoginAdmin.Username != "admin")
            {
                MessageBox.Show("Bạn không thể xoá tài khoản Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string userName = txbUserName.Text;

            if (DeleteAccount(userName))
            {
                MessageBox.Show($"Xoá tài khoản [{userName}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAccount();
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            if ((accountBindingSource.Current as Account).Type == AccountType.Admin && txbUserName.Text != LoginAdmin.Username && LoginAdmin.Username != "admin")
            {
                MessageBox.Show("Bạn không thể chỉnh sửa tài khoản Admin khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string username = txbUserName.Text;

            if (UpdateAccount(username, null, "0", null).Item2)
            {
                MessageBox.Show($"Đặt lại mật khẩu cho tài khoản [{username}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ckbShowByPage_CheckedChanged(object sender, EventArgs e)
        {
            btnFirst.Enabled = ckbShowByPage.Checked;
            btnPrevious.Enabled = ckbShowByPage.Checked;
            btnNext.Enabled = ckbShowByPage.Checked;
            btnLast.Enabled = ckbShowByPage.Checked;
            nmPageNumber.Enabled = ckbShowByPage.Checked;
            nmPageRow.Enabled = ckbShowByPage.Checked;

            if (ckbShowByPage.Checked)
                LoadDataBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, (int)nmPageNumber.Value, (int)nmPageRow.Value);
            else
                LoadDataBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

        }

        private void nmPageNumber_ValueChanged(object sender, EventArgs e)
        {
            LoadDataBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, (int)nmPageNumber.Value, (int)nmPageRow.Value);
        }

        private void nmPageNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) nmPageNumber.Update();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadReport(dtpkFromDate.Value, dtpkToDate.Value);
        }

        #endregion


    }

}
