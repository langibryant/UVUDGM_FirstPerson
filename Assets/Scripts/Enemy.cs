using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int curHp, maxHP, scoreToGive;

    public float moveSpeed, attackRange, yPathOffset;

    private List<Vector3> path;

    private Weapon weapon;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<PlayerController>().gameObject;
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);

        curHp = maxHP;
    }

    void UpdatePath(){
        UnityEngine.AI.NavMeshPath navmeshPath = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.transform.position, navmeshPath.AllAreas, navmeshPath);

        path = navmeshPath.corners.ToList();
    }

    void ChaseTarget(){
        if(path.Count == 0){
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0)){
            path.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist <= attackRange){
            if(weapon.CanShoot()){
                weapon.Shoot();
            }
        }
        else{
            ChaseTarget();
        }
    }

    public void TakeDamage(int damage){
        curHp -= damage;
        if(curHp <=0){
            Die();
        }
    }
    void Die(){
        GameManager.instance.AddScore(scoreToGive);
        Destroy(gameObject);
    }
}
