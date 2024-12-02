namespace Common
{
    public class UsuarioDto
    {
        public int Id { get; set; } // Opcional para manejar creaciones y actualizaciones
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string ImagenURL { get; set; } = string.Empty;
    }

}
