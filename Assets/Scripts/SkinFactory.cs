using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class SkinMap
{
    public AnimalType m_type;
    public GameObject m_skin;
}

public class SkinFactory : MonoBehaviour {
    [SerializeField]
    List<SkinMap> m_skinMap = new List<SkinMap>();
    
    public static SkinFactory INSTANCE;
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

    public GameObject getSkin(AnimalType a_type, int a_id = 0)
    {
        SkinMap res = m_skinMap.First<SkinMap>((o) => o.m_type == a_type);
        if (res != null)
        {
            return GameObject.Instantiate(res.m_skin);
        }
        else
        {
            return null;
        }
    }



}
