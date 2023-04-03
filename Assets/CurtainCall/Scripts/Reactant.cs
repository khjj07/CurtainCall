using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class Reactant : MonoBehaviour
{
    public ReactantType type;
    public ReactiveProperty<ReactantStatus> status;
    // Start is called before the first frame update
    void Start()
    {
        status.Subscribe(_ => { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
