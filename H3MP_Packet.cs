﻿using FistVR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace H3MP
{
    /// <summary>Sent from server to client.</summary>
    public enum ServerPackets
    {
        welcome = 1,
        spawnPlayer = 2,
        playerState = 3,
        playerScene = 4,
        addNonSyncScene = 5,
        trackedItems = 6,
        trackedItem = 7,
        shatterableCrateSetHoldingHealth = 8,
        giveControl = 9,
        destroyItem = 10,
        itemParent = 11,
        connectSync = 12,
        weaponFire = 13,
        playerDamage = 14,
        trackedSosig = 15,
        trackedSosigs = 16,
        giveSosigControl = 17,
        destroySosig = 18,
        sosigPickUpItem = 19,
        sosigPlaceItemIn = 20,
        sosigDropSlot = 21,
        sosigHandDrop = 22,
        sosigConfigure = 23,
        sosigLinkRegisterWearable = 24,
        sosigLinkDeRegisterWearable = 25,
        sosigSetIFF = 26,
        sosigSetOriginalIFF = 27,
        sosigLinkDamage = 28,
        sosigDamageData = 29,
        sosigWearableDamage = 30,
        sosigLinkExplodes = 31,
        sosigDies = 32,
        sosigClear = 33,
        sosigSetBodyState = 34,
        playSosigFootStepSound = 35,
        sosigSpeakState = 36,
        sosigSetCurrentOrder = 37,
        sosigVaporize = 38,
        sosigRequestHitDecal = 39,
        sosigLinkBreak = 40,
        sosigLinkSever = 41,
        updateRequest = 42,
        playerInstance = 43,
        addTNHInstance = 44,
        addTNHCurrentlyPlaying = 45,
        removeTNHCurrentlyPlaying = 46,
        setTNHProgression = 47,
        setTNHEquipment = 48,
        setTNHHealthMode = 49,
        setTNHTargetMode = 50,
        setTNHAIDifficulty = 51,
        setTNHRadarMode = 52,
        setTNHItemSpawnerMode = 53,
        setTNHBackpackMode = 54,
        setTNHHealthMult = 55,
        setTNHSosigGunReload = 56,
        setTNHSeed = 57,
        setTNHLevelID = 58,
        addInstance = 59,
        setTNHController = 60,
        spectatorHost = 61,
        TNHPlayerDied = 62,
        TNHAddTokens = 63,
        TNHSetLevel = 64,
        trackedAutoMeater = 65,
        trackedAutoMeaters = 66,
        destroyAutoMeater = 67,
        giveAutoMeaterControl = 68,
        autoMeaterSetState = 69,
        autoMeaterSetBladesActive = 70,
        autoMeaterDamage = 71,
        autoMeaterFireShot = 72,
        autoMeaterFirearmFireAtWill = 73,
        autoMeaterHitZoneDamage = 74,
        autoMeaterHitZoneDamageData = 75,
        TNHSosigKill = 76,
        TNHHoldPointSystemNode = 77,
        TNHHoldBeginChallenge = 78,
        TNHSetPhaseTake = 79,
        TNHHoldCompletePhase = 80,
        TNHHoldPointFailOut = 81,
        TNHSetPhaseComplete = 82,
        TNHSetPhase = 83,
        trackedEncryptions = 84,
        trackedEncryption = 85,
        giveEncryptionControl = 86,
        destroyEncryption = 87,
        encryptionDamage = 88,
        encryptionDamageData = 89,
        encryptionRespawnSubTarg = 90,
        encryptionSpawnGrowth = 91,
        encryptionInit = 92,
        encryptionResetGrowth = 93,
        encryptionDisableSubtarg = 94,
        encryptionSubDamage = 95,
        shatterableCrateDamage = 96,
        shatterableCrateDestroy = 97,
        initTNHInstances = 98,
        sosigWeaponFire = 99,
        sosigWeaponShatter = 100,
        sosigWeaponDamage = 101,
        LAPD2019Fire = 102,
        LAPD2019LoadBattery = 103,
        LAPD2019ExtractBattery = 104,
        minigunFire = 105,
        attachableFirearmFire = 106,
        breakActionWeaponFire = 107,
        playerIFF = 108,
        uberShatterableShatter = 109,
        TNHHoldPointBeginAnalyzing = 110,
        TNHHoldPointRaiseBarriers = 111,
        TNHHoldIdentifyEncryption = 112,
        TNHHoldPointBeginPhase = 113,
        TNHHoldPointCompleteHold = 114,
        sosigPriorityIFFChart = 115,
        leverActionFirearmFire = 116,
        revolvingShotgunFire = 117,
        derringerFire = 118,
        flintlockWeaponBurnOffOuter = 119,
        flintlockWeaponFire = 120,
        grappleGunFire = 121,
        HCBReleaseSled = 122,
        remoteMissileDetonate = 123,
        remoteMissileDamage = 124,
        revolverFire = 125,
        singleActionRevolverFire = 126,
        stingerLauncherFire = 127,
        stingerMissileDamage = 128,
        stingerMissileExplode = 129,
        pinnedGrenadeExplode = 130,
        FVRGrenadeExplode = 131,
        clientDisconnect = 132,
        serverClosed = 133,
        initConnectionData = 134,
        bangSnapSplode = 135,
        C4Detonate = 136,
        claymoreMineDetonate = 137,
        SLAMDetonate = 138,
        ping = 139,
        TNHSetPhaseHold = 140,
        shatterableCrateSetHoldingToken = 141,
        resetTNH = 142,
        reviveTNHPlayer = 143,
        playerColor = 144,
        colorByIFF = 145,
        nameplateMode = 146,
        radarMode = 147,
        radarColor = 148,
        TNHInitializer = 149,
        maxHealth = 150,
        fuseIgnite = 151,
        fuseBoom = 152,
        molotovShatter = 153,
        molotovDamage = 154,
        pinnedGrenadePullPin = 155,
        magazineAddRound = 156,
        clipAddRound = 157,
        speedloaderChamberLoad = 158,
        remoteGunChamber = 159
    }

    /// <summary>Sent from client to server.</summary>
    public enum ClientPackets
    {
        welcomeReceived = 1,
        playerState = 2,
        playerScene = 3,
        addNonSyncScene = 4,
        trackedItems = 5,
        trackedItem = 6,
        shatterableCrateSetHoldingHealth = 7,
        giveControl = 8,
        destroyItem = 9,
        itemParent = 10,
        weaponFire = 11,
        playerDamage = 12,
        trackedSosig = 13,
        trackedSosigs = 14,
        giveSosigControl = 15,
        destroySosig = 16,
        sosigPickupItem = 17,
        sosigPlaceItemIn = 18,
        sosigDropSlot = 19,
        sosigHandDrop = 20,
        sosigConfigure = 21,
        sosigLinkRegisterWearable = 22,
        sosigLinkDeRegisterWearable = 23,
        sosigSetIFF = 24,
        sosigSetOriginalIFF = 25,
        sosigLinkDamage = 26,
        sosigDamageData = 27,
        sosigWearableDamage = 28,
        sosigLinkExplodes = 29,
        sosigDies = 30,
        sosigClear = 31,
        sosigSetBodyState = 32,
        playSosigFootStepSound = 33,
        sosigSpeakState = 34,
        sosigSetCurrentOrder = 35,
        sosigVaporize = 36,
        sosigRequestHitDecal = 37,
        sosigLinkBreak = 38,
        sosigLinkSever = 39,
        updateItemRequest = 40,
        updateSosigRequest = 41,
        playerInstance = 42,
        addTNHInstance = 43,
        addTNHCurrentlyPlaying = 44,
        removeTNHCurrentlyPlaying = 45,
        setTNHProgression = 46,
        setTNHEquipment = 47,
        setTNHHealthMode = 48,
        setTNHTargetMode = 49,
        setTNHAIDifficulty = 50,
        setTNHRadarMode = 51,
        setTNHItemSpawnerMode = 52,
        setTNHBackpackMode = 53,
        setTNHHealthMult = 54,
        setTNHSosigGunReload = 55,
        setTNHSeed = 56,
        setTNHLevelID = 57,
        addInstance = 58,
        setTNHController = 59,
        spectatorHost = 60,
        TNHPlayerDied = 61,
        TNHAddTokens = 62,
        TNHSetLevel = 63,
        trackedAutoMeater = 64,
        trackedAutoMeaters = 65,
        destroyAutoMeater = 66,
        giveAutoMeaterControl = 67,
        updateAutoMeatersRequest = 68,
        autoMeaterSetState = 69,
        autoMeaterSetBladesActive = 70,
        autoMeaterDamage = 71,
        autoMeaterDamageData = 72,
        autoMeaterFireShot = 73,
        autoMeaterFirearmFireAtWill = 74,
        autoMeaterHitZoneDamage = 75,
        autoMeaterHitZoneDamageData = 76,
        TNHSosigKill = 77,
        TNHHoldPointSystemNode = 78,
        TNHHoldBeginChallenge = 79,
        shatterableCrateDamage = 80,
        TNHSetPhaseTake = 81,
        TNHHoldCompletePhase = 82,
        TNHHoldPointFailOut = 83,
        TNHSetPhaseComplete = 84,
        TNHSetPhase = 85,
        trackedEncryptions = 86,
        trackedEncryption = 87,
        giveEncryptionControl = 88,
        destroyEncryption = 89,
        encryptionDamage = 90,
        encryptionDamageData = 91,
        encryptionRespawnSubTarg = 92,
        encryptionSpawnGrowth = 93,
        encryptionInit = 94,
        encryptionResetGrowth = 95,
        encryptionDisableSubtarg = 96,
        encryptionSubDamage = 97,
        shatterableCrateDestroy = 98,
        updateEncryptionsRequest = 99,
        DoneLoadingScene = 100,
        DoneSendingUpdaToDateObjects = 101,
        sosigWeaponFire = 102,
        sosigWeaponShatter = 103,
        sosigWeaponDamage = 104,
        LAPD2019Fire = 105,
        LAPD2019LoadBattery = 106,
        LAPD2019ExtractBattery = 107,
        minigunFire = 108,
        attachableFirearmFire = 109,
        breakActionWeaponFire = 110,
        playerIFF = 111,
        uberShatterableShatter = 112,
        TNHHoldPointBeginAnalyzing = 113,
        TNHHoldPointRaiseBarriers = 114,
        TNHHoldIdentifyEncryption = 115,
        TNHHoldPointBeginPhase = 116,
        TNHHoldPointCompleteHold = 117,
        sosigPriorityIFFChart = 118,
        leverActionFirearmFire = 119,
        revolvingShotgunFire = 120,
        derringerFire = 121,
        flintlockWeaponBurnOffOuter = 122,
        flintlockWeaponFire = 123,
        grappleGunFire = 124,
        HCBReleaseSled = 125,
        remoteMissileDetonate = 126,
        remoteMissileDamage = 127,
        revolverFire = 128,
        singleActionRevolverFire = 129,
        stingerLauncherFire = 130,
        stingerMissileDamage = 131,
        stingerMissileExplode = 132,
        pinnedGrenadeExplode = 133,
        FVRGrenadeExplode = 134,
        clientDisconnect = 135,
        bangSnapSplode = 136,
        C4Detonate = 137,
        claymoreMineDetonate = 138,
        SLAMDetonate = 139,
        ping = 140,
        TNHSetPhaseHold = 141,
        shatterableCrateSetHoldingToken = 142,
        resetTNH = 143,
        reviveTNHPlayer = 144,
        playerColor = 145,
        requestTNHInit = 146,
        TNHInit = 147,
        fuseIgnite = 148,
        fuseBoom = 149,
        molotovShatter = 150,
        molotovDamage = 151,
        pinnedGrenadePullPin = 152,
        magazineAddRound = 153,
        clipAddRound = 154,
        speedloaderChamberLoad = 155,
        remoteGunChamber = 156
    }

    public class H3MP_Packet : IDisposable
    {
        public List<byte> buffer;
        public byte[] readableBuffer;
        public int readPos;

        /// <summary>Creates a new empty packet (without an ID).</summary>
        public H3MP_Packet()
        {
            buffer = new List<byte>(); // Intitialize buffer
            readPos = 0; // Set readPos to 0
        }

        /// <summary>Creates a new packet with a given ID. Used for sending.</summary>
        /// <param name="_id">The packet ID.</param>
        public H3MP_Packet(int _id)
        {
            buffer = new List<byte>(); // Intitialize buffer
            readPos = 0; // Set readPos to 0

            Write(_id); // Write packet id to the buffer
        }

        /// <summary>Creates a packet from which data can be read. Used for receiving.</summary>
        /// <param name="_data">The bytes to add to the packet.</param>
        public H3MP_Packet(byte[] _data)
        {
            buffer = new List<byte>(); // Intitialize buffer
            readPos = 0; // Set readPos to 0

            SetBytes(_data);
        }

        #region Functions
        /// <summary>Sets the packet's content and prepares it to be read.</summary>
        /// <param name="_data">The bytes to add to the packet.</param>
        public void SetBytes(byte[] _data)
        {
            Write(_data);
            readableBuffer = buffer.ToArray();
        }

        /// <summary>Inserts the length of the packet's content at the start of the buffer.</summary>
        public void WriteLength()
        {
            buffer.InsertRange(0, BitConverter.GetBytes(buffer.Count)); // Insert the byte length of the packet at the very beginning
        }

        /// <summary>Inserts the given int at the start of the buffer.</summary>
        /// <param name="_value">The int to insert.</param>
        public void InsertInt(int _value)
        {
            buffer.InsertRange(0, BitConverter.GetBytes(_value)); // Insert the int at the start of the buffer
        }

        /// <summary>Gets the packet's content in array form.</summary>
        public byte[] ToArray()
        {
            readableBuffer = buffer.ToArray();
            return readableBuffer;
        }

        /// <summary>Gets the length of the packet's content.</summary>
        public int Length()
        {
            return buffer.Count; // Return the length of buffer
        }

        /// <summary>Gets the length of the unread data contained in the packet.</summary>
        public int UnreadLength()
        {
            return Length() - readPos; // Return the remaining length (unread)
        }

        /// <summary>Resets the packet instance to allow it to be reused.</summary>
        /// <param name="_shouldReset">Whether or not to reset the packet.</param>
        public void Reset(bool _shouldReset = true)
        {
            if (_shouldReset)
            {
                buffer.Clear(); // Clear buffer
                readableBuffer = null;
                readPos = 0; // Reset readPos
            }
            else
            {
                readPos -= 4; // "Unread" the last read int
            }
        }
        #endregion

        #region Write Data
        /// <summary>Adds a byte to the packet.</summary>
        /// <param name="_value">The byte to add.</param>
        public void Write(byte _value)
        {
            buffer.Add(_value);
        }
        /// <summary>Adds an array of bytes to the packet.</summary>
        /// <param name="_value">The byte array to add.</param>
        public void Write(byte[] _value)
        {
            buffer.AddRange(_value);
        }
        /// <summary>Adds a short to the packet.</summary>
        /// <param name="_value">The short to add.</param>
        public void Write(short _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds an int to the packet.</summary>
        /// <param name="_value">The int to add.</param>
        public void Write(int _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds an uint to the packet.</summary>
        /// <param name="_value">The uint to add.</param>
        public void Write(uint _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds a long to the packet.</summary>
        /// <param name="_value">The long to add.</param>
        public void Write(long _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds a float to the packet.</summary>
        /// <param name="_value">The float to add.</param>
        public void Write(float _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds a double to the packet.</summary>
        /// <param name="_value">The double to add.</param>
        public void Write(double _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds a bool to the packet.</summary>
        /// <param name="_value">The bool to add.</param>
        public void Write(bool _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
        /// <summary>Adds a string to the packet.</summary>
        /// <param name="_value">The string to add.</param>
        public void Write(string _value)
        {
            Write(_value.Length); // Add the length of the string to the packet
            buffer.AddRange(Encoding.ASCII.GetBytes(_value)); // Add the string itself
        }
        /// <summary>Adds a Vector3 to the packet.</summary>
        /// <param name="_value">The Vector3 to add.</param>
        public void Write(Vector3 _value)
        {
            Write(_value.x);
            Write(_value.y);
            Write(_value.z);
        }
        /// <summary>Adds a Vector2 to the packet.</summary>
        /// <param name="_value">The Vector2 to add.</param>
        public void Write(Vector2 _value)
        {
            Write(_value.x);
            Write(_value.y);
        }
        /// <summary>Adds a Quaternion to the packet.</summary>
        /// <param name="_value">The Quaternion to add.</param>
        public void Write(Quaternion _value)
        {
            Write(_value.x);
            Write(_value.y);
            Write(_value.z);
            Write(_value.w);
        }
        /// <summary>Adds a H3MP_TrackedItemData to the packet.</summary>
        /// <param name="_value">The H3MP_TrackedItemData to add.</param>
        public void Write(H3MP_TrackedItemData trackedItem, bool incrementOrder, bool full)
        {
            Write(trackedItem.trackedID);
            Write(trackedItem.position);
            Write(trackedItem.rotation);
            if(trackedItem.data == null || trackedItem.data.Length == 0)
            {
                Write(0);
            }
            else
            {
                Write(trackedItem.data.Length);
                Write(trackedItem.data);
            }
            Write(trackedItem.active);
            Write(trackedItem.underActiveControl);

            // TODO: Optimization: Make which data we send dependent on what updated, cause we shouldn't send the entire data array if there was no update in it

            if (full)
            {
                Write(trackedItem.itemID);
                if(trackedItem.identifyingData == null || trackedItem.identifyingData.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedItem.identifyingData.Length);
                    Write(trackedItem.identifyingData);
                }
                Write(trackedItem.controller);
                Write(trackedItem.parent);
                Write(trackedItem.localTrackedID);
                Write(trackedItem.scene);
                Write(trackedItem.instance);
                Write(trackedItem.sceneInit);
                if (trackedItem.additionalData == null || trackedItem.additionalData.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedItem.additionalData.Length);
                    Write(trackedItem.additionalData);
                }
                Write(trackedItem.localWaitingIndex);
                Write(trackedItem.initTracker);
            }
            else
            {
                if (incrementOrder)
                {
                    Write(trackedItem.order++);
                }
                else
                {
                    Write(trackedItem.order);
                }
            }
        }
        /// <summary>Adds a Damage to the packet.</summary>
        /// <param name="_value">The Damage to add.</param>
        public void Write(Damage damage)
        {
            Write(damage.point);
            Write(damage.Source_IFF);
            Write(damage.Source_Point);
            Write(damage.Dam_Blunt);
            Write(damage.Dam_Piercing);
            Write(damage.Dam_Cutting);
            Write(damage.Dam_TotalKinetic);
            Write(damage.Dam_Thermal);
            Write(damage.Dam_Chilling);
            Write(damage.Dam_EMP);
            Write(damage.Dam_TotalEnergetic);
            Write(damage.Dam_Stunning);
            Write(damage.Dam_Blinding);
            Write(damage.hitNormal);
            Write(damage.strikeDir);
            Write(damage.edgeNormal);
            Write(damage.damageSize);
            Write((byte)damage.Class);
        }
        /// <summary>Adds a H3MP_TrackedSosigData to the packet.</summary>
        /// <param name="trackedSosig">The H3MP_TrackedSosigData to add.</param>
        /// <param name="full">Whether to include all necessary data to instantiate this sosig.</param>
        public void Write(H3MP_TrackedSosigData trackedSosig, bool incrementOrder, bool full)
        {
            Write(trackedSosig.trackedID);
            Write(trackedSosig.position);
            Write(trackedSosig.rotation); 
            Write(trackedSosig.active);
            Write(trackedSosig.mustard);
            if(trackedSosig.ammoStores != null && trackedSosig.ammoStores.Length > 0)
            {
                Write((byte)trackedSosig.ammoStores.Length);
                for(int i=0; i < trackedSosig.ammoStores.Length; ++i)
                {
                    Write(trackedSosig.ammoStores[i]);
                }
            }
            else
            {
                Write((byte)0);
            }
            Write((byte)trackedSosig.bodyPose);
            if (trackedSosig.linkIntegrity == null || trackedSosig.linkIntegrity.Length == 0)
            {
                Write((byte)0);
            }
            else
            {
                Write((byte)trackedSosig.linkIntegrity.Length);
                for (int i = 0; i < trackedSosig.linkIntegrity.Length; ++i)
                {
                    Write(trackedSosig.linkIntegrity[i]);
                }
            }
            Write((byte)trackedSosig.fallbackOrder);
            Write((byte)trackedSosig.currentOrder);

            if (full)
            {
                if (trackedSosig.linkData == null || trackedSosig.linkData.Length == 0)
                {
                    Write((byte)0);
                }
                else
                {
                    Write((byte)trackedSosig.linkData.Length);
                    for (int i = 0; i < trackedSosig.linkData.Length; ++i)
                    {
                        for (int k = 0; k < 5; ++k)
                        {
                            Write(trackedSosig.linkData[i][k]);
                        }
                    }
                }
                Write((byte)trackedSosig.IFF);
                Write(trackedSosig.configTemplate);
                Write(trackedSosig.controller);
                Write(trackedSosig.localTrackedID);
                Write((byte)trackedSosig.wearables.Count);
                for (int i=0; i < trackedSosig.wearables.Count; ++i)
                {
                    if (trackedSosig.wearables[i] == null || trackedSosig.wearables[i].Count == 0)
                    {
                        Write((byte)0);
                    }
                    else
                    {
                        Write((byte)trackedSosig.wearables[i].Count);
                        for (int j = 0; j < trackedSosig.wearables[i].Count; ++j)
                        {
                            Write(trackedSosig.wearables[i][j]);
                        }
                    }
                }
                Write(SosigTargetPrioritySystemPatch.BoolArrToInt(trackedSosig.IFFChart));
                Write(trackedSosig.scene);
                Write(trackedSosig.instance);
                Write(trackedSosig.sceneInit);
                if (trackedSosig.data == null || trackedSosig.data.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedSosig.data.Length);
                    Write(trackedSosig.data);
                }
                Write(trackedSosig.localWaitingIndex);
                Write(trackedSosig.initTracker);
                switch (trackedSosig.currentOrder)
                {
                    case Sosig.SosigOrder.GuardPoint:
                        Write(trackedSosig.guardPoint);
                        Write(trackedSosig.guardDir);
                        Write(trackedSosig.hardGuard);
                        break;
                    case Sosig.SosigOrder.Skirmish:
                        Write(trackedSosig.skirmishPoint);
                        Write(trackedSosig.pathToPoint);
                        Write(trackedSosig.assaultPoint);
                        Write(trackedSosig.faceTowards);
                        break;
                    case Sosig.SosigOrder.Investigate:
                        Write(trackedSosig.guardPoint);
                        Write(trackedSosig.hardGuard);
                        Write(trackedSosig.faceTowards);
                        break;
                    case Sosig.SosigOrder.SearchForEquipment:
                    case Sosig.SosigOrder.Wander:
                        Write(trackedSosig.wanderPoint);
                        break;
                    case Sosig.SosigOrder.Assault:
                        Write(trackedSosig.assaultPoint);
                        Write((byte)trackedSosig.assaultSpeed);
                        Write(trackedSosig.faceTowards);
                        break;
                    case Sosig.SosigOrder.Idle:
                        Write(trackedSosig.idleToPoint);
                        Write(trackedSosig.idleDominantDir);
                        break;
                    case Sosig.SosigOrder.PathTo:
                        Write(trackedSosig.pathToPoint);
                        Write(trackedSosig.pathToLookDir);
                        break;
                }
            }
            else
            {
                if (incrementOrder)
                {
                    Write(trackedSosig.order++);
                }
                else
                {
                    Write(trackedSosig.order);
                }
            }
        }
        /// <summary>Adds a H3MP_TrackedAutoMeaterData to the packet.</summary>
        /// <param name="trackedAutoMeater">The H3MP_TrackedAutoMeaterData to add.</param>
        /// <param name="full">Whether to include all necessary data to instantiate this AutoMeater.</param>
        public void Write(H3MP_TrackedAutoMeaterData trackedAutoMeater, bool incrementOrder, bool full)
        {
            Write(trackedAutoMeater.trackedID);
            Write(trackedAutoMeater.position);
            Write(trackedAutoMeater.rotation);
            Write(trackedAutoMeater.active);
            Write((byte)trackedAutoMeater.IFF);
            Write(trackedAutoMeater.sideToSideRotation);
            Write(trackedAutoMeater.hingeTargetPos);
            Write(trackedAutoMeater.upDownMotorRotation);
            Write(trackedAutoMeater.upDownJointTargetPos);

            if (full)
            {
                Write(trackedAutoMeater.ID);
                Write(trackedAutoMeater.controller);
                Write(trackedAutoMeater.localTrackedID);
                Write(trackedAutoMeater.scene);
                Write(trackedAutoMeater.instance);
                Write(trackedAutoMeater.sceneInit);
                if (trackedAutoMeater.data == null || trackedAutoMeater.data.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedAutoMeater.data.Length);
                    Write(trackedAutoMeater.data);
                }
                Write(trackedAutoMeater.localWaitingIndex);
                Write(trackedAutoMeater.initTracker);
            }
            else
            {
                if (incrementOrder)
                {
                    Write(trackedAutoMeater.order++);
                }
                else
                {
                    Write(trackedAutoMeater.order);
                }
            }
        }
        /// <summary>Adds a H3MP_TrackedEncryptionData to the packet.</summary>
        /// <param name="trackedEncryption">The H3MP_TrackedEncryptionData to add.</param>
        /// <param name="full">Whether to include all necessary data to instantiate this Encryption.</param>
        public void Write(H3MP_TrackedEncryptionData trackedEncryption, bool incrementOrder, bool full)
        {
            Write(trackedEncryption.trackedID);
            Write(trackedEncryption.position);
            Write(trackedEncryption.rotation);
            Write(trackedEncryption.active);

            if (full)
            {
                Write((byte)trackedEncryption.type);
                Write(trackedEncryption.controller);
                Write(trackedEncryption.localTrackedID);
                if(trackedEncryption.tendrilsActive == null || trackedEncryption.tendrilsActive.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.tendrilsActive.Length);
                    for(int i=0; i < trackedEncryption.tendrilsActive.Length; ++i)
                    {
                        Write(trackedEncryption.tendrilsActive[i]);
                    }
                }
                if(trackedEncryption.growthPoints == null || trackedEncryption.growthPoints.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.growthPoints.Length);
                    for(int i=0; i < trackedEncryption.growthPoints.Length; ++i)
                    {
                        Write(trackedEncryption.growthPoints[i]);
                    }
                }
                if(trackedEncryption.subTargsPos == null || trackedEncryption.subTargsPos.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.subTargsPos.Length);
                    for(int i=0; i < trackedEncryption.subTargsPos.Length; ++i)
                    {
                        Write(trackedEncryption.subTargsPos[i]);
                    }
                }
                if(trackedEncryption.subTargsActive == null || trackedEncryption.subTargsActive.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.subTargsActive.Length);
                    for(int i=0; i < trackedEncryption.subTargsActive.Length; ++i)
                    {
                        Write(trackedEncryption.subTargsActive[i]);
                    }
                }
                if(trackedEncryption.tendrilFloats == null || trackedEncryption.tendrilFloats.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.tendrilFloats.Length);
                    for(int i=0; i < trackedEncryption.tendrilFloats.Length; ++i)
                    {
                        Write(trackedEncryption.tendrilFloats[i]);
                    }
                }
                if(trackedEncryption.tendrilsRot == null || trackedEncryption.tendrilsRot.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.tendrilsRot.Length);
                    for(int i=0; i < trackedEncryption.tendrilsRot.Length; ++i)
                    {
                        Write(trackedEncryption.tendrilsRot[i]);
                    }
                }
                if(trackedEncryption.tendrilsScale == null || trackedEncryption.tendrilsScale.Length == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(trackedEncryption.tendrilsScale.Length);
                    for(int i=0; i < trackedEncryption.tendrilsScale.Length; ++i)
                    {
                        Write(trackedEncryption.tendrilsScale[i]);
                    }
                }
                Write(trackedEncryption.scene);
                Write(trackedEncryption.instance);
                Write(trackedEncryption.sceneInit);
                Write(trackedEncryption.localWaitingIndex);
                Write(trackedEncryption.initTracker);
            }
            else
            {
                if (incrementOrder)
                {
                    Write(trackedEncryption.order++);
                }
                else
                {
                    Write(trackedEncryption.order);
                }
            }
        }
        /// <summary>Adds a SosigConfigTemplate to the packet.</summary>
        /// <param name="config">The SosigConfigTemplate to add.</param>
        public void Write(SosigConfigTemplate config)
        {
            Write(config.AppliesDamageResistToIntegrityLoss);
            Write(config.DoesDropWeaponsOnBallistic);
            Write(config.TotalMustard);
            Write(config.BleedDamageMult);
            Write(config.BleedRateMultiplier);
            Write(config.BleedVFXIntensity);
            Write(config.SearchExtentsModifier);
            Write(config.ShudderThreshold);
            Write(config.ConfusionThreshold);
            Write(config.ConfusionMultiplier);
            Write(config.ConfusionTimeMax);
            Write(config.StunThreshold);
            Write(config.StunMultiplier);
            Write(config.StunTimeMax);
            Write(config.HasABrain);
            Write(config.RegistersPassiveThreats);
            Write(config.CanBeKnockedOut);
            Write(config.MaxUnconsciousTime);
            Write(config.AssaultPointOverridesSkirmishPointWhenFurtherThan);
            Write(config.ViewDistance);
            Write(config.HearingDistance);
            Write(config.MaxFOV);
            Write(config.StateSightRangeMults);
            Write(config.StateHearingRangeMults);
            Write(config.StateFOVMults);
            Write(config.CanPickup_Ranged);
            Write(config.CanPickup_Melee);
            Write(config.CanPickup_Other);
            Write(config.DoesJointBreakKill_Head);
            Write(config.DoesJointBreakKill_Upper);
            Write(config.DoesJointBreakKill_Lower);
            Write(config.DoesSeverKill_Head);
            Write(config.DoesSeverKill_Upper);
            Write(config.DoesSeverKill_Lower);
            Write(config.DoesExplodeKill_Head);
            Write(config.DoesExplodeKill_Upper);
            Write(config.DoesExplodeKill_Lower);
            Write(config.CrawlSpeed);
            Write(config.SneakSpeed);
            Write(config.WalkSpeed);
            Write(config.RunSpeed);
            Write(config.TurnSpeed);
            Write(config.MovementRotMagnitude);
            Write(config.DamMult_Projectile);
            Write(config.DamMult_Explosive);
            Write(config.DamMult_Melee);
            Write(config.DamMult_Piercing);
            Write(config.DamMult_Blunt);
            Write(config.DamMult_Cutting);
            Write(config.DamMult_Thermal);
            Write(config.DamMult_Chilling);
            Write(config.DamMult_EMP);
            Write(config.CanBeSurpressed);
            Write(config.SuppressionMult);
            Write(config.CanBeGrabbed);
            Write(config.CanBeSevered);
            Write(config.CanBeStabbed);
            Write(config.MaxJointLimit);
            if(config.LinkDamageMultipliers == null || config.LinkDamageMultipliers.Count == 0)
            {
                Write((byte)0);
            }
            else
            {
                Write((byte)config.LinkDamageMultipliers.Count);
                foreach(float f in config.LinkDamageMultipliers)
                {
                    Write(f);
                }
            }
            if(config.LinkStaggerMultipliers == null || config.LinkStaggerMultipliers.Count == 0)
            {
                Write((byte)0);
            }
            else
            {
                Write((byte)config.LinkStaggerMultipliers.Count);
                foreach(float f in config.LinkStaggerMultipliers)
                {
                    Write(f);
                }
            }
            if(config.StartingLinkIntegrity == null || config.StartingLinkIntegrity.Count == 0)
            {
                Write((byte)0);
            }
            else
            {
                Write((byte)config.StartingLinkIntegrity.Count);
                foreach(Vector2 v in config.StartingLinkIntegrity)
                {
                    Write(v);
                }
            }
            if (config.StartingChanceBrokenJoint == null || config.StartingChanceBrokenJoint.Count == 0)
            {
                Write((byte)0);
            }
            else
            {
                Write((byte)config.StartingChanceBrokenJoint.Count);
                foreach (float f in config.StartingChanceBrokenJoint)
                {
                    Write(f);
                }
            }
            Write(config.TargetCapacity);
            Write(config.TargetTrackingTime);
            Write(config.NoFreshTargetTime);
        }
        /// <summary>Adds a H3MP_TNHInstance to the packet.</summary>
        /// <param name="instance">The H3MP_TNHInstance to add.</param>
        public void Write(H3MP_TNHInstance instance, bool full = false)
        {
            Write(instance.instance);
            Write(instance.letPeopleJoin);
            Write(instance.progressionTypeSetting);
            Write(instance.healthModeSetting);
            Write(instance.equipmentModeSetting);
            Write(instance.targetModeSetting);
            Write(instance.AIDifficultyModifier);
            Write(instance.radarModeModifier);
            Write(instance.itemSpawnerMode);
            Write(instance.backpackMode);
            Write(instance.healthMult);
            Write(instance.sosiggunShakeReloading);
            Write(instance.TNHSeed);
            Write(instance.levelID);
            if (instance.playerIDs == null || instance.playerIDs.Count == 0)
            {
                Write(0);
            }
            else
            {
                Write(instance.playerIDs.Count);
                for (int i = 0; i < instance.playerIDs.Count; ++i)
                {
                    Write(instance.playerIDs[i]);
                }
            }
            Write(instance.initializer);
            Write(instance.controller);

            if (full)
            {
                if (instance.currentlyPlaying == null || instance.currentlyPlaying.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.currentlyPlaying.Count);
                    foreach (int playerID in instance.currentlyPlaying)
                    {
                        Write(playerID);
                    }
                }
                if (instance.played == null || instance.played.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.played.Count);
                    foreach (int playerID in instance.played)
                    {
                        Write(playerID);
                    }
                }
                if (instance.dead == null || instance.dead.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.dead.Count);
                    foreach (int playerID in instance.dead)
                    {
                        Write(playerID);
                    }
                }
                Write(instance.tokenCount);
                Write(instance.holdOngoing);
                Write(instance.curHoldIndex);
                Write(instance.level);
                Write((short)instance.phase);
                if (instance.activeSupplyPointIndices == null || instance.activeSupplyPointIndices.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.activeSupplyPointIndices.Count);
                    foreach (int index in instance.activeSupplyPointIndices)
                    {
                        Write(index);
                    }
                }
                if (instance.raisedBarriers == null || instance.raisedBarriers.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.raisedBarriers.Count);
                    foreach (int index in instance.raisedBarriers)
                    {
                        Write(index);
                    }
                }
                if (instance.raisedBarrierPrefabIndices == null || instance.raisedBarrierPrefabIndices.Count == 0)
                {
                    Write(0);
                }
                else
                {
                    Write(instance.raisedBarrierPrefabIndices.Count);
                    foreach (int prefabIndex in instance.raisedBarrierPrefabIndices)
                    {
                        Write(prefabIndex);
                    }
                }
            }
        }
        /// <summary>Adds a TNH_Manager.SosigPatrolSquad to the packet.</summary>
        /// <param name="instance">The TNH_Manager.SosigPatrolSquad to add.</param>
        public void Write(TNH_Manager.SosigPatrolSquad patrol)
        {
            Write(patrol.Squad.Count);
            for(int i=0; i < patrol.Squad.Count; ++i)
            {
                Write(patrol.Squad[i].GetComponent<H3MP_TrackedSosig>().data.trackedID);
            }
            Write(patrol.PatrolPoints.Count);
            for(int i=0; i < patrol.PatrolPoints.Count; ++i)
            {
                Write(patrol.PatrolPoints[i]);
            }
            Write(patrol.CurPatrolPointIndex);
            Write(patrol.IsPatrollingUp);
        }
        #endregion

        #region Read Data
        /// <summary>Reads a byte from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public byte ReadByte(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                byte _value = readableBuffer[readPos]; // Get the byte at readPos' position
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 1; // Increase readPos by 1
                }
                return _value; // Return the byte
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'!");
            }
        }

        /// <summary>Reads an array of bytes from the packet.</summary>
        /// <param name="_length">The length of the byte array.</param>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public byte[] ReadBytes(int _length, bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                byte[] _value = buffer.GetRange(readPos, _length).ToArray(); // Get the bytes at readPos' position with a range of _length
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += _length; // Increase readPos by _length
                }
                return _value; // Return the bytes
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'!");
            }
        }

        /// <summary>Reads a short from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public short ReadShort(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                short _value = BitConverter.ToInt16(readableBuffer, readPos); // Convert the bytes to a short
                if (_moveReadPos)
                {
                    // If _moveReadPos is true and there are unread bytes
                    readPos += 2; // Increase readPos by 2
                }
                return _value; // Return the short
            }
            else
            {
                throw new Exception("Could not read value of type 'short'!");
            }
        }

        /// <summary>Reads an int from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public int ReadInt(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                int _value = BitConverter.ToInt32(readableBuffer, readPos); // Convert the bytes to an int
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 4; // Increase readPos by 4
                }
                return _value; // Return the int
            }
            else
            {
                throw new Exception("Could not read value of type 'int'!");
            }
        }

        /// <summary>Reads an uint from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public uint ReadUInt(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                uint _value = BitConverter.ToUInt32(readableBuffer, readPos); // Convert the bytes to an uint
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 4; // Increase readPos by 4
                }
                return _value; // Return the uint
            }
            else
            {
                throw new Exception("Could not read value of type 'uint'!");
            }
        }

        /// <summary>Reads a long from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public long ReadLong(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                long _value = BitConverter.ToInt64(readableBuffer, readPos); // Convert the bytes to a long
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 8; // Increase readPos by 8
                }
                return _value; // Return the long
            }
            else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }

        /// <summary>Reads a float from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public float ReadFloat(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                float _value = BitConverter.ToSingle(readableBuffer, readPos); // Convert the bytes to a float
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 4; // Increase readPos by 4
                }
                return _value; // Return the float
            }
            else
            {
                throw new Exception("Could not read value of type 'float'!");
            }
        }

        /// <summary>Reads a double from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public double ReadDouble(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                double _value = BitConverter.ToDouble(readableBuffer, readPos); // Convert the bytes to a double
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 8; // Increase readPos by 8
                }
                return _value; // Return the double
            }
            else
            {
                throw new Exception("Could not read value of type 'double'!");
            }
        }

        /// <summary>Reads a bool from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public bool ReadBool(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                // If there are unread bytes
                bool _value = BitConverter.ToBoolean(readableBuffer, readPos); // Convert the bytes to a bool
                if (_moveReadPos)
                {
                    // If _moveReadPos is true
                    readPos += 1; // Increase readPos by 1
                }
                return _value; // Return the bool
            }
            else
            {
                throw new Exception("Could not read value of type 'bool'!");
            }
        }

        /// <summary>Reads a string from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public string ReadString(bool _moveReadPos = true)
        {
            try
            {
                int _length = ReadInt(); // Get the length of the string
                string _value = Encoding.ASCII.GetString(readableBuffer, readPos, _length); // Convert the bytes to a string
                if (_moveReadPos && _value.Length > 0)
                {
                    // If _moveReadPos is true string is not empty
                    readPos += _length; // Increase readPos by the length of the string
                }
                return _value; // Return the string
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }

        /// <summary>Reads a Vector3 from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public Vector3 ReadVector3(bool _moveReadPos = true)
        {
            return new Vector3(ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos));
        }

        /// <summary>Reads a Vector2 from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public Vector2 ReadVector2(bool _moveReadPos = true)
        {
            return new Vector2(ReadFloat(_moveReadPos), ReadFloat(_moveReadPos));
        }

        /// <summary>Reads a Quaternion from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public Quaternion ReadQuaternion(bool _moveReadPos = true)
        {
            return new Quaternion(ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos), ReadFloat(_moveReadPos));
        }

        /// <summary>Reads a H3MP_TrackedItemData from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public H3MP_TrackedItemData ReadTrackedItem(bool full = false, bool _moveReadPos = true)
        {
            H3MP_TrackedItemData trackedItem = new H3MP_TrackedItemData();
            trackedItem.trackedID = ReadInt();
            trackedItem.position = ReadVector3();
            trackedItem.rotation = ReadQuaternion();
            int dataLength = ReadInt();
            if (dataLength > 0)
            {
                trackedItem.data = ReadBytes(dataLength);
            }
            trackedItem.active = ReadBool();
            trackedItem.underActiveControl = ReadBool();

            if (full)
            {
                trackedItem.itemID = ReadString();
                int identifyingDataLength = ReadInt();
                if(identifyingDataLength > 0)
                {
                    trackedItem.identifyingData = ReadBytes(identifyingDataLength);
                }
                trackedItem.controller = ReadInt();
                trackedItem.parent = ReadInt();
                trackedItem.localTrackedID = ReadInt();
                trackedItem.scene = ReadString();
                trackedItem.instance = ReadInt();
                trackedItem.sceneInit = ReadBool();
                int additionalDataLen = ReadInt();
                if (additionalDataLen > 0)
                {
                    trackedItem.additionalData = ReadBytes(additionalDataLen);
                }
                trackedItem.localWaitingIndex = ReadUInt();
                trackedItem.initTracker = ReadInt();
            }
            else
            {
                trackedItem.order = ReadByte();
            }

            return trackedItem;
        }

        /// <summary>Reads a H3MP_TrackedSosigData from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public H3MP_TrackedSosigData ReadTrackedSosig(bool full = false, bool _moveReadPos = true)
        {
            H3MP_TrackedSosigData trackedSosig = new H3MP_TrackedSosigData();

            trackedSosig.trackedID = ReadInt();
            trackedSosig.position = ReadVector3();
            trackedSosig.rotation = ReadQuaternion();
            trackedSosig.active = ReadBool();
            trackedSosig.mustard = ReadFloat();
            byte ammoStoreLength = ReadByte();
            if(ammoStoreLength > 0)
            {
                trackedSosig.ammoStores = new int[ammoStoreLength];
                for(int i=0; i < ammoStoreLength; ++i)
                {
                    trackedSosig.ammoStores[i] = ReadInt();
                }
            }
            trackedSosig.bodyPose = (Sosig.SosigBodyPose)ReadByte();
            byte sosigLinkIntegrityLength = ReadByte();
            if (sosigLinkIntegrityLength > 0)
            {
                if (trackedSosig.linkIntegrity == null)
                {
                    trackedSosig.linkIntegrity = new float[sosigLinkIntegrityLength];
                }
                for (int i = 0; i < sosigLinkIntegrityLength; ++i)
                {
                    trackedSosig.linkIntegrity[i] = ReadFloat();
                }
            }
            trackedSosig.fallbackOrder = (Sosig.SosigOrder)ReadByte();
            trackedSosig.currentOrder = (Sosig.SosigOrder)ReadByte();

            if (full)
            {
                byte sosigLinkDataLength = ReadByte();
                if (sosigLinkDataLength > 0)
                {
                    if (trackedSosig.linkData == null)
                    {
                        trackedSosig.linkData = new float[sosigLinkDataLength][];
                    }
                    for (int i = 0; i < sosigLinkDataLength; ++i)
                    {
                        if (trackedSosig.linkData[i] == null || trackedSosig.linkData[i].Length != 5)
                        {
                            trackedSosig.linkData[i] = new float[5];
                        }

                        for (int j = 0; j < 5; ++j)
                        {
                            trackedSosig.linkData[i][j] = ReadFloat();
                        }
                    }
                }
                trackedSosig.IFF = ReadByte();
                trackedSosig.configTemplate = ReadSosigConfig();
                trackedSosig.controller = ReadInt();
                trackedSosig.localTrackedID = ReadInt();
                byte linkCount = ReadByte();
                trackedSosig.wearables = new List<List<string>>();
                for(int i=0; i < linkCount; ++i)
                {
                    trackedSosig.wearables.Add(new List<string>());
                    byte wearableCount = ReadByte();
                    if (wearableCount > 0)
                    {
                        for(int j = 0; j < wearableCount; ++j)
                        {
                            trackedSosig.wearables[i].Add(ReadString());
                        }
                    }
                }
                trackedSosig.IFFChart = SosigTargetPrioritySystemPatch.IntToBoolArr(ReadInt());
                trackedSosig.scene = ReadString();
                trackedSosig.instance = ReadInt();
                trackedSosig.sceneInit = ReadBool();
                int dataLen = ReadInt();
                if (dataLen > 0)
                {
                    trackedSosig.data = ReadBytes(dataLen);
                }
                trackedSosig.localWaitingIndex = ReadUInt();
                trackedSosig.initTracker = ReadInt();
                switch (trackedSosig.currentOrder) 
                {
                    case Sosig.SosigOrder.GuardPoint:
                        trackedSosig.guardPoint = ReadVector3();
                        trackedSosig.guardDir = ReadVector3();
                        trackedSosig.hardGuard = ReadBool();
                        break;
                    case Sosig.SosigOrder.Skirmish:
                        trackedSosig.skirmishPoint = ReadVector3();
                        trackedSosig.pathToPoint = ReadVector3();
                        trackedSosig.assaultPoint = ReadVector3();
                        trackedSosig.faceTowards = ReadVector3();
                        break;
                    case Sosig.SosigOrder.Investigate:
                        trackedSosig.guardPoint = ReadVector3();
                        trackedSosig.hardGuard = ReadBool();
                        trackedSosig.faceTowards = ReadVector3();
                        break;
                    case Sosig.SosigOrder.SearchForEquipment:
                    case Sosig.SosigOrder.Wander:
                        trackedSosig.wanderPoint = ReadVector3();
                        break;
                    case Sosig.SosigOrder.Assault:
                        trackedSosig.assaultPoint = ReadVector3();
                        trackedSosig.assaultSpeed = (Sosig.SosigMoveSpeed)ReadByte();
                        trackedSosig.faceTowards = ReadVector3();
                        break;
                    case Sosig.SosigOrder.Idle:
                        trackedSosig.idleToPoint = ReadVector3();
                        trackedSosig.idleDominantDir = ReadVector3();
                        break;
                    case Sosig.SosigOrder.PathTo:
                        trackedSosig.pathToPoint = ReadVector3();
                        trackedSosig.pathToLookDir = ReadVector3();
                        break;
                }
            }
            else
            {
                trackedSosig.order = ReadByte();
            }

            return trackedSosig;
        }

        /// <summary>Reads a SosigConfigTemplate from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public SosigConfigTemplate ReadSosigConfig(bool full = false, bool _moveReadPos = true)
        {
            SosigConfigTemplate config = ScriptableObject.CreateInstance<SosigConfigTemplate>();

            config.AppliesDamageResistToIntegrityLoss = ReadBool();
            config.DoesDropWeaponsOnBallistic = ReadBool();
            config.TotalMustard = ReadFloat();
            config.BleedDamageMult = ReadFloat();
            config.BleedRateMultiplier = ReadFloat();
            config.BleedVFXIntensity = ReadFloat();
            config.SearchExtentsModifier = ReadFloat();
            config.ShudderThreshold = ReadFloat();
            config.ConfusionThreshold = ReadFloat();
            config.ConfusionMultiplier = ReadFloat();
            config.ConfusionTimeMax = ReadFloat();
            config.StunThreshold = ReadFloat();
            config.StunMultiplier = ReadFloat();
            config.StunTimeMax = ReadFloat();
            config.HasABrain = ReadBool();
            config.RegistersPassiveThreats = ReadBool();
            config.CanBeKnockedOut = ReadBool();
            config.MaxUnconsciousTime = ReadFloat();
            config.AssaultPointOverridesSkirmishPointWhenFurtherThan = ReadFloat();
            config.ViewDistance = ReadFloat();
            config.HearingDistance = ReadFloat();
            config.MaxFOV = ReadFloat();
            config.StateSightRangeMults = ReadVector3();
            config.StateHearingRangeMults = ReadVector3();
            config.StateFOVMults = ReadVector3();
            config.CanPickup_Ranged = ReadBool();
            config.CanPickup_Melee = ReadBool();
            config.CanPickup_Other = ReadBool();
            config.DoesJointBreakKill_Head = ReadBool();
            config.DoesJointBreakKill_Upper = ReadBool();
            config.DoesJointBreakKill_Lower = ReadBool();
            config.DoesSeverKill_Head = ReadBool();
            config.DoesSeverKill_Upper = ReadBool();
            config.DoesSeverKill_Lower = ReadBool();
            config.DoesExplodeKill_Head = ReadBool();
            config.DoesExplodeKill_Upper = ReadBool();
            config.DoesExplodeKill_Lower = ReadBool();
            config.CrawlSpeed = ReadFloat();
            config.SneakSpeed = ReadFloat();
            config.WalkSpeed = ReadFloat();
            config.RunSpeed = ReadFloat();
            config.TurnSpeed = ReadFloat();
            config.MovementRotMagnitude = ReadFloat();
            config.DamMult_Projectile = ReadFloat();
            config.DamMult_Explosive = ReadFloat();
            config.DamMult_Melee = ReadFloat();
            config.DamMult_Piercing = ReadFloat();
            config.DamMult_Blunt = ReadFloat();
            config.DamMult_Cutting = ReadFloat();
            config.DamMult_Thermal = ReadFloat();
            config.DamMult_Chilling = ReadFloat();
            config.DamMult_EMP = ReadFloat();
            config.CanBeSurpressed = ReadBool();
            config.SuppressionMult = ReadFloat();
            config.CanBeGrabbed = ReadBool();
            config.CanBeSevered = ReadBool();
            config.CanBeStabbed = ReadBool();
            config.MaxJointLimit = ReadFloat();
            byte linkDamMultCount = ReadByte();
            if (linkDamMultCount > 0)
            {
                config.LinkDamageMultipliers = new List<float>();
                for (int i=0; i < linkDamMultCount; ++i)
                {
                    config.LinkDamageMultipliers.Add(ReadFloat());
                }
            }
            byte linkStaggerMultCount = ReadByte();
            if (linkStaggerMultCount > 0)
            {
                config.LinkStaggerMultipliers = new List<float>();
                for (int i = 0; i < linkStaggerMultCount; ++i)
                {
                    config.LinkStaggerMultipliers.Add(ReadFloat());
                }
            }
            byte startLinkIntegCount = ReadByte();
            if (startLinkIntegCount > 0)
            {
                config.StartingLinkIntegrity = new List<Vector2>();
                for (int i = 0; i < startLinkIntegCount; ++i)
                {
                    config.StartingLinkIntegrity.Add(ReadVector2());
                }
            }
            byte startBreakChanceCount = ReadByte();
            if (startBreakChanceCount > 0)
            {
                config.StartingChanceBrokenJoint = new List<float>();
                for (int i = 0; i < startBreakChanceCount; ++i)
                {
                    config.StartingChanceBrokenJoint.Add(ReadFloat());
                }
            }
            config.TargetCapacity = ReadInt();
            config.TargetTrackingTime = ReadFloat();
            config.NoFreshTargetTime = ReadFloat();

            return config;
        }

        /// <summary>Reads a H3MP_TrackedAutoMeaterData from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public H3MP_TrackedAutoMeaterData ReadTrackedAutoMeater(bool full = false, bool _moveReadPos = true)
        {
            H3MP_TrackedAutoMeaterData trackedAutoMeater = new H3MP_TrackedAutoMeaterData();

            trackedAutoMeater.trackedID = ReadInt();
            trackedAutoMeater.position = ReadVector3();
            trackedAutoMeater.rotation = ReadQuaternion();
            trackedAutoMeater.active = ReadBool();
            trackedAutoMeater.IFF = ReadByte();
            trackedAutoMeater.sideToSideRotation = ReadQuaternion();
            trackedAutoMeater.hingeTargetPos = ReadFloat();
            trackedAutoMeater.upDownMotorRotation = ReadQuaternion();
            trackedAutoMeater.upDownJointTargetPos = ReadFloat();

            if (full)
            {
                trackedAutoMeater.ID = ReadByte();
                trackedAutoMeater.controller = ReadInt();
                trackedAutoMeater.localTrackedID = ReadInt();
                trackedAutoMeater.scene = ReadString();
                trackedAutoMeater.instance = ReadInt();
                trackedAutoMeater.sceneInit = ReadBool();
                int dataLen = ReadInt();
                if (dataLen > 0)
                {
                    trackedAutoMeater.data = ReadBytes(dataLen);
                }
                trackedAutoMeater.localWaitingIndex = ReadUInt();
                trackedAutoMeater.initTracker = ReadInt();
            }
            else
            {
                trackedAutoMeater.order = ReadByte();
            }

            return trackedAutoMeater;
        }

        /// <summary>Reads a H3MP_TrackedEncryptionData from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public H3MP_TrackedEncryptionData ReadTrackedEncryption(bool full = false, bool _moveReadPos = true)
        {
            H3MP_TrackedEncryptionData trackedEncryption = new H3MP_TrackedEncryptionData();

            trackedEncryption.trackedID = ReadInt();
            trackedEncryption.position = ReadVector3();
            trackedEncryption.rotation = ReadQuaternion();
            trackedEncryption.active = ReadBool();

            if (full)
            {
                trackedEncryption.type = (TNH_EncryptionType)ReadByte();
                trackedEncryption.controller = ReadInt();
                trackedEncryption.localTrackedID = ReadInt();
                int length = ReadInt();
                if(length > 0)
                {
                    trackedEncryption.tendrilsActive = new bool[length];
                    for(int i=0; i < length; ++i)
                    {
                        trackedEncryption.tendrilsActive[i] = ReadBool();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.growthPoints = new Vector3[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.growthPoints[i] = ReadVector3();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.subTargsPos = new Vector3[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.subTargsPos[i] = ReadVector3();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.subTargsActive = new bool[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.subTargsActive[i] = ReadBool();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.tendrilFloats = new float[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.tendrilFloats[i] = ReadFloat();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.tendrilsRot = new Quaternion[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.tendrilsRot[i] = ReadQuaternion();
                    }
                }
                length = ReadInt();
                if (length > 0)
                {
                    trackedEncryption.tendrilsScale = new Vector3[length];
                    for (int i = 0; i < length; ++i)
                    {
                        trackedEncryption.tendrilsScale[i] = ReadVector3();
                    }
                }
                trackedEncryption.scene = ReadString();
                trackedEncryption.instance = ReadInt();
                trackedEncryption.sceneInit = ReadBool();
                trackedEncryption.localWaitingIndex = ReadUInt();
                trackedEncryption.initTracker = ReadInt();
            }
            else
            {
                trackedEncryption.order = ReadByte();
            }

            return trackedEncryption;
        }

        /// <summary>Reads a Damage from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public Damage ReadDamage(bool _moveReadPos = true)
        {
            Damage damage = new Damage();

            damage.point = ReadVector3();
            damage.Source_IFF = ReadInt();
            damage.Source_Point = ReadVector3();
            damage.Dam_Blunt = ReadFloat();
            damage.Dam_Piercing = ReadFloat();
            damage.Dam_Cutting = ReadFloat();
            damage.Dam_TotalKinetic = ReadFloat();
            damage.Dam_Thermal = ReadFloat();
            damage.Dam_Chilling = ReadFloat();
            damage.Dam_EMP = ReadFloat();
            damage.Dam_TotalEnergetic = ReadFloat();
            damage.Dam_Stunning = ReadFloat();
            damage.Dam_Blinding = ReadFloat();
            damage.hitNormal = ReadVector3();
            damage.strikeDir = ReadVector3();
            damage.edgeNormal = ReadVector3();
            damage.damageSize = ReadFloat();
            damage.Class = (Damage.DamageClass)ReadByte();

            return damage;
        }

        /// <summary>Reads a H3MP_TNHInstance from the packet.</summary>
        /// <param name="_moveReadPos">Whether or not to move the buffer's read position.</param>
        public H3MP_TNHInstance ReadTNHInstance(bool full = false, bool _moveReadPos = true)
        {
            int instanceID = ReadInt();
            bool letPeopleJoin = ReadBool();
            int progressionTypeSetting = ReadInt();
            int healthModeSetting = ReadInt();
            int equipmentModeSetting = ReadInt();
            int targetModeSetting = ReadInt();
            int AIDifficultyModifier = ReadInt();
            int radarModeModifier = ReadInt();
            int itemSpawnerMode = ReadInt();
            int backpackMode = ReadInt();
            int healthMult = ReadInt();
            int sosiggunShakeReloading = ReadInt();
            int TNHSeed = ReadInt();
            string levelID = ReadString();
            int playerCount = ReadInt();
            int hostID = ReadInt();
            H3MP_TNHInstance instance = new H3MP_TNHInstance(instanceID, hostID, letPeopleJoin,
                                                             progressionTypeSetting, healthModeSetting, equipmentModeSetting,
                                                             targetModeSetting, AIDifficultyModifier, radarModeModifier,
                                                             itemSpawnerMode, backpackMode, healthMult, sosiggunShakeReloading, TNHSeed, levelID);
            for (int i=1; i < playerCount; ++i)
            {
                int newPlayerID = ReadInt();

                if (instance.playerIDs.Contains(newPlayerID))
                {
                    Mod.LogWarning("ReadTNHInstance player ID: " + newPlayerID + " already in TNH instance " + instanceID + ":\n" + Environment.StackTrace);
                    continue;
                }

                instance.playerIDs.Add(ReadInt());
            }

            instance.initializer = ReadInt();
            instance.controller = ReadInt();

            if (full)
            {
                int currentlyPlayingCount = ReadInt();
                for(int i=0; i < currentlyPlayingCount; ++i)
                {
                    instance.currentlyPlaying.Add(ReadInt());
                }
                int playedCount = ReadInt();
                for(int i=0; i < playedCount; ++i)
                {
                    instance.played.Add(ReadInt());
                }
                int deadCount = ReadInt();
                for(int i=0; i < deadCount; ++i)
                {
                    instance.dead.Add(ReadInt());
                }
                instance.tokenCount = ReadInt();
                instance.holdOngoing = ReadBool();
                instance.curHoldIndex = ReadInt();
                instance.level = ReadInt();
                instance.phase = (TNH_Phase)ReadShort();
                int activeSupplyPointIndicesCount = ReadInt();
                if(activeSupplyPointIndicesCount > 0)
                {
                    instance.activeSupplyPointIndices = new List<int>();
                }
                for (int i = 0; i < activeSupplyPointIndicesCount; ++i)
                {
                    instance.activeSupplyPointIndices.Add(ReadInt());
                }
                int raisedBarriersCount = ReadInt();
                if (raisedBarriersCount > 0)
                {
                    instance.raisedBarriers = new List<int>();
                }
                for (int i = 0; i < raisedBarriersCount; ++i)
                {
                    instance.raisedBarriers.Add(ReadInt());
                }
                int raisedBarrierPrefabIndicesCount = ReadInt();
                if (raisedBarrierPrefabIndicesCount > 0)
                {
                    instance.raisedBarrierPrefabIndices = new List<int>();
                }
                for (int i = 0; i < raisedBarrierPrefabIndicesCount; ++i)
                {
                    instance.raisedBarrierPrefabIndices.Add(ReadInt());
                }
            }

            return instance;
        }
        #endregion

        private bool disposed = false;

        protected virtual void Dispose(bool _disposing)
        {
            if (!disposed)
            {
                if (_disposing)
                {
                    buffer = null;
                    readableBuffer = null;
                    readPos = 0;
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}