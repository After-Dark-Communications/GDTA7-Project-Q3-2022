using Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ShipSelection
{
    public static class SelectionCollectionInitializer
    {
        public static SelectableCollection CreateNewSelectableCollection<T>(List<T> parts) where T : Part
        {
            SelectableCollection selectableCollection = new SelectableCollection();

            foreach (Part part in parts)
            {
                Selectable toAdd = new Selectable() { Part = part };

                selectableCollection.Selectables.Add(toAdd);
            }

            return selectableCollection;
        }
    }
}
