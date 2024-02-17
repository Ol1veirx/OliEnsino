namespace Modelo1.Cadastros
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Nome { get; set; }
        public int InstituicaoID { get; set; }
        public Instituicao? Instituicao { get; set;}
    }
}
