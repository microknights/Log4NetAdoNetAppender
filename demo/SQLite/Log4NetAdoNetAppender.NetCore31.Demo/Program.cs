using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net.Util;
using Microsoft.Data.Sqlite;
using MicroKnights.Log4NetHelper;

namespace Log4NetAdoNetAppender.NetCore31.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Data Source=\"./log4net.db\"";

            // Create Log Table
            //创建表格
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                //Create a table (if it already exists, delete it first)
                //创建一个表（如果已经存在，则先删除）
                Console.WriteLine("Delete Table Log ......");
                Console.WriteLine("删除日志表 ......");
                await using var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Log";
                delTableCmd.ExecuteNonQuery();
                Console.WriteLine("Create Table Log ......");
                Console.WriteLine("创建日志表 ......");
                await using var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Log (" +
                                             "Id INTEGER PRIMARY KEY," +
                                             "Date DATETIME NOT NULL," +
                                             "Thread VARCHAR(255) NOT NULL," +
                                             "Level VARCHAR(50) NOT NULL," +
                                             "Logger VARCHAR(255) NOT NULL," +
                                             "Message TEXT DEFAULT NULL," +
                                             "Exception TEXT DEFAULT NULL" +
                                             "); ";
                createTableCmd.ExecuteNonQuery();
            }
            Console.WriteLine("Create Table Log Success ......");
            // Load configuration
            const string Log4netConfigFilename = "log4net.config";

            if (File.Exists(Log4netConfigFilename) == false)
            {
                throw new FileNotFoundException($"{Log4netConfigFilename} not found", Log4netConfigFilename);
            }

#if DEBUG
            InternalDebugHelper.EnableInternalDebug(delegate (object source, LogReceivedEventArgs eventArgs)
            {
                Console.WriteLine(eventArgs.LogLog.Message);
                if (eventArgs.LogLog.Exception != null)
                    Console.WriteLine(eventArgs.LogLog.Exception.StackTrace);
            });
#endif

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Log4netConfigFilename));

            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            // Analog input some logs
            //模拟输入一些日志
            log.Info("This is log information of an analog input info (Info)");
            Console.WriteLine("这是模拟输入信息（info）的日志信息");
            log.Debug("This is log information of an analog input info (Debug)");
            Console.WriteLine("这是模拟输入信息（Debug）的日志信息");
            log.Warn("This is log information of an analog input info (Warn)");
            Console.WriteLine("这是模拟输入信息（Warn）的日志信息");
            log.Error("This is log information of an analog input info (Error)");
            Console.WriteLine("这是模拟输入信息（Error）的日志信息");


            // Show logged data from the database
            //显示数据库中记录的数据
            await ShowLogDataFromDatabaseWith(connectionString);

            Console.WriteLine("Press Enter to continue");
            Console.WriteLine("按Enter键继续");
            Console.ReadLine();
        }

        private static async Task ShowLogDataFromDatabaseWith(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);

            connection.Open();

            await using var selectCmd = connection.CreateCommand();

            selectCmd.CommandText = "select * from Log";

            await using var reader = await selectCmd.ExecuteReaderAsync();

            Console.WriteLine(string.Join("\t", Enumerable.Range(0, reader.FieldCount)
                                                          .Select(i => reader.GetName(i))));

            while (await reader.ReadAsync())
            {
                Console.WriteLine(string.Join("\t", Enumerable.Range(0, reader.FieldCount)
                                                              .Select(i => reader.GetString(i))));
            }
        }
    }
}
