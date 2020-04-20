/*
    Name: Routes
    Date Last Updated: 4-18-2020
    Programmer Names: William Yung
    Description: This class is the model for the top_10_routes.csv database
    Important Methods/Structures/Etc: N/A
    Major Decisions: N/A
*/
public class Routes
{
    public int Route_ID { get; set; }
    public string Source_IATA { get; set; }
    public int Source_OpenFlights_ID { get; set; }
    public string Destination_IATA { get; set; }
    public int Destination_OpenFlights_ID { get; set; }
}