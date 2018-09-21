using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Hammer : Weapon
	{
		public override void GainLevel()
		{
			WeaponLevel += 1;
			Experience = 0;
		}
	}
}
