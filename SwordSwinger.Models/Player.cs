using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public class Player : IPlayer
	{
		public string Name { get; set; }
		public Weapon Weapon { get; set; }
		public int Lives { get; set; }
		public int Armor { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public int Experience { get; set; }
		public int Level { get; set; }
		public int CriticalStrikeChance { get; set; }

		public void DoDamage(int weapnDmg)
		{
			var damage = weapnDmg - Armor;
			Health -= damage;
		}

		public void GainLevel()
		{
			Level++;
			Experience = 0;
			Health += 20;
			MaxHealth += 20;
			CriticalStrikeChange += 5;
			Armor += 5;
		}

		public override string ToString()
		{
			return $"Name: {Name}\n" +
				$"Weapon: {Weapon.Name}\n" +
				$"\tLevel: {Weapon.WeaponLevel}\n" +
				$"\tExp: {Weapon.Experience}\n" +
				$"\tDamage: {Weapon.Damage}\n" +
				$"\tDurabity: {Weapon.Durability}\n" +
				$"Lives: {Lives}\n" +
				$"Armor: {Armor}\n" +
				$"Health: {Health}\n" +
				$"MaxHealth: {MaxHealth}\n" +
				$"Exp: {Experience}\n" +
				$"Level: {Level}\n" +
				$"Critical Strike Chance: {CriticalStrikeChange}";
		}
	}
}
