using Event.Business;
using Event.Business.Category;
using Event.Data.Models;
using System.Net.Sockets;


var paymentBusiness = new PaymentBusiness();

var paymentTest = new Payment()
{
    PaymentId = 1,
    //EventId = 1,
    PaymentType = "ShipCod",
    AmountPaid=100,
    PaymentDate=DateOnly.MaxValue,
    RegistrationId=1,
    Status=true,
    TicketId=1,
    TicketQuantity=1,

       
};
await paymentBusiness.Save(paymentTest);
Console.WriteLine("Business.GetAll()");
var paymentResult = paymentBusiness.GetAll();

if (paymentResult.Status > 0 && paymentResult.Result.Data != null)
{
    var payments = (List<Payment>)paymentResult.Result.Data;

    if (payments != null && payments.Count > 0)
    {
        foreach (var payment in payments)
        {
            Console.WriteLine(payment.PaymentId);
        }
    }

}
else
{
    Console.WriteLine("Get All Payment fail");
}