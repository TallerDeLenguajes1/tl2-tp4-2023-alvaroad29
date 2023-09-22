namespace tl2_tp4_2023_alvaroad29;
public class Cadeteria
{
    // Atributos
    private string? nombre;
    private string? telefono;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos;

    // Propiedades
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    // Metodos

    private static Cadeteria cadeteriaSingleton;
    public static Cadeteria GetCadeteria()
    {
        if (cadeteriaSingleton == null)
        {
            cadeteriaSingleton = new Cadeteria();
        }
        return cadeteriaSingleton;
    }

    public bool CargaDatos(string tipoArchivo) // cargo los datos dependiendo un tipo de archivo
    {
        bool bandera = false;
        if (tipoArchivo == "csv")
        {
            AccesoADatos acceso = new AccesoCSV();
            cadeteriaSingleton = acceso.cargarCadeteria("infoCadeteria.csv");
            cadeteriaSingleton.CargarCadetes(acceso.cargarCadetes("infoCadetes.csv"));
            bandera = true;
        }else
        {
            AccesoADatos acceso = new AccesoJSON();
            cadeteriaSingleton = acceso.cargarCadeteria("infoCadeteria.json");;
            cadeteriaSingleton.CargarCadetes(acceso.cargarCadetes("infoCadetes.json"));
            bandera = true;
        }
        return bandera;
    }
    public Cadeteria()
    {
        pedidos = new List<Pedido>();
        nombre = "Cadeteria de prueba";
    }
    public Cadeteria(string nombre, string telefono) // constructor
    {
        pedidos = new List<Pedido>();
        cadetes = new List<Cadete>();
        Nombre = nombre;
        Telefono = telefono;
    }

    public void AgregarCadete(int id, string nombre, string direccion, string telefono)
    {
        Cadete cadete = new Cadete(id, nombre, direccion, telefono);
        this.cadetes.Add(cadete);
    }

    public bool AgregarPedido(string obs,string nombre,string direccion,string telefono,string datosReferencia)
    {
        bool bandera = false;
        Pedido pedido = new Pedido(obs,nombre,direccion,telefono,datosReferencia);
        if (pedido != null)
        {
            pedidos.Add(pedido);
            bandera = true;
        }
        return bandera;
    }

    // public bool AgregarPedido(Pedido pedido)
    // {
    //     pedidos.Add(pedido);
    //     //pedido.Nro = pedidos.Count();
    //     return true;
    // }

    public void CargarCadetes(List<Cadete> cadetes)
    {
        this.cadetes = cadetes;
    }

    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        bool bandera = false;
        var pedido = DevolverPedido(idPedido);
        if (pedido != null)
        {
            pedido.IdCadete = idCadete;
            pedido.CambiarEstado();
            bandera = true;
        }
        return bandera;
    }

    public void EliminarPedido(int idPedido)
    {
        pedidos.RemoveAll(pedido => pedido.Nro == idPedido);
    }

    public bool CambiarEstadoPedido(int idPedido)
    {
        bool bandera = false; 
        var pedido = DevolverPedido(idPedido);
        if (pedido != null) 
        {
            pedido.CambiarEstado();
            bandera = true;
        }
        return bandera;
    }

    public bool CancelarPedido(int idPedido)
    {
        var pedido = DevolverPedido(idPedido);
        pedido.CancelarPedido();
        return true;
    }
    public bool ReasignarPedido(int idPedido, int idCadeteAgregar)
    {
        bool bandera = false;
        var pedido = pedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
        if (pedido != null)
        {
            pedido.IdCadete = idCadeteAgregar;
            bandera = true;
        }
        return bandera;
    }
    public string ListarCadetes()
    {
        
        string devolver = "";
        foreach (var item in cadetes)
        {
            devolver = devolver + "\n" + item.mostrarCadete();
        }
        return devolver;
    }

    public List<Pedido> GetPedidos()
    {
        return pedidos;
    }

    public List<Cadete> GetCadetes()
    {
        return cadetes;
    }


    public Cadete DevolverCadetePedido(int idPedido) //devuelve el cadete a partir del id del pedido
    {
        var pedido = DevolverPedido(idPedido);
        return DevolverCadete(pedido.IdCadete);
    }
    public Cadete DevolverCadete(int id) // devuelve cadete a parter de su id
    {
        return cadetes.SingleOrDefault(cadete => cadete.Id == id);
    }

    public Pedido DevolverPedido(int idPedido) // devuelve pedido a partir de un id
    {
        return pedidos.SingleOrDefault(pedido => pedido.Nro == idPedido);;
    }

    public int CantPedidos(enumEstado estado)
    {
        return pedidos.Count(item => item.Estado == estado);
    }

    public int TotalPedidos()
    {
        return pedidos.Count();
    }

    public int CantPedidosCadete(int idCadete, enumEstado estado)
    {
        return pedidos.Count(item => item.Estado == estado && item.IdCadete == idCadete);
    }

    // public int TotalPedidosCadetes(int idCadete)
    // {
    //     return
    // }
    public string MostrarPedidos(enumEstado estado) // muestro los cadetes que tiene por lo menos un pedido y que esten pendiente
    {
        string devolver = "";
        foreach (var item in pedidos)
        {
            if (item.Estado == estado)
            {
                devolver = devolver + "\n" + item.MostrarPedido();
            }
        }
        return devolver;
    }

    public int JornalACobrar(int idCadete)
    {
        return CantPedidosCadete(idCadete,enumEstado.entregado) * 500;
    }
    public List<Informe> GenerarInforme()
    {
        List<Informe> info = new List<Informe>();
        Informe infoCadete;
        foreach (var item in cadetes)
        {
            infoCadete = new Informe();
            infoCadete.NombreCadete = item.Nombre;
            infoCadete.MontoGanado = JornalACobrar(item.Id);
            infoCadete.EntregadosCadete = CantPedidosCadete(item.Id, enumEstado.entregado);
            info.Add(infoCadete);
        }
        return info;
    }
}