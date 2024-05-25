using Event.Business;
using Event.Data.Models;


var ticketBusiness = new TicketBusiness();

var ticketTest = new Ticket()
{
    TicketId = 1,
    //EventId = 1,
    TicketType = "VIP",
    Price = 100,
    AvailableQuantity = 100
};
await ticketBusiness.Save(ticketTest);
Console.WriteLine("Business.GetAll()");
var ticketResult = ticketBusiness.GetAll();

if (ticketResult.Status > 0 && ticketResult.Result.Data != null)
{
    var tickets = (List<Ticket>)ticketResult.Result.Data;

    if (tickets != null && tickets.Count > 0)
    {
        foreach (var ticket in tickets)
        {
            Console.WriteLine(ticket.TicketId);
        }
    }

}
else
{
    Console.WriteLine("Get All Ticket fail");
}
