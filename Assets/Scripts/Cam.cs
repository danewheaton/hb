using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    // I use regions sometimes for a chunk of variables or statements that are all for one thing, so I can press the little minus sign next to them and get them out of the way while I work on other nearby unrelated code
    #region LookVertical variables

    [SerializeField]
    float cameraSensitivityX = 3;

    float rotationX;
    const float minimumLookAngle = -45, maximumLookAngle = 45;

    #endregion

    //GameObject player;

    // HeadBob stuff is stolen and confusing - we'll just seal it up and leave it alone
    #region HeadBob variables
    [SerializeField]
    float bobbingSpeed = 0.15f;
    [SerializeField]
    float bobbingAmount = 0.05f;

    float cameraHeight;
    float timer;
    #endregion

    void Start()
    {
        cameraHeight = transform.localPosition.y;
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        LookVertical(); // looking up and down turns the camera, not the whole player
        HeadBob();
    }

    void LookVertical()
    {
        // up and down look direction is input direction (default is the y axis on the mouse and right thumbstick) times horizontal camera sensitivity
        rotationX -= Input.GetAxis("Mouse Y") * cameraSensitivityX;

        // you can't look up or down further than your neck will allow
        rotationX = Mathf.Clamp(rotationX, minimumLookAngle, maximumLookAngle);

        // look up and down - there's a lot of interesting stuff in this line. this isn't a simple transform.rotate because that function becomes confused when the above clamping is applied. euler (pronounced oiler) angles are the numbers from 0 to 360 stored in the camera's transform component's rotation vector (not just regular floats! they're calculated using a mathematical unit called a quaternion (quotient of two vectors) to get around a phenomenon called gimbal lock). it's also using LOCAL euler angles because for some reason using regular euler angles doesn't work
        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }

    // HeadBob stuff is stolen and confusing - we'll just seal it up and leave it alone
    void HeadBob()
    {
        float waveslice = 0; // whatever the fuck "waveslice" is supposed to mean
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraPosition = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2); // sine? pi? man, this is just supposed to move the camera up and down while you're walking
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0, 1);
            translateChange = totalAxes * translateChange;
            cameraPosition.y = cameraHeight + translateChange;
        }
        else
        {
            cameraPosition.y = cameraHeight;
        }

        transform.localPosition = cameraPosition;
    }
}
