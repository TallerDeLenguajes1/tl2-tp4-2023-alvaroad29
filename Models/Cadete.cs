namespace tl2_tp4_2023_alvaroad29;
public class Cadete
{

    // Atributos
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    // Propiedades
    public int Id { get => id;}
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    // Metodos
    public Cadete(int id, string nombre, string direccion, string telefono) //constructor
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }
    public string mostrarCadete()
    {
        string devolver = $"ID: {Id} -- Nombre: {Nombre} --  ";
        return devolver;
    }

}