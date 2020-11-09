using System;

namespace Core.Exception
{
    public class EntityNotFoundException: System.Exception
    {
        public EntityNotFoundException()
        {
            
        }
        public EntityNotFoundException(string message): base(message)
        {
            
        }
    }
}