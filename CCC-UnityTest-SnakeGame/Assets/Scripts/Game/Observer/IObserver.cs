using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver {

    // Use this for initialization
    void Notify();
    void NotifyWith(Vector3 position1, Vector3 position2);
}
