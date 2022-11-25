interface ICommand
{
    void Land();
}

interface IATCMediator
{
    public void RegisterRunway(Runway runway);
    public void RegisterFlight(Flight flight);
    public bool IsLandingOk();
    public void SetLandingStatus(bool status);
}

class Flight : ICommand
{
    private IATCMediator AtcMediator;
    public Flight(IATCMediator atcMediator) => AtcMediator = atcMediator;
    
    public void Land()
    {
        if (AtcMediator.IsLandingOk())
        {
            Console.WriteLine("Successfully Landed.");
            AtcMediator.SetLandingStatus(true);
        }
        else
            Console.WriteLine("Waiting for landing.");
    }
    
    public void GetReady() => Console.WriteLine("Ready for landing.");
}

class ATCMediator: IATCMediator
{
    private Flight Flight;
    private Runway Runway;
    public bool Land;

    public void RegisterRunway(Runway runway) => Runway = runway;
    public void RegisterFlight(Flight flight) => Flight = flight;
    public bool IsLandingOk() => Land;

    public void SetLandingStatus(bool status) => Land = status;
}

class Runway : ICommand
{
    private IATCMediator AtcMediator;

    public Runway(IATCMediator atcMediator)
    {
        AtcMediator = atcMediator;
        AtcMediator.SetLandingStatus(true);
    }
    
    public void Land()
    {
        Console.WriteLine("Landing permission granted.");
        AtcMediator.SetLandingStatus(true);
    }
 
}

class Program
{
    static void Main()
    {
        IATCMediator atcMediator = new ATCMediator();
        Flight sparrow101 = new Flight(atcMediator);
        Runway mainRunway = new Runway(atcMediator);
        atcMediator.RegisterFlight(sparrow101);
        atcMediator.RegisterRunway(mainRunway);
        sparrow101.GetReady();
        mainRunway.Land();
        sparrow101.Land();
    }
}