using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private GunProperty gunProperty;
    [SerializeField] private LayerMask enemyLayer;

    private float nextFire;        
    private Stack stack;
    private Animator otherAnimator;

    private bool hitted;

    void Awake()
    {
        stack = GetComponent<Stack>();
        otherAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckShooting();
    }

    void CheckShooting()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + gunProperty.fireRate;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, gunProperty.weaponRange, enemyLayer))
            {

                ShootableObject health = hit.collider.GetComponent<ShootableObject>();

                if (health.currentHealth > 0 && !hitted && health != null && Stack.numberOfBalls > 0)
                {

                    health.Damage(gunProperty.gunDamage);
                    hitted = true;
                    GameObject bullet = stack.lastChild();

                    Shoot(bullet, hit.transform.position);

                    StartCoroutine(WaitForNextShoot());

                }
                else
                {
                    otherAnimator.ResetTrigger("isShooting");
                }

            }
        }
    }
    private void Shoot(GameObject bullet, Vector3 hitPosition)
    {

        otherAnimator.SetTrigger("isShooting");
        stack.RemoveFromStack(bullet);

        BulletController bulletcontroller = bullet.AddComponent<BulletController>();

        bulletcontroller.transform.position = handTransform.position;
        bulletcontroller.Target = hitPosition;

    }

    private IEnumerator WaitForNextShoot()
    {
        yield return new WaitForSeconds(0.2f);

        hitted = false;
    }

}
