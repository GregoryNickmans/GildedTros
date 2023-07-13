using GildedTros.App.Configuration;
using System;

namespace GildedTros.App.Business.Strategies
{
	internal class UpdateBackstagePassItemStrategy : IUpdateItemStrategy
	{
		private const int UPCOMING_TRESHOLD_1 = 10;
		private const int UPCOMING_TRESHOLD_2 = 5;
		private const int QUALITY_UPDATE_TRESHOLD_1_REACHED = 2;
		private const int QUALITY_UPDATE_TRESHOLD_2_REACHED = 3;

		private Item _item;

		public UpdateBackstagePassItemStrategy(Item item)
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
			// Quality drops to minimum after the conference
			if (item.SellIn < 0)
			{
				item.Quality = GildedTrosAppSettings.MinAllowedQuality;
				return;
			}
			else if (item.SellIn < UPCOMING_TRESHOLD_2)
			{
				item.Quality += QUALITY_UPDATE_TRESHOLD_2_REACHED;
			}
			else if (item.SellIn < UPCOMING_TRESHOLD_1)
			{
				item.Quality += QUALITY_UPDATE_TRESHOLD_1_REACHED;
			}
			else 
			{
				item.Quality += GildedTrosAppSettings.QualityUpdateAmount;
			}

			item.Quality = Math.Min(item.Quality, GildedTrosAppSettings.MaxAllowedQuality);
		}
	}
}
