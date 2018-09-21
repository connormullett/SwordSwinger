using SwordSwinger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Player : IPlayer
	{
		public string Name { get; set; }
		public IWeapon Weapon { get; set; }
		public int Lives { get; set; }
		public int Armor { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public int Experience { get; set; }
		public int Level { get; set; }
		public int CriticalStrikeChange { get; set; }

		public void DoDamage(int dmg)
		{
			Health -= dmg;
		}

		public void GainLevel()
		{
			Level++;
			Experience = 0;
			Health += 20;
		}
	}
}
