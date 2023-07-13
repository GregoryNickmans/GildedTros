using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	internal class UpdateNormalItemStrategy : IUpdateItemStrategy
	{
		private Item _item;

		public UpdateNormalItemStrategy(Item item)
			=> _item = item;

		public void Execute()
		{
			_item.SellIn -= GildedTrosAppSettings.SellInUpdateAmount;

			if (_item.Quality == GildedTrosAppSettings.MinAllowedQuality)
			{
				return;
			}

			UpdateQuality(_item);
		}

		private void UpdateQuality(Item item) 
		{
			item.Quality -= GildedTrosAppSettings.QualityUpdateAmount;

			// Once the sell by date has passed, Quality degrades twice as fast
			if (item.SellIn < 0)
			{
				item.Quality -= GildedTrosAppSettings.QualityUpdateAmount;
			}

			item.Quality = Math.Max(item.Quality, GildedTrosAppSettings.MinAllowedQuality);
		}
	}
}
