namespace tp_a_rendre.Domain;

public class Model
{
    private string _name;
    public int Id{get; private set;}//J'ai cru comprendre que cette version était plus utilisé lorsqu'on n'ajoute pas de règle métier 
    private int _brandId;
    private Brand _brand;
    public int BrandId
    {
        get
        {
            return _brandId;
        }
    }
    public Brand Brand
    {
        get
        {
            return _brand;
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentException("Brand cannot be null");
            }
            _brand = value;
            _brandId = value.Id;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The name cannot be blank");
            }
            _name = value;
        }
    }
    private Model() {} //Utiliser par ef pour créer un objet sans passer par mon contructeur public
    public Model(string name, Brand brand)
    {
        Name = name;
        Brand = brand;
    }

}