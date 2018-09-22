using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public enum WeaponType
	{
		Sword, Hammer, Axe, AR15
	}

	public abstract class Weapon
	{
		public string Name { get; set; }
		public WeaponType WeaponType { get; set; }
		public int Damage { get; set; }
		public int Durability { get; set; }
		public int WeaponLevel { get; set; }
		public int Experience { get; set; }
		public string Description { get; set; }

		public abstract void GainLevel();
	}
}
