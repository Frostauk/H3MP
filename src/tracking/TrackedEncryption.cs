﻿using FistVR;
using H3MP.Networking;
using System.Collections.Generic;
using UnityEngine;

namespace H3MP.Tracking
{
    public class TrackedEncryption : TrackedObject
    {
        public TNH_EncryptionTarget physicalEncryption;
        public TrackedEncryptionData encryptionData;

        // Unknown tracked ID queues
        public static Dictionary<uint, KeyValuePair<List<int>, List<Vector3>>> unknownInit = new Dictionary<uint, KeyValuePair<List<int>, List<Vector3>>>();
        public static Dictionary<uint, List<int>> unknownSpawnSubTarg = new Dictionary<uint, List<int>>();
        public static Dictionary<uint, List<int>> unknownDisableSubTarg = new Dictionary<uint, List<int>>();
        public static Dictionary<uint, List<KeyValuePair<int, Vector3>>> unknownSpawnGrowth = new Dictionary<uint, List<KeyValuePair<int, Vector3>>>();
        public static Dictionary<uint, List<KeyValuePair<int, Vector3>>> unknownResetGrowth = new Dictionary<uint, List<KeyValuePair<int, Vector3>>>();

        // TrackedEncryptionReferences array
        // Used by Encryptions who need to get access to their TrackedItem very often (On Update for example)
        // This is used to bypass having to find the item in a datastructure too often
        public static TrackedEncryption[] trackedEncryptionReferences = new TrackedEncryption[100];
        public static List<int> availableTrackedEncryptionRefIndices = new List<int>() {  1,2,3,4,5,6,7,8,9,
                                                                                        10,11,12,13,14,15,16,17,18,19,
                                                                                        20,21,22,23,24,25,26,27,28,29,
                                                                                        30,31,32,33,34,35,36,37,38,39,
                                                                                        40,41,42,43,44,45,46,47,48,49,
                                                                                        50,51,52,53,54,55,56,57,58,59,
                                                                                        60,61,62,63,64,65,66,67,68,69,
                                                                                        70,71,72,73,74,75,76,77,78,79,
                                                                                        80,81,82,83,84,85,86,87,88,89,
                                                                                        90,91,92,93,94,95,96,97,98,99};

        public override void Awake()
        {
            base.Awake();

            TNH_EncryptionTarget targetScript = GetComponent<TNH_EncryptionTarget>();
            if (targetScript.SpawnPoints == null)
            {
                targetScript.SpawnPoints = new List<Transform>();
            }
            GameObject trackedEncryptionRef = new GameObject();
            trackedEncryptionRef.SetActive(false);
            if (availableTrackedEncryptionRefIndices.Count == 0)
            {
                TrackedEncryption[] tempEncryptions = trackedEncryptionReferences;
                trackedEncryptionReferences = new TrackedEncryption[tempEncryptions.Length + 100];
                for (int i = 0; i < tempEncryptions.Length; ++i)
                {
                    trackedEncryptionReferences[i] = tempEncryptions[i];
                }
                for (int i = tempEncryptions.Length; i < trackedEncryptionReferences.Length; ++i)
                {
                    availableTrackedEncryptionRefIndices.Add(i);
                }
            }
            int refIndex = availableTrackedEncryptionRefIndices[availableTrackedEncryptionRefIndices.Count - 1];
            availableTrackedEncryptionRefIndices.RemoveAt(availableTrackedEncryptionRefIndices.Count - 1);
            trackedEncryptionReferences[refIndex] = this;
            trackedEncryptionRef.name = refIndex.ToString();
            targetScript.SpawnPoints.Add(trackedEncryptionRef.transform);
        }

        protected override void OnDestroy()
        {
            // A skip of the entire destruction process may be used if H3MP has become irrelevant, like in the case of disconnection
            if (skipFullDestroy)
            {
                return;
            }

            // Type specific destruction
            // In the case of encryptions we want to make sure the tendrils and subtargs are also destroyed because they usually are in TNH_EncryptionTarget.Destroy
            // but this will not have been called if we are not the one to have destroyed it
            if(data.controller != GameManager.ID && physicalEncryption.UsesRegenerativeSubTarg)
            {
                for (int i = 0; i < physicalEncryption.Tendrils.Count; i++)
                {
                    Destroy(physicalEncryption.Tendrils[i]);
                    Destroy(physicalEncryption.SubTargs[i]);
                }
            }

            // Remove from tracked lists, which has to be done no matter what OnDestroy because we will not have the phyiscalObject anymore
            GameManager.trackedEncryptionByEncryption.Remove(physicalEncryption);

            base.OnDestroy();
        }
    }
}
