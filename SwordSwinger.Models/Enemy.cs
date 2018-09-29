using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Enemy : IPlayer
	{
		public string Name { get; set; }
		public Weapon Weapon { get; set; }
		public int Lives { get; set; }
		public int Armor { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public int Experience { get; set; }
		public int Level { get; set; }
		public int CriticalStrikeChance { get; set; }
		public int MissChance { get; set; }
		public int Gold { get; set; }
		public int FleeChance { get; set; }
		public ICollection<object> Inventory { get; set; }

		public void DoDamage(int dmg)
		{
			var damage = dmg - Armor;
			Health -= damage;
		}

		public void GainLevel()
		{
			Experience = 0;
			Level++;
			MaxHealth += 20;
			Health = MaxHealth;
			Armor += 5;
			Weapon.GainLevel();
		}
	}
}
