using Clinic.Context.Contracts.Models;
using System;
using System.Xml;

namespace Clinic.Context.Contracts.Interface
{
    public interface IClinicContext
    {
        IEnumerable<MedClinic> MedClinic { get; }
        IEnumerable<Doctor> Doctor { get; }
        IEnumerable<Department> Department { get; }
        IEnumerable<TimeTable> TimeTable { get; }
        IEnumerable<Patient> Patient { get; }
        IEnumerable<BookingAppointment> BookingAppointment { get; }
    }
}