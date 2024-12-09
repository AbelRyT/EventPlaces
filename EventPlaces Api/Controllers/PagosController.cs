using Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[Route("api/[controller]/[action]")]
[ApiController]
public class PagosController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PagosController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/Pagos
    [HttpGet]
    public IActionResult GetPagos()
    {
        var pagos = new List<PagoDto>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT id, reservacion_id, monto, fecha_pago, metodo_pago, estado FROM pagos";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagos.Add(new PagoDto
                        {
                            Id = reader.GetInt32(0),
                            ReservacionId = reader.GetInt32(2),
                            Monto = reader.GetDecimal(3),
                            FechaPago = reader.GetDateTime(4),
                            MetodoPago = reader.GetString(5),
                            Estado = reader.GetString(6)
                        });
                    }
                }
            }
        }
        return Ok(pagos);
    }

    // GET: api/Pagos/{id}
    [HttpGet("{id}")]
    public IActionResult GetPagoById(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT id, usuario_id, reservacion_id, monto, fecha_pago, metodo_pago, estado FROM pagos WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var pago = new PagoDto
                        {
                            Id = reader.GetInt32(0),
                            ReservacionId = reader.GetInt32(2),
                            Monto = reader.GetDecimal(3),
                            FechaPago = reader.GetDateTime(4),
                            MetodoPago = reader.GetString(5),
                            Estado = reader.GetString(6)
                        };
                        return Ok(pago);
                    }
                    else
                    {
                        return NotFound("Pago no encontrado.");
                    }
                }
            }
        }
    }

    // POST: api/Pagos
    [HttpPost]
    public IActionResult CreatePago([FromBody] PagoDto pago)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction()) // Iniciar una transacción
            {
                try
                {
                    // 1. Insertar el pago
                    var sqlPago = @"INSERT INTO pagos ( reservacion_id, monto, fecha_pago, metodo_pago, estado_id) 
                            VALUES ( @reservacion_id, @monto, @fecha_pago, @metodo_pago, @estado_id)";
                    using (var commandPago = new NpgsqlCommand(sqlPago, connection))
                    {
                        commandPago.Parameters.AddWithValue("reservacion_id", pago.ReservacionId);
                        commandPago.Parameters.AddWithValue("monto", pago.Monto);
                        commandPago.Parameters.AddWithValue("fecha_pago", pago.FechaPago);
                        commandPago.Parameters.AddWithValue("metodo_pago", 2);
                        commandPago.Parameters.AddWithValue("estado_id",2);
                        commandPago.ExecuteNonQuery();
                    }

                    // 2. Actualizar el estado de la reservación a 2 (por ejemplo, pagado)
                    var sqlActualizarReservacion = @"UPDATE reservaciones 
                                             SET estado_id = @estado_id 
                                             WHERE id = @reservacion_id";
                    using (var commandActualizar = new NpgsqlCommand(sqlActualizarReservacion, connection))
                    {
                        commandActualizar.Parameters.AddWithValue("estado_id", 2); // Nuevo estado
                        commandActualizar.Parameters.AddWithValue("reservacion_id", pago.ReservacionId);
                        commandActualizar.ExecuteNonQuery();
                    }

                    // Confirmar transacción
                    transaction.Commit();
                    return Ok("Pago registrado exitosamente.");
                }
                catch (Exception ex)
                {
                    // Revertir en caso de error
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                    throw new Exception();
                }
            }
        }

       
    }

    // PUT: api/Pagos/{id}
    [HttpPut("{id}")]
    public IActionResult UpdatePago(int id, [FromBody] PagoDto pago)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"UPDATE pagos 
                        SET usuario_id = @usuario_id, reservacion_id = @reservacion_id, monto = @monto, fecha_pago = @fecha_pago, 
                            metodo_pago = @metodo_pago, estado = @estado 
                        WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("usuario_id", pago.UsuarioId);
                command.Parameters.AddWithValue("reservacion_id", pago.ReservacionId);
                command.Parameters.AddWithValue("monto", pago.Monto);
                command.Parameters.AddWithValue("fecha_pago", pago.FechaPago);
                command.Parameters.AddWithValue("metodo_pago", pago.MetodoPago);
                command.Parameters.AddWithValue("estado", pago.Estado);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Pago actualizado exitosamente.");
                }
                else
                {
                    return NotFound("Pago no encontrado.");
                }
            }
        }
    }

    // DELETE: api/Pagos/{id}
    [HttpDelete("{id}")]
    public IActionResult DeletePago(int id)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "DELETE FROM pagos WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Pago eliminado exitosamente.");
                }
                else
                {
                    return NotFound("Pago no encontrado.");
                }
            }
        }
    }
}
