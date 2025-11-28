using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents_Toshmatov.Classes;

namespace Documents_Toshmatov.Interfaces
{
    public interface IDocument
    {
        /// <summary> Метод сохранения </summary>
        /// <param name="update">Обновить существующую запись</param>
        void Save(bool update = false);

        List<DocumentContext> AllDocuments();


        void Delete();
    }
}