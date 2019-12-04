using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer : MonoBehaviour
{
    [System.Serializable]
    public class order
    {
       // public int orderid;
        public Gamedata.veggie  vg1, vg2, vg3;
    }

    public order activeOrder;
    public float ordertime=0;


    public List<order> orders;
    public GameObject orderdisk;

    public GameObject ordertimer;

    private float ormaxtime;
    void Start()
    {
        ormaxtime = Gamedata.customermaxwaittime;
        ordertimer = transform.Find("timer/time").gameObject;
        orderdisk = transform.Find("orderdisk").gameObject;
    }

    private void Update()
    {
        
        ordertime -= Time.deltaTime;
        if (ordertimer != null)
        {
            ordertimer.transform.localScale = new Vector3(ordertime / ormaxtime, 1, 1);

        }
        if (ordertime <= 0)
        {
            GenrateRandomOrder();
           
        }
    }

   public void GenrateRandomOrder()
    {
        ordertime = ormaxtime;

        order ord = new order();
        List<Gamedata.veggie> allvegie = new List<Gamedata.veggie>(Gamedata.vegetable);
      //  allvegie = Gamedata.vegetable;
        ord.vg1 = allvegie[Random.Range(0, allvegie.Count)];
        allvegie.Remove(ord.vg1);
        orderdisk.transform.Find("vg1").gameObject.GetComponent<SpriteRenderer>().sprite = ord.vg1.vegiImage;


        ord.vg2 = allvegie[Random.Range(0, allvegie.Count)];
        allvegie.Remove(ord.vg2);
        orderdisk.transform.Find("vg2").gameObject.GetComponent<SpriteRenderer>().sprite = ord.vg2.vegiImage;


        ord.vg3 = allvegie[Random.Range(0, allvegie.Count)];
        allvegie.Remove(ord.vg3);
        orderdisk.transform.Find("vg3").gameObject.GetComponent<SpriteRenderer>().sprite = ord.vg3.vegiImage;


        orders.Add(ord);
        activeOrder = ord;
        Debug.Log("vgtotal==" + Gamedata.vegetable.Count);
   }
}
