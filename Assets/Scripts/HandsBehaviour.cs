using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandsBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Space(10)]
    [SerializeField] private GameObject leftHandDirect;
    [SerializeField] private GameObject leftHandRay;
    [SerializeField] private GameObject rightHandDirect;
    [SerializeField] private GameObject rightHandRay;
    [Space(10)]
    [SerializeField] private GameObject visual_leftHandWithCandy1;
    [SerializeField] private GameObject visual_leftHandWithCandy2;
    [SerializeField] private GameObject visual_rightHandWithCandy1;
    [SerializeField] private GameObject visual_rightHandWithCandy2;
    [Space(10)]
    [SerializeField] private CandyPool candyPool;
    [Space(10)]
    [SerializeField] private InputActionReference leftGrabAction;
    [SerializeField] private InputActionReference rightGrabAction;
    [Space(10)]
    [SerializeField] private AudioClip[] throwingCandyAudioClips;


    private enum HandGrabbing { Left, Right, None, Both };
    private HandGrabbing handGrabbing;

    private Candy candyGrabbedByLeftHand;
    private Candy candyGrabbedByRightHand;


    private void Start()
    {
        leftGrabAction.action.Enable();
        leftGrabAction.action.canceled += OnLeftGrabAction_canceled;

        rightGrabAction.action.Enable();
        rightGrabAction.action.canceled += OnRightGrabAction_canceled;

        gameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.OnGameStateChangedEventArgs e)
    {
        if (e.newState != GameManager.GameState.Playing)
        {
            leftHandDirect.SetActive(false);
            leftHandRay.SetActive(true);

            rightHandDirect.SetActive(false);
            rightHandRay.SetActive(true);
        }
    }

    private void OnLeftGrabAction_canceled(InputAction.CallbackContext obj)
    {
        if (gameManager.GetGameState() != GameManager.GameState.Playing)
        {
            return;
        }

        if (handGrabbing == HandGrabbing.Left || handGrabbing == HandGrabbing.Both)
        {
            XRRayInteractor rayInteractor = leftHandRay.GetComponent<XRRayInteractor>();

            if (rayInteractor.rayEndTransform != null)
            {
                Debug.Log($"A Candy throwed by left hand on {rayInteractor.rayEndTransform.name}");

                Vector3 candyFinalPosition = rayInteractor.rayEndPoint;

                //Candy candy = candyPool.GetCandyFromPool(candyFinalPosition);
                candyGrabbedByLeftHand.transform.position = candyFinalPosition;
                candyGrabbedByLeftHand.gameObject.SetActive(true);
                candyGrabbedByLeftHand = null;

                if (handGrabbing == HandGrabbing.Left)
                {
                    handGrabbing = HandGrabbing.None;
                }
                else
                {
                    handGrabbing = HandGrabbing.Right;
                }

                int randomIndex = Random.Range(0, throwingCandyAudioClips.Length);
                AudioSource.PlayClipAtPoint(throwingCandyAudioClips[randomIndex], rayInteractor.transform.position);

                leftHandDirect.SetActive(true);
                leftHandRay.SetActive(false);
            }
        }
    }


    private void OnRightGrabAction_canceled(InputAction.CallbackContext obj)
    {
        if (gameManager.GetGameState() != GameManager.GameState.Playing)
        {
            return;
        }

        if (handGrabbing == HandGrabbing.Right || handGrabbing == HandGrabbing.Both)
        {
            XRRayInteractor rayInteractor = rightHandRay.GetComponent<XRRayInteractor>();

            if (rayInteractor.rayEndTransform != null)
            {
                Debug.Log($"A Candy throwed by right hand on {rayInteractor.rayEndTransform.name}");

                Vector3 candyFinalPosition = rayInteractor.rayEndPoint;

                candyGrabbedByRightHand.transform.position = candyFinalPosition;
                candyGrabbedByRightHand.gameObject.SetActive(true);
                candyGrabbedByRightHand = null;

                if (handGrabbing == HandGrabbing.Right)
                {
                    handGrabbing = HandGrabbing.None;
                }
                else
                {
                    handGrabbing = HandGrabbing.Left;
                }

                int randomIndex = Random.Range(0, throwingCandyAudioClips.Length);
                AudioSource.PlayClipAtPoint(throwingCandyAudioClips[randomIndex], rayInteractor.transform.position);

                rightHandDirect.SetActive(true);
                rightHandRay.SetActive(false);
            }
        }
    }



    public void GrabCandy(SelectEnterEventArgs args)
    {
        if (gameManager.GetGameState() != GameManager.GameState.Playing)
        {
            return;
        }

        if (args.interactableObject.transform.CompareTag("CandyBucket"))
        {
            Debug.Log($"A Candy grabbed by {args.interactableObject.transform}");

            if (args.interactorObject.transform.name.Contains("Right"))
            {
                rightHandDirect.SetActive(false);
                rightHandRay.SetActive(true);

                candyGrabbedByRightHand = candyPool.GetCandyFromPool();

                if (candyGrabbedByRightHand.GetCandyType() == Candy.CandyType.Type1)
                {
                    visual_rightHandWithCandy1.SetActive(true);
                    visual_rightHandWithCandy2.SetActive(false);
                }
                else
                {
                    visual_rightHandWithCandy1.SetActive(false);
                    visual_rightHandWithCandy2.SetActive(true);
                }

                if (handGrabbing == HandGrabbing.Left)
                {
                    handGrabbing = HandGrabbing.Both;
                }
                else
                {
                    handGrabbing = HandGrabbing.Right;
                }
            }
            else
            {
                leftHandDirect.SetActive(false);
                leftHandRay.SetActive(true);

                candyGrabbedByLeftHand = candyPool.GetCandyFromPool();

                if (candyGrabbedByLeftHand.GetCandyType() == Candy.CandyType.Type1)
                {
                    visual_leftHandWithCandy1.SetActive(true);
                    visual_leftHandWithCandy2.SetActive(false);
                }
                else
                {
                    visual_leftHandWithCandy1.SetActive(false);
                    visual_leftHandWithCandy2.SetActive(true);
                }

                if (handGrabbing == HandGrabbing.Right)
                {
                    handGrabbing = HandGrabbing.Both;
                }
                else
                {
                    handGrabbing = HandGrabbing.Left;
                }
            }
        }
    }
}
