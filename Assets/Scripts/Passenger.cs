using UnityEngine;

public class Passenger : MonoBehaviour
{

    public enum PassengerType { civilian, rightEngineer, leftEngineer, rightGunner, leftGunner, pilote };
    public PassengerType type;

    public Vector2 deathSpeed;
    [SerializeField]private bool isDragged = false;
    [SerializeField] private bool isOutside = false;

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer spriteRenderer = null;

    private Vector3 mousePosition = Vector3.zero;
    private Vector2 initialPassengerPosition;

    void Start()
    {
        initialPassengerPosition = transform.position;
        rigidBody = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }



    


    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        

        if (isDragged)
        {
            if(YeetController.instance.yeetTimeScale < YeetController.instance.yeetDuration)
            {
                transform.position = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
                
            }
            else
            {
                transform.position = initialPassengerPosition;
                isDragged = false;
            }
        }


    }

    

    //Permet de savoir quand on drag le passenger
    void OnMouseDown()
    {
        if (!isOutside)
        {
            isDragged = true;
        }
    }

    //Permet de détecter quand le passenger est en dehors du vaisseau
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlaneShip"))
        {
            isOutside = true;
        }
    }

    //Permet de détecter quand le passenger est dans du vaisseau
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlaneShip"))
        {
            isOutside = false;
        }
    }


    //Permet de savoir quand on drop le passenger 
    void OnMouseUp()
    {
        isDragged = false;

        //Si le passenger est droppé dans le vaisseau, alors il revient à se position initiale
        if (!isOutside)
        {
            transform.position = initialPassengerPosition;
        }
        //Si le passager est droppé en dehors du vaisseau, alors il die
        else
        {
            rigidBody.velocity = deathSpeed;
            YeetController.instance.yeetedPassenger = this.gameObject;
            YeetController.instance.OnYeeted(type);
            YeetController.instance.nbPassenger--;
        }
    }


    
}
