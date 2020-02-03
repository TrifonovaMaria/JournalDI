using DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessJSON
{
    public class DataJSON: IRepository
    {
        string path;

        public DataJSON(string str)
        {
            path = str;
        }
        public DataJSON()
        {

        }
        public List<Journal> Load()
        {
            List<Journal> journal = new List<Journal>();
            using (StreamReader fs = File.OpenText("F:\\journal.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                journal = (List<Journal>)serializer.Deserialize(fs, typeof(List<Journal>));
            }
            return journal;
        }

        public void Save(List<Journal> listJournal)
        {

            using (StreamWriter fs = new StreamWriter("F:\\journal.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(fs, listJournal);
            }
        }

        public void Create(Journal journal)
        {
            List<Journal> listJournal = Load();
            journal.Id = listJournal.Count + 1;
            listJournal.Add(journal);
            Save(listJournal);
        }

        public void Edit(Journal journal)
        {
            List<Journal> listJournal = Load();
            int i = listJournal.FindIndex(j => j.Id == journal.Id);
            listJournal[i] = journal;
            Save(listJournal);
        }

        public void Delete(Journal journal)
        {
            List<Journal> listJournal = Load();
            listJournal.Remove(listJournal.Find(j => j.Id == journal.Id));
            Save(listJournal);
        }

        public Profit SummProfit()
        {
            Profit profit = new Profit();
            List<Journal> journalList = Load();
            for (int i = 0; i < journalList.Count(); i++)
            {
                if (Convert.ToInt32((DateTime.Now - journalList[i].Date).Days) <= 7)
                    profit.Summ += journalList[i].Cost;
            }
            return profit;
        }

    }
}
