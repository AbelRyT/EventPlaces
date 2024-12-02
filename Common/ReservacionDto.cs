namespace Common
{
    public class ReservacionDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LugarId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; } // Nuevo campo para el nombre del estado
    }

}
