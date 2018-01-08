using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StrategyMap
{
    public AnimalType m_type;
    public GameObject m_strategy;
}


public class AbilityStrategyFactory : MonoBehaviour {

    [SerializeField]
    List<StrategyMap> m_strategyMap = new List<StrategyMap>();


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

    public GameObject getAbilityStrategy(AnimalType a_type)
    {

        StrategyMap res = m_strategyMap.First<StrategyMap>((o) => o.m_type == a_type);
        if (res != null)
        {
            return GameObject.Instantiate(res.m_strategy, Vector3.zero, Quaternion.identity);
        }
        else
        {
            return null;
        }

    }

}
