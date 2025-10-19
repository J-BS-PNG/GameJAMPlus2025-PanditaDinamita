using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{

    void UseAbility();
    string GetName();
    bool canUse();
    int remainingUses();
}
