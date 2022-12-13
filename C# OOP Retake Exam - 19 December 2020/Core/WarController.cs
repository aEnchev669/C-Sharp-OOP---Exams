using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> characters;
		private List<Item> items;
		public WarController()
		{
			items = new List<Item>();
			characters = new List<Character>();
		}

		public string JoinParty(string[] args)
		{
			string charackterType = args[0];
			string name = args[1];

			Character character = null;

			if (charackterType == "Priest")
            {
				character = new Priest(name);
            }
            else if (charackterType == "Warrior")
            {
				character = new Warrior(name);
			}
			else
            {
				throw new ArgumentException($"Invalid character type {charackterType}!");
            }

			characters.Add(character);
			return $"{name} joined the party!";
		}

		public string AddItemToPool(string[] args)
		{
			string itemName = args[0];

			Item item = null;
            if (itemName == "FirePotion")
            {
				item = new FirePotion();
            }
            else if (itemName == "HealthPotion")
            {
				item = new HealthPotion();
			}
			else
            {
				throw new ArgumentException($"Invalid item {itemName}!");
            }

			items.Add(item);
			return $"{itemName} added to pool.";
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			Character character = characters.FirstOrDefault(c => c.Name == characterName);
            if (character == null)
            {
				throw new ArgumentException($"Character {characterName} not found!");
			}
			Item item = null;
            if (items.Count > 0)
            {
				item = items[items.Count - 1];
            }
            else
            {
				throw new InvalidOperationException($"No items left in pool!");
            }

			items.RemoveAt(items.Count - 1);
			character.Bag.AddItem(item);

			return $"{characterName} picked up {item.GetType().Name}!";
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];
			Character character = characters.FirstOrDefault(c => c.Name == characterName);
            if (character == null)
            {
				throw new ArgumentException($"Character {characterName} not found!");
            }

            if (!character.Bag.Items.Any())
            {
				throw new InvalidOperationException(ExceptionMessages.EmptyBag);
			}
			Item item = character.Bag.Items.FirstOrDefault(i => i.GetType().Name == itemName);
            if (item == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, itemName));
			}

			character.UseItem(item);
			return $"{character.Name} used {itemName}.";
		}

		public string GetStats()
		{
			
			StringBuilder sb = new StringBuilder();

            foreach (var character in characters.OrderByDescending(c => c.IsAlive).ThenByDescending(c => c.Health))
            {
				sb.AppendLine($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {(character.IsAlive ? "Alive" : "Dead")}");
            }
			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			var attacker = characters.FirstOrDefault(x => x.Name == attackerName);
			var receiver = characters.FirstOrDefault(x => x.Name == receiverName);
			if (attacker == null || receiver == null)
			{
				string nullCharackter = attacker == null ? attackerName : receiverName;
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, nullCharackter));
			}
			if (!receiver.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
			if (attacker is Priest)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attacker.Name));
			}
			Warrior warrior = (Warrior)attacker;
			warrior.Attack(receiver);

			string output = receiver.IsAlive
				? string.Format(SuccessMessages.AttackCharacter, attackerName, receiverName, attacker.AbilityPoints, receiverName, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor)
				: string.Format(SuccessMessages.AttackCharacter, attackerName, receiverName, attacker.AbilityPoints, receiverName, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor)
				+ Environment.NewLine
				+ string.Format(SuccessMessages.AttackKillsCharacter, receiverName);

			return output;
		}

		public string Heal(string[] args)
		{
			string healerName = args[0];
			string healerReceiverName = args[1];

			var healer = characters.FirstOrDefault(x => x.Name == healerName);
			var healerReceiver = characters.FirstOrDefault(x => x.Name == healerReceiverName);
			if (healer == null || healerReceiver == null)
			{
				string nullCharackter = healer == null ? healerName : healerReceiverName;
				throw new ArgumentException(ExceptionMessages.CharacterNotInParty, nullCharackter);
			}
			if (healer is Warrior)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
			}
			Priest priest = (Priest)healer;
			priest.Heal(healerReceiver);

			return string.Format(SuccessMessages.HealCharacter, healerName, healerReceiverName, healer.AbilityPoints, healerReceiverName, healerReceiver.Health);

		}
	}
}
