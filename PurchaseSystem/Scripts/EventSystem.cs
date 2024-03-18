using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public bool isEvent;
    private bool eventActivated = false;

    public ItemEvents[] events;
    public Item[] items;

    public PurchaseSystem purchaseSystem;
    public DataParser dataParser;

    // Start is called before the first frame update
    void Start()
    {
        events = dataParser.List2Array<ItemEvents>(dataParser.events);
        items = purchaseSystem.items;
    }

    // Update is called once per frame
    void Update()
    {
        if(!eventActivated && isEvent)
        {
            ActiveEvent();
            eventActivated = true;
        }
    }

    public void ActiveEvent()
    {
        Debug.Log(events[0].Event_Script);

        purchaseSystem.AdjustPrices(1.3f, 0);
    }
}
