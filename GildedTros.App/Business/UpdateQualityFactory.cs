

using GildedTros.App.Business.Strategies;

namespace GildedTros.App.Business
{
	public static class UpdateQualityFactory
	{
		public static IUpdateItemStrategy DefineStrategy(Item item) 
		{
			switch (item.Name)
			{
				case "Good Wine":
					return new UpdateGoodWineItemStrategy(item);

				case "B-DAWG Keychain":
					//A legendary item never has to be sold or decreases in Quality
					return null;

				case "Backstage passes for Re:factor":
				case "Backstage passes for HAXX":
					return new UpdateBackstagePassItemStrategy(item);

				case "Duplicate Code":
				case "Long Methods":
				case "Ugly Variable Names":
					return new UpdateSmellyItemStrategy(item);

				default:
					return new UpdateNormalItemStrategy(item);
			}
		}
	}
}
