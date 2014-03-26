namespace Ropnoy.Lattice.Domain
{
    public class Parameter
    {
        public int Id { get; set; }

        public string ParameterType { get; set; }

        public int Position { get; set; }

        public Call Call { get; set; }
    }
}