using SwordSwinger.Interfaces;
using SwordSwinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public class PlayerRepository
	{
		public IPlayer CreateNewEnemy(IWeapon weapon, string name)
		{
			return new Enemy
			{
				Lives = 1,
				Armor = 30,
				Experience = 0,
				Level = 1,
				Name = name,
				Weapon = weapon,
				Health = 100,
				MaxHealth = 100
			};
		}
	}
}
