using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStrategyFactory : MonoBehaviour {

    public static AbilityStrategyFactory INSTANCE;
    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
    }

    public AbilityStrategyFactory getAbilityStrategy(AnimalType a_type)
    {
        switch (a_type)
        {
            case AnimalType.MOUTON:
                return this;

            case AnimalType.LOUP:
                return null;

        }
        return null;
    }

}
