using Event.Business;
using Event.Business.Category;
using Event.Data.Models;
using System.Net.Sockets;


var paymentBusiness = new PaymentBusiness();
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