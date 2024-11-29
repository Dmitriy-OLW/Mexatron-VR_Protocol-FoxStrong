using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float rotationSpeed = 2f;
  public float targetHeight = 10f;
  public float heightAmplitude = 2f;
  public float heightFrequency = 1f;
  public float detectionRange = 10f;
  public float shootingCooldown = 1f;
  public Transform gunTransform;
  public GameObject bulletPrefab;
  public float bulletSpeed = 10f;
  public string patrolPointTag = "point";
  public GameObject[] patrolPoints;
  private Rigidbody rb;
  private float originalY;
  private Transform player;
  private bool isPatrolling = true;
  private bool isAttacking;
  private float lastShotTime;
  private Transform currentPatrolPoint;
  private int currentPatrolIndex;
  

  private void Start()
  {
    this.rb = this.GetComponent<Rigidbody>();
    this.originalY = this.transform.position.y;
    this.player = GameObject.FindGameObjectWithTag("Player").transform;
    this.FindNextPatrolPoint();
  }

  private void Update()
  {
    if (this.isPatrolling)
      this.PatrollingBehavior();
    else
      this.AttackPlayerBehavior();
  }

  private void PatrollingBehavior()
  {
    if ((Object) this.currentPatrolPoint == (Object) null)
    {
      this.FindNextPatrolPoint();
    }
    else
    {
      this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation((this.currentPatrolPoint.position - this.transform.position).normalized), this.rotationSpeed * Time.deltaTime);
      this.rb.velocity = this.transform.forward * this.moveSpeed;
      if ((double) Vector3.Distance(this.transform.position, this.currentPatrolPoint.position) < 1.0)
        this.FindNextPatrolPoint();
      if (!this.CanSeePlayer())
        return;
      this.isPatrolling = false;
    }
  }

  private void AttackPlayerBehavior()
  {
    if (!this.isAttacking)
    {
      this.originalY = this.transform.position.y;
      this.isAttacking = true;
    }
    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation((this.player.position - this.transform.position).normalized), this.rotationSpeed * Time.deltaTime);
    if ((double) Vector3.Distance(this.transform.position, this.player.position) < (double) this.detectionRange && (double) Time.time - (double) this.lastShotTime > (double) this.shootingCooldown)
    {
      this.Shoot();
      this.lastShotTime = Time.time;
    }
    if (!this.CanSeePlayer() && (double) Time.time - (double) this.lastShotTime > 2.0)
    {
      this.isPatrolling = true;
      this.isAttacking = false;
    }
    /*this.transform.position = this.transform.position with
    {
      y = this.targetHeight
    };*/
  }

  private bool CanSeePlayer()
  {
    RaycastHit hitInfo;
    return Physics.SphereCast(this.gunTransform.position, 5f, this.player.position - this.gunTransform.position, out hitInfo, this.detectionRange) && hitInfo.collider.CompareTag("Player");
  }

  private void Shoot()
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.bulletPrefab, this.gunTransform.position, this.gunTransform.rotation);
    gameObject.GetComponent<Rigidbody>().velocity = (this.player.position - this.gunTransform.position).normalized * this.bulletSpeed;
    Object.Destroy((Object) gameObject, 2f);
  }

  private void FindNextPatrolPoint()
  {
    if (this.patrolPoints.Length == 0)
    {
      Debug.LogWarning((object) "help");
    }
    else
    {
      this.currentPatrolIndex = (this.currentPatrolIndex + 1) % this.patrolPoints.Length;
      this.currentPatrolPoint = this.patrolPoints[this.currentPatrolIndex].transform;
    }
  }

  private void OnDrawGizmos() => Gizmos.DrawWireSphere(this.gunTransform.position, 5f);
}
