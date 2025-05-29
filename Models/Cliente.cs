namespace ApiLafise.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public double Ingresos { get; set; }
    }
}
