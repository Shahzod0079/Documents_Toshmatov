using System.Windows;
using System.Collections.Generic;
using Documents_Toshmatov.Pages;
using Documents_Toshmatov.Classes;

namespace Documents_Toshmatov
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public List<DocumentContext> AllDocuments = new DocumentContext().AllDocuments();
        public enum pages
        {
            main, 
            add   
        }

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.main); 
        }

        public void OpenPages(pages _pages)
        {
            if (_pages == pages.main) 
                frame.Navigate(new Main()); 
            else if (_pages == pages.add) 
            {
                MessageBox.Show("Страница добавления пока не реализована");
                // frame.Navigate(new Add());
            }
        }
    }
}