using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Event.Business.Category;
using Event.Data.Models;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        
        var registrationBusiness = new RegistrationBusiness();

        Console.WriteLine("This is the Save method!");

        var registration = new Registration
        {
            EventId = 1,
            VistorCode = "VC123",
            ParticipantName = "John Doe",
            ParticipantType = "VIP",
            AttendeeEmail = "john.doe@example.com",
            RegistrationDate = DateTime.Now,
            RegistrationPhone = "123-456-7890",
            Gender = true,
            FeePaid = 100.00M,
            Checkin = false,
            CheckinTime = null,
            CustomerId = 1
        };

        var resultSave = await registrationBusiness.Save(registration);
        Console.WriteLine(resultSave.Message);

        Console.WriteLine("\nThis is the GetAll method!");

        var registrationList = await registrationBusiness.GetAll();
        if (registrationList.Status == 1)
        {
            var registrations = registrationList.Data as List<Registration>;
            if (registrations != null)
            {
                foreach (var reg in registrations)
                {
                    Console.WriteLine($"ID: {reg.RegistrationId}, Name: {reg.ParticipantName}, Email: {reg.AttendeeEmail}, Event ID: {reg.EventId}");
                }
            }
        }
        else
        {
            Console.WriteLine(registrationList.Message);
        }

        Console.WriteLine("\nThis is the Update method!");

        var updateRegistration = new Registration
        {
            RegistrationId = 1,
            ParticipantName = "Jane Doe",
            ParticipantType = "Regular",
            AttendeeEmail = "jane.doe@example.com"
        };

        var registrationUpdate = await registrationBusiness.GetById(1);
        var regToUpdate = registrationUpdate.Data as Registration;
        if (regToUpdate != null)
        {
            regToUpdate.ParticipantName = "Jane Doe Updated";
            regToUpdate.AttendeeEmail = "jane.doe.updated@example.com";

            var resultUpdate = await registrationBusiness.Update(regToUpdate);
            Console.WriteLine(resultUpdate.Message);
        }
        else
        {
            Console.WriteLine(registrationUpdate.Message);
        }

        Console.WriteLine("\nThis is the Delete method!");

        var resultDelete = await registrationBusiness.DeleteById(1);
        Console.WriteLine(resultDelete.Message);

        var registrationListAfterDelete = await registrationBusiness.GetAll();
        if (registrationListAfterDelete.Status == 1)
        {
            var registrations = registrationListAfterDelete.Data as List<Registration>;
            if (registrations != null)
            {
                foreach (var reg in registrations)
                {
                    Console.WriteLine($"ID: {reg.RegistrationId}, Name: {reg.ParticipantName}, Email: {reg.AttendeeEmail}, Event ID: {reg.EventId}");
                }
            }
        }
        else
        {
            Console.WriteLine(registrationListAfterDelete.Message);
        }
    }
}