using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Potion : IShopItem
	{
		public string Name { get; set; }

		public int GoldValue { get; set; }

		public string Description { get; set; }

		public ItemType ItemType { get; set; }
	}
}
