using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwordSwinger.Models;
using SwordSwinger.Repositories;

namespace SwordSlingerTests
{
	[TestClass]
	public class WeaponRepositoryTests
	{
		WeaponRepository _repo = new WeaponRepository();

		[TestMethod]
		public void WeaponRepository_CheckExperience_ShouldReturnTrue()
		{
			Assert.IsTrue(_repo.CheckExperience(new Axe
			{
				Experience = 120
			}));
		}

		[TestMethod]
		public void WeaponRepository_CheckExperience_ShouldReturnFalse()
		{
			Assert.IsFalse(_repo.CheckExperience(new Sword
			{
				Experience = 90
			}));
		}

		[TestMethod]
		public void WeaponRepository_CreateNewWeapon_ShouldCreateWeaponBasedOnInputEnum()
		{
			var weaponType = WeaponType.Axe;

			var actual = _repo.CreateNewWeapon(weaponType);

			Assert.IsInstanceOfType(actual, typeof(Weapon));
			Assert.IsInstanceOfType(actual, typeof(Axe));
		}

		[TestMethod]
		public void WeaponRepository_CreateNewWeapon_ShouldReturnNewRandomWeapon()
		{
			Assert.IsInstanceOfType(_repo.CreateNewWeapon(), typeof(Weapon));
		}
	}
}
