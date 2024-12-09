namespace Common
{
    public class ReservacionDto
    {
        public int Id { get; set; }
        public int LugarId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; } = string.Empty;

        // Objeto Lugar
        public LugarDto Lugar { get; set; } = new LugarDto();

        public decimal PrecioTotal => Lugar.PrecioPorDia * ((FechaFin - FechaInicio).Days);
        public string FechaFormatted =>
            $"{FechaInicio:dd/MM/yyyy} - {FechaFin:dd/MM/yyyy}";
    }



}
