using System.Windows;
using System.Collections.Generic;
using Documents_Toshmatov.Pages;
using Documents_Toshmatov.Classes;

namespace Documents_Toshmatov
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public List<DocumentContext> AllDocuments { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            // Загружаем документы при запуске
            AllDocuments = new DocumentContext().AllDocuments();
            OpenPages("main");
        }

        public void OpenPages(string page)
        {
            switch (page)
            {
                case "main":
                    frame.Navigate(new Main());
                    break;
                case "add":
                    // frame.Navigate(new Add());
                    break;
            }
        }
    }
}