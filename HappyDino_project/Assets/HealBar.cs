using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerLife Health;
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealBarCurrent;
    void Start()
    {
        HealthBar.fillAmount = Health.currentHealth / 5;
    }

    // Update is called once per frame
    void Update()
    {
        HealBarCurrent.fillAmount = Health.currentHealth / 5;
    }
}
