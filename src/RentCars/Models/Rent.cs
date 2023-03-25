using RentCars.Types;

namespace RentCars.Models;

public class Rent
{
    public Vehicle Vehicle { get; set; }
    public Person Person { get; set; }
    public int DaysRented { get; set; }
    public double Price { get; set; }
    public RentStatus Status { get; set; } = RentStatus.Confirmed;

    public Rent(Vehicle vehicle, Person person, int daysRented)
    {
        Vehicle = vehicle;
        Person = person;
        DaysRented = daysRented;
        Vehicle.IsRented = true;
        Person.Debit = Price;

        var physicalPerson = Person.GetType().GetProperty("CPF");

        if (physicalPerson != null)
        {
            Price = Vehicle.PricePerDay * daysRented;
        }
        else
        {
            var fullPrice = Vehicle.PricePerDay * daysRented;
            var sumOfDiscount = fullPrice * 10/100;
            Price = fullPrice - sumOfDiscount;
        }
    }

    public void Cancel()
    {
        Status = RentStatus.Cancelled;
    }

    public void Finish()
    {
        Status = RentStatus.Finished;
    }
}
