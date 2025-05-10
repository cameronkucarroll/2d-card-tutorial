using UnityEngine;
using UnityEngine.EventSystems;


public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    // variables 
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition; // store our mouse postion (mouse pointer) 
    private Vector3 originalPanelLocalPosition; // cards origanal location?
    private Vector3 originalScale; // the card and or mouse pointers origanal scale
    private int currentState = 0;
    private Quaternion originalRotation; // origanal card rotation
    private Vector3 originalPosition; //    card prefab origanal position? 

    [SerializeField] private float selectScale = 1.1f; // scales the card when hovering over it
    // serializefeild makes it so that even a private variable can be edited and looked at in the inspector
    [SerializeField] private Vector2 cardPlay; // position where if our mouse goes past it its gonna push said card into play postiton
    [SerializeField] private Vector3 playPosition; // where card is gonna jump to when card is played
    [SerializeField] private GameObject glowEffect; // stores are new sprite for glow effect
    [SerializeField] private GameObject playArrow; //
    [SerializeField] private float lerpFacotor = 0.1f;

    void Awake() // when this object is activated
    {
       
        // store our starting position variables
        rectTransform = GetComponent<RectTransform>(); 
        // RectTransform is used to store and manipulate the position, size and anchoring of a rectangle
        canvas = GetComponentInParent<Canvas>(); // store whatever canvas componet is stored here
        originalScale = rectTransform.localScale; // sets  our origanalScale to the origanal scale for the card
        originalPosition = rectTransform.localPosition; // gets the current rect transform postion of the card and sets the origanalPosition to that number
        originalRotation = rectTransform.localRotation;

    }
    void Update()
    {
        switch (currentState) // match state casements in python instead of using alot of else if statements
        {                       // if current state is 1 = case 1:
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0)) // if mouse button is released
                {
                    TransitionToState0(); // a method to set the card back to its origanal position
                } // check if mouse button is released
                break;
            case 3:
                HandlePlayState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
        }

    }

    private void TransitionToState0()
    {
        currentState = 0; // changing the switch current state statement to 0
        rectTransform.localScale = originalScale; // sets the rectTransform scale back to the origanal scale we set earlier, tldr resets scale
        rectTransform.localRotation = originalRotation; // resets rotation
        rectTransform.localPosition = originalPosition; // resets position
        glowEffect.SetActive(false); // disable the glow effect on the card
        playArrow.SetActive(false); // disables play arrow

    }

    public void OnPointerEnter(PointerEventData eventData) // on mouse pointer enter ( the mouse enters the canvas ) set currentState to 1 which will run HandleHoverState
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }


    }

    public void OnPointerExit(PointerEventData eventData) // on mouse pointer exit of the canvas set state to 0 (reset its postion)
    {
        if (currentState == 1)
        {
            
            TransitionToState0();

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            // this makes sure we get the correct position within the camrea and the world
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;

        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFacotor);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFacotor);
                }
            }
        }
    }



    private void HandleHoverState() // sets the current canvases glow effect to ture and scale it up by the select state
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

   
    private void HandleDragState()
    {
        // sets the cards rotation to 0
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if(Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
        
    }
}
