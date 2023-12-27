using UnityEngine;
using System.Collections.Generic;
public class Character : MonoBehaviour{
        public class Stats{
        public int HP;
        public int LVL;
        public int EXP;
        public int ATK;
        public string[] Skill;
        public int[] Modifier;}
        public Dictionary<string, Stats> CharDatabase = new Dictionary<string, Stats>
        {{"Party1", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                Skill = new string[] {"Utopia's Will", "Utopia's Wrath", "Utopia's Wager"}, 
                Modifier = new int[] {5, 4, 3}}},
        {"Party2", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                Skill = new string[] {"Overclock", "Overtime", "Overload"}, 
                Modifier = new int[] {5, 4, 3}}},
        {"Party3", new Stats{
                HP = 100,
                EXP = 0,
                LVL = 1,
                ATK = 10,
                Skill = new string[] {"Data Breach", "Data Crash", "Data Drive"}, 
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
        public void LevelUp(string Name, int Amount){
                CharDatabase[Name].EXP += Amount;}}