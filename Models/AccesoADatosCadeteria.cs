namespace tl2_tp4_2023_alvaroad29;
using System.Text.Json;

public abstract class AccesoADatosCadeteria
{
    public abstract Cadeteria Obtener(string nombreArchivo);
}

public class AccesoADatosCadeteriaCSV : AccesoADatosCadeteria
{
    private static Cadeteria cadeteria;
    public override Cadeteria Obtener(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            using (StreamReader reader = new StreamReader(nombreArchivo))
            {
                string linea = reader.ReadLine(); //lee una linea
                cadeteria = new Cadeteria(linea.Split(",")[0],linea.Split(",")[1]);
                
            }
        }
        else
        {
            cadeteria = new Cadeteria("La prueba","(3876) 123456");
            //System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
        }
        return cadeteria;
    }
}

public class AccesoADatosCadeteriaJSON : AccesoADatosCadeteria
{
    private static Cadeteria cadeteria;
    public override Cadeteria Obtener(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            
            string jsonString = File.ReadAllText(nombreArchivo); //lee el json y lo guardo en un string
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString); //<tipo de dato>(variable donde esta la informacion (string)), pasa el string a una lista
        }           
        else
        {
            //System.Console.WriteLine($"El archivo {nombreArchivo} NO EXISTE");
            cadeteria = new Cadeteria("La prueba","(3876) 123456");
        }
        return cadeteria;
    }
}
