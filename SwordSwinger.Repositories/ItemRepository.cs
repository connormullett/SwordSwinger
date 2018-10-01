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
		public void UseItem(Player player, IShopItem item)
		{
			switch (item.Name)
			{
				case "Elixer":
					player.FleeChance += 20;
					break;
				case "Potion of Health":
					Heal(player);
					break;
				case "Power Up":
					player.Weapon.Damage += 15;
					break;
			}

			
			player._inventory.Remove(item);
		}

		private void Heal(Player player)
		{
			if(player.Health + 20 > player.MaxHealth)
			{
				player.Health = player.MaxHealth;
			}
			else
			{
				player.Health += 20;
			}
		}
	}
}
