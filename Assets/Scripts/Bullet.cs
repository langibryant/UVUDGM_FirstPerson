using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float shootTime;

    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable(){
        shootTime = Time.time;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage(damage);
        else if(other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(damage);
        
        gameObject.SetActive(false);

        GameObject obj = Instantiate(hitParticle, transform.position, Quanternion.identity);
        Destroy(obj);
    }
}
