using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class ArcRenderer : MonoBehaviour
{
    public GameObject arrowPrefab; // the arrow head prefab // the blueprint
    public GameObject dotPrefab; // the dots
    public int poolSize = 50; // size of our dot pool ( how many dots there are)
    private List<GameObject> dotPool = new List<GameObject>(); // the dot pool itself
    private GameObject arrowInstance; // the refrence to the arrow head  // the acutal instance of the prefab at work
    public float spacing = 50; // the spacing of the dots
    public float arrowAngleAdjustment = 0; // Angle Correction for the arrow head
    public int dotsToSkip = 1; // the amount of dots to skip to give the arrowhead space
    private Vector3 arrowDirection; // holds the position the arrow needs to point from
    
    
    void Start() // called on game start
    {
        arrowInstance = Instantiate(arrowPrefab, transform); // creates an instance of the arrow and stores it in arrowInstance game object
        arrowInstance.transform.localPosition = Vector3.zero; // this gets the current location of the object and sets its position to (0, 0, 0) also works with vector2
        InitializeDotPool(poolSize); // this will create the dots from the dot prefab and store them in the dot pool
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition; // stores an x, y, z of the mouse position in a vector3 names mousePos

        mousePos.z = 0; // sets the z value of the mouse to 0

        Vector3 startPos = transform.position; // sets the starting position to the x, y, z of the object this script is attached to
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos); // sets a mid point variable based on a new function that calculates the mid point

        UpdateArc(startPos, midPoint, mousePos); // this function updates the arc based on the 3 points now defined
        PositionAndRotateArrow(mousePos); // this rotates the arrow based on the mouse position



    }

    void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / spacing); // determines the number of dots needed in the arc by
        // dividing the distance between the start and the end points by the spacing, uses math f ceil to in which takes a float and rounds it up to the nearest int

        for (int i = 0; i < numDots && i < dotPool.Count; i++) // for num in numDots and dots in dotpool.count
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f); // to ensure t stays in between 0 and 1 for the curve 0 represents the beggining of line 1 represents the end of the line

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);

            if (i != numDots - dotsToSkip) // this creates space for the arrow head to display fully
            {
                dotPool[i].transform.position = position; // sets the position of the dot
                dotPool[i].SetActive(true); // turns the game object at the index of i in the list dotPool to acitve and shows it
            }
            if (i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
            {
                arrowDirection = dotPool[i].transform.position;
            }


        }

        // Deactivate unused dots
        for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
        {
            if (i > 0)
            {
                dotPool[i].SetActive(false);
            }
        }


    }
    
    void PositionAndRotateArrow(Vector3 position)
    {
        arrowInstance.transform.position = position; // setting the arrows instances position to mouse position
        Vector3 direction = arrowDirection - position; // setting the diretion of the arrow
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // sets the angle of the arrow
        angle += arrowAngleAdjustment;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    Vector3 CalculateMidPoint(Vector3 start, Vector3 end) // starting point and ending point of the line that is mid point
    {
        Vector3 midPoint = (start + end) / 2;  // finds the mid point of the 2 entered in vectors
        float arcHeight = Vector3.Distance(start, end) / 3f; // finds about a third of the distance between start and end
        midPoint.y += arcHeight; // add that distance to the midpoint.y to give the mid point the arc
        return midPoint;
        

    }
    Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;

    }

    void InitializeDotPool(int count) // count is the local variable passed into the function/method and it is an intager being passed back
    {
        for (int i = 0; i < count; i++) // for each number in the entered value of count (poolSize)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform); // instantiate a dot and set its position to (0,0,0) at an angle of 0 and the base transform
            dot.SetActive(false); // sets the Instanatied dots active status to false, you will not see it
            dotPool.Add(dot); // adds the Instantiated dot to the GameObject list of dotPool
        }
        
    }


}
