namespace ApiLafise.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }
        public double SaldoInicial { get; set; }
        public decimal Saldo { get; set; }
    }
}
