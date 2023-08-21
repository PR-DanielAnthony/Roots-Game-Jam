using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    private float shootTime;
    private bool keyShoot;
    private bool isShooting;
    private bool keyShootRelease;

    private void Update()
    {
        ShootInput();
    }

    private void ShootInput()
    {
        float shootTimeLength;
        float keyShootReleaseTimeLength = 0;

        keyShoot = Input.GetKeyDown(KeyCode.X);

        if (keyShoot && keyShootRelease)
        {
            isShooting = true;
            keyShootRelease = false;
            shootTime = Time.time;

            // Shoot Bullet
            Invoke(nameof(Shoot), .1f);
        }
        
        if (!keyShoot && !keyShootRelease)
        {
            keyShootReleaseTimeLength = Time.time - shootTime;
            keyShootRelease = true;
        }
        
        if (isShooting)
        {
            shootTimeLength = Time.time - shootTime;
            
            if (shootTimeLength >= 0.25f || keyShootReleaseTimeLength >= 0.15f)
                isShooting = false;
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = ObjectPool.GetObject(projectilePrefab);
        newProjectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, transform.rotation);
        newProjectile.GetComponent<Projectile>().Shoot();
    }
}