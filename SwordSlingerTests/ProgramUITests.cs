using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwordSwinger;
using SwordSwinger.Models;
using SwordSwinger.Repositories;

namespace SwordSlingerTests
{
	[TestClass]
	public class ProgramUITests
	{
		[TestMethod]
		public void ProgramUI_NewCharacter_ShouldCreateNewCharacter()
		{
			//-- Arrange
			var mock = new MockConsole(new string[] { "1", "Connor", "1", "8" });
			ProgramUI program = new ProgramUI(mock);

			//-- Act
			program.Run();

			//-- Assert
			Assert.IsInstanceOfType(program._player, typeof(IPlayer));
			Assert.IsTrue(program._player != null);
		}

		[TestMethod]
		public void ProgramUI_NewFight_ShouldRedirectToMenuIfNoCharacterExists()
		{
			//-- arrange
			var mock = new MockConsole(new string[] { "2", "8"});
			var program = new ProgramUI(mock);

			//-- act
			program.Run();

			//-- assert
			Assert.IsTrue(program._player == null);
		}

		[TestMethod]
		public void ProgramUI_NewFight_ShouldCreateNewFight()
		{
			//-- arrange
			var mock = new MockConsole(new string[] {"2", "1", "n", "8" });

			var godWeapon = new Axe()
			{
				Damage = 2000
			};

			var godPlayer = new Player()
			{
				Weapon = godWeapon
			};

			ProgramUI program = new ProgramUI(mock, godPlayer);

			//-- act
			program.Run();

			//-- assert
			Assert.IsTrue(program._player != null);
			Assert.IsTrue(program._player.Experience > 0);
		}

		[TestMethod]
		public void ProgramUI_SeeStats_ShouldntShowStatsIfPlayerIsNull()
		{
			//-- arrange
			var mock = new MockConsole(new string[] { "4", "8" });
			var program = new ProgramUI(mock);

			//-- act
			program.Run();

			//-- assert
			Assert.IsTrue(program._player is null);
			Assert.IsTrue(mock.Output.Contains("stat"));
		}

		[TestMethod]
		public void ProgramUI_ShouldShowStatsIfPlayerIsNotNull()
		{
			var weapon = new Axe()
			{
				Name = "TestingName",
				WeaponType = WeaponType.Axe,
				Damage = 25,
				Durability = 150,
				WeaponLevel = 1,
				Experience = 0,
			};

			var player = new Player
			{
				Lives = 1,
				Armor = 14,
				Experience = 0,
				Level = 1,
				Name = "bill",
				Weapon = weapon,
				Health = 100,
				MaxHealth = 100,
				MissChance = 10,
				Gold = 0
			};

			var mock = new MockConsole(new string[] { "4", "8" });
			var program = new ProgramUI(mock, player);

			program.Run();

			Assert.IsTrue(mock.Output.Contains("TestingName"));
			Assert.IsTrue(mock.Output.Contains("bill"));
		}

		[TestMethod]
		public void ProgramUI_SelectWeapon_ShouldBeDifferentThanInjectedWeapon()
		{
			var weapon = new Axe()
			{
				Name = "TestingName",
				WeaponType = WeaponType.Axe,
				Damage = 25,
				Durability = 150,
				WeaponLevel = 1,
				Experience = 0,
			};

			var player = new Player
			{
				Lives = 1,
				Armor = 14,
				Experience = 0,
				Level = 1,
				Name = "bill",
				Weapon = weapon,
				Health = 100,
				MaxHealth = 100,
				MissChance = 10,
				Gold = 0
			};

			var mock = new MockConsole(new string[] { "5", "1", "8" });
			var program = new ProgramUI(mock, player);

			var actual = program._player.Weapon.WeaponType;
			var expected = WeaponType.Axe;

			Assert.AreEqual(actual, expected);
		}
	}
}
