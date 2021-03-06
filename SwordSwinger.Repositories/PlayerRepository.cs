﻿using SwordSwinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public class PlayerRepository
	{
		private WeaponRepository _weaponRepo = new WeaponRepository();
		Random _random = new Random();

		private List<string> _enemyNames = new List<string>()
		{
			"Bill The Snake Smasher ", "Nick the Tax Attorney", "Jim the Sword Slinger"
		};

		public IPlayer CreateNewEnemy()
		{
			Array values = Enum.GetValues(typeof(WeaponType));
			var weapon = _weaponRepo.CreateNewWeapon();

			var enemy = new Enemy
			{
				Lives = 1,
				Armor = 14,
				Experience = _random.Next(20, 60),
				Level = 1,
				Name = _enemyNames[_random.Next(_enemyNames.Count)],
				Weapon = weapon,
				Health = 100,
				MaxHealth = 100,
				MissChance = 10,
				Gold = _random.Next(100),
			};

			enemy.Weapon.Experience = _random.Next(100);

			return enemy;
		}

		public bool CheckExperience(IPlayer player)
		{
			if(player.Experience >= 100)
			{
				return true;
			}
			return false;
		}
	}
}
