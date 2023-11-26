using DvdFormApp.Repositories;
using DvdFormApp.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace DvdFormApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create Database Context
            var dbContext = new MediaContext();
            dbContext.Database.EnsureCreated();

            // Initialize Services and Repositories
            var logger = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("DvdFormApp.Program", LogLevel.Debug)
                       .AddConsole();
            });
            var bookshelfRepository = new BookshelfRepository(dbContext, logger);
            var bookshelfService = new BookshelfService(bookshelfRepository, logger);
            var itemRepository = new ItemRepository(dbContext, logger);
            var itemService = new ItemService(itemRepository, logger);

            Application.Run(new Form1(bookshelfService, itemService, logger));
        }
    }
}
