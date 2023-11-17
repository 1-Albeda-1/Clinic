using Clinic.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Xml;

namespace Clinic.Context.Contracts.Interface
{
    public interface IClinicContext
    {
        IEnumerable<MedClinic> MedClinic { get; }

        IEnumerable<Doctor> Doctor { get; }

        IEnumerable<Diagnosis> Diagnosis { get; }

        IEnumerable<TimeTable> TimeTable { get; }

        IEnumerable<Patient> Patient { get; }

        IEnumerable<BookingAppointment> BookingAppointment { get; }

    }
}