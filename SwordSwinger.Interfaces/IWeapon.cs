using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Interfaces
{
	public enum WeaponType
	{
		Axe, Sword, Hammer, AR15
	}

	public interface IWeapon
	{
		string Name { get; set; }

		WeaponType WeaponType { get; set; }

		int Damage { get; set; }

		int Durability { get; set; }

		int WeaponLevel { get; set; }

		int Experience { get; set; }

		void GainLevel();
	}
}
