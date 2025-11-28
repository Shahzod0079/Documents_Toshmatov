using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;
using Documents_Toshmatov.Interfaces;
using Documents_Toshmatov.Model;

namespace Documents_Toshmatov.Classes
{
    public class DocumentContext : Document, IDocument
    {
        public DocumentContext() { }

        public DocumentContext(int id, string src, string name, string user, string id_document, DateTime date, int status, string vector)
            : base(id, src, name, user, id_document, date, status, vector)  // ИЗМЕНИТЬ ТИП id_document
        {
        }

        /// <summary> Метод сохранения </summary>
        public void Save(bool update = false)
        {
            if (update)
            {
                OleDbConnection connection = Common.DBConnect.Connection();
                Common.DBConnect.Query("UPDATE [Документы] " +
                "SET " +
                $"[Изображение] = '{this.src}', " +
                $"[Наименование] = '{this.name}', " +
                $"[Ответственный] = '{this.user}', " +
                $"[Код документа] = '{this.id_document}', " +  // УБРАТЬ Parse, т.к. теперь string
                $"[Дата поступления] = '{this.date.ToString("dd.MM.yyyy")}', " +
                $"[Статус] = {this.status}, " +
                $"[Направление] = '{this.vector}' " +
                $"WHERE [Код] = {this.id}", connection);
                Common.DBConnect.CloseConnection(connection);
            }
            else
            {
                OleDbConnection connection = Common.DBConnect.Connection();
                Common.DBConnect.Query("INSERT INTO " +
                "[Документы] " +
                "([Изображение], " +
                "[Наименование], " +
                "[Ответственный], " +
                "[Код документа], " +
                "[Дата поступления], " +
                "[Статус], " +
                "[Направление]) " +
                "VALUES (" +
                $"'{this.src}', " +
                $"'{this.name}', " +
                $"'{this.user}', " +
                $"'{this.id_document}', " +  // УБРАТЬ Parse, т.к. теперь string
                $"'{this.date.ToString("dd.MM.yyyy")}', " +
                $"{this.status}, " +
                $"'{this.vector}')", connection);
                Common.DBConnect.CloseConnection(connection);
            }
        }

        /// <summary> Список всех документов </summary>
        public List<DocumentContext> AllDocuments()
        {
            List<DocumentContext> allDocuments = new List<DocumentContext>();
            OleDbConnection connection = Common.DBConnect.Connection();

            try
            {
                OleDbDataReader dataDocuments = Common.DBConnect.Query("SELECT * FROM [Документы]", connection);

                // Получаем количество столбцов
                int fieldCount = dataDocuments.FieldCount;
                MessageBox.Show($"Количество столбцов в таблице: {fieldCount}");

                if (dataDocuments.HasRows)
                {
                    while (dataDocuments.Read())
                    {
                        DocumentContext newDocument = new DocumentContext();

                        // Заполняем только те поля, для которых есть столбцы
                        if (fieldCount > 0) newDocument.id = dataDocuments.IsDBNull(0) ? 0 : dataDocuments.GetInt32(0);
                        if (fieldCount > 1) newDocument.src = dataDocuments.IsDBNull(1) ? "" : dataDocuments.GetString(1);
                        if (fieldCount > 2) newDocument.name = dataDocuments.IsDBNull(2) ? "" : dataDocuments.GetString(2);
                        if (fieldCount > 3) newDocument.user = dataDocuments.IsDBNull(3) ? "" : dataDocuments.GetString(3);
                        if (fieldCount > 4) newDocument.id_document = dataDocuments.IsDBNull(4) ? "" : dataDocuments.GetString(4);
                        if (fieldCount > 5) newDocument.date = dataDocuments.IsDBNull(5) ? DateTime.Now : dataDocuments.GetDateTime(5);
                        if (fieldCount > 6) newDocument.status = dataDocuments.IsDBNull(6) ? 0 : dataDocuments.GetInt32(6);
                        if (fieldCount > 7) newDocument.vector = dataDocuments.IsDBNull(7) ? "" : dataDocuments.GetString(7);

                        allDocuments.Add(newDocument);
                    }
                }
                else
                {
                    MessageBox.Show("Таблица пустая");
                }

                dataDocuments.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}\nПодробности: {ex.StackTrace}");
            }
            finally
            {
                Common.DBConnect.CloseConnection(connection);
            }

            return allDocuments;
        }
        public void Delete()
        {
            OleDbConnection connection = Common.DBConnect.Connection();

            var command = new OleDbCommand("DELETE FROM [Документы] WHERE [Код] = ?", connection);
            command.Parameters.AddWithValue("@id", this.id);
            command.ExecuteNonQuery();

            Common.DBConnect.CloseConnection(connection);
        }
    }
}