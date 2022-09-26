using CanteenManager.DTO;

namespace CanteenManager.Interface
{
    public interface IAccountDAO
    {
        bool DeleteAccount(string userName);
        List<Account> GetListAccount();
        bool InsertAccount(string userName);
        Account Login(string userName, string passWord);
        bool Update(string userName, string displayName = null, string passWord = null, int? accType = null);
    }
}