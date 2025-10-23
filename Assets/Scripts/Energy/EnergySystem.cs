using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public EnergyInfo energyInfo ;
    private DateTime nowTime;
    /// <summary>
    /// ����ʱ�ָ�ʱ��
    /// </summary>
    public float time;
    private bool shouldRecoverEnergy =true;
    void Awake()
    {
        // ��������
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        // ��ʼ��
        energyInfo = GameMgr.Instance.energyInfo;
        time = energyInfo.recoverTimeDistance;
        Debug.Log(energyInfo.nowEnergyNumber.ToString());
        ////�����Ƿ�ָ�����
        //StartCoroutine(RecoverEnergy());
    }

    // Update is called once per frame
    void Update()
    {
       /* if (energyInfo.nowEnergyNumber < 5)
        {
            energyInfo.isRecover = true;
        }*/
        //�����Ƿ�ָ�����
      
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
            // ��������ʱ��ֹ
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
    /// ��������
    /// </summary>
    public void ReduceEnergy()
    {
        // ��������
        if(energyInfo.nowEnergyNumber > 0)
        {
            energyInfo.nowEnergyNumber--;
            energyInfo.isRecover = true;
            PlayerPrfsMgr.Instance.SaveData("EnergyInfo", energyInfo);
        }
        if (energyInfo.nowEnergyNumber == 4)
        {
            //��һ�μ�������,��¼ʱ��
            nowTime = DateTime.Now;
        }
          
    }


 
    /// <summary>
    /// �ָ�����,���ר��
    /// </summary>
    /// <param name="isEnable">�Ƿ�����</param>
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
            Debug.Log("���δ���꽱���޷����");
        }
    }

}
