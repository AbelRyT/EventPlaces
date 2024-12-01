using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[Route("api/[controller]")]
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
        var pagos = new List<dynamic>();
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = "SELECT * FROM pagos";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagos.Add(new
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt64(1),
                            ReservacionId = reader.GetInt64(2),
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
            var sql = "SELECT * FROM pagos WHERE id = @id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var pago = new
                        {
                            Id = reader.GetInt32(0),
                            UsuarioId = reader.GetInt64(1),
                            ReservacionId = reader.GetInt64(2),
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
    public IActionResult CreatePago([FromBody] dynamic pago)
    {
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var sql = @"INSERT INTO pagos (usuario_id, reservacion_id, monto, fecha_pago, metodo_pago, estado) 
                        VALUES (@usuario_id, @reservacion_id, @monto, @fecha_pago, @metodo_pago, @estado)";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("usuario_id", (long)pago.usuario_id);
                command.Parameters.AddWithValue("reservacion_id", (long)pago.reservacion_id);
                command.Parameters.AddWithValue("monto", (decimal)pago.monto);
                command.Parameters.AddWithValue("fecha_pago", (DateTime)pago.fecha_pago);
                command.Parameters.AddWithValue("metodo_pago", (string)pago.metodo_pago);
                command.Parameters.AddWithValue("estado", (string)pago.estado);
                command.ExecuteNonQuery();
            }
        }
        return Ok("Pago registrado exitosamente.");
    }

    // PUT: api/Pagos/{id}
    [HttpPut("{id}")]
    public IActionResult UpdatePago(int id, [FromBody] dynamic pago)
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
                command.Parameters.AddWithValue("usuario_id", (long)pago.usuario_id);
                command.Parameters.AddWithValue("reservacion_id", (long)pago.reservacion_id);
                command.Parameters.AddWithValue("monto", (decimal)pago.monto);
                command.Parameters.AddWithValue("fecha_pago", (DateTime)pago.fecha_pago);
                command.Parameters.AddWithValue("metodo_pago", (string)pago.metodo_pago);
                command.Parameters.AddWithValue("estado", (string)pago.estado);

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
