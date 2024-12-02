using Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[Route("api/[controller]/[action]")]
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
        var reservaciones = new List<ReservacionDto>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"SELECT r.id, r.usuario_id, r.lugar_id, r.fecha_inicio, r.fecha_fin, r.estado_id, e.nombre AS estado_nombre
                    FROM reservaciones r
                    JOIN estados e ON r.estado_id = e.id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservaciones.Add(new ReservacionDto
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt32(1),
                            LugarId = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            EstadoId = reader.GetInt32(5),
                            EstadoNombre = reader.GetString(6) // Extraer el nombre del estado
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
            var sql = @"SELECT r.id, r.usuario_id, r.lugar_id, r.fecha_inicio, r.fecha_fin, r.estado_id, e.nombre AS estado_nombre
                    FROM reservaciones r
                    JOIN estados e ON r.estado_id = e.id
                    WHERE r.id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var reservacion = new ReservacionDto
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt32(1),
                            LugarId = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            EstadoId = reader.GetInt32(5),
                            EstadoNombre = reader.GetString(6) // Extraer el nombre del estado
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
    public IActionResult CreateReservacion([FromBody] ReservacionDto reservacion)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();

            // Validación para evitar solapamientos
            var checkSql = @"
            SELECT fecha_inicio, fecha_fin 
            FROM reservaciones 
            WHERE lugar_id = @lugar_id AND estado_id <> 3 
                AND(fecha_inicio <= @fecha_fin AND fecha_fin >= @fecha_inicio)";
    

        using (var checkCommand = new NpgsqlCommand(checkSql, connection))
            {
                checkCommand.Parameters.AddWithValue("lugar_id", reservacion.LugarId);
                checkCommand.Parameters.AddWithValue("fecha_inicio", reservacion.FechaInicio);
                checkCommand.Parameters.AddWithValue("fecha_fin", reservacion.FechaFin);

                using (var reader = checkCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var fechaInicioExistente = reader.GetDateTime(0);
                        var fechaFinExistente = reader.GetDateTime(1);
                        return Conflict($"Ya existe una reservación del {fechaInicioExistente:dd/MM/yyyy} al {fechaFinExistente:dd/MM/yyyy}.");
                    }
                }
            }

            // Inserción si no hay conflictos
            var sql = @"INSERT INTO reservaciones (usuario_id, lugar_id, fecha_inicio, fecha_fin, estado_id) 
                    VALUES (@usuario_id, @lugar_id, @fecha_inicio, @fecha_fin, @estado_id)";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("usuario_id", reservacion.UsuarioId);
                command.Parameters.AddWithValue("lugar_id", reservacion.LugarId);
                command.Parameters.AddWithValue("fecha_inicio", reservacion.FechaInicio);
                command.Parameters.AddWithValue("fecha_fin", reservacion.FechaFin);
                command.Parameters.AddWithValue("estado_id", reservacion.EstadoId);
                command.ExecuteNonQuery();
            }
        }
        return Ok("Reservación creada exitosamente.");
    }



    [HttpPut("{id}")]
    public IActionResult UpdateReservacion(int id, [FromBody] ReservacionDto reservacion)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();

            // Validación de solapamiento, excluyendo la reservación que se está actualizando
            var checkSql = @"
            SELECT fecha_inicio, fecha_fin 
            FROM reservaciones 
            WHERE lugar_id = @lugar_id AND id <> @id AND estado_id <> 3
            AND (fecha_inicio <= @fecha_fin AND fecha_fin >= @fecha_inicio)";

            using (var checkCommand = new NpgsqlCommand(checkSql, connection))
            {
                checkCommand.Parameters.AddWithValue("lugar_id", reservacion.LugarId);
                checkCommand.Parameters.AddWithValue("id", id);
                checkCommand.Parameters.AddWithValue("fecha_inicio", reservacion.FechaInicio);
                checkCommand.Parameters.AddWithValue("fecha_fin", reservacion.FechaFin);

                using (var reader = checkCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var fechaInicioExistente = reader.GetDateTime(0);
                        var fechaFinExistente = reader.GetDateTime(1);
                        return Conflict($"Ya existe una reservación del {fechaInicioExistente:dd/MM/yyyy} al {fechaFinExistente:dd/MM/yyyy}.");
                    }
                }
            }

            // Actualización si no hay conflictos
            var sql = @"UPDATE reservaciones 
                    SET usuario_id = @usuario_id, lugar_id = @lugar_id, fecha_inicio = @fecha_inicio, fecha_fin = @fecha_fin, estado_id = @estado_id 
                    WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("usuario_id", reservacion.UsuarioId);
                command.Parameters.AddWithValue("lugar_id", reservacion.LugarId);
                command.Parameters.AddWithValue("fecha_inicio", reservacion.FechaInicio);
                command.Parameters.AddWithValue("fecha_fin", reservacion.FechaFin);
                command.Parameters.AddWithValue("estado_id", reservacion.EstadoId);

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
