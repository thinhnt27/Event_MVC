// See https://aka.ms/new-console-template for more information
using Event.Business.Category;
using Event.Data.Models;
Console.WriteLine("Day la ham Save ne!");

var customerBusiness = new CustomerBussiness();
await customerBusiness.DeleteById(16);

var customer = new Customer
{
    CustomerName = "thangbinhbeo",
    Password = "*******",
    Email = "thangbinhbeo1122@gmail.com",
    Phone = "0838097512",
    Address = "19/1 Pham Ngu Lao",
    Registrations = new List<Registration>()
};
var resultSave = await customerBusiness.Save(customer);
Console.WriteLine(resultSave.Message);
Console.WriteLine();

Console.WriteLine("Day la ham GetAll ne!");

var customerList = await customerBusiness.GetAll();

if (customerList.Status == 4)
{
    Console.WriteLine(customerList.Message);
    var customers = customerList.Data as List<Customer>;
    if (customers != null)
    {
        foreach (var item in customers)
        {
            Console.WriteLine($"ID: {item.CustomerId}, Name: {item.CustomerName}, Email: {item.Email}, Password: {item.Password}, Address: {item.Address}, Registration: {item.Registrations}");
        }
    }
}

Console.WriteLine();
Console.WriteLine("Day la ham Update ne!");

var updateCustomer = new Customer
{
    CustomerId = 16,
    CustomerName = "Trinh Huu Tuan",
    Password = null,
    Email = null,
    Phone = null,
    Address = null,
    Registrations = null
};

var customerUpdate = await customerBusiness.GetById(19);
var customersne = customerUpdate.Data as Customer;
customersne.CustomerName = "Trinh Huu Tuan";

var resultUpdate = await customerBusiness.Update(customersne);
Console.WriteLine(resultUpdate.Message);

var customerList1 = await customerBusiness.GetAll();

if (customerList1.Status == 4)
{
    Console.WriteLine(customerList1.Message);
    var customers = customerList1.Data as List<Customer>;
    if (customers != null)
    {
        foreach (var item in customers)
        {
            Console.WriteLine($"ID: {item.CustomerId}, Name: {item.CustomerName}, Email: {item.Email}, Password: {item.Password}, Address: {item.Address}, Registration: {item.Registrations}");
        }
    }
}

//Console.WriteLine("Day la ham Delete ne!");

await customerBusiness.DeleteById(16);

var customerListNull = await customerBusiness.GetAll();
Console.WriteLine(customerListNull.Message);

if (customerListNull.Status == 4)
{
    Console.WriteLine(customerList1.Message);
    var customers = customerList1.Data as List<Customer>;
    if (customers != null)
    {
        foreach (var item in customers)
        {

            Console.WriteLine($"ID: {item.CustomerId}, Name: {item.CustomerName}, Email: {item.Email}, Password: {item.Password}, Address: {item.Address}, Registration: {item.Registrations}");
        }
    }
    else
    {
        Console.WriteLine(customerListNull.Message);
    }
}