using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	internal class UpdateGoodWineItemStrategy : IUpdateItemStrategy
	{
		private Item _item;

		public UpdateGoodWineItemStrategy(Item item)
			=> _item = item;

		public void Execute()
		{
			_item.SellIn -= GildedTrosAppSettings.SellInUpdateAmount;

			if (_item.Quality == GildedTrosAppSettings.MaxAllowedQuality)
			{
				return;
			}

			UpdateQuality(_item);
		}

		private void UpdateQuality(Item item)
		{
			// "Good Wine" actually increases in Quality the older it gets
			item.Quality += GildedTrosAppSettings.QualityUpdateAmount;

			// Once the sell by date has passed, Quality increases twice as fast
			if (item.SellIn < 0)
			{
				item.Quality += GildedTrosAppSettings.QualityUpdateAmount;
			}

			item.Quality = Math.Min(item.Quality, GildedTrosAppSettings.MaxAllowedQuality);
		}
	}
}
