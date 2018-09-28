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
					CheckCharacter();
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

		private void CheckCharacter()
		{
			_console.Clear();

			if (_player == null)
			{
				_console.WriteLine("Can't Shop if you dont have a character");
				_console.ReadKey();
				MainMenu();
			}
			else
			{
				Shop();
			}
		}

		private void Shop()
		{
			_console.WriteLine($"Concession Stand\nGold: {_player.Gold}\n1. elixer of speed 20g\n2. Potion 50g\n3. Power Up 100g\n4. Main Menu");
			switch (ParseInput())
			{
				case 1:
					if(CheckGold(20))
					{
						_player.Gold -= 20;
						var elixer = new Elixer
						{
							Name = "Elixer",
							GoldValue = 20,
							Description = "An elixer that improves flee chance",
							ItemType = ItemType.Elixer
						};
						_player.AddItem(elixer);

					}
					else
					{
						_console.Clear();
						_console.WriteLine("You need more Gold");
						_console.ReadKey();
					}
					Shop();
					break;
				case 2:
					if (CheckGold(50))
					{
						_player.Gold -= 50;
						Potion potion = new Potion()
						{
							Name = "Potion of Health",
							GoldValue = 50,
							Description = "A Potion to heal you during battle",
							ItemType = ItemType.Potion
						};
						_player.AddItem(potion);
					}
					else
					{
						_console.Clear();
						_console.WriteLine("You need more gold");
						_console.ReadKey();
					}
					Shop();
					break;
				case 3:
					if (CheckGold(100))
					{
						_player.Gold -= 100;
						PowerUp powerUp = new PowerUp
						{
							Name = "Power Up",
							GoldValue = 100,
							Description = "Use to Gain Power",
							ItemType = ItemType.PowerUp
						};
						_player.AddItem(powerUp);
					}
					else
					{
						_console.Clear();
						_console.WriteLine("You need more gold");
						_console.ReadKey();
					}
					Shop();
					break;
				case 4:
					MainMenu();
					break;
				default:
					break;
			}

			MainMenu();
		}

		private bool CheckGold(int value)
		{
			if (_player.Gold > value) return true;
			else return false;
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
			_console.WriteLine("\n1. Fight\t2. Run\t3. Inventory");

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
				case 3:
					ShowInventory();
					break;
				default:
					_console.WriteLine("Enter Valid Input");
					_console.ReadKey();
					Fight();
					break;
			}

			if (_enemy.Health > 0) Fight();
		}

		private void ShowInventory()
		{
			if(_player._inventory == null)
			{
				_console.Clear();
				_console.WriteLine("No Items");
				_console.ReadKey();
			}
			else
			{

				Console.Clear();
				var iterator = 1;
					foreach(IShopItem i in _player._inventory)
					{
						_console.WriteLine($"{iterator++}: {i.Name}");
					}
				_console.ReadKey();
			}
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

				if(_player._inventory != null)
				{
					foreach(IShopItem i in _player._inventory)
					{
						Console.WriteLine(i.Name);
					}
				}

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
