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
		public Weapon CreateNewWeapon(WeaponType type)
		{
			switch (type)
			{
				case WeaponType.Axe:

					var axe = new Axe
					{
						Name = "Axe",
						Damage = 100,
						WeaponType = WeaponType.Axe,
						Durability = 150,
						WeaponLevel = 1,
						Experience = 0
					};
					return axe;

				case WeaponType.Hammer:

					var hammer = new Hammer
					{
						Name = "Hammer",
						Damage = 120,
						WeaponType = WeaponType.Hammer,
						Durability = 120,
						WeaponLevel = 1,
						Experience = 0
					};
					return hammer;

				case WeaponType.Sword:

					var sword = new Sword
					{
						Name = "Sword",
						WeaponType = WeaponType.Sword,
						Damage = 130,
						Durability = 90,
						WeaponLevel = 1,
						Experience = 0
					};
					return sword;

				default:
					var ar = new AR15
					{
						Name = "Assault Rifle 15",
						WeaponType = WeaponType.AR15,
						Damage = 200,
						Durability = 300,
						WeaponLevel = 100,
						Experience = 0,
						Description = "semi-full auto assault rifle 15 with high capacity belt loaded caliber clip.. and a bump stock"
					};
					return ar;
			}
		}
	}
}
