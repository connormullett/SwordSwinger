using SwordSwinger.Interfaces;
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
		private readonly WeaponRepository _weaponRepo = new WeaponRepository();
		private readonly PlayerRepository _playerRepo = new PlayerRepository();
		private Player _player;

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
					MaxHealth = 100
				};

			SelectWeapon();

			Console.Clear();
			Console.WriteLine("Character Created\n");
			
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

		}

		private void SeeStats()
		{

		}

		private int ParseInput()
		{
			if (int.TryParse(Console.ReadLine(), out int input))
				return input;
			else return 0;
		}
	}
}
