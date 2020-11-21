using System;

namespace TesteIntegrado.Factory
{
    public interface IFactoryFake<T>
    {
        T Constroi();

        T Constroi(string id);

    }
}