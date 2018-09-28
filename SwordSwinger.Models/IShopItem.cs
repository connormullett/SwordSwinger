using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public enum ItemType
	{
		Potion, Elixer, PowerUp
	}

	public interface IShopItem
	{
		string Name { get; set; }

		int GoldValue { get; set; }

		string Description { get; set; }

		ItemType ItemType { get; set; }
	}
}
