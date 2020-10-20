using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] points;
    private int destPoint = 0;


    void Start()
    {
        //La désactivation du freinage automatique permet un mouvement continu entre les points
        _agent.autoBraking = false;

        GotoNextPoint();
    }


    void Update()
    {
        // Choisissez le prochain point de destination lorsque l'agent se rapproche du point actuel
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }


    private void GotoNextPoint()
    {
        // Retours si le point est null
        if (points.Length == 0)
            return;

        //Réglez l'agent pour qu'il se rende à la destination sélectionnée
        _agent.destination = points[destPoint].position;

        //Choisissez le point suivant du tableau comme destination   
        destPoint = (destPoint + 1) % points.Length;
    }
}

