﻿using FistVR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace H3MP
{
    public class H3MP_PlayerManager : MonoBehaviour
    {
        public int ID;
        public string username;

        // Player transforms and state data
        public Transform head;
        public H3MP_PlayerHitbox headHitBox;
        public AIEntity headEntity;
        public Transform torso;
        public H3MP_PlayerHitbox torsoHitBox;
        public AIEntity torsoEntity;
        public Transform leftHand;
        public H3MP_PlayerHitbox leftHandHitBox;
        public Transform rightHand;
        public H3MP_PlayerHitbox rightHandHitBox;
        public H3MP_Billboard overheadDisplayBillboard;
        public Text usernameLabel;
        public Text healthIndicator;
        public int IFF;
        public int colorIndex;
        public TAH_ReticleContact reticleContact;
        public float health;
        public Material mat;

        public string scene;
        public int instance;

        private void Awake()
        {
            head = transform.GetChild(0);
            headEntity = head.GetChild(1).gameObject.AddComponent<AIEntity>();
            headEntity.Beacons = new List<AIEntityIFFBeacon>();
            headEntity.IFFCode = IFF;
            headHitBox = head.gameObject.AddComponent<H3MP_PlayerHitbox>();
            headHitBox.manager = this;
            headHitBox.part = H3MP_PlayerHitbox.Part.Head;
            torso = transform.GetChild(1);
            torsoEntity = torso.GetChild(0).gameObject.AddComponent<AIEntity>();
            torsoEntity.Beacons = new List<AIEntityIFFBeacon>();
            torsoEntity.IFFCode = IFF;
            torsoHitBox = torso.gameObject.AddComponent<H3MP_PlayerHitbox>();
            torsoHitBox.manager = this;
            torsoHitBox.part = H3MP_PlayerHitbox.Part.Torso;
            leftHand = transform.GetChild(2);
            leftHandHitBox = leftHand.gameObject.AddComponent<H3MP_PlayerHitbox>();
            leftHandHitBox.manager = this;
            leftHandHitBox.part = H3MP_PlayerHitbox.Part.LeftHand;
            rightHand = transform.GetChild(3);
            rightHandHitBox = rightHand.gameObject.AddComponent<H3MP_PlayerHitbox>();
            rightHandHitBox.manager = this;
            rightHandHitBox.part = H3MP_PlayerHitbox.Part.RightHand;
            overheadDisplayBillboard = transform.GetChild(4).GetChild(0).GetChild(0).gameObject.AddComponent<H3MP_Billboard>();
            usernameLabel = transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            healthIndicator = transform.GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            mat = head.GetComponent<Renderer>().sharedMaterial;
        }

        public void Damage(H3MP_PlayerHitbox.Part part, Damage damage)
        {
            if (ID != -1)
            {
                if (H3MP_ThreadManager.host)
                {
                    H3MP_ServerSend.PlayerDamage(ID, (byte)part, damage);
                }
                else
                {
                    H3MP_ClientSend.PlayerDamage(ID, (byte)part, damage);
                }
            }
            else
            {
                Mod.LogInfo("Dummy player has receive damage on " + part);
            }
        }

        public void SetEntitiesRegistered(bool registered)
        {
            if(GM.CurrentAIManager != null)
            {
                if (registered)
                {
                    GM.CurrentAIManager.RegisterAIEntity(headEntity);
                    GM.CurrentAIManager.RegisterAIEntity(torsoEntity);
                }
                else
                {
                    GM.CurrentAIManager.DeRegisterAIEntity(headEntity);
                    GM.CurrentAIManager.DeRegisterAIEntity(torsoEntity);
                }
            }
        }

        public void SetVisible(bool hidden)
        {
            head.gameObject.SetActive(hidden);
            torso.gameObject.SetActive(hidden);
            leftHand.gameObject.SetActive(hidden);
            rightHand.gameObject.SetActive(hidden);
            overheadDisplayBillboard.gameObject.SetActive(!hidden && (H3MP_GameManager.nameplateMode == 0 || (H3MP_GameManager.nameplateMode == 1 && GM.CurrentPlayerBody.GetPlayerIFF() == IFF)));
        }

        public void SetIFF(int IFF)
        {
            this.IFF = IFF;
            if (headEntity != null)
            {
                headEntity.IFFCode = IFF;
                torsoEntity.IFFCode = IFF;
            }

            if (H3MP_GameManager.colorByIFF)
            {
                SetColor(IFF);
            }

            overheadDisplayBillboard.gameObject.SetActive(head.gameObject.activeSelf && (H3MP_GameManager.nameplateMode == 0 || (H3MP_GameManager.nameplateMode == 1 && GM.CurrentPlayerBody.GetPlayerIFF() == IFF)));
        }

        public void SetColor(int colorIndex)
        {
            this.colorIndex = colorIndex % H3MP_GameManager.colors.Length;

            mat.color = H3MP_GameManager.colors[colorIndex];
        }
    }
}
