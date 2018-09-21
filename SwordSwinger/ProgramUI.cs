using SwordSwinger.Models;
using SwordSwinger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger
{
	public class ProgramUI
	{
		private WeaponRepository _weaponRepo = new WeaponRepository();
		private PlayerRepository _playerRepo = new PlayerRepository();
		private Player _player;
		private Enemy _enemy;
		private Random _random = new Random();

		internal void Run()
		{
			MainMenu();
		}

		private void MainMenu()
		{
			Console.Clear();
			Console.WriteLine("WELCOME TO SWORD SWINGERS ARENA \n1. New Character\n2. Fight!\n3. See Stats\n4. Exit");

			switch (ParseInput())
			{
				case 1:
					NewCharacter();
					break;
				case 2:
					Fight();
					break;
				case 3:
					SeeStats();
					break;
				case 4:
					break;
				default:
					Console.WriteLine("Enter Valid Input");
					Console.ReadKey();
					MainMenu();
					break;
			}
		}

		private void NewCharacter()
		{
			Console.Clear();
			Console.Write("Enter Name: ");
			var name = Console.ReadLine();

			_player = new Player
				{
					Name = name,
					Lives = 3,
					Armor = 30,
					Experience = 0,
					Level = 1,
					Health = 100,
					MaxHealth = 100,
					CriticalStrikeChance = 20,
					MissChance = 10,
					EnemiesSlain = 0
				};

			SelectWeapon();

			Console.Clear();
			Console.WriteLine("Character Created\n");
			Console.ReadKey();
			MainMenu();
			
		}
		
		private void SelectWeapon()
		{ 
			Console.WriteLine("Choose your weapon\n1. Axe\n2. Hammer\n3. Sword");

			switch (ParseInput())
			{
				case 1:
					_player.Weapon = _weaponRepo.CreateNewWeapon(WeaponType.Axe);
					break;
				case 2:
					_player.Weapon = _weaponRepo.CreateNewWeapon(WeaponType.Hammer);
					break;
				case 3:
					_player.Weapon = _weaponRepo.CreateNewWeapon(WeaponType.Sword);
					break;
				case 4:
					_player.Weapon = _weaponRepo.CreateNewWeapon(WeaponType.AR15);
					break;
				default:
					Console.WriteLine("Enter Valid Input");
					SelectWeapon();
					break;
			}
		}

		private void Fight()
		{
			if (_player == null)
			{
				NewCharacter();
			}

			_enemy = (Enemy)_playerRepo.CreateNewEnemy();

			Console.Clear();
			Console.WriteLine($"You are challenged by {_enemy.Name}");
			Console.ReadKey();

			Console.Clear();

				DisplayFightStats();
				Console.WriteLine("\n1. Fight\t2. Run");
				switch (ParseInput())
				{
					case 1:
						PlayerAttack();
						EnemyAttack();
						break;
					case 2:
						if(_random.Next(100) > _player.FleeChance)
						{
							Console.WriteLine("Couldn't Escape");
							Console.ReadKey();
							EnemyAttack();
						}
						break;
				}
		}

		private void PlayerAttack()
		{
			Console.WriteLine($"Swung {_player.Weapon.Name}");
			Console.ReadKey();

			if (_random.Next(100) < _player.MissChance)
			{
				Console.WriteLine("Attack Missed");
				Console.ReadKey();
				EnemyAttack();
			}

			if (_random.Next(100) < _player.CriticalStrikeChance)
			{
				_enemy.DoDamage(_player.Weapon.Damage + _player.CriticalStrikeChance);
				Console.WriteLine($"{_enemy.Name} Damaged for { _player.Weapon.Damage + _player.CriticalStrikeChance}");
				Console.ReadKey();
			}
			else
			{
				_enemy.DoDamage(_player.Weapon.Damage);
				Console.WriteLine($"{_enemy.Name} Damaged for {_player.Weapon.Damage}");
				Console.ReadKey();
			}

			if (_enemy.Health <= 0)
			{
				Console.Clear();
				_player.GainLevel();
				Console.WriteLine("You Won");
				Console.ReadKey();
				_player.EnemiesSlain++;
				NextFight();
			}
		}

		private void EnemyAttack()
		{
			Console.WriteLine($"{_enemy.Name} Swung {_enemy.Weapon.Name}");
			_player.DoDamage(_enemy.Weapon.Damage);
			Console.WriteLine($"{_player.Name} Damaged for {_enemy.Weapon.Damage}");
			Console.ReadKey();

			if (_player.Health <= 0)
			{
				Console.Clear();
				Console.WriteLine("You Died");
				Console.ReadKey();
				_player = null;
				MainMenu();
			}
		}

		private void NextFight()
		{
			Console.Clear();
			Console.WriteLine("Next Fight (y/n) ");
			switch (Console.ReadLine().ToLower())
			{
				case "y":
					Fight();
					break;
				case "n":
					MainMenu();
					break;
				default:
					Console.WriteLine("Enter Valid Input");
					NextFight();
					break;
			}
		}

		private void DisplayFightStats()
		{
			Console.Clear();
			Console.WriteLine($"{_player.Name}\t\t{_enemy.Name}\n" +
				$"Health: {_player.Health}\t{_enemy.Health}");
		}

		private void SeeStats()
		{
			Console.Clear();
			Console.WriteLine(_player);
			Console.ReadKey();
			MainMenu();
		}

		private int ParseInput()
		{
			if (int.TryParse(Console.ReadLine(), out int input))
				return input;
			else return 0;
		}
	}
}
