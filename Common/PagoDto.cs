namespace Common
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ReservacionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
    }

}
