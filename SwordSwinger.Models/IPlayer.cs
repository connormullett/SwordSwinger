using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{

	public interface IPlayer	
	{
		string Name { get; set; }

		Weapon Weapon { get; set; }

		int Lives { get; set; }

		int Armor { get; set; }

		int Health { get; set; }

		int MaxHealth { get; set; }

		int Experience { get; set; }

		int Level { get; set; }

		int CriticalStrikeChance { get; set; }

		int MissChance { get; set; }

		void DoDamage(int dmg);

		void GainLevel();
	}
}
