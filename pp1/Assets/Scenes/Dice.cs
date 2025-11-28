using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    public bool canCheck = false;
    public static Vector3 diceVelocity;

    private int[] angles = {0, 90, 180, 270, 360};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null || !rb.useGravity) return;
        if (rb.linearVelocity.y < 0) rb.AddForce(Physics.gravity * 3.0f, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.linearVelocity;
    }

    public void DiceRoll()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float dirX = Random.Range(0, 10);
		float dirY = Random.Range(0, 10);
		float dirZ = Random.Range(0, 10);
        
		Quaternion currentRotation = transform.localRotation;
		float randomIndex_x = Random.Range(0, angles.Length);
		float randomIndex_z = Random.Range(0, angles.Length);
		
        transform.localRotation = Quaternion.Euler(
            angles[(int)randomIndex_x], 
            currentRotation.eulerAngles.y, 
            angles[(int)randomIndex_z]
            );

		float ForceRand = Random.Range(10, 20);
		rb.AddForce(Vector3.up * ForceRand, ForceMode.Impulse);
		rb.AddTorque(new Vector3(dirX, dirY, dirZ), ForceMode.Impulse);
	}

    public void ResetPosition()
    {
        this.transform.position = new Vector3(Random.Range(-1, 1), 0.69f, Random.Range(-1, 1));
    }
}
