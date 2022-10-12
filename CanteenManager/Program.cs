using CanteenManager.DAO;
using CanteenManager.Interface;
using Unity;

namespace CanteenManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Config.RegisterSQLite();

            //Config.RegisterSQLServer();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new fLogin());
        }
    }

    static class Config
    {
        public static UnityContainer Container { get; } = new UnityContainer();

        /// <summary>
        /// Dùng SQLite.
        /// </summary>
        public static void RegisterSQLite()
        {
            Container.RegisterInstance<IDataProvider>(Activator.CreateInstance(typeof(SQLiteDataProvider), true) as SQLiteDataProvider, InstanceLifetime.Singleton);
            Container.RegisterInstance<IAccountDAO>(Activator.CreateInstance(typeof(SQLiteAccountDAO), true) as SQLiteAccountDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDAO>(Activator.CreateInstance(typeof(SQLiteBillDAO), true) as SQLiteBillDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDetailDAO>(Activator.CreateInstance(typeof(SQLiteBillDetailDAO), true) as SQLiteBillDetailDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillInfoDAO>(Activator.CreateInstance(typeof(SQLiteBillInfoDAO), true) as SQLiteBillInfoDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ICategoryDAO>(Activator.CreateInstance(typeof(SQLiteCategoryDAO), true) as SQLiteCategoryDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IFoodDAO>(Activator.CreateInstance(typeof(SQLiteFoodDAO), true) as SQLiteFoodDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ITableDAO>(Activator.CreateInstance(typeof(SQLiteTableDAO), true) as SQLiteTableDAO, InstanceLifetime.Singleton);

        }

        /// <summary>
        /// Dùng SQL Server.
        /// </summary>
        public static void RegisterSQLServer()
        {
            Container.RegisterInstance<IDataProvider>(Activator.CreateInstance(typeof(DataProvider), true) as DataProvider, InstanceLifetime.Singleton);
            Container.RegisterInstance<IAccountDAO>(Activator.CreateInstance(typeof(AccountDAO), true) as AccountDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDAO>(Activator.CreateInstance(typeof(BillDAO), true) as BillDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDetailDAO>(Activator.CreateInstance(typeof(BillDetailDAO), true) as BillDetailDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillInfoDAO>(Activator.CreateInstance(typeof(BillInfoDAO), true) as BillInfoDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ICategoryDAO>(Activator.CreateInstance(typeof(CategoryDAO), true) as CategoryDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IFoodDAO>(Activator.CreateInstance(typeof(FoodDAO), true) as FoodDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ITableDAO>(Activator.CreateInstance(typeof(TableDAO), true) as TableDAO, InstanceLifetime.Singleton);
        }
    }
}