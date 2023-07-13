using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	public class UpdateNormalItemStrategy : IUpdateItemStrategy
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
