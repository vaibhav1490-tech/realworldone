using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public List<string> salad;

    public float speed;
    Rigidbody2D rb;

    public bool player2;

    float x = 0, y = 0;


    public SpriteRenderer Holdvg1, Holdvg2;


    public bool actionbutton;

    public bool atdisk = false,atcuttingboard=false,chopping=false,atcustomer=false,attrashcan=false;

    public GameObject activedisk,activecuttingboard,activeCustomer;


    public Transform playerhud;


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Holdvg1 = transform.Find("vg1").gameObject.GetComponent<SpriteRenderer>();
        Holdvg2 = transform.Find("vg2").gameObject.GetComponent<SpriteRenderer>();
    }


    private void LateUpdate()
    {
        playerhud.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (player2)
        {
            x = Input.GetAxis("P2horizontal") * Time.deltaTime * speed;
            y = Input.GetAxis("P2vertical") * Time.deltaTime * speed;
            actionbutton = Input.GetButtonDown("p2action");

        }
        else {
           x= Input.GetAxis("Horizontal") * Time.deltaTime * speed;
           y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            actionbutton = Input.GetButtonDown("p1action");
        }

        // 
        if (chopping == false)
        { 
        
         if (x != 0)
            {
                transform.Rotate(-Vector3.forward * x);
            }

            // Translate the object
            // transform.Translate(x, y, 0);
            //   transform.rotation = Quaternion.Euler(new Vector3(x, y,0));
            if (actionbutton == true)
            {
                if (atdisk == true)
                {


                    if (activedisk.name == "resdisk" && activedisk.tag == "disk")
                    {
                        diskscr dsr = activedisk.GetComponent<diskscr>();
                        if (dsr.empty == false && canpick())
                        {
                            pickveggie(activedisk);
                        }
                        else if (dsr.empty == true)
                        {
                            dropveggie(activedisk);
                        }
                    }
                    else
                    {
                        pickveggie(activedisk);
                    }

                }

                if (atcuttingboard == true)
                {

                    if (Holdvg1.sprite != null)
                    {
                        chopveggie(Holdvg1.sprite);
                    }


                }

                if (atcustomer == true)
                {
                    if (salad.Count>0)
                    {
                        deliverOrder();
                    }

                }

                if (attrashcan == true)
                {
                    if (salad.Count > 0)
                    {
                        trashSalad();
                    }
                }

            }

        }

    }

    private void FixedUpdate()
    {
        if (chopping == false)
        {
            if (y != 0)
            {
                rb.AddRelativeForce(new Vector2(x, y) * speed * Time.deltaTime, ForceMode2D.Force);
            }
        }
        else if (chopping == true)
        {
            rb.velocity = Vector2.zero;
        }
      


       
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "disk")
        {
            atdisk = true;
            activedisk = collision.gameObject;
        }
        if (collision.tag == "cuttingboard")
        {
            atcuttingboard = true;
            activecuttingboard = collision.gameObject;
        }

        if (collision.tag == "customer")
        {
            atcustomer = true;
            activeCustomer = collision.gameObject;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "disk" )
        {
            atdisk = false;
            activedisk = null;
        }
        if (collision.tag == "cuttingboard")
        {
            atcuttingboard = false;
            activecuttingboard = null;
        }
        if (collision.tag == "customer")
        {
            atcustomer = false;
            activeCustomer = collision.gameObject;
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trash")
        {
            attrashcan = true;

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trash")
        {
            attrashcan = false;

        }
    }


    void pickveggie(GameObject disk)
    {
       GameObject vg = disk.transform.Find("veggie").gameObject;
        if (Holdvg1.sprite == null)
        {
          //  vg1 = vg;
            Holdvg1.sprite = vg.GetComponent<SpriteRenderer>().sprite;
            if(disk.name == "resdisk")
            {
              vg.gameObject.GetComponent<SpriteRenderer>().sprite = null;
               disk.GetComponent<diskscr>().empty = true;
            }
        }
        else if (Holdvg2.sprite == null)
        {
          //  vg2 = vg;
            Holdvg2.sprite = vg.GetComponent<SpriteRenderer>().sprite;
            if(disk.name == "resdisk")
            {
              vg.gameObject.GetComponent<SpriteRenderer>().sprite = null;
              disk.GetComponent<diskscr>().empty = true;
            }
        }
   
    }

    void dropveggie(GameObject disk)
    {
        GameObject vg = disk.transform.Find("veggie").gameObject;
        if (Holdvg1 != null)
        {
            vg.gameObject.GetComponent<SpriteRenderer>().sprite = Holdvg1.GetComponent<SpriteRenderer>().sprite;
            disk.GetComponent<diskscr>().empty = false;
            Holdvg1.sprite = Holdvg2.sprite;

            Holdvg2.sprite = null;
           
        }
    }


    public bool canpick()
    {

        bool canpickveggie=false;

        if (Holdvg1.sprite == null || Holdvg2.sprite == null)
        {
            canpickveggie = true;
            Debug.Log("player can pick vege");

        }

        return canpickveggie;
    }



    public void chopveggie(Sprite sr)
    {
       
        if (salad.Count < 3)
        {
            StartCoroutine(choppingveg(sr));
        }
       
    
    }
    IEnumerator choppingveg(Sprite sr)
    {
        GameObject vg = activecuttingboard.transform.Find("veggie").gameObject;
        activecuttingboard.transform.Find("knife").gameObject.SetActive(true);
        chopping = true;
        vg.gameObject.GetComponent<SpriteRenderer>().sprite = Holdvg1.GetComponent<SpriteRenderer>().sprite;
        Holdvg1.sprite = Holdvg2.sprite;
        Holdvg2.sprite = null;

        yield return new WaitForSeconds(3);
        vg.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        activecuttingboard.transform.Find("knife").gameObject.SetActive(false);
        
        salad.Add(sr.name);

      int i =   salad.IndexOf(sr.name);
        playerhud.GetChild(i).gameObject.SetActive(true);
        playerhud.GetChild(i).gameObject.GetComponent<Image>().sprite = sr;
       chopping = false;
    }

    public void deliverOrder()
    {
        string customerOrder, myorder;

        customer.order cor = activeCustomer.GetComponent<customer>().activeOrder;

        customerOrder = cor.vg1.vegiImage.name + "," + cor.vg2.vegiImage.name + "," + cor.vg3.vegiImage.name;
        myorder = salad[0] + "," + salad[1] + "," + salad[2];

        if (myorder == customerOrder)
        {
            Gamedata.addscore(gameObject,activeCustomer);

           
        }
        else {
            Debug.Log("====wrong order"+customerOrder+"/=/"+myorder);
        }


        salad.Clear();
        foreach (Transform ts in playerhud)
        {
            ts.gameObject.SetActive(false);
        }
    }



    public void trashSalad()
    {
      

        if (player2 == true)
        {
            Gamedata.addplayer2score(-3*salad.Count);
        }
        else
        {
            Gamedata.addplayer1score(-3 * salad.Count);
        }
        salad.Clear();
        foreach (Transform ts in playerhud)
        {
            ts.gameObject.SetActive(false);
        }
    }
    
}
