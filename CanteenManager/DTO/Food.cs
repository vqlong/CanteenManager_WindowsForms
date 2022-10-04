using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Tượng trưng cho bảng Food của database.
    /// </summary>
    public class Food
    {
        public Food(int id, string name, int categoryID, double price, UsingState foodStatus)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
            this.FoodStatus = foodStatus;
        }

        /// <summary>
        /// Khởi tạo đối tượng từ 1 hàng của bảng Food.
        /// </summary>
        /// <param name="row"></param>
        public Food(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = row["Name"].ToString();
            this.CategoryID = (int)row["CategoryID"];
            this.Price = (double)row["Price"];
            this.FoodStatus = (UsingState)row["FoodStatus"];
        }

        private int iD;
        private string name;
        private int categoryID;
        private double price;
        private UsingState foodStatus;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public double Price { get => price; set => price = value; }
        public UsingState FoodStatus { get => foodStatus; set => foodStatus = value; }
    }

    /// <summary>
    /// Trạng thái sử dụng.
    /// </summary>
    [TypeConverter(typeof(UsingStateConverter))]
    public enum UsingState
    {
        /// <summary>
        /// Đang được phục vụ.
        /// </summary>
        [Description("Đang bán")]
        Serving = 1,

        /// <summary>
        /// Đã dừng phục vụ.
        /// </summary>
        [Description("Dừng bán")]
        StopServing = 0
    }

    public class UsingStateConverter : EnumConverter
    {
        public UsingStateConverter([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicFields)] Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(string))
            {
                FieldInfo info = typeof(UsingState).GetField(value.ToString());
                DescriptionAttribute description = info?.GetCustomAttribute<DescriptionAttribute>();

                return description?.Description;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
