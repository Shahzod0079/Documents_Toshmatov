using System.Windows;
using System.Windows.Controls;
using Documents_Toshmatov.Classes;

namespace Documents_Toshmatov.Pages
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            CreatedUI();
        }

        /// <summary> Добавление записи </summary>
        private void AddDocuments_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.OpenPages(MainWindow.pages.add);
            }
        }

        /// <summary> Функция закрытия приложения </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary> Выводим элементы в интерфейс пользователя </summary>
        public void CreatedUI()
        {
            parent.Children.Clear();

            if (MainWindow.init?.AllDocuments != null)
            {
                foreach (DocumentContext document in MainWindow.init.AllDocuments)
                {
                    parent.Children.Add(new Elements.Item(document));
                }
            }
        }
    }
}