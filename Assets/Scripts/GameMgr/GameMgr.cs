using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance =>instance;
    private static GameMgr instance;
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public EnergyInfo energyInfo;
    void Start()
    {
        //�������ݣ����û�������򴴽�
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
