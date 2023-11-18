using Clinic.Context.Contracts.Interface;
using Clinic.Context.Contracts.Models;
using System.Xml;

namespace Clinic.Context
{
    public class ClinicContext : IClinicContext
    {
        private readonly IList<MedClinic> medClinic;
        private readonly IList<Doctor> doctor;
        private readonly IList<Department> department;
        private readonly IList<TimeTable> timeTable;
        private readonly IList<Patient> patient;
        private readonly IList<BookingAppointment> bookingAppointment;

        public ClinicContext()
        {
            medClinic = new List<MedClinic>();
            doctor = new List<Doctor>();
            department = new List<Department>();
            timeTable = new List<TimeTable>();
            patient = new List<Patient>();
            bookingAppointment = new List<BookingAppointment>();
        }

        IEnumerable<MedClinic> IClinicContext.MedClinic => medClinic;
        IEnumerable<Doctor> IClinicContext.Doctor => doctor;
        IEnumerable<Department> IClinicContext.Department => department;
        IEnumerable<TimeTable> IClinicContext.TimeTable => timeTable;
        IEnumerable<Patient> IClinicContext.Patient => patient;
        IEnumerable<BookingAppointment> IClinicContext.BookingAppointment => bookingAppointment;

    }
}
