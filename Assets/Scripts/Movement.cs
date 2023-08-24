using UnityEngine;
public class Movement : MonoBehaviour
{
    
    [SerializeField] AudioClip engine;
    [SerializeField] float mainthrust = 2f;
    [SerializeField] float rotationthrust = 2f;
    [SerializeField] ParticleSystem mainThrust;
    [SerializeField] ParticleSystem leftThrust; 
    [SerializeField] ParticleSystem rightThrust; 

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D)) //a and d cannot be pressed at the same time
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    } 
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engine); //playoneshot is used to play multiple audios in a script 
        }
        if (!mainThrust.isPlaying)
        {
            mainThrust.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainThrust.Stop();
    }
    void RotateLeft()
    {
        ApplyRotation(rotationthrust);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotationthrust);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }
    void ApplyRotation(float rotationPerFrame)
    {
        rb.freezeRotation = true; //freeze rotation so that we can rotate without interruption of physics system
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so that physics system takes over
    }
     void StopRotating()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }

    
}
