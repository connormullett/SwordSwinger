using SwordSwinger.Interfaces;
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
		public IWeapon Weapon { get; set; }
		public int Lives { get; set; }
		public int Armor { get; set; }
		public int Health { get; set; }
		public int Experience { get; set; }
		public int Level { get; set; }

		public void DoDamage()
		{
			throw new NotImplementedException();
		}

		public void GainLevel()
		{
			throw new NotImplementedException();
		}
	}
}
