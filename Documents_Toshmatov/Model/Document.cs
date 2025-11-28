using System;

namespace Documents_Toshmatov.Model
{
    public class Document
    {
        public int id { get; set; }

        public string src { get; set; }

        public string name { get; set; }

        public string user { get; set; }

        public int id_document { get; set; }

        public DateTime date { get; set; }

        public int status { get; set; }

        public string vector { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Document() { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public Document(int id, string src, string name, string user, int id_document, DateTime date, int status, string vector)
        {
            this.id = id;
            this.src = src;
            this.name = name;
            this.user = user;
            this.id_document = id_document;
            this.date = date;
            this.status = status;
            this.vector = vector;
        }
    }
}