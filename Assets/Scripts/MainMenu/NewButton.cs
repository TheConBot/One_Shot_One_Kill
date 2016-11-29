using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class NewButton : Button {

    private EventSystem eventSystem;

    protected override void Awake()
    {
        base.Awake();
        eventSystem = GetComponent<EventSysProvider>().eventSystem;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        // Selection tracking
        if (IsInteractable() && navigation.mode != Navigation.Mode.None)
            eventSystem.SetSelectedGameObject(gameObject, eventData);

        base.OnPointerDown(eventData);
    }

    public override void Select()
    {
        if (eventSystem.alreadySelecting)
            return;

        eventSystem.SetSelectedGameObject(gameObject);
    }
}
