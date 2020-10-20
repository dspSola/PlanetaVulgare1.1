using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossEvent : MonoBehaviour
{
    [SerializeField] private GameObject _waterBoss;
    [SerializeField] private WaterBossEntity _waterBossEntity;
    [SerializeField] private WaterBossAgentController _waterBossAgentController;

    private void Awake()
    {
        _waterBoss = GameObject.Find("Water Boss");
        _waterBossEntity = _waterBoss.GetComponent<WaterBossEntity>();
        _waterBossAgentController = _waterBoss.GetComponentInChildren<WaterBossAgentController>();
    }

    private void Start()
    {
        _waterBoss.SetActive(false);
    }

    public void ActiveWaterBoss(GameObject playerGo)
    {
        _waterBoss.SetActive(true);
        _waterBossAgentController.SetPlayerTransform(playerGo.transform);
    }
}
