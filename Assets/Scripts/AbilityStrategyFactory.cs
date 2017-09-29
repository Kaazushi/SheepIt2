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

    public AbilityStrategy getAbilityStrategy(AnimalType a_type)
    {
        switch (a_type)
        {
            case AnimalType.SHEEP:
                return new SheepStrategy();

            case AnimalType.WOLF:
                return new WolfStrategy();

        }
        return null;
    }

}
