using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SerialController))]
public class SerialEventDispatcher : MonoBehaviour
{
    [Tooltip("Use one event for each trigger")]
    public UnityEvent[] eventDispatch;

    SerialController sc;
    // Start is called before the first frame update
    void Awake()
    {
        sc = GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        string s = sc.ReadSerialMessage();
        if (s != null){
            if (int.TryParse(s, out int triggerIndex)){
                if(triggerIndex < eventDispatch.Length){
                    eventDispatch[triggerIndex].Invoke();
                }
            }
        }
    }
}
