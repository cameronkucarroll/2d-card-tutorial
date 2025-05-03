using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CardTutorialData;
using System;

public class HandManager : MonoBehaviour
{

    public DeckManager deckManager;
    public GameObject cardPrefab; // creates a card game object based on our card prefab Assign card prefab in inspector                      
    public Transform handTransform; // Root of the hand position ( where the card hand is gonna be placed )
    public float fanSpread = 7.5f; // how much the cards will spread out
    public List<GameObject> cardsInHand = new List<GameObject>(); // holds list of what cards objects are in hand
    public float cardSpacing = -100f;
    public float verticalSpacing = 100f;

    // sets a variable that can change later based on other factors
    public int maxHandSize = 12;

   
    
    
    void Start() // called once when the game starts
    {
      
    }

    public void AddCardToHand(Card cardData) // for each diffrent card from the deck manager and card prefab
    {
        // Instantiate the card if the cards in hand are less then cardsInHandMax
        
        if (cardsInHand.Count < maxHandSize)
        {
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard); // adds the newCard game object to cardsInHand list
                                      
            // Set the CardData of the Instantiated card
            newCard.GetComponent<CardDisplay>().cardData = cardData;
        }


        UpdateHandVisuals();



    }

    void Update()
    {
        //UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        
        // this makes it so that if there is one card in hand it wont have an error
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            // this makes it so that if there is one card in hand it wont have an error
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }
        
        for (int i = 0; i < cardCount; i++)  // for every card in cardCount i = current card in card list

        {
            // sets the cards angle in the hand:

            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f)); // how much more each card is rotated from the last
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);


            // sets the card spacing in hand and also the pulls the cards down in the hand:

            float horizantalOffset = (cardSpacing * (i - (cardCount - 1) / 2f)); // how much each card is spaced out from each other card in the cardsInHand list

            float normalizedPostiton = (2f * i / (cardCount - 1) - 1f); // Normalize card postion between -1 and 1 (creates the arch)
            float verticalOffset = verticalSpacing * (1 - normalizedPostiton * normalizedPostiton);
            
            
            // Sets card position
            cardsInHand[i].transform.localPosition = new Vector3(horizantalOffset, verticalOffset, 0f);

        }
    }
}
