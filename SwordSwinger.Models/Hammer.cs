﻿using SwordSwinger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Hammer : IWeapon
	{
		public string Name { get; set; }
		public WeaponType WeaponType { get; set; }
		public int Damage { get; set; }
		public int Durability { get; set; }
		public int WeaponLevel { get; set; }
		public int Experience { get; set; }

		public void GainLevel()
		{
			WeaponLevel += 1;
			Experience = 0;
		}
	}
}
