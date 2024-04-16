using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenterRepository;
        private readonly IRepository<Vaccine> _vaccineRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository, IRepository<Vaccine> vaccineRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
            _vaccineRepository = vaccineRepository;
        }

        public void AddVaccinatedPatient(VaccinationDTO vaccinationDTO)
        {
           Vaccine vaccine = new Vaccine();
            vaccine.Manufacturer = vaccinationDTO.manufacturer;
            vaccine.Certificate = Guid.NewGuid();
            vaccine.PatientId = vaccinationDTO.patientId;
            vaccine.DateTaken = vaccinationDTO.vaccinationDate;
            vaccine.VaccinationCenter = vaccinationDTO.vaccinationCenterId;
            vaccine.Center =_vaccinationCenterRepository.Get(vaccinationDTO.vaccinationCenterId);
            _vaccineRepository.Insert(vaccine);
        }

        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccinationCenterRepository.Insert(vaccinationCenter);
        }

        public VaccinationCenter DeleteVaccinationCenter(Guid id)
        {
            var center_to_delete = this.GetVaccinationCenterById(id);
            return _vaccinationCenterRepository.Delete(center_to_delete);
        }

        public VaccinationCenter GetVaccinationCenterById(Guid? id)
        {
            return _vaccinationCenterRepository.Get(id);
        }

        public List<VaccinationCenter> GetVaccinationCenters()
        {
            return _vaccinationCenterRepository.GetAll().ToList();
        }


        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccinationCenterRepository.Update(vaccinationCenter);
        }

    }
}
