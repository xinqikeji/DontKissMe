using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyInfo 
{
    /// <summary>
    /// ��������
    /// </summary>
   public int energyVolumes;
    
    /// <summary>
    /// ��ǰ��������
    /// </summary>
    
   public int nowEnergyNumber;
    /// <summary>
    /// �Ƿ����ڻָ�
    /// </summary>
   public bool isRecover;
   
    /// <summary>
    /// �ָ�ʱ��������λ ��
    /// </summary>
    public float recoverTimeDistance;

    /// <summary>
    /// ���캯����ʼ��
    /// </summary>
    public EnergyInfo()
    {
        energyVolumes = 5;
        nowEnergyNumber = 5;
        isRecover = false;
        recoverTimeDistance = 300;
    }
}
