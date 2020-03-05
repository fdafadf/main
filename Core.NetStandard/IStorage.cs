using System;

namespace Core.NetStandard
{
    public interface IStorage
    {
        T Read<T>(string resourceName);
        void Write<T>(string resourceName, T resource);
    }
}
