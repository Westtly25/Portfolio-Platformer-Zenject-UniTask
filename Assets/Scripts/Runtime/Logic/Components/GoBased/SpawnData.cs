using System;

namespace Scripts.Components.GoBased
{
    public partial class SpawnListComponent
    {
        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component;
        }
    }
}