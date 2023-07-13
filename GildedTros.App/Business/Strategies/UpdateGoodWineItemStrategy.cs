using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	public class UpdateGoodWineItemStrategy : IUpdateItemStrategy
	{
		public void Execute(Item item)
		{
			item.SellIn -= GildedTrosAppSettings.SellInUpdateAmount;

			if (item.Quality == GildedTrosAppSettings.MaxAllowedQuality)
			{
				return;
			}

			UpdateQuality(item);
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
