using UnityEngine;
using System.Collections.Generic;
public class Character : MonoBehaviour{
        public class Stats{
        public int HP;
        public int LVL;
        public int EXP;
        public int ATK;
        public int XPN;
        public string[] Skill;
        public string[] Role;
        public int[] Modifier;}
        public Dictionary<string, Stats> CharDatabase = new Dictionary<string, Stats>
        {{"Party1", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                XPN = 100,
                Skill = new string[] {"Utopic Will", "Utopic Wrath", "Utopic Wager"}, 
                Role = new string[]{"Attack", "Heal", "Stack"},
                Modifier = new int[] {5, 4, 3}}},
        {"Party2", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                XPN = 100,
                Skill = new string[] {"Overclock", "Overtime", "Overload"}, 
                Role = new string[]{"Attack", "Heal", "Stack"},
                Modifier = new int[] {5, 4, 3}}},
        {"Party3", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                XPN = 100,
                Skill = new string[] {"Data Breach", "Data Crash", "Data Drive"}, 
                Role = new string[]{"Attack", "Heal", "Stack"},
                Modifier = new int[] {5, 4, 3}}},
        {"Enemy1", new Stats{
                HP = 100,
                ATK = 10,
                Modifier = new int[] {1, 1, 1}}},
        {"Enemy2", new Stats{
                HP = 100,
                ATK = 10,
                Modifier = new int[] {1, 1, 1}}},
        {"Enemy3", new Stats{
                HP = 100,
                ATK = 10,
                Modifier = new int[] {1, 1, 1}}}};
        public int DamageMod(string Name, int Position){
                return CharDatabase[Name].Modifier[Position] * CharDatabase[Name].ATK;}
        public bool LevelUp(string Name, int Amount){
                CharDatabase[Name].EXP += Amount;
                if(CharDatabase[Name].EXP >= CharDatabase[Name].XPN){
                        CharDatabase[Name].LVL++;
                        CharDatabase[Name].XPN += CharDatabase[Name].LVL * 100;
                        CharDatabase[Name].EXP = 0;
                        CharDatabase[Name].HP += 100;
                        CharDatabase[Name].ATK += 10;
                        return true;}
                else{return false;}}}