using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnakeBodyPart{

    void MovementHandler();
    bool CollideHandler();
    void DestroyPart();
}
