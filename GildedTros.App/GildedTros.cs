using GildedTros.App.Business;
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
				var strategy = UpdateQualityFactory.DefineStrategy(item);
				strategy?.Execute();
			}
        }
	}
}
