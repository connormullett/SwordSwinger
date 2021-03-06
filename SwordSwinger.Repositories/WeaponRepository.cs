﻿using SwordSwinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public class WeaponRepository
	{
		public bool CheckExperience(Weapon weapon)
		{
			if(weapon.Experience > 100)
			{
				return true;
			}
			return false;
		}

		public Weapon CreateNewWeapon(WeaponType weapon)
		{
			switch (weapon)
			{
				case WeaponType.Axe:
					return new Axe
					{
						Name = "Axe",
						WeaponType = weapon,
						Damage = 25,
						Durability = 150,
						WeaponLevel = 1,
						Experience = 0,
					};
				case WeaponType.Hammer:
					return new Hammer
					{
						Name = "Hammer",
						Damage = 20,
						WeaponType = weapon,
						Durability = 120,
						WeaponLevel = 1,
						Experience = 0
					};
				case WeaponType.Sword:
					return new Sword
					{
						Name = "Sword",

						WeaponType = WeaponType.Sword,
						Damage = 30,
						Durability = 90,
						WeaponLevel = 1,
						Experience = 0
					};
				default:
					return new AR15
					{
						Name = "Assault Rifle 15",
						WeaponType = WeaponType.AR15,
						Damage = 50,
						Durability = 300,
						WeaponLevel = 1,
						Experience = 0,
						Description = "semi-full auto assault rifle 15 with high capacity belt loaded caliber clip.. and a bump stock"
					};
			}
		}

		public Weapon CreateNewWeapon()
		{
			var w = (WeaponType)(new Random().Next(4));
			switch (w)
			{
				case WeaponType.Axe:

					return new Axe
					{
						Name = "Axe",
						Damage = 25,
						WeaponType = WeaponType.Axe,
						Durability = 150,
						WeaponLevel = 1,
						Experience = 0
					};

				case WeaponType.Hammer:

					return new Hammer
					{
						Name = "Hammer",
						Damage = 20,
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
						Damage = 30,
						Durability = 90,
						WeaponLevel = 1,
						Experience = 0
					};

				default:
					return new AR15
					{
						Name = "Assault Rifle 15",
						WeaponType = WeaponType.AR15,
						Damage = 50,
						Durability = 300,
						WeaponLevel = 100,
						Experience = 0,
						Description = "semi-full auto assault rifle 15 with high capacity belt loaded caliber clip.. and a bump stock"
					};
			}
		}
	}
}
