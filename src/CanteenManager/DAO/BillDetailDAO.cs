using Interfaces;
using Models;
using System.Data;
using System.Drawing.Printing;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các thao tác xử lý với lớp BillDetail.
    /// </summary>
    public class BillDetailDAO : IBillDetailDAO
    {
        private static IBillDetailDAO instance;

        public static IBillDetailDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IBillDetailDAO>());
            private set => instance = value;
        }

        private BillDetailDAO() { }

        /// <summary>
        /// Lấy danh sách BillDetail dựa vào TableID của Bill.
        /// <br>Bill này phải là Bill chưa thanh toán (BillStatus = 0).</br>
        /// </summary>
        /// <param name="tableId">TableID của Bill.</param>
        /// <returns></returns>
        public List<BillDetail> GetListBillDetailByTableId(int tableId)
        {          
            string query = "select Food.Name as FoodName, FoodCategory.Name as CategoryName, BillInfo.FoodCount as FoodCount, Food.Price as Price, FoodCount*Price as TotalPrice from Bill, BillInfo, Food, FoodCategory where BillInfo.BillId = Bill.Id and BillInfo.FoodId = Food.Id and Food.CategoryId = FoodCategory.Id and Bill.BillStatus = 0 and Bill.TableId = " + tableId;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<BillDetail> listBillDetail = new List<BillDetail>(data.Rows.Count);

            foreach (DataRow row in data.Rows)
            {
                BillDetail billDetail = new BillDetail(row);

                listBillDetail.Add(billDetail);
            }

            return listBillDetail;
        }

        /// <summary>
        /// Lấy danh sách BillDetail dựa vào ID của Bill.
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public List<BillDetail> GetListBillDetailByBillId(int billId)
        {           
            string query = "select Food.Name as FoodName, FoodCategory.Name as CategoryName, BillInfo.FoodCount as FoodCount, Food.Price as Price, FoodCount*Price as TotalPrice from BillInfo, Food, FoodCategory where BillInfo.FoodId = Food.Id and Food.CategoryId = FoodCategory.Id and BillInfo.BillId = " + billId;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<BillDetail> listBillDetail = new List<BillDetail>(data.Rows.Count);

            foreach (DataRow row in data.Rows)
            {
                BillDetail billDetail = new BillDetail(row);

                listBillDetail.Add(billDetail);
            }

            return listBillDetail;
        }

        /// <summary>
        /// Danh sách BillDetail sắp in từ hàm PrintListBillDetailByBillID(int billID).
        /// </summary>
        private List<BillDetail> printList;

        /// <summary>
        /// Nội dung in ra từ hàm PrintListBillDetailByBillID(int billID).
        /// </summary>
        private string content = "";

        /// <summary>
        /// Số trang in ra từ hàm PrintListBillDetailByBillID(int billID).
        /// </summary>
        private int page = 0;

        /// <summary>
        /// Số chỉ mục để in ra các món từ hàm PrintListBillDetailByBillID(int billID).
        /// </summary>
        private int indexFood = 0;

        /// <summary>
        /// ID của Bill sắp in từ hàm PrintListBillDetailByBillID(int billID).
        /// </summary>
        private int printBillId;

        /// <summary>
        /// In ra máy in danh sách BillDetail dựa vào ID của 1 Bill.
        /// </summary>
        /// <param name="billId"></param>
        public void PrintListBillDetailByBillId(int billId)
        {
            printList = GetListBillDetailByBillId(billId);

            printBillId = billId;

            content = "";
            page = 0;
            indexFood = 0;

            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDialog.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize { RawKind = 70, }; //Để mặc định là A6

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                printDocument.DefaultPageSettings.PaperSize = printDialog.PrinterSettings.DefaultPageSettings.PaperSize;  //Phải chọn PaperSize A6 để in ra cân đối
                printDocument.DocumentName = $"BillID_{billId}_" + DateTime.Now.ToString("ddMMyyy_hhmmss_tt");
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fontBold = new Font("Cambria", 12, FontStyle.Bold);
            Font fontRegular = new Font("Cambria", 12, FontStyle.Regular);

            StringFormat formatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat formatNear = new StringFormat { Alignment = StringAlignment.Near };
            formatNear.SetTabStops(0, new float[] { 4 });
            StringFormat formatFar = new StringFormat { Alignment = StringAlignment.Far };

            content = $"[Tờ {++page}]";
            e.Graphics.DrawString(content, fontRegular, Brushes.Green, new Rectangle(new Point(0, 30), e.PageBounds.Size), formatCenter);

            content = "**HOÁ ĐƠN THANH TOÁN**";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, new Rectangle(new Point(0, 50), e.PageBounds.Size), formatCenter);

            Bill bill = BillDAO.Instance.GetBillById(printBillId);
            content = $"Ngày:\t\t\t\t\t\t\t\t\t\t\t\t {DateTime.Now.ToString("G")}\nID hoá đơn:\t {printBillId}\nID bàn:\t\t\t\t\t\t\t\t\t {bill.TableId}";
            e.Graphics.DrawString(content, fontRegular, Brushes.Green, 35, 110, formatNear);

            content = "-------------------------------------------------------------";
            //e.Graphics.DrawString(content, fontRegular, Brushes.Green, 35, 160, formatNear);
            e.Graphics.DrawString(content, fontRegular, Brushes.Green, new Rectangle(new Point(0, 160), e.PageBounds.Size), formatCenter);

            content = "Tên món      Số lượng      Đơn giá      Tổng tiền";
            //e.Graphics.DrawString(content, fontBold, Brushes.Green, 35, 175, formatNear);
            e.Graphics.DrawString(content, fontBold, Brushes.Green, new Rectangle(new Point(0, 175), e.PageBounds.Size), formatCenter);

            content = "-------------------------------------------------------------";
            //e.Graphics.DrawString(content, fontRegular, Brushes.Green, 35, 185, formatNear);
            e.Graphics.DrawString(content, fontRegular, Brushes.Green, new Rectangle(new Point(0, 185), e.PageBounds.Size), formatCenter);

            int indexLine = 0;
            for (; indexFood < printList.Count; indexFood++)
            {
                content = $"{printList[indexFood].FoodName}";
                e.Graphics.DrawString(content, fontRegular, Brushes.Green, 35, 200 + 20 * indexLine, formatNear);

                content = $"{printList[indexFood].FoodCount}";
                e.Graphics.DrawString(content, fontRegular, Brushes.Green, 200, 200 + 20 * indexLine, formatFar);

                content = $"{printList[indexFood].Price}";
                e.Graphics.DrawString(content, fontRegular, Brushes.Green, 280, 200 + 20 * indexLine, formatFar);

                content = $"{printList[indexFood].TotalPrice}";
                e.Graphics.DrawString(content, fontRegular, Brushes.Green, 375, 200 + 20 * indexLine, formatFar);

                indexLine++;

                //Nếu in quá 10 món sẽ nhảy trang
                if (indexLine > 9)
                {
                    e.HasMorePages = true;
                    indexFood++;
                    break;
                }
            }

            content = "-------------------------------------------------------------";
            //e.Graphics.DrawString(content, fontRegular, Brushes.Green, 35, 200 + 20 * indexLine, formatNear);
            e.Graphics.DrawString(content, fontRegular, Brushes.Green, new Rectangle(new Point(0, 200 + 20 * indexLine), e.PageBounds.Size), formatCenter);

            content = "Thành tiền:";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 280, 220 + 20 * indexLine, formatFar);

            double billTotalPrice = (double)printList.Sum(billDetail => billDetail.TotalPrice);
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-vn");
            content = $"{billTotalPrice.ToString("c0", culture)}";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 375, 220 + 20 * indexLine, formatFar);

            content = "Giảm giá:";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 280, 240 + 20 * indexLine, formatFar);

            int discount = bill.Discount;
            content = $"{discount}%";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 375, 240 + 20 * indexLine, formatFar);

            content = "Phải trả:";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 280, 260 + 20 * indexLine, formatFar);

            double billFinalPrice = billTotalPrice - billTotalPrice * discount / 100;
            content = $"{billFinalPrice.ToString("c0", culture)}";
            e.Graphics.DrawString(content, fontBold, Brushes.Green, 375, 260 + 20 * indexLine, formatFar);

        }
    }
}
