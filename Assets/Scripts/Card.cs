using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardData
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public List<DamageType> damageType;
        public Sprite card_Sprite;

        public enum CardType
        {
            Fire,
            Water,
            Earth,
            Air,
            Dark,
            Light,
        }

        public enum DamageType
        {
            Fire,
            Water,
            Earth,
            Air,
            Dark,
            Light,
        }




    }
}