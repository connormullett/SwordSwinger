using SwordSwinger.Models;
using SwordSwinger.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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

		public ProgramUI() { }

		public ProgramUI(IPlayer player)
		{
			_player = (Player)player;
		}

		internal void Run()
		{
			MainMenu();
		}

		private void MainMenu()
		{
			Console.Clear();
			Console.WriteLine("WELCOME TO SWORD SWINGERS ARENA \n1. New Character\n2. Fight!\n3. See Stats\n4. Switch Weapon\n5. Rules\n6. Exit");

			switch (ParseInput())
			{
				case 1:
					NewCharacter();
					break;
				case 2:
					NewFight();
					break;
				case 3:
					SeeStats();
					break;
				case 4:
					SelectWeapon();
					break;
				case 5:
					Rules();
					break;
				case 6:
					break;
				default:
					Console.WriteLine("Enter Valid Input");
					Console.ReadKey();
					MainMenu();
					break;
			}
		}

		private void Rules()
		{
			Console.Clear();
			Console.WriteLine(File.ReadAllText(@"C:\Users\Connor Mullett\Desktop\DotNetProjects\Projects\SwordSwinger\SwordSwinger\Rules.txt"));
			Console.ReadKey();
			MainMenu();
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
					Armor = 10,
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
			if(_player is null)
			{
				Console.WriteLine("Create a Character First");
				Console.ReadKey();
				MainMenu();
			}

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

		private void NewFight()
		{
			if (_player == null)
			{
				NewCharacter();
			}

			_enemy = (Enemy)_playerRepo.CreateNewEnemy();

			Console.Clear();
			Console.WriteLine($"You are challenged by {_enemy.Name}");
			Console.ReadKey();
			Fight();
		}

		private void Fight()
		{
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
					if (_random.Next(100) > _player.FleeChance)
					{
						Console.WriteLine("Couldn't Escape");
						Console.ReadKey();
						EnemyAttack();
					}
					Console.Clear();
					Console.WriteLine("Ran Away Safely");
					Console.ReadKey();
					MainMenu();
					break;
			}
		}

		private void PlayerAttack()
		{
			Console.WriteLine($"Swung {_player.Weapon.Name}");

			if (_random.Next(100) < _player.MissChance)
			{
				Console.WriteLine("Attack Missed");
				Console.ReadKey();
			}

			else if (_random.Next(100) < _player.CriticalStrikeChance)
			{
				_enemy.DoDamage(_player.Weapon.Damage + _player.CriticalStrikeChance);
				Console.WriteLine($"{_enemy.Name} Damaged for { _player.Weapon.Damage + _player.CriticalStrikeChance} with Critical Hit");
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
				Console.WriteLine($"You Won, Level Gained\nYou are now level {_player.Level}");
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
				_player.Lives--;
				_player.Health = 100;
				if(_player.Lives == 0)
				{
					Console.WriteLine("No More Lives Remaining");
					Console.ReadKey();
					_player = null;
					MainMenu();
				}
			}

			Fight();
		}

		private void NextFight()
		{
			Console.Clear();
			Console.WriteLine("Next Fight (y/n) ");
			switch (Console.ReadLine().ToLower())
			{
				case "y":
					NewFight();
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
			if (_player.Weapon.Name == "Assault Rifle 15")
				Console.WriteLine(_player.Weapon.Description);
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
