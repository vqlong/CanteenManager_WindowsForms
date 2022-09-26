using CanteenManager.DAO;
using CanteenManager.DTO;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace CanteenManager
{
    /// <summary>
    /// Form quản lý bàn ăn.
    /// </summary>
    public partial class fTableManager : Form
    {
        
        public fTableManager(Account loginAccount)
        {
            InitializeComponent();

            this.LoginAccount = loginAccount;

            LoadButton();

            LoadCbListTable();

            LoadCategory();

        }

        private Account loginAccount;
        public Account LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount.AccType); }
        }

        int tableHeight = 90;
        int tableWidth = 90;      

        #region Methods

        /// <summary>
        /// Bật/tắt nút hiện form quản lý của Admin.
        /// </summary>
        /// <param name="accType"></param>
        void ChangeAccount(AccountType accType)
        {
            tsmiAdmin.Enabled = accType == AccountType.Admin;
            tsmiAccountProfile.Text = "Thông tin cá nhân [" + this.LoginAccount.DisplayName + "]";
        }

        /// <summary>
        /// Hiển thị các bàn ăn trên form dựa vào danh sách lấy từ database.
        /// <br>Mỗi bàn được hiển thị bởi 1 Button.</br>
        /// </summary>
        void LoadButton()
        {
            //Xoá các bàn ăn đang hiển thị để cập nhật lại.
            flpTable.Controls.Clear();

            //Lấy danh sách bàn ăn.
            List<Table> listTable = TableDAO.Instance.GetListTableUsing();

            foreach (Table table in listTable)
            {
                //Tạo Button tượng trưng cho các bàn.
                Button button = new Button();
                button.Text = table.Name + "\n" + table.Status;
                button.Height = tableHeight;
                button.Width = tableWidth;
                button.TabStop = false;
                //Tạo event click cho mỗi button để hiện thông tin mỗi khi click vào.
                button.Click += Button_Click;
                //Lưu đối tượng bàn vào Tag của button để dùng cho các bước sau.
                button.Tag = table;
                //button hiển thị màu dựa theo trạng thái của bàn.
                switch (table.Status)
                {
                    case "Có người":
                        button.BackColor = Color.LimeGreen;
                        break;
                    case "Trống":
                        button.BackColor = Color.LightGray;
                        break;
                }
                flpTable.Controls.Add(button);
            }

            
        }

        /// <summary>
        /// Làm mới lại tên và màu nền của button tượng trưng cho các bàn.
        /// <br>Tạo 1 đối tượng Table mới (để cập nhật lại các thuộc tính) và gán lại cho button.</br>
        /// </summary>
        /// <param name="button"></param>
        void RefreshButton(Button button)
        {
            if (button == null)
            {
                return;
            }

            int tableID = (button.Tag as Table).ID;

            //Gán lại 1 đối tượng table mới cho button.
            Table table = TableDAO.Instance.GetTableByID(tableID);

            button.Tag = table;

            button.Text = table.Name + "\n" + table.Status;

            switch (table.Status)
            {
                case "Có người":
                    button.BackColor = Color.LimeGreen;
                    break;
                case "Trống":
                    button.BackColor = Color.LightGray;
                    break;
            }


        }

        /// <summary>
        /// Load danh sách bàn ăn lên ComboBox.
        /// </summary>
        void LoadCbListTable()
        {
            //Lấy danh sách bàn ăn.
            List<Table> listTable = TableDAO.Instance.GetListTableUsing();

            cbListTable.DataSource = listTable;
            cbListTable.DisplayMember = "Name";
            
        }

        /// <summary>
        /// Hiển thị hoá đơn chưa thanh toán của bàn lên ListView dựa vào ID của nó.
        /// </summary>
        /// <param name="tableID">ID của bàn (TableID của Bill).</param>
        void ShowBillByTableID(int tableID)
        {
            List<BillDetail> listBillDetail = BillDetailDAO.Instance.GetListBillDetailByTableID(tableID);

            //Biến cộng tổng giá tiền của các món.
            double billTotalPrice = 0;

            //Xoá thông tin cũ.
            lsvBill.Items.Clear();

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-vn");

            //Thêm thông tin mới.
            foreach (BillDetail billDetail in listBillDetail)
            {
                ListViewItem item = new ListViewItem(billDetail.FoodName);
                item.SubItems.Add(billDetail.CategoryName);
                item.SubItems.Add(billDetail.FoodCount.ToString());
                item.SubItems.Add(billDetail.Price.ToString("c0", culture));
                item.SubItems.Add(billDetail.TotalPrice.ToString("c0", culture));

                billTotalPrice += (double)billDetail.TotalPrice;

                lsvBill.Items.Add(item);
            }

            ////Cách 1: Đặt lại culture cho Thread chính (đang chạy form) để hiện số theo kiểu tiếng Việt.
            //Thread.CurrentThread.CurrentCulture = culture;
            
            //Cách 2: truyền culture vào hàm ToString().
            txbTotalPrice.Text = billTotalPrice.ToString("c0", culture);
            //Lưu tổng giá tiền vào Tag của txbTotalPrice để dùng cho các bước sau.
            txbTotalPrice.Tag = billTotalPrice;

            lsvBill.Columns.Clear(); 
            lsvBill.Columns.Add("Tên món", 140);
            lsvBill.Columns.Add("Thể loại", 75);
            lsvBill.Columns.Add("Số lượng", 75, HorizontalAlignment.Right);
            lsvBill.Columns.Add("Đơn giá", 75, HorizontalAlignment.Right);
            lsvBill.Columns.Add("Tổng tiền", 75, HorizontalAlignment.Right);

            lsvBill.View = View.Details;
        }

        /// <summary>
        /// In ra máy in 1 hoá đơn dựa vào ID của nó.
        /// </summary>
        /// <param name="tableID"></param>
        void PrintBillByID(int? id)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                printDocument.DefaultPageSettings.PaperSize = printDialog.PrinterSettings.PaperSizes[8]; //FoxitReader_PDF_Printer: 10: giấy A5(827 - 583), 8: giấy A4(1169 - 827), 69: giấy A6(583 - 413)
                printDocument.DocumentName = $"BillID_{id}_" + DateTime.Now.ToString("ddMMyyy_hhmmss_tt");
                printDocument.Print();
            }
        }

        /// <summary>
        /// Lấy các danh mục món ăn đưa vào ComboBox.
        /// </summary>
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategoryServing();

            cbCategory.Text = "";
            cbFood.DataSource = null;
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        /// <summary>
        /// Lấy danh sách các món ăn trong danh mục đã chọn (dựa theo categoryID) đưa vào ComboBox.
        /// </summary>
        /// <param name="categoryID"></param>
        void LoadListFoodByCategoryID(int categoryID)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodByCategoryID(categoryID);

            cbFood.Text = "";
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }       

        #endregion

        #region Events

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem == null) return;
            
            Category category = comboBox.SelectedItem as Category;
            
            LoadListFoodByCategoryID(category.ID);
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            //Ép kiểu object của sender về Button để lấy Tag, ép kiểu object của Tag về Table để lấy ID.
            int tableID = ((sender as Button).Tag as Table).ID;

            gbBill.Text = "Hoá đơn chưa thanh toán [" + ((sender as Button)?.Tag as Table)?.Name + "]";

            //Đổi lại màu nền cho button được click lúc trước
            RefreshButton(lsvBill.Tag as Button);

            //Button này vừa được click
            //=> Lưu nó vào Tag của lsvBill để dùng cho các bước sau.
            lsvBill.Tag = sender;

            //Đổi màu nền cho button vừa được click
            (sender as Button).BackColor = Color.Yellow;

            ShowBillByTableID(tableID);
        }

        private void tsmiAccountProfile_Click(object sender, EventArgs e)
        {
            fAccountProfile accountProfile = new fAccountProfile(this.LoginAccount);
            accountProfile.ShowDialog();

            //Load lại tài khoản đăng nhập sau khi kết thúc form fAccountProfile
            this.LoginAccount = accountProfile.LoginAccount;
            
            ////Khi set lại LoginAccount, hàm ChangeAccount sẽ được gọi, không cần Refresh hay Update nữa 
            //this.Refresh();
            //this.Update();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new fAbout().ShowDialog(); 
        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAdmin_Click(object sender, EventArgs e)
        {
            fAdmin admin = new fAdmin(this.LoginAccount);
            admin.FoodChange += delegate { LoadListFoodByCategoryID((cbCategory.SelectedItem as Category).ID); };
            admin.CategoryChange += delegate { LoadCategory(); };
            admin.TableChange += delegate { LoadButton(); LoadCbListTable(); };
            admin.AdminChange += delegate { LoginAccount = admin.LoginAdmin; };

            admin.ShowDialog();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            //Nếu chưa có hoá đơn nào được hiển thị lên lsvBill (người dùng chưa click vào bàn nào) thì không làm gì cả và thoát
            if (lsvBill.Tag == null) return;
            //Nếu danh sách Food trống thì không làm gì cả và thoát
            if (cbFood.SelectedItem == null) return;

            //Lấy ra Table lưu trong Tag của lsvBill
            Table table = (lsvBill.Tag as Button).Tag as Table;

            int billID = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            int foodID = (int)(cbFood.SelectedItem as Food).ID;
            int foodCount = (int)nmFoodCount.Value;

            if (billID == -1)
            {
                //Trường hợp Bill chưa tồn tại, tức là cái bàn này chưa có cái Bill nào mà chưa thanh toán

                //Bill chưa tồn tại mà người dùng lại chọn số món ăn < 1 thì cũng thoát và không tạo mới Bill
                if (foodCount < 1) return;

                //Tạo 1 Bill mới cho cái bàn được chọn
                BillDAO.Instance.InsertBill(table.ID);

                //Sau khi 1 Bill mới được tạo, nó sẽ có Status = 0, mỗi bàn chỉ có duy nhất 1 Bill như vậy
                //=> Lúc này chạy BillDAO.Instance.GetUnCheckBillIDByTableID() sẽ được luôn ID của Bill vừa tạo
                //Thêm món cho Bill mới vừa tạo
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID), foodID, foodCount);

                ////Hiển thị lại trạng thái của bàn ăn sau khi thêm hoá đơn mới
                ////Nếu dùng hàm LoadButton() load lại tất cả button thì màn hình sẽ nháy => xấu vđ
                //LoadButton();
                ////Nếu dùng hàm RefreshButton() thì button sẽ mất màu vàng (biểu thị button này đang được click)
                //RefreshButton(lsvBill.Tag as Button);
                Button_Click(lsvBill.Tag as Button, new EventArgs());

                //Tạo lại danh sách bàn ăn (để cập nhật lại các thuộc tính)
                LoadCbListTable();
            }
            else
            {
                //Trường hợp Bill đã tồn tại, tức là cái bàn này đã có 1 Bill chưa thanh toán

                //Thêm món cho Bill này
                BillInfoDAO.Instance.InsertBillInfo(billID, foodID, foodCount);

                //Trường hợp người dùng xoá hết các món ăn trên bàn cũng cần hiển thị lại trạng thái của bàn
                Button_Click(lsvBill.Tag as Button, new EventArgs());

                LoadCbListTable();
            }

            //Cập nhật lại thông tin cho lsvBill
            ShowBillByTableID(table.ID);

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            //Nếu chưa có hoá đơn nào được hiển thị lên lsvBill (người dùng chưa click vào bàn nào) thì không làm gì cả và thoát
            if (lsvBill.Tag == null) return;

            //Lấy ra Table lưu trong Tag của lsvBill
            Table table = (lsvBill.Tag as Button).Tag as Table;

            int billID = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);

            int discount = (int)nmDiscount.Value;

            double billTotalPrice = (double)txbTotalPrice.Tag;

            double billFinalPrice = billTotalPrice - billTotalPrice * discount / 100;

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-vn");

            if (billID != -1)
            {
                if (MessageBox.Show($"Bạn muốn thanh toán hoá đơn cho {table.Name}?\nTổng tiền:\t {txbTotalPrice.Text}\nGiảm giá:\t {nmDiscount.Value.ToString()}%\nPhải trả:\t\t {billFinalPrice.ToString("c0", culture)}", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK) 
                {
                    BillDAO.Instance.CheckOut(billID, billFinalPrice, discount);

                    //Cập nhật lại thông tin cho lsvBill
                    ShowBillByTableID(table.ID);

                    nmDiscount.Value = 0;

                    //Hiển thị lại trạng thái các bàn ăn sau khi thanh toán
                    RefreshButton(lsvBill.Tag as Button);

                    LoadCbListTable();

                    if (MessageBox.Show("Bạn có muốn in hoá đơn?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        ////In từng tí một
                        //BillDetailDAO.Instance.PrintListBillDetailByBillID(billID);

                        ////In luôn cái lsvBill
                        //PrintBillByID(billID);

                        //In ra report đẹp tuyệt vời
                        new fPrintBill(billID).ShowDialog();
                    }
                }
                
            }
           
        }   

        private void btnSwapTable_Click(object sender, EventArgs e)
        {
            //Nếu chưa có hoá đơn nào được hiển thị lên lsvBill (người dùng chưa click vào bàn nào) thì không làm gì cả và thoát
            if (lsvBill.Tag == null)
            {
                MessageBox.Show("Hãy chọn 1 bàn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                
            //Lấy ra Table lưu trong Tag của lsvBill
            Table table = (lsvBill.Tag as Button).Tag as Table;

            //Nếu bàn được chọn đang trống thì không làm gì cả và thoát
            if (table.Status == "Trống")
            {
                MessageBox.Show("Hãy chọn 1 bàn có người.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int firstTableID = table.ID;

            int secondTableID = (cbListTable.SelectedValue as Table).ID;

            //Nếu 2 bàn được chọn trùng nhau thì không làm gì cả và thoát
            if (firstTableID == secondTableID)
            {
                MessageBox.Show($"Không thể chuyển {table.Name} tới {(cbListTable.SelectedValue as Table)?.Name}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có thực sự muốn chuyển {table.Name} tới {(cbListTable.SelectedValue as Table)?.Name}?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                TableDAO.Instance.SwapTable(firstTableID, secondTableID);

                //Click lại vào Button chứa bàn được chọn thứ 2 (chọn từ cbListTable)
                foreach (Button button in flpTable.Controls)
                {
                    if ((button.Tag as Table).ID == secondTableID)
                    {
                        RefreshButton(button);

                        Button_Click(button, new EventArgs());
                    }
                }

                LoadCbListTable();
            }

            
        }

        private void btnCombineTable_Click(object sender, EventArgs e)
        {
            //Nếu chưa có hoá đơn nào được hiển thị lên lsvBill (người dùng chưa click vào bàn nào) thì không làm gì cả và thoát
            if (lsvBill.Tag == null)
            {
                MessageBox.Show("Hãy chọn 1 bàn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Lấy ra Table lưu trong Tag của lsvBill
            Table table = (lsvBill.Tag as Button).Tag as Table;

            //Nếu bàn thứ 1 (được chọn trong flpTable) hoặc bàn thứ 2 (được chọn trong cbListTable) đang trống thì không làm gì cả và thoát
            if ((table.Status == "Trống") || ((cbListTable.SelectedValue as Table)?.Status == "Trống"))
            {
                MessageBox.Show("Hãy chọn 1 bàn có người.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int firstTableID = table.ID;
           
            int secondTableID = (cbListTable.SelectedValue as Table).ID;

            //Nếu 2 bàn được chọn trùng nhau thì không làm gì cả và thoát
            if (firstTableID == secondTableID)
            {
                MessageBox.Show($"Không thể gộp {table.Name} vào {(cbListTable.SelectedValue as Table)?.Name}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có thực sự muốn gộp {table.Name} vào {(cbListTable.SelectedValue as Table)?.Name}?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                TableDAO.Instance.CombineTable(firstTableID, secondTableID);

                //Cập nhật lại trạng thái cho bàn được chọn thứ 1 (chọn trong flpTable)
                //RefreshButton(lsvBill.Tag as Button);

                //Click lại vào Button chứa bàn được chọn thứ 2 (chọn từ cbListTable)
                foreach (Button button in flpTable.Controls)
                {
                    if ((button.Tag as Table).ID == secondTableID)
                    {
                        Button_Click(button, new EventArgs());
                    }
                }

                LoadCbListTable();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fontBold = new Font(FontFamily.Families[159], 12, FontStyle.Bold); //159: font segoe UI
            Font fontRegular = new Font(FontFamily.Families[159], 9, FontStyle.Regular);

            string content = "**HOÁ ĐƠN THANH TOÁN**";
            e.Graphics.DrawString(content, fontBold, Brushes.Black, 300, 50);

            string pattern = @"\d+";
            var id = int.Parse(Regex.Match((sender as PrintDocument).DocumentName, pattern).Value);
            var bill = BillDAO.Instance.GetBillByID(id);
            
            content = $"Ngày:\t\t {DateTime.Now.ToString("F")}\nID hoá đơn:\t {id}\nID bàn:\t\t {bill.TableID}";
            e.Graphics.DrawString(content, fontRegular, Brushes.Black, 192, 120);

            Bitmap bitmap = new Bitmap(lsvBill.Width, lsvBill.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            lsvBill.DrawToBitmap(bitmap, new Rectangle(0, 0, lsvBill.Width - 1, lsvBill.Height));
            //bitmap.Save(@"C:\Users\HolyShit\Desktop\Bill.PNG");           
            e.Graphics.DrawImage(bitmap, new Point(192, 170));

            int discount = (int)nmDiscount.Value;
            double billTotalPrice = (double)txbTotalPrice.Tag;
            double billFinalPrice = billTotalPrice - billTotalPrice * discount / 100;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-vn");

            content = $"Thành tiền:\t {txbTotalPrice.Text}\nGiảm giá:\t {discount}%\nPhải trả:\t {billFinalPrice.ToString("c0", culture)}";
            e.Graphics.DrawString(content, fontBold, Brushes.Black, 192, 550);
        }



        #endregion

 
    }
}
