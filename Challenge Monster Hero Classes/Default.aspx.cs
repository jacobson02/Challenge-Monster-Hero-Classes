using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Challenge_Monster_Hero_Classes
{
    public partial class Default : System.Web.UI.Page
    {
        class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int DamageMaximum { get; set; }
            public bool AttackBonus { get; set; }

            public int Attack(Dice dice)
            {
                float damage = (dice.Roll() * DamageMaximum) / 6;
                return (int)damage;
            }

            public void Defend(int damage)
            {
                this.Health = this.Health - damage;
            }
        }

        class Dice
        {
            public int sides { get; set; }
            public Random random { get; set; }

            public Dice()
            {
                random = new Random();
            }
            public int Roll()
            {
                int rollRandom;
                rollRandom = random.Next(1, sides);
                return rollRandom;
            }
        }

    //}
//}


        public string FormatGameRoundStats(string attacker, string defender, int damage, int attackerHealth, int defenderHealth)
        {
            return string.Format("{0} attacks {1}, inflicting {2} damage. {0} health: {3}, {1} health: {4} <br>", attacker, defender, damage.ToString(), attackerHealth.ToString(), defenderHealth.ToString());
        }

        public string DisplayResult(int heroHealth, int monsterHealth)
        {
            if (heroHealth > 0 && heroHealth > monsterHealth) { return string.Format("Hero wins! yay :)"); }
            if (monsterHealth > 0 && monsterHealth > heroHealth) { return string.Format("Monster wins! weh :("); }
            else if (monsterHealth <= 0 && heroHealth <= 0) { return string.Format("Both have perished in battle!"); }
            else return "error!";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Character Hero = new Character();
            Character Monster = new Character();
            Dice dice = new Dice();
            int damage;

            dice.sides = 6;

            Hero.Name = "Jacob";
            Hero.Health = 100;
            Hero.DamageMaximum = 50;
            Hero.AttackBonus = true;

            Monster.Name = "Ammonia";
            Monster.Health = 100;
            Monster.DamageMaximum = 50;
            Monster.AttackBonus = false;

            while (Hero.Health > 0 && Monster.Health > 0)
            {
                damage = Hero.Attack(dice);
                Monster.Defend(damage);
                resultLabel.Text += FormatGameRoundStats(Hero.Name, Monster.Name, damage, Hero.Health, Monster.Health);
                
                damage = Monster.Attack(dice);
                Hero.Defend(damage);
                resultLabel.Text += FormatGameRoundStats(Monster.Name, Hero.Name, damage, Hero.Health, Monster.Health);


                if (Hero.AttackBonus)
                {
                    resultLabel.Text += "Bonus attack from Hero!<br>";
                    damage = Hero.Attack(dice);
                    Monster.Defend(damage);
                    resultLabel.Text += FormatGameRoundStats(Hero.Name, Monster.Name, damage, Hero.Health, Monster.Health);
                }
                else if (Monster.AttackBonus)
                {
                    resultLabel.Text += "Bonus attack from Monster!<br>";
                    damage = Monster.Attack(dice);
                    Hero.Defend(damage);
                    resultLabel.Text += FormatGameRoundStats(Monster.Name, Hero.Name, damage, Hero.Health, Monster.Health);
                }
                else { resultLabel.Text = "No bonus this round";  }
            }

            resultLabel.Text += DisplayResult(Hero.Health, Monster.Health);

            resultLabel.Text += "<p>";

        }

        protected void playButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}