using CanteenManager.DAO;
using Help;
using Interfaces;
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

            //Config.RegisterSQLite();
            //Config.RegisterSQLServer();
            Config.RegisterEntity();
            
            //Chọn đường dẫn cho thư mục chứa file log
            log4net.GlobalContext.Properties["LogPath"] = Application.StartupPath;
            //Hiện tên DataProvider trong message log
            log4net.GlobalContext.Properties["DataProvider"] = Config.DataProvider.Name;
            //Cấu hình đặt trong App.config
            log4net.Config.XmlConfigurator.Configure();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;

                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log.Error($"UnhandledException - {ex.Message}");
            };
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(e.Exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log.Error($"UnhandledException - {e.Exception.Message}");
            };

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new fLogin());
        }
    }

    static class Config
    {
        public static UnityContainer Container { get; private set; } //= new UnityContainer();

        public static IDataProvider DataProvider { get; private set; }

        public static string ConnectionString { get; private set; }

        /// <summary>
        /// Dùng SQLite.
        /// </summary>
        public static void RegisterSQLite()
        {
            Container = new UnityContainer();

            Container.RegisterInstance<IAccountDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.AccountDAO), true) as SQLiteDataAccess.AccountDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDAO>(SQLiteDataAccess.BillDAO.Instance, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDetailDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.BillDetailDAO), true) as SQLiteDataAccess.BillDetailDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillInfoDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.BillInfoDAO), true) as SQLiteDataAccess.BillInfoDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ICategoryDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.CategoryDAO), true) as SQLiteDataAccess.CategoryDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IFoodDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.FoodDAO), true) as SQLiteDataAccess.FoodDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ITableDAO>(Activator.CreateInstance(typeof(SQLiteDataAccess.TableDAO), true) as SQLiteDataAccess.TableDAO, InstanceLifetime.Singleton);

            DataProvider = SQLiteDataAccess.DataProvider.Instance;
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;
        }

        /// <summary>
        /// Dùng SQL Server.
        /// </summary>
        public static void RegisterSQLServer()
        {
            Container = new UnityContainer();

            Container.RegisterInstance<IAccountDAO>(Activator.CreateInstance(typeof(AccountDAO), true) as AccountDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDAO>(Activator.CreateInstance(typeof(BillDAO), true) as BillDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDetailDAO>(Activator.CreateInstance(typeof(BillDetailDAO), true) as BillDetailDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillInfoDAO>(Activator.CreateInstance(typeof(BillInfoDAO), true) as BillInfoDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ICategoryDAO>(Activator.CreateInstance(typeof(CategoryDAO), true) as CategoryDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IFoodDAO>(Activator.CreateInstance(typeof(FoodDAO), true) as FoodDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ITableDAO>(Activator.CreateInstance(typeof(TableDAO), true) as TableDAO, InstanceLifetime.Singleton);

            DataProvider = CanteenManager.DAO.DataProvider.Instance;
            //ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString.Replace("{ApplicationFolder}", Application.StartupPath);
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;

        }

        public static void RegisterEntity()
        {
            Container = new UnityContainer();

            Container.RegisterInstance<IAccountDAO>(Activator.CreateInstance(typeof(EntityDataAccess.AccountDAO), true) as EntityDataAccess.AccountDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDAO>(Activator.CreateInstance(typeof(EntityDataAccess.BillDAO), true) as EntityDataAccess.BillDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillDetailDAO>(Activator.CreateInstance(typeof(EntityDataAccess.BillDetailDAO), true) as EntityDataAccess.BillDetailDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IBillInfoDAO>(Activator.CreateInstance(typeof(EntityDataAccess.BillInfoDAO), true) as EntityDataAccess.BillInfoDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ICategoryDAO>(Activator.CreateInstance(typeof(EntityDataAccess.CategoryDAO), true) as EntityDataAccess.CategoryDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<IFoodDAO>(Activator.CreateInstance(typeof(EntityDataAccess.FoodDAO), true) as EntityDataAccess.FoodDAO, InstanceLifetime.Singleton);
            Container.RegisterInstance<ITableDAO>(Activator.CreateInstance(typeof(EntityDataAccess.TableDAO), true) as EntityDataAccess.TableDAO, InstanceLifetime.Singleton);

            DataProvider = EntityDataAccess.DataProvider.Instance;
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
        }
    }
}