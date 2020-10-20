using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;

    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, this.transform.position)<10)
        {
            Vector3 _direction = player.position - this.transform.position;
            _direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(_direction), 0.1f);

            if(_direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.5f);
            }
        }
    }
}
