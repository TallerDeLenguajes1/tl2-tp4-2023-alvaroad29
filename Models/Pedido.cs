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
    public int Nro { get => nro; }
    public string Obs { get => obs; }
    public enumEstado Estado { get => estado; }
    public int IdCadete { get => idCadete; set => idCadete = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }

    // Metodos

    private static int numPedido = 1;
    public Pedido(string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion) 
    {
        this.idCadete = 0;
        this.estado = enumEstado.noAasignado;
        this.nro = numPedido++;
        this.obs = obs;
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
        if (estado == enumEstado.noAasignado) 
        {
            estado = enumEstado.pendiente;
        }else
        {
            estado = enumEstado.entregado;
        }
    }

    public void CancelarPedido()
    {
        this.estado = enumEstado.cancelado;
    }

    public string MostrarPedido()
    {
        string devolver = $"Numero de pedido: {Nro} -- Observaciones: {Obs} -- Estado: {Estado}";
        return devolver;
    }
}