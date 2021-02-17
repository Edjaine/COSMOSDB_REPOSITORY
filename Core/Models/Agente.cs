namespace Core.Models
{
    public class Agente<T> : Entity
    {        
        public T documento { get; set; }
    }
}