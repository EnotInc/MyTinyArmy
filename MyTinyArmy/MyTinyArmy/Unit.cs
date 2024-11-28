using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyTinyArmy
{
    abstract class Unit
    {
        public abstract string GetInfo();
        public abstract double HP { get; set; }
        public abstract double MP { get; set; }
        public abstract double DMG { get; set; }
        public abstract double LVL { get; set; }
        public abstract double Price { get; }

    }

    class Warrior : Unit
    {
        Random rnd = new Random();
        public override double HP { get; set; } 
        public override double MP { get; set; }
        public override double DMG { get; set; }
        public override double LVL { get; set; }

        public override double Price => (HP + MP + DMG + LVL) / 4;

        public override string GetInfo()
        {
            string info = $"{GetType().Name}:\t Price = {Price}";
            return info;
        }
    }
    class Archer : Unit
    {
        Random rnd = new Random();
        public override double HP { get; set; }
        public override double MP { get; set; }
        public override double DMG { get; set; }
        public override double LVL { get; set; }

        public override double Price => (HP + MP + DMG + LVL) / 4;

        public override string GetInfo()
        {
            string info = $"{GetType().Name}:\t Price = {Price}";
            return info;
        }
    }
    class Wizard : Unit
    {
        Random rnd = new Random();
        public override double HP { get; set; }
        public override double MP { get; set; }
        public override double DMG { get; set; }
        public override double LVL { get; set; }

        public override double Price => (HP + MP + DMG + LVL) / 4;

        public override string GetInfo()
        {
            string info = $"{GetType().Name}:\t Price = {Price}";
            return info;
        }
    }

}
