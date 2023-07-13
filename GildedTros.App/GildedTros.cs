using GildedTros.App.Business;
using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        private readonly IList<Item> _items;

        public GildedTros(IList<Item> items)
			=> _items = items ?? Array.Empty<Item>();

		public void UpdateQuality()
        {
			foreach (var item in _items)
			{
				var strategy = UpdateQualityFactory.DefineStrategy(item);
				strategy?.Execute();
			}
        }
	}
}
