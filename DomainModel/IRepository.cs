using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public interface IRepository
    {
        List<Journal> Load();
        void Save(List<Journal> listJournal);
        void Create(Journal journal);
        void Edit(Journal journal);
        void Delete(Journal journal);
        Profit SummProfit();
    }
}
