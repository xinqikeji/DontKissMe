using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance =>instance;
    private static GameMgr instance;
    /// <summary>
    /// 能量信息
    /// </summary>
    public EnergyInfo energyInfo;
    void Start()
    {
        //加载数据，如果没有数据则创建
        energyInfo = PlayerPrfsMgr.Instance.LoadData("EnergyInfo", new EnergyInfo());
        
        //energyInfo.nowEnergyNumber = 3;
    }
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    
 

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
