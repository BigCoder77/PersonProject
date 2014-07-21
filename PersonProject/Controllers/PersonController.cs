using System.Threading.Tasks;
using System.Web.Mvc;
using PersonProject.Model;

namespace PersonProject.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult All()
        {
            using (var repository = NewRepository())
            {
                return View(repository.All());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            using (var repository = NewRepository())
            {
                repository.Create(person);
            }

            return new RedirectResult("/People");
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Person person)
        {
            using (var repository = NewRepository())
            {
                await repository.CreateAsync(person);
            }

            return new RedirectResult("/People");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var repository = NewRepository())
            {
                var person = repository.GetById(id);
                return View(person);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            using (var repository = NewRepository())
            {
                repository.Update(person);
            }

            return new RedirectResult("/People");
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(int id, Person person)
        {
            using (var repository = NewRepository())
            {
                await repository.UpdateAsync(person);
            }

            return new RedirectResult("/People");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var repository = NewRepository())
            {
                var person = repository.GetById(id);
                return View(person);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var repository = NewRepository())
            {
                repository.Delete(id);
                return new RedirectResult("/People");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var repository = NewRepository())
            {
                await repository.DeleteAsync(id);
                return new RedirectResult("/People");
            }
        }

        protected virtual PersonRepository NewRepository()
        {
            return new PersonRepository();
        }
    }
}