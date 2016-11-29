using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NewEventSystem : EventSystem {

    protected override void OnEnable()
    {
        // do not assign EventSystem.current
    }

    protected override void Update()
    {
        EventSystem originalCurrent = EventSystem.current;
        current = this; // in order to avoid reimplementing half of the EventSystem class, just temporarily assign this EventSystem to be the globally current one
        //if (p == player.p1 && currentSelectedGameObject != null)
        //{
        //    Debug.Log(currentSelectedGameObject.name.StartsWith("p1"));
        //    if (!currentSelectedGameObject.name.StartsWith("p1"))
        //    {
        //        SetSelectedGameObject(firstSelectedGameObject);
        //    }
        //}
        base.Update();
        current = originalCurrent;
    }
}
