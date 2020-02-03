using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class JournalController : Controller
    {
        private IRepository instance;

       /* public JournalController()
        { }*/
        public JournalController(IRepository _instance)
        {
            instance = _instance;
        }
        public ActionResult Index()
        {
            List<Journal> journal = instance.Load();
            return View(journal);
        }

        public ActionResult Create()
        {
            var journal = new Journal();
            return View(journal);
        }
        [HttpPost]
        public ActionResult Create(Journal journal)
        {
            try
            {
                instance.Create(journal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Journal/Edit/5
        public ActionResult Edit(int id)
        {

            List<Journal> listJournal = instance.Load();
            Journal journal = listJournal.Find(j => j.Id == id);

            return View(journal);
        }

        // POST: Journal/Edit/5
        [HttpPost]
        public ActionResult Edit(Journal journal)
        {
            try
            {
                instance.Edit(journal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Journal/Delete/5
        public ActionResult Delete(int id)
        {
            List<Journal> listJournal = instance.Load();
            Journal journal = listJournal.Find(j => j.Id == id);

            return View(journal);
        }

        // POST: Journal/Delete/5
        [HttpPost]
        public ActionResult Delete(Journal journal)
        {
            try
            {
                instance.Delete(journal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SummProfit()
        {
            Profit profit = new Profit();
            List<Journal> journalList = instance.Load();
            for (int i = 0; i < journalList.Count(); i++)
            {
                if (Convert.ToInt32((DateTime.Now - journalList[i].Date).Days) <= 7)
                    profit.Summ += journalList[i].Cost;
            }
            return View(profit);
        }
    }
}
