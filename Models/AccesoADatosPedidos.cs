namespace tl2_tp4_2023_alvaroad29;
using System.Text.Json;

public abstract class AccesoADatosPedidos
{
    public abstract List<Pedido> Obtener(string nombreArchivo);
    public abstract void Guardar(List<Pedido> pedidos, string nombreArchivo);
}

public class AccesoADatosPedidosCSV : AccesoADatosPedidos
{
    public override List<Pedido> Obtener(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            using (StreamReader reader = new StreamReader(nombreArchivo))
            {
                List<Pedido> pedidos = new List<Pedido>();
                Pedido pedido;
                string linea = "";
                string [] lineaSeparada;
                while ((linea = reader.ReadLine()) != null)
                {
                    lineaSeparada = linea.Split(",");
                    pedido = new Pedido(lineaSeparada[0],lineaSeparada[1],lineaSeparada[2],lineaSeparada[3],lineaSeparada[4]);
                    pedidos.Add(pedido);
                }
                return pedidos;
            }
        }
        else
        {
            System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return null;
        }
    }

    public override void Guardar(List<Pedido> pedidos, string nombreArchivo){
        using (StreamWriter archivo = new StreamWriter(nombreArchivo)) 
        {
            foreach (var item in pedidos)
            {
                archivo.WriteLine($"{item.Obs}, {item.Cliente.Nombre}, {item.Cliente.Direccion}, {item.Cliente.Telefono}, {item.Cliente.DatosReferenciaDireccion}");//escribe una linea
            }
        }
    }
}

public class AccesoADatosPedidosJSON : AccesoADatosPedidos
{
    public override List<Pedido> Obtener(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {

            string jsonString = File.ReadAllText(nombreArchivo); //lee el json y lo guardo en un string
            List<Pedido> pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonString); //accedo a cadetes
            return pedidos;
        }           
        else
        {
            Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return null;
        }
    }

    public override void Guardar(List<Pedido> pedidos, string nombreArchivo){
        string pedidosGuardar = JsonSerializer.Serialize(pedidos); // mando objeto y lo convierte a string con un formato json
        File.WriteAllText(nombreArchivo,pedidosGuardar); 
    }
}