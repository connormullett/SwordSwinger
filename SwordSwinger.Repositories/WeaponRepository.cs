using SwordSwinger.Interfaces;
using SwordSwinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public class WeaponRepository
	{
		public IWeapon CreateNewWeapon(WeaponType type)
		{
			switch (type)
			{
				case WeaponType.Axe:

					return new Axe
					{
						Name = "Axe",
						Damage = 100,
						WeaponType = WeaponType.Axe,
						Durability = 150,
						WeaponLevel = 1,
						Experience = 0
					};

				case WeaponType.Hammer:

					return new Hammer
					{
						Name = "Hammer",
						Damage = 120,
						WeaponType = WeaponType.Hammer,
						Durability = 120,
						WeaponLevel = 1,
						Experience = 0
					};

				case WeaponType.Sword:

					return new Sword
					{
						Name = "Sword",
						WeaponType = WeaponType.Sword,
						Damage = 130,
						Durability = 90,
						WeaponLevel = 1,
						Experience = 0
					};

				default:
					return new AR15
					{
						Name = "Assault Rifle 15",
						WeaponType = WeaponType.AR15,
						Damage = 200,
						Durability = 300,
						WeaponLevel = 100,
						Experience = 0,
						Description = "semi-full auto assault rifle 15 with high capacity caliber clip.. and a bump stock"
					};
			
			}
		}
	}
}
