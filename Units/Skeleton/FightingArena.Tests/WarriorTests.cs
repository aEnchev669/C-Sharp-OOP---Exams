namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void ConstructorShouldInitializeWarriorWithCurrName()
        {
            string expectedName = "Pesho";

            Warrior warrior = new Warrior(expectedName, 12, 40);

            string actualName = warrior.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void ConstructorShouldInitializeWarriorWithCurrDmg()
        {
            int expectedDamage = 20;
            Warrior warrior = new Warrior("Gosho", expectedDamage, 40);

            int actualDamage = warrior.Damage;
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [Test]
        public void ConstructorShouldInitializeWarriorWithCurrHp( )
        {
            int expectedHp = 40;
            Warrior warrior = new Warrior("Gosho", 20, expectedHp);

            int actualHp = warrior.HP;

            Assert.AreEqual(expectedHp, actualHp);
        }
        [TestCase("Pesho")]
        [TestCase("S")]
        [TestCase("Very very very very very very very very very long name")]
        public void NameSetterShouldSatValueWithValidName(string name)
        {

            Warrior warrior = new Warrior(name, 20, 40);

            string expectedName = name;
            string actualName = warrior.Name;

            Assert.AreEqual(expectedName, actualName);
        }
        [TestCase("")]
        [TestCase("     ")]
        [TestCase(null)]
        public void NameShouldThrowExceptionIfValueNameIsNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, 20, 40);
            }, "Name should not be empty or whitespace!");
        }
        [TestCase(100000000)]
        [TestCase(50)]
        [TestCase(1)]
        public void DamageSetterSouldSetValueWithValidDemage(int damage)
        {

            Warrior warrior = new Warrior("Pesho", damage, 40);

            int expectedDmg = damage;
            int actualDmg = warrior.Damage;

            Assert.AreEqual(expectedDmg, actualDmg);
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1000000)]
        public void DamageShouldThrowExceptionIfTheValueIsNotPossitiveNumber(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", damage, 40);
            }, "Damage value should be positive!");
        }

        [TestCase(100000000)]
        [TestCase(50)]
        [TestCase(1)]
        [TestCase(0)]
        public void HPSetterShouldSetValueWIthValidHP(int hp)
        {

            Warrior warrior = new Warrior("Pesho", 20, hp);

            int expectedHp = hp;
            int actualHp = warrior.HP;

            Assert.AreEqual(expectedHp, actualHp);
        }

        [TestCase(-1)]
        [TestCase(-100000)]
        public void HPShouldThrowExcpetionsIfTheValueIsNegativeNumber(int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Gosho", 20 , hp);
            }, "HP should not be negative!");
        }

        [TestCase(0)]
        [TestCase(20)]
        [TestCase(30)]
        public void AttackerCannotAttackIfHihHPIsBelow30(int hp)
        {
            Warrior attacker = new Warrior("Gosho", 20, hp);
            Warrior deffender = new Warrior("Gosho", 20, 40);

            Assert.Throws<InvalidOperationException>(()=>
            {
              attacker.Attack(deffender);

            }, "Your HP is too low in order to attack other warriors!");
        }
        [TestCase(0)]
        [TestCase(20)]
        [TestCase(30)]
        public void AttackerCannotAttackWarriorWithHPBelow30(int hp)
        {
            Warrior attacker = new Warrior("Gosho", 20, 40);
            Warrior deffender = new Warrior("Gosho", 20, hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(deffender);

            }, "Your HP is too low in order to attack other warriors!");
        }
        [Test]
        public void AttackerCannotAttackWarriorWithMoreDmgThatAttackerHP()
        {
            Warrior attacker = new Warrior("Gosho", 20, 40);
            Warrior deffender = new Warrior("Gosho", 50, 40);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(deffender);

            }, "You are trying to attack too strong enemy");
        }
        [Test]
        public void AttackWithNoKill()
        {
            int dmgOfAttacker = 30;
            int hpOfAttacker = 100;

            int dmgOfDeffender = 30;
            int hpOfDeffender = 70;

            Warrior attacker = new Warrior("Gosho", dmgOfAttacker, hpOfAttacker);
            Warrior deffender = new Warrior("Pesho", dmgOfDeffender, hpOfDeffender);

            attacker.Attack(deffender);

            int attackerExpectedHp = hpOfAttacker - dmgOfDeffender;

            int deffenderExpectedHp = hpOfDeffender - dmgOfAttacker;

            int actualHpOfAttacker = attacker.HP;
            int actualHpOfDeffender = deffender.HP;

            Assert.AreEqual(attackerExpectedHp, actualHpOfAttacker);
            Assert.AreEqual(deffenderExpectedHp, actualHpOfDeffender);
        }
    }
}