using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CardTutorialData;


public class DeckManager : MonoBehaviour
{


    public List<Card> allCards = new List<Card>(); // new list of Card game object from CardTutorialData from the CardData script

    private int currentIndex = 0;

    void Start()
    {
        // Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        // add the loaded cards to the allCards list
        allCards.AddRange(cards);
    } 

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        Card nextCard = allCards[currentIndex]; // look at next card in our list all cards
        handManager.AddCardToHand(nextCard); // then add the new cards data to the add card to hand funtion 
        currentIndex = (currentIndex + 1) % allCards.Count;

    }





}
