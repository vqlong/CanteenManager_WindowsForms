using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Tượng trưng cho bảng Account của database.
    /// </summary>
    public class Account
    {
        public Account(string userName, string displayName, AccountType accType, string passWord = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.AccType = accType;
            this.PassWord = passWord;
        }

        public Account(DataRow row)
        {
            this.UserName = row["UserName"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.AccType = (AccountType)row["AccType"];
            //this.PassWord = row["PassWord"].ToString();
        }

        private string userName;
        private string displayName;
        private string passWord;
        private AccountType accType;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public AccountType AccType { get => accType; set => accType = value; }
    }

    [TypeConverter(typeof(AccountTypeConverter))]
    public enum AccountType
    {
        [Description("Quản lý")]
        Admin = 1,
        [Description("Nhân viên")]
        Staff = 0
    }

    public class AccountTypeConverter : EnumConverter
    {
        public AccountTypeConverter([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicFields)] Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                FieldInfo info = typeof(AccountType).GetField(value.ToString());
                DescriptionAttribute description = info?.GetCustomAttribute<DescriptionAttribute>();

                return description?.Description;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
