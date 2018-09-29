using SwordSwinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public class ItemRepository
	{
		public void UseElixer(Player player, IShopItem item)
		{
			switch (item.Name)
			{
				case "Elixer":
					player.FleeChance += 20;
					break;
				case "Potion":
					player.Health += 20;
					break;
				case "PowerUp":
					player.Weapon.Damage += 15;
					break;
			}

			player._inventory.Remove(item);
		}
	}
}
