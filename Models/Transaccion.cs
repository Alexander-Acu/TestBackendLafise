namespace ApiLafise.Models
{
        public class Transaccion
        {
            public int Id { get; set; }
            public int CuentaId { get; set; }
            public string NumeroCuenta { get; set; }
            public string Tipo { get; set; } //Deposito o Retiro
            public double Monto { get; set; }
            public double SaldoDespues { get; set; }
            public DateTime FechaTransaccion { get; set; }
        }
}


