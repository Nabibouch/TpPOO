using System.Runtime.CompilerServices;

public class Brand
{
    //Nous avons décider de garder la syntaxe private _ et public Maj même si techniquement,
    //  elle est "moins bonne" que juste {get; private set} + methode Set();. On le fait pour respecter ce qu'on a vu en cours
    private string _name; 
    private string _origin;
    public int Id{get; private set;}
    private readonly List<Model> _models = new List<Model>();
    public IReadOnlyCollection<Model> Models => _models; //On rend lisible _models sans qu'il soit modifiable par tout le monde 
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _name = value;
        }
    }
    public string Origin
    {
        get
        {
            return _origin;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _origin = value;
        }
    }
    private Brand() {}
    public Brand(string name, string origin)
    {
        Name = name;
        Origin = origin;
    }
    
    public void AddModel(Model model)
    {
     _models.Add(model);   
    }
    
    public void DeleteModel(Model model)
    {
        _models.Remove(model);
    }
}