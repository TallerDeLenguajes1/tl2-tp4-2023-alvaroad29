namespace tl2_tp4_2023_alvaroad29;
using System.Text.Json;

public abstract class AccesoADatos
{
    public abstract Cadeteria cargarCadeteria(string nombreArchivo);
    public abstract List<Cadete> cargarCadetes(string nombreArchivo);

}

public class AccesoCSV : AccesoADatos
{
    public override Cadeteria cargarCadeteria(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            using (StreamReader reader = new StreamReader(nombreArchivo))
            {
                string linea = reader.ReadLine(); //lee una linea
                Cadeteria cadeteria = new Cadeteria(linea.Split(",")[0],linea.Split(",")[1]);
                return cadeteria;
            }
        }
        else
        {
            Cadeteria cadeteria = new Cadeteria("prueba","asdas");
            System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return cadeteria;
        }
    }

    public override List<Cadete> cargarCadetes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            using (StreamReader reader = new StreamReader(nombreArchivo))
            {
                List<Cadete> cadetes = new List<Cadete>();
                Cadete cadete;
                string linea = "";
                string [] lineaSeparada;
                while ((linea = reader.ReadLine()) != null)
                {
                    lineaSeparada = linea.Split(",");
                    cadete = new Cadete(int.Parse(lineaSeparada[0]),lineaSeparada[1],lineaSeparada[2],lineaSeparada[3]);
                    cadetes.Add(cadete);
                }
                return cadetes;
            }
        }
        else
        {
            System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return null;
        }
    }
}

public class AccesoJSON : AccesoADatos
{
    public override Cadeteria cargarCadeteria(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            
            string jsonString = File.ReadAllText(nombreArchivo); //lee el json y lo guardo en un string
            Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString); //<tipo de dato>(variable donde esta la informacion (string)), pasa el string a una lista
            return cadeteria;
        }           
        else
        {
            System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return null;
        }
    }

    public override List<Cadete> cargarCadetes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {

            string jsonString = File.ReadAllText(nombreArchivo); //lee el json y lo guardo en un string
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonString); //accedo a cadetes
            return cadetes;
        }           
        else
        {
            System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            return null;
        }
    }

}