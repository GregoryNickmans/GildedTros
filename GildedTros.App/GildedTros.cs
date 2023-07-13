using GildedTros.App.Business.Strategies;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        private IList<Item> Items;

        public GildedTros(IList<Item> items)
			=> Items = items;

		public void UpdateQuality()
        {
            foreach (var item in Items) 
			{
				IUpdateItemStrategy strategy = null;
				switch (item.Name)
				{
					case "Good Wine":
						strategy = new UpdateGoodWineItemStrategy();
						break;
					case "B-DAWG Keychain":
						//A legendary item never has to be sold or decreases in Quality
						break;
					case "Backstage passes for Re:factor":
					case "Backstage passes for HAXX":
						strategy = new UpdateBackstagePassItemStrategy();
						break;
					case "Duplicate Code":
					case "Long Methods":
					case "Ugly Variable Names":
						strategy = new UpdateSmellyItemStrategy();
						break;
					default:
						strategy = new UpdateNormalItemStrategy();
						break;
				}

				strategy?.Execute(item);
			}
        }
	}
}
