﻿using FistVR;
using H3MP.Networking;
using H3MP.Patches;
using H3MP.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace H3MP.Tracking
{
    public class TrackedPlayerBodyData : TrackedObjectData
    {
        public TrackedPlayerBody physicalPlayerBody;

        public string playerPrefabID;

        public TrackedPlayerBodyData()
        {

        }

        public TrackedPlayerBodyData(Packet packet, string typeID, int trackedID) : base(packet, typeID, trackedID)
        {
            // Update
            // NONE

            // Full
            playerPrefabID = packet.ReadString();
        }

        public static bool IsOfType(Transform t)
        {
            PlayerBody playerBody = t.GetComponent<PlayerBody>();
            if (playerBody != null)
            {
                return playerBody.playerPrefabID != null;
            }

            return false;
        }

        public override bool IsIdentifiable()
        {
            return GameManager.playerPrefabs.ContainsKey(playerPrefabID);
        }

        public static TrackedPlayerBody MakeTracked(Transform root, TrackedObjectData parent)
        {
            TrackedPlayerBody trackedPlayerBody = root.gameObject.AddComponent<TrackedPlayerBody>();
            TrackedPlayerBodyData data = new TrackedPlayerBodyData();
            trackedPlayerBody.data = data;
            trackedPlayerBody.playerBodyData = data;
            data.physical = trackedPlayerBody;
            data.physicalPlayerBody = trackedPlayerBody;
            data.physicalPlayerBody.physicalPlayerBody = root.GetComponent<PlayerBody>();
            data.physical.physical = data.physicalPlayerBody.physicalPlayerBody;

            data.playerPrefabID = data.physicalPlayerBody.physicalPlayerBody.playerPrefabID;

            data.typeIdentifier = "TrackedPlayerBodyData";
            data.active = trackedPlayerBody.gameObject.activeInHierarchy;
            data.scene = GameManager.sceneLoading ? LoadLevelBeginPatch.loadingLevel : GameManager.scene;
            data.instance = GameManager.instance;
            data.initTracker = GameManager.ID;
            data.controller = GameManager.ID;
            data.sceneInit = SpawnVaultFileRoutinePatch.inInitSpawnVaultFileRoutine || AnvilPrefabSpawnPatch.inInitPrefabSpawn || GameManager.inPostSceneLoadTrack;

            GameManager.currentTrackedPlayerBody = trackedPlayerBody;
            GameManager.trackedObjectByObject.Add(data.physicalPlayerBody.physicalPlayerBody, trackedPlayerBody);
            for (int i = 0; i < data.physicalPlayerBody.physicalPlayerBody.hitboxes.Length; ++i)
            {
                GameManager.trackedObjectByDamageable.Add(data.physicalPlayerBody.physicalPlayerBody.hitboxes[i], trackedPlayerBody);
            }

            // Add to local list
            data.localTrackedID = GameManager.objects.Count;
            GameManager.objects.Add(data);

            return trackedPlayerBody;
        }

        public override IEnumerator Instantiate()
        {
            GameObject playerPrefab = null;
            if (GameManager.playerPrefabs.TryGetValue(playerPrefabID, out UnityEngine.Object prefabObject))
            {
                if(prefabObject == null)
                {
                    if(IM.OD.TryGetValue(playerPrefabID, out FVRObject fvrObject))
                    {
                        yield return fvrObject.GetGameObjectAsync();
                        playerPrefab = fvrObject.GetGameObject();
                    }
                    else
                    {
                        Mod.LogWarning($"Attempted to instantiate player body \"{playerPrefabID}\" sent from {controller} but failed to get prefab. Using H3MP default player body instead");
                        playerPrefab = Mod.playerPrefab;
                    }
                }
                else if(prefabObject is FVRObject)
                {
                    FVRObject fvrObject = prefabObject as FVRObject;
                    yield return fvrObject.GetGameObjectAsync();
                    playerPrefab = fvrObject.GetGameObject();
                }
                else if(prefabObject is GameObject)
                {
                    playerPrefab = prefabObject as GameObject;
                }
            }

            if (playerPrefab == null)
            {
                Mod.LogWarning($"Attempted to instantiate player body \"{playerPrefabID}\" sent from {controller} but failed to get prefab. Using H3MP default player body instead");
                playerPrefab = Mod.playerPrefab;
            }

            if (!awaitingInstantiation)
            {
                // Could have cancelled a player body instantiation if received destruction order while we were waiting to get the prefab
                yield break;
            }

            try
            {
                ++Mod.skipAllInstantiates;
                GameObject playerBodyObject = GameObject.Instantiate(playerPrefab);
                --Mod.skipAllInstantiates;
                awaitingInstantiation = false;
                UnityEngine.Object.DontDestroyOnLoad(playerBodyObject);
                physicalPlayerBody = playerBodyObject.AddComponent<TrackedPlayerBody>();
                physical = physicalPlayerBody;
                physicalPlayerBody.playerBodyData = this;
                physical.data = this;
                physicalPlayerBody.physicalPlayerBody = playerBodyObject.GetComponent<PlayerBody>();
                physical.physical = physicalPlayerBody.physicalPlayerBody;

                GameManager.currentTrackedPlayerBody = physicalPlayerBody;
                GameManager.trackedObjectByObject.Add(physicalPlayerBody.physicalPlayerBody, physicalPlayerBody);
                for (int i = 0; i < physicalPlayerBody.physicalPlayerBody.hitboxes.Length; ++i)
                {
                    GameManager.trackedObjectByDamageable.Add(physicalPlayerBody.physicalPlayerBody.hitboxes[i], physicalPlayerBody);
                }

                // Initially set itself
                UpdateFromData(this);
            }
            catch (Exception e)
            {
                Mod.LogError("Error while trying to instantiate playerBody: " + playerPrefabID + ":\n" + e.Message + "\n" + e.StackTrace);
            }
        }

        public override void WriteToPacket(Packet packet, bool incrementOrder, bool full)
        {
            base.WriteToPacket(packet, incrementOrder, full);

            if (full)
            {
                packet.Write(playerPrefabID);
            }
        }

        public override void OnControlChanged(int newController)
        {
            base.OnControlChanged(newController);

            if (newController != initTracker)
            {
                // A player body should never have its controller changed. If it changes it is because a player disconnected
                // and their objects' control was ditributed, or something went very wrong
                GameObject.Destroy(physical.gameObject);
            }
        }

        public static bool IsControlled(Transform root)
        {
            // This will only be called upong initial tracking of the body
            // Implying we are inherently in control
            return true;
        }

        public override bool IsControlled(out int interactionID)
        {
            interactionID = 1000; // Player body
            return controller == GameManager.ID;
        }

        public override void RemoveFromLocal()
        {
            base.RemoveFromLocal();

            // Manage unknown lists
            if (trackedID == -1)
            {
                // If not tracked, make sure we remove from tracked lists in case object was unawoken
                if (physicalPlayerBody != null && physicalPlayerBody.physicalPlayerBody != null)
                {
                    for (int i = 0; i < physicalPlayerBody.physicalPlayerBody.hitboxes.Length; ++i)
                    {
                        GameManager.trackedObjectByDamageable.Remove(physicalPlayerBody.physicalPlayerBody.hitboxes[i]);
                    }
                }
            }
        }
    }
}
