using System;

namespace Core.Common.GameResources
{
    [Serializable]
    public class Resource
    {
        public ResourceType resourceType;
        public int id;
        public int value;
    }
}