using Documents_Toshmatov.Classes;
using Documents_Toshmatov.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Documents_Toshmatov.Elements
{
    /// <summary> Логика взаимодействия для Item.xaml </summary>
    public partial class Item : UserControl
    {
        /// <summary> Документ для изменения или удаления </summary>
        DocumentContext Document;

        public Item(DocumentContext document)
        {
            InitializeComponent();

            // Выводим изображение
            if (!string.IsNullOrEmpty(document.src))
            {
                img.Source = new BitmapImage(new Uri(document.src, UriKind.RelativeOrAbsolute));
            }

            // Выводим данные
            lName.Text = document.name;
            lUser.Text = $"Ответственный: {document.user}";
            lCode.Text = $"Код документа: {document.id_document}";
            lDate.Text = $"Дата поступления: {document.date.ToString("dd.MM.yyyy")}";
            lStatus.Text = document.status == 0 ? "Статус: Входящий" : "Статус: Исходящий";
            lDirect.Text = "Направление: " + document.vector;

            // Сохраняем документ для изменения или удаления
            this.Document = document;
        }

        /// <summary> Редактирование документа </summary>
        private void EditDocument_Click(object sender, RoutedEventArgs e)
        {
            // Временная заглушка - нужно реализовать навигацию
            MessageBox.Show($"Редактирование документа: {Document.name}");
            /*
            // Открываем страницу изменения передавая документ
            MainWindow.init.frame.Navigate(new Pages.Add(Document));
            */
        }

        /// <summary> Удаление документа </summary>
        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить документ: {Document.name}?",
                "Подтверждение удаления", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Удаляем документ
                    Document.Delete();
                    MessageBox.Show("Документ успешно удален");

                    // Временная заглушка - нужно реализовать обновление списка
                    /*
                    // Обновляем документы из БД
                    MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
                    // Открываем страницу Main
                    MainWindow.init.OpenPages(MainWindow.pages.main);
                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении документа: {ex.Message}");
                }
            }
        }
    }
}