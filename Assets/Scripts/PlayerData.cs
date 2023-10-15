using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float hp;
    public float mana;
    public float manaRatio;
    public bool[,] levelCleared;
    public bool[,,] ownedGECKO;
    [System.Serializable]
    public struct DeathPoint
    {
        public float Distance;
        public int seq;

        public float posX;
        public float posY;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
        public float rotW;

    }
    public DeathPoint[,,] deathPoints;


    public int coinTotal;
   
    public bool[] activatedGECKO;

    public int[] itemStep;
    public float[] itemTime;
    public float powerupTime;

  

    public bool[] ownedGecko;
    public bool[] ownedTree;
    public bool[] ownedAnimal;

    public int currentGecko;
    public int currentTree;


    public int totalScore1;
    public int totalScore2;

   
   
}
