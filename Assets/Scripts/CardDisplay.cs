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
    public TMP_Text nameText;  // this sets a text mesh pro text to nameText
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages; // this creates an array of images and sets that arrays name to typeImages


    // this sets up a type colors array that is an array of colors that is private (the editor cant see it) for cards
    private Color[] cardColors =
    {
        Color.red, //Fire
        new Color(0.8f, 0.52f, 0.24f), // Earth, creates a new color by editing the red blue green colors with floats and intagers under the color funtion (red, green, blue)
        Color.blue, //water
        new Color(0.23f, 0.06f, 0.21f), // Dark
        Color.yellow, // Light
        Color.cyan //Air
    };

    // this sets up a type colors array that is an array of colors that is private (the editor cant see it) for types
    private Color[] typeColors =
    {
        Color.red, //Fire
        new Color(0.8f, 0.52f, 0.24f), // Earth
        Color.blue, //water
        new Color(0.47f, 0.0f, 0.4f), // Dark
        Color.yellow, // Light
        Color.cyan //Air
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



        nameText.text = cardData.cardName;  // this sets the nameText text mesh pro (the text on the card for the name) to the card that is created cardName inside the object cardData
        healthText.text = cardData.health.ToString(); // this does the same for the spesific cards health but also converts it to a string using the ToString() method
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";
        // the .text sets the name to the text class inside the tmp inside the editor so the Card Name in the editor will change to the nameText on the card data

        // Update type images
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
