namespace tl2_tp4_2023_alvaroad29;
using System.Text.Json;

public abstract class AccesoADatosCadetes
{
    public abstract List<Cadete> Obtener(string nombreArchivo);
}

public class AccesoADatosCadetesCSV : AccesoADatosCadetes
{
    public override List<Cadete> Obtener(string nombreArchivo)
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

public class AccesoADatosCadetesJSON : AccesoADatosCadetes
{
    public override List<Cadete> Obtener(string nombreArchivo)
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

    public void Guardar(List<Cadete> cadetes, string nombreArchivo){
        string cadetesGuardar = JsonSerializer.Serialize(cadetes); 
        File.WriteAllText(nombreArchivo,cadetesGuardar); 
    }
}