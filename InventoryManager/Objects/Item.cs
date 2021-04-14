using InventoryManager.Objects;
using System.Collections.Generic;

namespace InventoryManager
{
    public class Item
    {
        public string Name { get; set; }
        public string Quality { get; set; }
        public int Sockets { get; set; }
        public string Type { get; set; }
        public int iLvl { get; set; }
        public string OwningCharacter { get; set; }
        private List<Stat> stats;

        internal List<Stat> Stats { get => stats; set => stats = value; }
    }

    

}
