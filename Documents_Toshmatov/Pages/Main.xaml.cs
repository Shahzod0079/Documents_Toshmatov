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
            LoadDocuments();
        }

        /// <summary> Загрузка документов </summary>
        private void LoadDocuments()
        {
            // TODO: Загрузить документы из базы данных и добавить в parent
            // Пример:
            // var documents = new DocumentContext().AllDocuments();
            // foreach (var doc in documents)
            // {
            //     parent.Children.Add(new Elements.Item(doc));
            // }
        }

        private void AddDocuments_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.OpenPages("add");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
        public void CreatedUI()
        {
            parent.Children.Clear();

                foreach (DocumentContext document in MainWindow.init.AllDocuments)
                {
                    parent.Children.Add(new Elements.Item(document));
                }
            }
        }
    }
