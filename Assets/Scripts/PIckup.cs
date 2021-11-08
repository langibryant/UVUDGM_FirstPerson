using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckup : MonoBehaviour
{

    public PickupType type;
    public int value;

    private Vector3 startPos;

    [Header("Bobbing Animation")]
    public float rotation;
    public float bobSpeed;
    public float bobHeight;
    private bool isBobingUp;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    public enum PickupType{
        Health,
        Ammo,
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotation(Vector3.up, rotationSpeed * Time.deltaTime);

        Vector3 offset = (isBobingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0));

        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

        if(transform.position == startPos + offset){
            isBobingUp = !isBobingUp;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            PlayerController player = other.GetComponent<PlayerController>();
            switch(type){
                case PickupType.Ammo:
                    player.GiveAmmo(value);
                    break;
                case PickupType.Health:
                    player.GiveHealth(value);
                    break;
                default:
                    print("Type not accepted");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
