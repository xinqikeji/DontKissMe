using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    /// <summary>
    /// 能量信息
    /// </summary>
    public EnergyInfo energyInfo ;
    private DateTime nowTime;
    /// <summary>
    /// 倒计时恢复时间
    /// </summary>
    public float time;
    private bool shouldRecoverEnergy =true;
    void Awake()
    {
        // 创建单例
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        // 初始化
        energyInfo = GameMgr.Instance.energyInfo;
        time = energyInfo.recoverTimeDistance;
        Debug.Log(energyInfo.nowEnergyNumber.ToString());
        ////监听是否恢复能量
        //StartCoroutine(RecoverEnergy());
    }

    // Update is called once per frame
    void Update()
    {
       /* if (energyInfo.nowEnergyNumber < 5)
        {
            energyInfo.isRecover = true;
        }*/
        //监听是否恢复能量
      
    }

    private const int SAVE_INTERVAL = 3;
    private int saveCounter;
    private bool isRecovering;

    public void StartRecover()
    {
        if (isRecovering) return;
        shouldRecoverEnergy = true;
        isRecovering = true;
        StartCoroutine(RecoverEnergy());
    }

    private IEnumerator RecoverEnergy()
    {
        while (shouldRecoverEnergy && energyInfo.isRecover)
        {
            // 能量已满时终止
            if (energyInfo.nowEnergyNumber >= energyInfo.energyVolumes)
            {
                energyInfo.isRecover = false;
                shouldRecoverEnergy = false;
                yield break;
            }

            yield return new WaitForSeconds(1);
            time--;

            if (time <= 0)
            {
                AddEnergy();
                time = energyInfo.recoverTimeDistance;

                saveCounter++;
                if (saveCounter >= SAVE_INTERVAL)
                {
                    PlayerPrfsMgr.Instance.SaveData("EnergyInfo", energyInfo);
                    saveCounter = 0;
                }
            }
        }
        isRecovering = false;
    }

    void AddEnergy()
    {
        energyInfo.nowEnergyNumber = Mathf.Min(energyInfo.nowEnergyNumber + 1, energyInfo.energyVolumes);
        if (energyInfo.nowEnergyNumber == energyInfo.energyVolumes)
        {
            energyInfo.isRecover = false;
            shouldRecoverEnergy = false;
        }
    }

    /// <summary>
    /// 消耗能量
    /// </summary>
    public void ReduceEnergy()
    {
        // 消耗能量
        if(energyInfo.nowEnergyNumber > 0)
        {
            energyInfo.nowEnergyNumber--;
            energyInfo.isRecover = true;
            PlayerPrfsMgr.Instance.SaveData("EnergyInfo", energyInfo);
        }
        if (energyInfo.nowEnergyNumber == 4)
        {
            //第一次减少能量,记录时间
            nowTime = DateTime.Now;
        }
          
    }


 
    /// <summary>
    /// 恢复能量,广告专用
    /// </summary>
    /// <param name="isEnable">是否看完广告</param>
    public void AddEnergy(bool isEnable)
    {
        if (isEnable)
        {
            energyInfo.nowEnergyNumber++;
            if(energyInfo.nowEnergyNumber == energyInfo.energyVolumes)
                energyInfo.isRecover = false;
            PlayerPrfsMgr.Instance.SaveData("EnergyInfo", energyInfo);
        }
        else
        {
            Debug.Log("广告未看完奖励无法获得");
        }
    }

}
