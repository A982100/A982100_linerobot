using Microsoft.AspNetCore.Mvc;

namespace A982100_linerobot.Controllers
{
    public class NpcController : Controller
    {
        // GET: NpcController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NpcController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NpcController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NpcController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NpcController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NpcController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NpcController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NpcController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
