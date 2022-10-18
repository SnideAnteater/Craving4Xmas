using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSprite : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    PhotonView view;
    Player player;

    public int[] partTest;

    public TextMeshProUGUI username;


    // String Arrays
    [SerializeField] public string[] bodyPartTypes;
    [SerializeField] public string[] characterStates;
    [SerializeField] public string[] characterDirections;

    //body parts
    public GameObject playerHair;
    public GameObject playerBody;
    public GameObject playerTorso;
    public GameObject playerLeg;

    //sprites
    public Sprite[] hairs;
    public Sprite[] bodies;
    public Sprite[] torsos;
    public Sprite[] legs;

    // Animation
    private Animator animator;
    private AnimationClip animationClip;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides defaultAnimationClips;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        
    }

    

    public void Start()
    {
        if(view.IsMine)
        {
            username.text = PhotonNetwork.NickName;
        }
        else
        {
            username.text = view.Owner.NickName;
        }
        setBodyPartInt();
        SetSprite();

         // Set animator
         animator = GetComponent<Animator>();
         animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
         animator.runtimeAnimatorController = animatorOverrideController;

         defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
         animatorOverrideController.GetOverrides(defaultAnimationClips);

         // Set body part animations
         UpdateBodyParts();
    }

    public void setBodyPartInt()
    {
        partTest[0] = (int)view.Owner.CustomProperties["playerHair"];
        partTest[1] = (int)view.Owner.CustomProperties["playerBody"];
        partTest[2] = (int)view.Owner.CustomProperties["playerTorso"];
        partTest[3] = (int)view.Owner.CustomProperties["playerLegs"];
    }

    public void SetSprite()
    {
        playerHair.GetComponent<SpriteRenderer>().sprite = hairs[partTest[0]];
        playerBody.GetComponent<SpriteRenderer>().sprite = bodies[partTest[1]];
        playerTorso.GetComponent<SpriteRenderer>().sprite = torsos[partTest[2]];
        playerLeg.GetComponent<SpriteRenderer>().sprite = legs[partTest[3]];
    }

    public void UpdateBodyParts()
    {
        // Override default animation clips with character body parts
        for (int partIndex = 0; partIndex < bodyPartTypes.Length; partIndex++)
        {
            // Get current body part
            string partType = bodyPartTypes[partIndex];
            // Get current body part ID
            string partID = partTest[partIndex].ToString();

            for (int stateIndex = 0; stateIndex < characterStates.Length; stateIndex++)
            {
                string state = characterStates[stateIndex];
                for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
                {
                    string direction = characterDirections[directionIndex];

                    // Get players animation from player body
                    // ***NOTE: Unless Changed Here, Animation Naming Must Be: "[Type]_[Index]_[state]_[direction]" (Ex. Body_0_idle_down)
                    animationClip = Resources.Load<AnimationClip>("Player Animations/" + partType + "/" + partType + "_" + partID + "_" + state + "_" + direction);

                    // Override default animation
                    defaultAnimationClips[partType + "_" + 0 + "_" + state + "_" + direction] = animationClip;
                }
            }
        }

        // Apply updated animations
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}
