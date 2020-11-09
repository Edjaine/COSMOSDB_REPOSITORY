using System;

namespace Core.Exception
{
    public class EntityAlreadyExistsException: System.Exception
    {
        public EntityAlreadyExistsException()
        {
            
        }
        public EntityAlreadyExistsException(string message) :  base(message)
        {
            
        }
        
    }
}