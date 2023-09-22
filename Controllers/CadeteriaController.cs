using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_alvaroad29.Controllers; //las clases tinen q tener el namespace tl2_tp4_2023_alvaroad29 y en esta tl2_tp4_2023_alvaroad29.Controllers

[ApiController] // atributo que indica que es un constrolador
[Route("[controller]")] // ruta con que se direcciona el recurso, en este caso se usa el nombre de la clase
public class CadeteriaController : ControllerBase // nombreControlador(el recurso va a ser ruteado con ese nombre)Controller(x convension, no impacta en la ruta) : herencia 
{
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> _logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria(); //inicializo a traves del controlador
    }

    [HttpGet] 
    public ActionResult<string> GetNombreCadeteria()
    {
        return Ok(cadeteria.Nombre);
    }

    [HttpPost("CargaDatos")]
    public ActionResult<string> CargaDatos(string tipoArchivo)
    {
        if (cadeteria.CargaDatos(tipoArchivo))
        {
            return Ok("DATOS CARGADOS");
        }
        else
        {
            return BadRequest("DATOS NO CARGADOS");
        }
    }

    [HttpGet] //muestra datos
    [Route("Pedidos")] // Route, cuando hay mas de un get sin datos
    public ActionResult<IEnumerable<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet] //muestra datos , //si no pongo el Route y dejo [HttpGet("Cadetes")] tmb funciona
    [Route("Cadetes")] // Route, cuando hay mas de un get sin datos
    public ActionResult<IEnumerable<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.GetCadetes();
        return Ok(cadetes);
    }



    [HttpPost("AddPedidoParametros")] //agrega datos
    public ActionResult<Pedido> AddPedido(string obs,string nombre,string direccion,string telefono,string datosReferencia)
    {
        var estado = cadeteria.AgregarPedido(obs, nombre, direccion, telefono, datosReferencia);
        return Ok(estado);
    }

    // [HttpPost("AddPedido")] //agrega datos
    // public ActionResult<Pedido> AddPedido(Pedido pedido)
    // {
    //     var nuevoPedido = cadeteria.AgregarPedido(pedido);
    //     return Ok(nuevoPedido);
    // }

    [HttpPut("AsignarPedido")]
    public ActionResult<string> Asignar(int idPedido, int idCadete)
    {
        if (cadeteria.DevolverCadete(idCadete) != null && cadeteria.DevolverPedido(idPedido) != null)
        {
            cadeteria.AsignarCadeteAPedido(idCadete,idPedido);
            return Ok("Pedido asignado");
        }
        else
        {
            return BadRequest("Pedidos no asignado, verifique los datos ingresados");
        }
    }

    [HttpPut("CambiarCadetePedido")]
    public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        if (cadeteria.DevolverCadete(idNuevoCadete) != null && cadeteria.DevolverPedido(idPedido) != null)
        {
            cadeteria.ReasignarPedido(idPedido,idNuevoCadete);
            return Ok("Pedido Reasignado");
        }
        else
        {
            return BadRequest("Pedidos no Reasignado");
        }
    }

    [HttpPut("CambiarEstadoPedido")]
    public ActionResult<bool> CambiarEstadoPedido(int idPedido, int idNuevoEstado){
        if (idNuevoEstado == 1 || idNuevoEstado == 2)
        {
            return Ok(cadeteria.DevolverPedido(idPedido) != null && cadeteria.CambiarEstadoPedido(idPedido));
        }else
        {
            return Ok(cadeteria.DevolverPedido(idPedido) != null && cadeteria.CancelarPedido(idPedido));
        }
    }

}

//pedidos.Add(pedido)
//pedido.Nro = pedidos.Count forma de autonumerico

 // [HttpGet]
    // public ActionResult<string> GetNombreCadeteria()
    // {
    //     return Ok(cadeteria.nombre);
    // }
    // public Pedido Get()
    // {
    //     //llamada a un metodo de la clase
    // }

    // [HttpGet] //tipo de llamada
    // [Route("Pedidos")] //agrega /Pedidos
    // public ActionResult<IEnumerable<Pedido>> GetPedidos()
    // {
    //     var pedidos = catederia.GetPedidos();
    //     return Ok(pedidos);
    //     //llamada a un metodo de la clase
    // }

    // [HttpPost("AddPedido")]
    // public ActionResult<Pedido> AddPeido(Pedido pedido)
    // {
    //     var nuevoPedido = cadeteria.AddPedido(pedido);
    //     return Ok(nuevoPedido);
    // }

    // private static CadeteriaController cadeteriaSingleton;

    // public static Cadeteria GetCadeteria()
    // {
    //     if (cadeteriaSingleton == null)
    //     {
    //         return 
    //     }
    //     else
    //     {
    //         return 
    //     }
    // }

    //  [HttpPost("UpdatePedido")]
    //  public ActionResult<Pedido> UpdatePedido(Pedido pedido)
    //  {
        
    //  }