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
        /// <summary>Список <inheritdoc cref="MedClinic"/></summary>
        DbSet<MedClinic> MedClinics { get; }

        /// <summary>Список <inheritdoc cref="Doctor"/></summary>
        DbSet<Doctor> Doctors { get; }

        /// <summary>Список <inheritdoc cref="Diagnosis"/></summary>
        DbSet<Diagnosis> Diagnosises { get; }

        /// <summary>Список <inheritdoc cref="TimeTable"/></summary>
        DbSet<TimeTable> TimeTables { get; }

        /// <summary>Список <inheritdoc cref="Patient"/></summary>
        DbSet<Patient> Patients { get; }

        /// <summary>Список <inheritdoc cref="BookingAppointment"/></summary>
        DbSet<BookingAppointment> BookingAppointments { get; }
       

    }
}