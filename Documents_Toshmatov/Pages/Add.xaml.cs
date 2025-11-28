using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Documents_Toshmatov.Classes;
using Documents_Toshmatov.Model;
using Microsoft.Win32;

namespace Documents_Toshmatov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add()
        {
            InitializeComponent();
        }
        public Document Document;

        /// <summary> Ссылка до изображения </summary>
        public string s_src = "";

        public Add(Document Document = null)
        {
            InitializeComponent();

            // Если входящие данные не пустые (происходит изменение)
            if (Document != null)
            {
                // Запоминаем элемент
                this.Document = Document;

                // Если файл существует
                if (File.Exists(Document.src))
                {
                    // Запоминаем ссылку
                    s_src = Document.src;
                    // Выводим изображение
                    src.Source = new BitmapImage(new Uri(s_src));
                }

                // Выводим данные
                tb_name.Text = this.Document.name;
                tb_user.Text = this.Document.user;
                tb_id.Text = this.Document.id_document.ToString();
                tb_date.Text = this.Document.date.ToString("dd.MM.yyyy");
                tb_status.SelectedIndex = this.Document.status;
                tb_vector.Text = this.Document.vector;
                btnAdd.Content = "Изменить";
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void tb_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            // Создаём диалог
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Указываем начальную директорию
            openFileDialog.InitialDirectory = "c:\\";
            // Указываем типы выбранных файлов
            openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";
            // Указываем приоритетный тип файла
            openFileDialog.FilterIndex = 1;
            // Открываем диалог
            if (openFileDialog.ShowDialog() == true)
            {
                // Если файл выбран
                if (openFileDialog.FileName != "")
                {
                    // Выводим изображение
                    src.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    // Сохраняем ссылку
                    s_src = openFileDialog.FileName;
                }
            }
        }


        private void tb_vector_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tb_status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем поля на пустоту
            if (s_src.Length == 0)
            {
                MessageBox.Show("Выберите изображение");
                return;
            }
            if (tb_name.Text.Length == 0)
            {
                MessageBox.Show("Укажите наименование");
                return;
            }
            if (tb_user.Text.Length == 0)
            {
                MessageBox.Show("Укажите ответственного");
                return;
            }
            if (tb_id.Text.Length == 0)
            {
                MessageBox.Show("Укажите код документа");
                return;
            }
            if (tb_date.Text.Length == 0)
            {
                MessageBox.Show("Укажите дату поступления");
                return;
            }
            if (tb_status.SelectedIndex == -1)
            {
                MessageBox.Show("Укажите статус");
                return;
            }
            if (tb_vector.Text.Length == 0)
            {
                MessageBox.Show("Укажите направление");
                return;
            }

            if (Document == null) // если входящий документ отсутствует
            {
                DocumentContext newDocument = new DocumentContext();
                newDocument.src = s_src;
                newDocument.name = tb_name.Text;
                newDocument.user = tb_user.Text;
                newDocument.id_document = tb_id.Text; // ИСПРАВЛЕНО: убрано int.Parse()

                DateTime newDate = new DateTime();
                DateTime.TryParse(tb_date.Text, out newDate);
                newDocument.date = newDate;
                newDocument.status = tb_status.SelectedIndex;
                newDocument.vector = tb_vector.Text;
                newDocument.Save();
                MessageBox.Show("Документ добавлен.");
            }
            else
            {
                DocumentContext newDocument = new DocumentContext();
                newDocument.src = s_src;
                newDocument.id = Document.id;
                newDocument.name = tb_name.Text;
                newDocument.user = tb_user.Text;
                newDocument.id_document = tb_id.Text; // ИСПРАВЛЕНО: убрано int.Parse()

                DateTime newDate = new DateTime();
                DateTime.TryParse(tb_date.Text, out newDate);
                newDocument.date = newDate;
                newDocument.status = tb_status.SelectedIndex;
                newDocument.vector = tb_vector.Text;
                newDocument.Save(true);
                MessageBox.Show("Документ изменён.");
            }

            MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}
