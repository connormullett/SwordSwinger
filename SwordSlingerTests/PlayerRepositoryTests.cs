using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwordSwinger.Models;
using SwordSwinger.Repositories;

namespace SwordSlingerTests
{
	[TestClass]
	public class PlayerRepositoryTests
	{
		PlayerRepository _repo = new PlayerRepository();

		[TestMethod]
		public void PlayerRepository_CreateNewEnemy_ShouldCreateNewEnemy()
		{
			var enemy = _repo.CreateNewEnemy();

			Assert.IsInstanceOfType(enemy, typeof(IPlayer));
			Assert.IsInstanceOfType(enemy, typeof(Enemy));
		}

		[TestMethod]
		public void PlayerRepository_CheckExperience_ShouldReturnTrue()
		{
			Assert.IsTrue(_repo.CheckExperience(new Player
			{
				Experience = 120
			}));
		}

		[TestMethod]
		public void PlayerRepository_CheckExperience_ShouldReturnFalse()
		{
			Assert.IsFalse(_repo.CheckExperience(new Player
			{
				Experience = 90
			}));
		}
	}
}
