using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	public class UpdateSmellyItemStrategy : IUpdateItemStrategy
	{
		public void Execute(Item item)
		{
			item.SellIn -= GildedTrosAppSettings.SellInUpdateAmount;

			if (item.Quality == GildedTrosAppSettings.MinAllowedQuality)
			{
				return;
			}

			UpdateQuality(item);
		}

		private void UpdateQuality(Item item)
		{
			// Smelly items degrade in Quality twice as fast as normal items
			item.Quality -= 2 * GildedTrosAppSettings.QualityUpdateAmount;

			// Smelly items degrade in Quality twice as fast as normal items
			// And once the sell by date has passed, Quality degrades twice as fast
			if (item.SellIn < 0)
			{
				item.Quality -= 2 * GildedTrosAppSettings.QualityUpdateAmount;
			}

			item.Quality = Math.Max(item.Quality, GildedTrosAppSettings.MinAllowedQuality);
		}
	}
}
