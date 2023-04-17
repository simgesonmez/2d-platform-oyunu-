using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    private void onTriggerEnter2D(Collider2D other)
    {  
        other.GetComponent<PlayerManagament>().getDamage(damage); 
        
    } 
}

