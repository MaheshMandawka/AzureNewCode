﻿using DvdFormApp.Repositories;
using DvdFormApp.Services;
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
            var bookshelfRepository = new BookshelfRepository(dbContext);
            var bookshelfService = new BookshelfService(bookshelfRepository);
            var itemRepository = new ItemRepository(dbContext);
            var itemService = new ItemService(itemRepository);

            Application.Run(new Form1(bookshelfService, itemService));
        }
    }
}
