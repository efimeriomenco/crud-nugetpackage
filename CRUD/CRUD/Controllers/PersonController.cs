using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CRUD.Repository.Interfaces;
using CRUD.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult List(string searchValue)
        {
            var filteredCourses = _personRepository.FindPerson(searchValue).ToList();
            return View(filteredCourses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new Person();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = new Person()
                    {
                       Firstname = model.Firstname,
                       Lastname = model.Lastname,
                       Age = model.Age
                    };

                    _personRepository.Add(person);

                    return RedirectToAction(nameof(Create));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "The personRepository already exists");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Person currentPerson = _personRepository.GetPersonById(id);
            Person model = new Person()
            {
                Firstname = currentPerson.Firstname,
                Lastname = currentPerson.Lastname,
                Age = currentPerson.Age
            };
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person model)
        {
            Person updatedPerson = new Person()
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Age = model.Age
            };
            _personRepository.Edit(updatedPerson);
            return View(updatedPerson);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            return View(id);
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            var hasDeleted = _personRepository.Delete(id);

            if (!hasDeleted)
            {
                ModelState.AddModelError("message", $"Could not find courseViewModel with id {id}");
                return View("Delete");
            }
            return RedirectToAction("List", "Person");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var person = _personRepository.GetPersonById(id);

            return View(person);
        }
    }
}
