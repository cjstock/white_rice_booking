/*
    Name: Flights
    Date Last Updated: 4-22-2020
    Programmer Names: William Yung
    Description: This class is the model for the flights.csv database
    Important Methods/Structures/Etc: N/A
    Major Decisions: N/A
*/
public class Flights
{
    public int Flight_ID { get; set; }
    public int Route_ID { get; set; }
    public int Seats { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Price { get; set; }
}