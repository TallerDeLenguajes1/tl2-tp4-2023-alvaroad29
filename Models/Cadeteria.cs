namespace tl2_tp4_2023_alvaroad29;
public class Cadeteria
{
    // Atributos
    private string? nombre;
    private string? telefono;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos; 
    private AccesoADatosPedidos accesoADatosPedidos;
    private AccesoADatosCadetesJSON accesoADatosCadetes;
    private static Cadeteria instance;

    public static Cadeteria GetInstance()
    {
        if (instance == null)
        {
            var accesoADatosCadeteria = new AccesoADatosCadeteriaJSON();
            instance = accesoADatosCadeteria.Obtener("infoCadeteria.json");
            instance.accesoADatosPedidos = new AccesoADatosPedidosJSON();
            instance.accesoADatosCadetes = new AccesoADatosCadetesJSON();
            instance.CargarCadetes();
            instance.CargarPedidos();
        }
        return instance;
    }
    // Propiedades
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }

    // Metodos
    // private static Cadeteria cadeteriaSingleton;
    // public static Cadeteria GetCadeteria()
    // {
    //     if (cadeteriaSingleton == null)
    //     {
    //         cadeteriaSingleton = new Cadeteria();
    //         cadeteriaSingleton.CargarDatos("json");
    //     }
    //     return cadeteriaSingleton;
    // }

    // private void CargarDatos(string tipoArchivo) // cargo los datos dependiendo un tipo de archivo
    // {
    //     if (tipoArchivo == "csv")
    //     {
    //         cadeteriaSingleton = accesoCadeteria.Obtener("infoCadeteria.csv");
    //         cadeteriaSingleton.CargarCadetes(accesoCadetes.Obtener("infoCadetes.csv"));
    //         cadeteriaSingleton.CargarPedidos(accesoADatosPedidos.Obtener("infoPedidos.csv"));
    //     }else
    //     {
    //         cadeteriaSingleton = accesoCadeteria.Obtener("infoCadeteria.json");
    //         cadeteriaSingleton.CargarCadetes(accesoCadetes.Obtener("infoCadetes.json"));
    //         cadeteriaSingleton.CargarPedidos(accesoADatosPedidos.Obtener("infoPedidos.json"));
    //     }
    // }

    public Cadeteria()
    {
        nombre = "Cadeteria 'Por Defecto'";
        telefono = "0123-456789";
        pedidos = new List<Pedido>();
        cadetes = new List<Cadete>();
    }
    public Cadeteria(string nombre, string telefono) // constructor
    {
        Nombre = nombre;
        Telefono = telefono;
        pedidos = new List<Pedido>();
        cadetes = new List<Cadete>();
        cadetes = new List<Cadete>();
    }

    public Cadete AgregarCadete(int id, string nombre, string direccion, string telefono)
    {
        Cadete cadete = new Cadete(id, nombre, direccion, telefono);
        cadetes.Add(cadete);
        accesoADatosCadetes.Guardar(cadetes,"infoCadetes.json");
        return cadete;
    }

    public bool AgregarPedido(string obs,string nombre,string direccion,string telefono,string datosReferencia)
    {
        bool bandera = false;
        Pedido pedido = new Pedido(obs,nombre,direccion,telefono,datosReferencia);
        if (pedido != null)
        {
            pedidos.Add(pedido);
            accesoADatosPedidos.Guardar(pedidos,"infoPedidos.json");
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

    public void CargarCadetes()
    {
        cadetes = accesoADatosCadetes.Obtener("infoCadetes.json");
    }

    public void CargarPedidos()
    {
        pedidos = accesoADatosPedidos.Obtener("infoPedidos.json");
    }

    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        bool bandera = false;
        var pedido = DevolverPedido(idPedido);
        var cadete = DevolverCadete(idCadete);
        if (pedido != null && cadete != null)
        {
            pedido.IdCadete = idCadete;
            pedido.CambiarEstado();
            accesoADatosPedidos.Guardar(pedidos,"infoPedidos.csv");
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
            accesoADatosPedidos.Guardar(pedidos,"infoPedidos.csv");
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
        var pedido = DevolverPedido(idPedido);
        var cadete = DevolverCadete(idCadeteAgregar);
        if (pedido != null && cadete != null)
        {
            pedido.IdCadete = idCadeteAgregar;
            accesoADatosPedidos.Guardar(pedidos,"infoPedidos.csv");
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
        return pedidos.SingleOrDefault(pedido => pedido.Nro == idPedido);
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