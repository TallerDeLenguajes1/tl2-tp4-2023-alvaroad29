namespace tl2_tp4_2023_alvaroad29;

public enum enumEstado{noAasignado,pendiente,entregado,cancelado};

public class Pedido
{
    // Atributos
    private int nro;
    private string obs;
    private Cliente cliente;
    private enumEstado estado;
    private int idCadete;

    // Propiedades
    
    public int IdCadete { get => idCadete; set => idCadete = value; } //si no pongo el set no deserializa bien
    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public enumEstado Estado { get => estado; set => estado = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }

    // Metodos

    private static int numPedido = 1;
    public Pedido(string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion,int idCadete = 0,enumEstado estado = enumEstado.noAasignado, int nro = 0) 
    {
        this.idCadete = 0;
        this.Estado = enumEstado.noAasignado;
        this.Nro = numPedido++;
        this.Obs = obs;
        Cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
    }

    public Pedido(){}
    public string VerDireccionCliente()
    {
        return $"Direccion: {Cliente.Direccion} \nReferencias direccion: {Cliente.DatosReferenciaDireccion} ";
    }

    public string VerDatosCliente()
    { 
        return $"Nombre: {Cliente.Nombre} \n Telefono: {Cliente.Telefono} ";
    }

    public void CambiarEstado()
    {
        if (Estado == enumEstado.noAasignado) 
        {
            Estado = enumEstado.pendiente;
        }else
        {
            Estado = enumEstado.entregado;
        }
    }

    public void CancelarPedido()
    {
        this.Estado = enumEstado.cancelado;
    }

    public string MostrarPedido()
    {
        string devolver = $"Numero de pedido: {Nro} -- Observaciones: {Obs} -- Estado: {Estado}";
        return devolver;
    }
}