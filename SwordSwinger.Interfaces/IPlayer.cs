using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Interfaces
{

	public interface IPlayer	
	{
		string Name { get; set; }

		IWeapon Weapon { get; set; }

		int Lives { get; set; }

		int Armor { get; set; }

		int Health { get; set; }

		int Experience { get; set; }

		int Level { get; set; }

		void DoDamage();

		void GainLevel();
	}
}
