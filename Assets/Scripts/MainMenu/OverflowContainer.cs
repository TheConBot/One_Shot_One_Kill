using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OverflowContainer : MonoBehaviour {

    public enum playerNum
    {
        p1,
        p2
    }

    public playerNum p;
    private NewEventSystem sys;

	// Use this for initialization
	void Start () {
        sys = GetComponent<NewEventSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (p == playerNum.p1 && sys.currentSelectedGameObject != null)
        {
            if (!sys.currentSelectedGameObject.name.StartsWith("p1"))
            {
                sys.SetSelectedGameObject(sys.firstSelectedGameObject);
            }
        }
        else if (p == playerNum.p2 && sys.currentSelectedGameObject != null)
            {
                if (!sys.currentSelectedGameObject.name.StartsWith("p2"))
                {
                    sys.SetSelectedGameObject(sys.firstSelectedGameObject);
                }
            }
    }
}
