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
		private readonly IConsole _console;
		private WeaponRepository _weaponRepo = new WeaponRepository();
		private PlayerRepository _playerRepo = new PlayerRepository();
		private Random _random = new Random();

		public Player _player;
		public Enemy _enemy;

		public ProgramUI(IConsole console)
		{
			_console = console;
		}

		public ProgramUI(IConsole console, IPlayer player)
		{
			_console = console;
			_player = (Player)player;
		}

		public ProgramUI(IConsole console, IPlayer player, IPlayer enemy)
		{
			_console = console;
			_player = (Player)player;
			_enemy = (Enemy)enemy;
		}

		public void Run()
		{
			MainMenu();
		}

		private void MainMenu()
		{
			_console.Clear();
			_console.WriteLine("WELCOME TO SWORD SWINGERS ARENA \n1. New Character\n2. Fight!\n3. Shop\n4. See Stats\n5. Switch Weapon\n6. Rules\n7. Scores\n8. Exit");

			switch (ParseInput())
			{
				case 1:
					NewCharacter();
					break;
				case 2:
					if(_player == null)
					{
						NullCharacterPrompt();
					}
					else
					{
						NewFight();
					}
					break;
				case 3:
					Shop();
					break;
				case 4:
					SeeStats();
					break;
				case 5:
					SelectWeapon();
					MainMenu();
					break;
				case 6:
					Rules();
					break;
				case 7:
					Scores();
					break;
				case 8:
					Exit();
					break;
				default:
					_console.WriteLine("Enter Valid Input");
					_console.ReadKey();
					MainMenu();
					break;
			}
		}

		private void NullCharacterPrompt()
		{
			_console.Clear();
			_console.WriteLine("Make a Character");
			_console.ReadKey();
			MainMenu();
		}

		private void Shop()
		{
			_console.Clear();

			MainMenu();
		}

		private void Scores()
		{
			_console.Clear();
			_console.WriteLine(File.ReadAllText(@"C:\Users\Connor Mullett\Desktop\DotNetProjects\Projects\SwordSwinger\SwordSwinger\Scores.txt"));
			_console.ReadKey();
			MainMenu();
		}

		private void Rules()
		{
			_console.Clear();
			_console.WriteLine(File.ReadAllText(@"C:\Users\Connor Mullett\Desktop\DotNetProjects\Projects\SwordSwinger\SwordSwinger\Rules.txt"));
			_console.ReadKey();
			MainMenu();
		}

		private void NewCharacter()
		{
			_console.Clear();
			_console.Write("Enter Name: ");
			var name = _console.ReadLine();

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

			_console.Clear();
			_console.WriteLine("Character Created\n");
			_console.ReadKey();
			MainMenu();
			
		}
		
		private void SelectWeapon()
		{
			_console.Clear();
			if(_player is null)
			{
				_console.WriteLine("Create a Character First");
				_console.ReadKey();
				MainMenu();
			}

			_console.WriteLine("Choose your weapon\n1. Axe\n2. Hammer\n3. Sword");

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
					_console.WriteLine("Enter Valid Input");
					_console.ReadKey();
					SelectWeapon();
					break;
			}
		}

		private void NewFight()
		{
			_enemy = (Enemy)_playerRepo.CreateNewEnemy();

			_console.Clear();
			_console.WriteLine($"You are challenged by {_enemy.Name}");
			_console.ReadKey();
			Fight();
		}

		private void Fight()
		{
			_console.Clear();

			DisplayFightStats();
			_console.WriteLine("\n1. Fight\t2. Run");

			switch (ParseInput())
			{
				case 1:
					PlayerAttack();
					EnemyAttack();
					break;
				case 2:
					if (_random.Next(100) > _player.FleeChance)
					{
						_console.WriteLine("Couldn't Escape");
						_console.ReadKey();
						EnemyAttack();
					}
					_console.Clear();
					_console.WriteLine("Ran Away Safely");
					_console.ReadKey();
					MainMenu();
					break;
				default:
					_console.WriteLine("Enter Valid Input");
					_console.ReadKey();
					Fight();
					break;
			}

			if (_enemy.Health > 0) Fight();
		}

		private void PlayerAttack()
		{
			_console.WriteLine($"Swung {_player.Weapon.Name}");

			if (_random.Next(100) < _player.MissChance)
			{
				_console.WriteLine("Attack Missed");
				_console.ReadKey();
			}

			else if (_random.Next(100) < _player.CriticalStrikeChance)
			{
				_enemy.DoDamage(_player.Weapon.Damage + _player.CriticalStrikeChance);
				_console.WriteLine($"{_enemy.Name} Damaged for { _player.Weapon.Damage + _player.CriticalStrikeChance} with Critical Hit");
				_console.ReadKey();
			}
			else
			{
				_enemy.DoDamage(_player.Weapon.Damage);
				_console.WriteLine($"{_enemy.Name} Damaged for {_player.Weapon.Damage}");
				_console.ReadKey();
			}

			if (_enemy.Health <= 0)
			{
				_console.Clear();
				_console.WriteLine($"You Won");
				_console.WriteLine($"Gained {_enemy.Gold} Gold and {_enemy.Experience} experience for winning");
				_console.ReadKey();
				_player.Weapon.Experience += _enemy.Weapon.Experience;
				_player.Experience += _enemy.Experience;
				_player.Gold += _enemy.Gold;
				_player.EnemiesSlain++;

				if(_playerRepo.CheckExperience(_player))
				{
					_player.GainLevel();
					_console.WriteLine("Player Level Gained");
					_console.ReadKey();
				}

				if (_weaponRepo.CheckExperience(_player.Weapon))
				{
					_player.Weapon.GainLevel();
					_console.WriteLine("Weapon Level Gained");
					_console.ReadKey();
				}

				NextFight();
			}
		}

		private void EnemyAttack()
		{
			_console.WriteLine($"{_enemy.Name} Swung {_enemy.Weapon.Name}");
			_player.DoDamage(_enemy.Weapon.Damage);
			_console.WriteLine($"{_player.Name} Damaged for {_enemy.Weapon.Damage}");
			_console.ReadKey();

			if (_player.Health <= 0)
			{
				_console.Clear();
				_console.WriteLine("You Died");
				_console.ReadKey();
				_player.Lives--;
				_player.Health = 100;
				if(_player.Lives == 0)
				{
					_console.WriteLine("No More Lives Remaining");
					_console.WriteLine("Would you like to write your scores? (y/n)");
					WriteScoresPrompt();
					_console.ReadKey();
					_player = null;
					MainMenu();
				}
			}
		}

		private void NextFight()
		{
			_console.Clear();
			_console.WriteLine("Next Fight (y/n) ");
			switch (_console.ReadLine().ToLower())
			{
				case "y":
					NewFight();
					break;
				case "n":
					MainMenu();
					break;
				default:
					_console.WriteLine("Enter Valid Input");
					NextFight();
					break;
			}
		}

		private void WriteScoresPrompt()
		{
			switch (_console.ReadLine().ToLower())
			{
				case "y":
					WriteScores();
					break;
				case "n":
					MainMenu();
					break;
				default:
					_console.Clear();
					_console.WriteLine("Enter valid Number");
					_console.ReadKey();
					break;
			}
		}

		private void WriteScores()
		{
			using(StreamWriter file = new StreamWriter(@"C:\Users\Connor Mullett\Desktop\DotNetProjects\Projects\SwordSwinger\SwordSwinger\Scores.txt"))
			{
				file.WriteLine(_player);
			}
		}

		private void DisplayFightStats()
		{
			_console.Clear();
			_console.WriteLine($"{_player.Name}\t\t{_enemy.Name}\n" +
				$"Health: {_player.Health}\t{_enemy.Health}");
		}

		private void SeeStats()
		{
			if(_player is null)
			{
				_console.Clear();
				_console.WriteLine("No stats for this character");
				_console.ReadKey();
				MainMenu();
			}
			else
			{
				_console.Clear();
				_console.WriteLine(_player.ToString());
				if (_player.Weapon.Name == "Assault Rifle 15")
					_console.WriteLine(_player.Weapon.Description);
				_console.ReadKey();
				MainMenu();
			}
		}

		private int ParseInput()
		{
			if (int.TryParse(_console.ReadLine(), out int input))
				return input;
			else return 0;
		}

		private void Exit()
		{

		}
	}
}
