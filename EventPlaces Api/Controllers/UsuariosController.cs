using Microsoft.AspNetCore.Mvc;
using Npgsql;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UsuariosController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        var usuarios = new List<dynamic>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using var command = new NpgsqlCommand("SELECT * FROM usuarios", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Email = reader.GetString(2),
                    Telefono = reader.GetString(3),
                    ImagenURL = reader.GetString(4)
                });
            }
        }
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public IActionResult GetUsuarioById(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM usuarios WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var usuario = new
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Email = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            ImagenURL = reader.GetString(4)
                        };
                        return Ok(usuario);
                    }
                    else
                    {
                        return NotFound("Usuario no encontrado.");
                    }
                }
            }
        }
    }


    [HttpPost]
    public IActionResult CreateUsuario([FromBody] dynamic usuario)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "INSERT INTO usuarios (nombre, email, telefono, ImagenURL) VALUES (@nombre, @email, @telefono, @imagenurl)";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("nombre", (string)usuario.nombre);
            command.Parameters.AddWithValue("email", (string)usuario.email);
            command.Parameters.AddWithValue("telefono", (string)usuario.telefono);
            command.Parameters.AddWithValue("imagenurl", (string)usuario.ImagenURL);
            command.ExecuteNonQuery();
        }
        return Ok("Usuario creado exitosamente.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUsuario(int id, [FromBody] dynamic usuario)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "UPDATE usuarios SET nombre = @nombre, email = @email, telefono = @telefono, ImagenURL = @imagenurl WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("nombre", (string)usuario.nombre);
                command.Parameters.AddWithValue("email", (string)usuario.email);
                command.Parameters.AddWithValue("telefono", (string)usuario.telefono);
                command.Parameters.AddWithValue("imagenurl", (string)usuario.ImagenURL);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Usuario actualizado exitosamente.");
                }
                else
                {
                    return NotFound("Usuario no encontrado.");
                }
            }
        }
    }

}


