using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using System.Security.Claims;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Service.Implementation;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly IPatientService _patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, IPatientService patientService)
        {
            _vaccinationCenterService = vaccinationCenterService;
            _patientService = patientService;
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetVaccinationCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);

            if (center == null)
            {
                return NotFound();
            }

            return View(center);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                _vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (center == null)
            {
                return NotFound();
            }
            return View(center);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (center == null)
            {
                return NotFound();
            }

            return View(center);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _vaccinationCenterService.DeleteVaccinationCenter(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetVaccinationCenterById(id) != null;
        }

        public IActionResult AddVaccinatedPatient(Guid id)
        {
            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (vaccinationCenter.MaxCapacity <= 0)
            {
                return RedirectToAction(nameof(NoMoreCapacity));
            }
            VaccinationDTO dto = new VaccinationDTO();
            dto.vaccinationCenterId = id;
            dto.patients = _patientService.GetPatients();
            dto.manufacturers = new List<string>()
            {
                "prv", "vtor", "tret"
            };

            return View(dto);
        }

        /* [HttpPost, ActionName("Schedule")]
         [ValidateAntiForgeryToken]
         public IActionResult ConfirmVaccination(VaccinationDTO dto)
         {
             if (ModelState.IsValid)
             {
                 var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(dto.vaccinationCenterId);
                 vaccinationCenter.MaxCapacity--;
                 _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                 _vaccinationCenterService.AddVaccinatedPatient(dto);
                 return RedirectToAction(nameof(Index));

             }

             return View(dto);
         }*/
        [HttpPost, ActionName("Schedule")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmVaccination(VaccinationDTO dto)
        {
            if (ModelState.IsValid)
            {
                var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenterById(dto.vaccinationCenterId);
                vaccinationCenter.MaxCapacity--;
                _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                _vaccinationCenterService.AddVaccinatedPatient(dto);

                var updatedCenter = _vaccinationCenterService.GetVaccinationCenterById(dto.vaccinationCenterId);

                return View("Details", updatedCenter);
            }

            return View(dto);
        }

        public IActionResult NoMoreCapacity()
        {
            return View();
        }

    }
}
