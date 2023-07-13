using System.Threading.Tasks;

namespace GildedTros.App.Business.Strategies
{
	public interface IUpdateItemStrategy
	{
		public void Execute(Item item);
	}
}
