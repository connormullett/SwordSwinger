using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwordSwinger.Models;

namespace SwordSlingerTests
{
	[TestClass]
	public class WeaponTests
	{
		[TestMethod]
		public void Weapon_Sword_ShouldBeOfTypeWeaponAndSword()
		{
			Weapon sword = new Sword();

			Assert.IsInstanceOfType(sword, typeof(Weapon));
			Assert.IsInstanceOfType(sword, typeof(Sword));
		}

		[TestMethod]
		public void Weapon_Axe_ShouldBeOfTypeWeaponAndAxe()
		{
			Weapon axe = new Axe();

			Assert.IsInstanceOfType(axe, typeof(Weapon));
			Assert.IsInstanceOfType(axe, typeof(Axe));
		}

		[TestMethod]
		public void Weapon_AR15_ShouldBeOfTypeWeaponAndAR15()
		{
			Weapon ar = new AR15();

			Assert.IsInstanceOfType(ar, typeof(Weapon));
			Assert.IsInstanceOfType(ar, typeof(AR15));
		}

		[TestMethod]
		public void Weapon_Hammer_ShouldBeOfTypeWeaponAndHammer()
		{
			Weapon hammer = new Hammer();

			Assert.IsInstanceOfType(hammer, typeof(Weapon));
			Assert.IsInstanceOfType(hammer, typeof(Hammer));
		}
	}
}
