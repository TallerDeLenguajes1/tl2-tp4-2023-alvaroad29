namespace tl2_tp4_2023_alvaroad29;
public class Cliente
{
    // Atributos
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    // Propiedades
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion;  }
    public string Telefono { get => telefono;  }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; }

    // Metodos
    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    
}

// las clases padre e hijos las creo en el mismo archivo
//tratar de evitar poner writreLine dentro de una clase, ej que devolver lo que quiero mostrar , ej devolver un string

// public class ObjetoVisual
// {
//     public ObjetoVisual() // la clase base puede no tener constructor y redefinir en la clases hijos
//     {

//     }

//     public virtual void Dibujar()
//     {

//     }

// }

// public class Texto : ObjetoVisual
// {
//     public Texto() : base() //base es por defecto
//     {

//     }

//     public override void Dibujar()
//     {
//         System.Console.WriteLine("Texto");
//     }
// }

// public class Imagen : ObjetoVisual
// {
//     public Imagen() : base() //base es por defecto
//     {

//     }

//     public override void Dibujar()
//     {
//         System.Console.WriteLine("Imagen");
//     }
// }

// //clase abstracta por lo menos un metodo abstracto

// public abstract class Vehiculo
// {
//     protected int velocidad;

//     public abstract int acelerar();
// }

// public class Auto : Vehiculo
// {
//     public override int acelerar()
//     {
//         velocidad = velocidad + 10;
//         return velocidad;
//     }
// }

// public class AutoFormula1 : Vehiculo
// {
//     public override int acelerar()
//     {
//         velocidad = velocidad + 100;
//         return velocidad;
//     }
// }
