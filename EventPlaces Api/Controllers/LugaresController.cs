using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class LugaresController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LugaresController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/Lugares
    [HttpGet]
    public IActionResult GetLugares()
    {
        var lugares = new List<dynamic>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM lugares";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lugares.Add(new
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Direccion = reader.GetString(2),
                            Capacidad = reader.GetInt64(3),
                            Descripcion = reader.GetString(4),
                            PrecioPorDia = reader.GetString(5),
                            ImagenURL = reader.GetString(6),
                            Favorito = reader.GetBoolean(7)
                        });
                    }
                }
            }
        }
        return Ok(lugares);
    }

    // GET: api/Lugares/{id}
    [HttpGet("{id}")]
    public IActionResult GetLugarById(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM lugares WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var lugar = new
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Direccion = reader.GetString(2),
                            Capacidad = reader.GetInt64(3),
                            Descripcion = reader.GetString(4),
                            PrecioPorDia = reader.GetString(5),
                            ImagenURL = reader.GetString(6),
                            Favorito = reader.GetBoolean(7)
                        };
                        return Ok(lugar);
                    }
                    else
                    {
                        return NotFound("Lugar no encontrado.");
                    }
                }
            }
        }
    }

    // POST: api/Lugares
    [HttpPost]
    public IActionResult CreateLugar([FromBody] dynamic lugar)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"INSERT INTO lugares (nombre, direccion, capacidad, descripcion, precioxdia, imagenurl, favorito) 
                        VALUES (@nombre, @direccion, @capacidad, @descripcion, @precioxdia, @imagenurl, @favorito)";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("nombre", (string)lugar.nombre);
                command.Parameters.AddWithValue("direccion", (string)lugar.direccion);
                command.Parameters.AddWithValue("capacidad", (long)lugar.capacidad);
                command.Parameters.AddWithValue("descripcion", (string)lugar.descripcion);
                command.Parameters.AddWithValue("precioxdia", (string)lugar.precioxdia);
                command.Parameters.AddWithValue("imagenurl", (string)lugar.imagenurl);
                command.Parameters.AddWithValue("favorito", (bool)lugar.favorito);
                command.ExecuteNonQuery();
            }
        }
        return Ok("Lugar creado exitosamente.");
    }

    // PUT: api/Lugares/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateLugar(int id, [FromBody] dynamic lugar)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"UPDATE lugares 
                        SET nombre = @nombre, direccion = @direccion, capacidad = @capacidad, descripcion = @descripcion, 
                            precioxdia = @precioxdia, imagenurl = @imagenurl, favorito = @favorito 
                        WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("nombre", (string)lugar.nombre);
                command.Parameters.AddWithValue("direccion", (string)lugar.direccion);
                command.Parameters.AddWithValue("capacidad", (long)lugar.capacidad);
                command.Parameters.AddWithValue("descripcion", (string)lugar.descripcion);
                command.Parameters.AddWithValue("precioxdia", (string)lugar.precioxdia);
                command.Parameters.AddWithValue("imagenurl", (string)lugar.imagenurl);
                command.Parameters.AddWithValue("favorito", (bool)lugar.favorito);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Lugar actualizado exitosamente.");
                }
                else
                {
                    return NotFound("Lugar no encontrado.");
                }
            }
        }
    }

    // DELETE: api/Lugares/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteLugar(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "DELETE FROM lugares WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Lugar eliminado exitosamente.");
                }
                else
                {
                    return NotFound("Lugar no encontrado.");
                }
            }
        }
    }
}
