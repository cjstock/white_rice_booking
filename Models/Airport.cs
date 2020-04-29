/*
    Name: Airport
    Date Last Updated: 4-18-2020
    Programmer Names: William Yung
    Description: This class is the model for the top_10_Airports.csv database
    Important Methods/Structures/Etc: N/A
    Major Decisions: N/A
*/
public class Airport
{
    public int OpenFlights_ID { get; set; }
    public string Airport_Name { get; set; }
    public string City_Name { get; set; }
    public string IATA { get; set; }
    public int Timezone { get; set; }
    public string Timezone_Name { get; set; } 
}