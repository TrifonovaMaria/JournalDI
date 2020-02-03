using DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccessXML
{
    public class DataXML: IRepository
    {
        string path;

        public DataXML(string str)
        {
            path = str;
        }
        public DataXML()
        {

        }
        public List<Journal> Load()
        {
            List<Journal> journal = new List<Journal>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<Journal>));
            using (FileStream fs = File.OpenRead("F:\\journal.xml"))
            {
                journal = (List<Journal>)formatter.Deserialize(fs);
            }
            return journal;
        }

        public void Save(List<Journal> listJournal)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Journal>));
            using (FileStream fs = new FileStream("F:\\journal.xml", FileMode.Create))
            {
                formatter.Serialize(fs, listJournal);
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
            for (int i = journal.Id - 1; i < listJournal.Count; i++)
                listJournal[i].Id = i + 1;
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
