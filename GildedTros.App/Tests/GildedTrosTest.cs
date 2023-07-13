using System.Collections.Generic;
using GildedTros.App.Configuration;
using Xunit;

namespace GildedTros.App.Tests
{
    public class GildedTrosTest
    {
        private const string _normalItemName = "foo";
        private const string _goodWineItemName = "Good Wine";
        private const string _keychainItemName = "B-DAWG Keychain";
        private const string _backstagePassesItemName = "Backstage passes for Re:factor";
        private const string _smellyItemName = "Duplicate Code";

        [Fact]
        public void NormalItem()
        {
            var sellIn = 10;
            var quality = 8;
            var result = ExecuteTest(_normalItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality - GildedTrosAppSettings.DecreaseOfQuality, result[0].Quality);
        }

        [Fact]
        public void NormalItem_SellDateInThePast()
        {
            var sellIn = 0;
            var quality = 8;
            var result = ExecuteTest(_normalItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality - 2 * GildedTrosAppSettings.DecreaseOfQuality, result[0].Quality);
        }

        [Fact]
        public void NormalItem_SellDateInThePast_MinQuality()
        {
            var sellIn = 0;
            var quality = GildedTrosAppSettings.MinAllowedQuality;
            var result = ExecuteTest(_normalItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(GildedTrosAppSettings.MinAllowedQuality, result[0].Quality);
        }

        [Fact]
        public void GoodWine()
        {
            var sellIn = 10;
            var quality = 8;
            var result = ExecuteTest(_goodWineItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality + GildedTrosAppSettings.DecreaseOfQuality, result[0].Quality);
        }

        [Fact]
        public void GoodWine_SellDateInThePast_MaxQuality()
        {
            var sellIn = 0;
            var quality = GildedTrosAppSettings.MaxAllowedQuality;
            var result = ExecuteTest(_goodWineItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(GildedTrosAppSettings.MaxAllowedQuality, result[0].Quality);
        }

        [Fact]
        public void BDAWGKeychain()
        {
            var sellIn = 10;
            var quality = 8;
            var result = ExecuteTest(_keychainItemName, sellIn, quality);

            Assert.Equal(sellIn, result[0].SellIn);
            Assert.Equal(quality, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses()
        {
            var sellIn = 15;
            var quality = 10;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality + GildedTrosAppSettings.DecreaseOfQuality, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_MaxQuality()
        {
            var sellIn = 15;
            var quality = GildedTrosAppSettings.MaxAllowedQuality;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(GildedTrosAppSettings.MaxAllowedQuality, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_10Days()
        {
            var sellIn = 10;
            var quality = 10;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality + 2, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_6Days()
        {
            var sellIn = 6;
            var quality = 10;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality + 2, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_5Days()
        {
            var sellIn = 5;
            var quality = 10;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality + 3, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_5Days_MaxQuality()
        {
            var sellIn = 5;
            var quality = GildedTrosAppSettings.MaxAllowedQuality - 1;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(GildedTrosAppSettings.MaxAllowedQuality, result[0].Quality);
        }

        [Fact]
        public void BackstagePasses_InThePast()
        {
            var sellIn = 0;
            var quality = 10;
            var result = ExecuteTest(_backstagePassesItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(0, result[0].Quality);
        }

        [Fact]
        public void SmellyItem()
        {
            var sellIn = 20;
            var quality = 10;
            var result = ExecuteTest(_smellyItemName, sellIn, quality);

            Assert.Equal(sellIn - GildedTrosAppSettings.DecreaseOfSellIn, result[0].SellIn);
            Assert.Equal(quality - 2 * GildedTrosAppSettings.DecreaseOfQuality, result[0].Quality);
        }

        private List<Item> ExecuteTest(string name, int sellIn, int quality)
        {
            var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
            GildedTros app = new GildedTros(items);
            app.UpdateQuality();

            return items;
        }
    }
}