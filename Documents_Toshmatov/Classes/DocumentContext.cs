using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Documents_Toshmatov.Interfaces;
using Documents_Toshmatov.Model;

namespace Documents_Toshmatov.Classes
{
    public class DocumentContext : Document, IDocument
    {
        public DocumentContext() { }

        public DocumentContext(int id, string src, string name, string user, int id_document, DateTime date, int status, string vector)
            : base(id, src, name, user, id_document, date, status, vector)
        {
        }

        /// <summary> Метод сохранения </summary>
        public void Save(bool Update = false)
        {
            if (Update)
            {
                OleDbConnection connection = Common.DBConnect.Connection();
                Common.DBConnect.Query("UPDATE [Документы] " +
                "SET " +
                $"[Изображение] = '{this.src}', " +
                $"[Наименование] = '{this.name}', " +
                $"[Ответственный] = '{this.user}', " +
                $"[Код документа] = '{this.id_document}', " +
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
                $"'{this.id_document}', " +
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

                while (dataDocuments.Read())
                {
                    DocumentContext newDocument = new DocumentContext();
                    newDocument.id = dataDocuments.GetInt32(0);
                    newDocument.src = dataDocuments.GetString(1);
                    newDocument.name = dataDocuments.GetString(2);
                    newDocument.user = dataDocuments.GetString(3);
                    newDocument.id_document = dataDocuments.GetInt32(4);
                    newDocument.date = dataDocuments.GetDateTime(5);
                    newDocument.status = dataDocuments.GetInt32(6);
                    newDocument.vector = dataDocuments.GetString(7);

                    allDocuments.Add(newDocument);
                }

                dataDocuments.Close();
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