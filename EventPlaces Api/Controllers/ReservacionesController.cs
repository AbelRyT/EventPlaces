using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class ReservacionesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ReservacionesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetReservaciones()
    {
        var reservaciones = new List<dynamic>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM reservaciones";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservaciones.Add(new
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt64(1),
                            LugarId = reader.GetInt64(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            EstadoId = reader.GetInt64(5)
                        });
                    }
                }
            }
        }
        return Ok(reservaciones);
    }

    [HttpGet("{id}")]
    public IActionResult GetReservacionById(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM reservaciones WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var reservacion = new
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt64(1),
                            LugarId = reader.GetInt64(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            EstadoId = reader.GetInt64(5)
                        };
                        return Ok(reservacion);
                    }
                    else
                    {
                        return NotFound("Reservación no encontrada.");
                    }
                }
            }
        }
    }

    [HttpPost]
    public IActionResult CreateReservacion([FromBody] dynamic reservacion)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"INSERT INTO reservaciones (usuario_id, lugar_id, fecha_inicio, fecha_fin, estado_id) 
                        VALUES (@usuario_id, @lugar_id, @fecha_inicio, @fecha_fin, @estado_id)";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("usuario_id", (long)reservacion.usuario_id);
                command.Parameters.AddWithValue("lugar_id", (long)reservacion.lugar_id);
                command.Parameters.AddWithValue("fecha_inicio", (DateTime)reservacion.fecha_inicio);
                command.Parameters.AddWithValue("fecha_fin", (DateTime)reservacion.fecha_fin);
                command.Parameters.AddWithValue("estado_id", (long)reservacion.estado_id);
                command.ExecuteNonQuery();
            }
        }
        return Ok("Reservación creada exitosamente.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservacion(int id, [FromBody] dynamic reservacion)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"UPDATE reservaciones 
                        SET usuario_id = @usuario_id, lugar_id = @lugar_id, fecha_inicio = @fecha_inicio, fecha_fin = @fecha_fin, estado_id = @estado_id 
                        WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("usuario_id", (long)reservacion.usuario_id);
                command.Parameters.AddWithValue("lugar_id", (long)reservacion.lugar_id);
                command.Parameters.AddWithValue("fecha_inicio", (DateTime)reservacion.fecha_inicio);
                command.Parameters.AddWithValue("fecha_fin", (DateTime)reservacion.fecha_fin);
                command.Parameters.AddWithValue("estado_id", (long)reservacion.estado_id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Reservación actualizada exitosamente.");
                }
                else
                {
                    return NotFound("Reservación no encontrada.");
                }
            }
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservacion(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "DELETE FROM reservaciones WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Reservación eliminada exitosamente.");
                }
                else
                {
                    return NotFound("Reservación no encontrada.");
                }
            }
        }
    }
}
