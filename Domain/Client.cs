namespace tp_a_rendre.Domain;
public class Client
{
    public int Id {get; private set;}
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _phoneNumber;
    private readonly List<Reservation> _reservations = new List<Reservation>();
    public IReadOnlyCollection<Reservation> Reservations => _reservations;
    public string FirstName
    {
        get
        {
            return _firstName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _firstName = value;
        }
    }
    public string LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _lastName = value;
        }
    }
    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _email = value;
        }
    }
    public string PhoneNumber
    {
        get
        {
            return _phoneNumber;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 10 || !value.All(char.IsDigit))
            {
                throw new ArgumentException("Phone number must have 10 digits");
            }
            _phoneNumber = value;
        }
    }

    private Client() {}

    public static Client CreateForForm() => new Client();

    public Client(string firstName, string lastname, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastname;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }
}
