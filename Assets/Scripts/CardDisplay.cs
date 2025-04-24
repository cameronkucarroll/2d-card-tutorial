using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CardTutorialData; // the namespace we created earlier
using UnityEngine.UI;
using TMPro; // stands for text mesh pro allows you to use text mesh pro classes, methods and data


public class CardDisplay : MonoBehaviour
{
    public Card cardData; // this refrences the Card class from the Card scrpit you can do this because the card class is a scrpitable object

    public Image cardImage; // this sets an image data from unity ui and sets it to cardImage
    public Image damageImage;
    public TMP_Text nameText;  // this sets a text mesh pro text to nameText
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages; // this creates an array of images and sets that arrays name to typeImages




    // this sets up a type colors array that is an array of colors that is private (the editor cant see it) for cards
    private Color[] cardColors =
    {
        new Color(0.55f, 0.10f, 0.16f), //Fire
        new Color(0.55f, 0.34f, 0.13f), // Earth, creates a new color by editing the red blue green colors with floats and intagers under the color funtion (red, green, blue)
        new Color(0.08f, 0.26f, 0.41f), //water
        new Color(0.22352f, 0.11f, 0.1960f), // Dark
        new Color(0.68867f, 0.63f, 0.37f), // Light
        new Color(0.22f, 0.61f, 0.41f) //Air
    };

    // this sets up a type colors array that is an array of colors that is private (the editor cant see it) for types
    private Color[] typeColors =
    {
        new Color(0.91f, 0.19f, 0.09f), //Fire
        new Color(0.8f, 0.52f, 0.24f), // Earth
        new Color(0.18f, 0.48f, 0.73f), //water
        new Color(0.58f, 0.0f, 0.49f), // Dark
        new Color(0.88f, 1.0f, 0.47f), // Light
        new Color(0.713195f, 0.8584f, 0.700560f) //Air
    };





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {

        // Update the main card image color based on the first card type
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        
        // updates the color of image you put in the damage image slot in the card prefab
        damageImage.color = typeColors[(int)cardData.damageType[0]];





        nameText.text = cardData.cardName;  // this sets the nameText text mesh pro (the text on the card for the name) to the card that is created cardName inside the object cardData
        healthText.text = cardData.health.ToString(); // this does the same for the spesific cards health but also converts it to a string using the ToString() method
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";


        // the .text sets the name to the text class inside the tmp inside the editor so the Card Name in the editor will change to the nameText on the card data

        // Update type images ( at the top of the card )
        for (int i = 0; i < typeImages.Length; i++) // for every image in typeImages (sets and iterable of an intager set to i, as long as i is less than the amount of images there are in typeImages iterate once more or i++
        {
            if (i < cardData.cardType.Count) // gonna look at i's spot in the image array and then make it active so if there are multiple it will make them seen
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
                
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }


    }
}
