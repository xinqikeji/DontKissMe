using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    public Text EnergyText;
    public Text EnergyTimeText;
    public EnergySystem EnergySystem;
    private StringBuilder sb = new StringBuilder();
    private StringBuilder sb1 = new StringBuilder();
    void Start()
    {
        sb.Clear();
        EnergyText.text =sb.AppendFormat("{0}{1}","x",EnergySystem.energyInfo.nowEnergyNumber.ToString()).ToString();
    }

    // Update is called once per frame
    void Update()
    {

            sb.Clear();
            EnergyText.text = sb.AppendFormat("{0}{1}", "x", EnergySystem.energyInfo.nowEnergyNumber.ToString()).ToString();
            sb1.Clear();
            EnergyTimeText.text = sb1.AppendFormat("{0}",  EnergySystem.time.ToString()).ToString();
        
    }

    
}
