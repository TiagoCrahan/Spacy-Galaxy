using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour, IPointerDownHandler
{
    Player _player;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) => { OnPointerDown((PointerEventData)eventData); });

        trigger.triggers.Add(entry);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _player.Tiro();
    }
}
