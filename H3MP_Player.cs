﻿using UnityEngine;

namespace H3MP
{
    internal class H3MP_Player
    {
        public int ID;
        public string username;

        // State vars
        public int IFF;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 headPos;
        public Quaternion headRot;
        public Vector3 torsoPos;
        public Quaternion torsoRot;
        public Vector3 leftHandPos;
        public Quaternion leftHandRot;
        public Vector3 rightHandPos;
        public Quaternion rightHandRot;
        public float health;
        public int maxHealth;

        public string scene;
        public int instance;
        public int colorIndex;

        public H3MP_Player(int ID, string username, Vector3 spawnPos, int IFF, int colorIndex)
        {
            this.ID = ID;
            this.username = username;
            this.position = spawnPos;
            this.rotation = Quaternion.identity;
            this.IFF = IFF;
            this.colorIndex = colorIndex;
        }

        public void UpdateState()
        {
            H3MP_ServerSend.PlayerState(this, scene, instance);
        }
    }
}
