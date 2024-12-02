using Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

[Route("api/[controller]/[action]")]
[ApiController]
public class LugaresController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LugaresController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetLugares()
    {
        var lugares = new List<LugarDto>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM lugares";
            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lugares.Add(new LugarDto
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Capacidad = reader.GetInt32(3),
                    Descripcion = reader.GetString(4),
                    PrecioPorDia = reader.GetDecimal(5),
                    Favorito = reader.GetBoolean(6),
                    Habitaciones = reader.GetInt32(7),
                    Banos = reader.GetInt32(8),
                    ImagenURL = reader.GetString(9)
                });
            }
        }
        return Ok(lugares);
    }

    [HttpGet("{id}")]
    public IActionResult GetLugarById(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM lugares WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var lugar = new LugarDto
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Capacidad = reader.GetInt32(3),
                    Descripcion = reader.GetString(4),
                    PrecioPorDia = reader.GetDecimal(5),
                    Favorito = reader.GetBoolean(6),
                    Habitaciones = reader.GetInt32(7),
                    Banos = reader.GetInt32(8),
                    ImagenURL = reader.GetString(9)
                };
                return Ok(lugar);
            }
            return NotFound("Lugar no encontrado.");
        }
    }

    [HttpPost]
    public IActionResult CreateLugar([FromBody] LugarDto lugar)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"INSERT INTO lugares (nombre, direccion, capacidad, descripcion, precioxdia, imagenurl, favorito) 
                        VALUES (@nombre, @direccion, @capacidad, @descripcion, @precioxdia, @imagenurl, @favorito)";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("nombre", lugar.Nombre);
            command.Parameters.AddWithValue("direccion", lugar.Direccion);
            command.Parameters.AddWithValue("capacidad", lugar.Capacidad);
            command.Parameters.AddWithValue("descripcion", lugar.Descripcion);
            command.Parameters.AddWithValue("precioxdia", lugar.PrecioPorDia);
            command.Parameters.AddWithValue("imagenurl", lugar.ImagenURL);
            command.Parameters.AddWithValue("favorito", lugar.Favorito);
            command.ExecuteNonQuery();
        }
        return Ok("Lugar creado exitosamente.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLugar(int id, [FromBody] LugarDto lugar)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"UPDATE lugares SET nombre = @nombre, direccion = @direccion, capacidad = @capacidad, 
                        descripcion = @descripcion, precioxdia = @precioxdia, imagenurl = @imagenurl, favorito = @favorito 
                        WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("nombre", lugar.Nombre);
            command.Parameters.AddWithValue("direccion", lugar.Direccion);
            command.Parameters.AddWithValue("capacidad", lugar.Capacidad);
            command.Parameters.AddWithValue("descripcion", lugar.Descripcion);
            command.Parameters.AddWithValue("precioxdia", lugar.PrecioPorDia);
            command.Parameters.AddWithValue("imagenurl", lugar.ImagenURL);
            command.Parameters.AddWithValue("favorito", lugar.Favorito);

            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return Ok("Lugar actualizado exitosamente.");
            }
            return NotFound("Lugar no encontrado.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLugar(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "DELETE FROM lugares WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return Ok("Lugar eliminado exitosamente.");
            }
            return NotFound("Lugar no encontrado.");
        }
    }
}
