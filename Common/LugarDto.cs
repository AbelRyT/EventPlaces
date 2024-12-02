namespace Common
{
    public class LugarDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioPorDia { get; set; }
        public string ImagenURL { get; set; } = string.Empty;
        public bool Favorito { get; set; }
        public int Habitaciones { get; set; }
        public int Banos {  get; set; }
    }

}
