﻿using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using FistVR;
using System.Collections.Generic;

namespace H3MP
{
    internal class H3MP_ServerClient
    {
        public static int dataBufferSize = 4096;

        public int ID;
        public H3MP_Player player;
        public TCP tcp;
        public UDP udp;
        public bool connected;
        public long ping;

        public H3MP_ServerClient(int ID)
        {
            this.ID = ID;
            tcp = new TCP(ID);
            udp = new UDP(ID);
        }

        public class TCP
        {
            public TcpClient socket;

            public long openTime;

            private readonly int ID;
            private NetworkStream stream;
            private H3MP_Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int ID)
            {
                this.ID = ID;
            }

            public void Connect(TcpClient socket)
            {
                openTime = Convert.ToInt64((DateTime.Now.ToUniversalTime() - H3MP_ThreadManager.epoch).TotalMilliseconds);

                this.socket = socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new H3MP_Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                H3MP_ServerSend.Welcome(ID, "Welcome to the server", H3MP_GameManager.colorByIFF, H3MP_GameManager.nameplateMode, H3MP_GameManager.radarMode, H3MP_GameManager.radarColor);
            }

            public void SendData(H3MP_Packet packet)
            {
                try
                {
                    if(socket != null)
                    {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch(Exception ex)
                {
                    Mod.LogError($"Error sending data to player {ID} via TCP: {ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                int byteLength = 0;
                byte[] data = null;
                try
                {
                    byteLength = stream.EndRead(result);
                    if (byteLength == 0 && H3MP_Server.clients[ID].connected)
                    {
                        H3MP_Server.clients[ID].Disconnect(0);
                        return;
                    }

                    data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    receivedData.Reset(HandleData(data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception ex)
                {
                    if (H3MP_Server.clients[ID].connected)
                    {
                        if (data != null)
                        {
                            for (int i = 0; i < byteLength; ++i)
                            {
                                Mod.LogWarning("data[" + i + "] = " + data[i]);
                            }
                        }
                        H3MP_Server.clients[ID].Disconnect(1, ex);
                    }
                }
            }

            private bool HandleData(byte[] data)
            {
                int packetLength = 0;

                receivedData.SetBytes(data);

                if (receivedData.UnreadLength() >= 4)
                {
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
                {
                    byte[] packetBytes = receivedData.ReadBytes(packetLength);
                    H3MP_ThreadManager.ExecuteOnMainThread(() =>
                    {
                        if (H3MP_Server.tcpListener != null)
                        {
                            using (H3MP_Packet packet = new H3MP_Packet(packetBytes))
                            {
                                int packetID = packet.ReadInt();
                                H3MP_Server.packetHandlers[packetID](ID, packet);
                            }
                        }
                    });

                    packetLength = 0;

                    if (receivedData.UnreadLength() >= 4)
                    {
                        packetLength = receivedData.ReadInt();
                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (packetLength <= 1)
                {
                    return true;
                }

                return false;
            }

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                receiveBuffer = null;
                receivedData = null;
                socket = null;
            }
        }

        public class UDP
        {
            public IPEndPoint endPoint;

            private int ID;

            public UDP(int ID)
            {
                this.ID = ID;
            }

            public void Connect(IPEndPoint endPoint)
            {
                this.endPoint = endPoint;
            }

            public void SendData(H3MP_Packet packet)
            {
                H3MP_Server.SendUDPData(endPoint, packet);
            }

            public void HandleData(H3MP_Packet packetData)
            {
                int packetLength = packetData.ReadInt();
                byte[] packetBytes = packetData.ReadBytes(packetLength);

                H3MP_ThreadManager.ExecuteOnMainThread(() =>
                {
                    if (H3MP_Server.tcpListener != null)
                    {
                        using (H3MP_Packet packet = new H3MP_Packet(packetBytes))
                        {
                            int packetID = packet.ReadInt();
                            H3MP_Server.packetHandlers[packetID](ID, packet);
                        }
                    }
                });
            }

            public void Disconnect()
            {
                endPoint = null;
            }
        }

        public void SendIntoGame(string playerName, string scene, int instance, int IFF, int colorIndex)
        {
            player = new H3MP_Player(ID, playerName, Vector3.zero, IFF, colorIndex);
            player.scene = scene;
            player.instance = instance;

            // Spawn this client's player in all connected client but itself
            foreach(H3MP_ServerClient client in H3MP_Server.clients.Values)
            {
                if(client.player != null)
                {
                    if(client.ID != ID)
                    {
                        H3MP_ServerSend.SpawnPlayer(ID, client.player, scene, instance, IFF, colorIndex);
                    }
                }
            }

            // Also spawn player for host
            H3MP_GameManager.singleton.SpawnPlayer(player.ID, player.username, scene, instance, player.position, player.rotation, IFF, colorIndex);

            // Spawn all clients' players in this client
            bool inControl = true;
            foreach (H3MP_ServerClient client in H3MP_Server.clients.Values)
            {
                if(client.player != null && client.ID != ID)
                {
                    H3MP_ServerSend.SpawnPlayer(client.ID, player, client.player.scene, client.player.instance, IFF, colorIndex, true);
                    inControl &= !scene.Equals(client.player.scene);
                }
            }

            // Also spawn host player in this client
            H3MP_ServerSend.SpawnPlayer(ID, 0, Mod.config["Username"].ToString(), H3MP_GameManager.sceneLoading ? LoadLevelBeginPatch.loadingLevel : H3MP_GameManager.scene, H3MP_GameManager.instance, GM.CurrentPlayerBody.transform.position, GM.CurrentPlayerBody.transform.rotation, IFF, H3MP_GameManager.colorIndex, true);
            inControl &= !scene.Equals(H3MP_GameManager.sceneLoading ? LoadLevelBeginPatch.loadingLevel : H3MP_GameManager.scene);

            if (!H3MP_GameManager.nonSynchronizedScenes.ContainsKey(scene))
            {
                Mod.LogInfo("Player " + ID + " join server in scene " + scene);

                if (H3MP_GameManager.playersByInstanceByScene.TryGetValue(scene, out Dictionary<int, List<int>> instances) &&
                    instances.TryGetValue(instance, out List<int> otherPlayers))
                {
                    // There are other players in the client's scene/instance, request up to date objects before sending
                    for (int i = 0; i < otherPlayers.Count; ++i)
                    {
                        if (otherPlayers[i] != ID)
                        {
                            if (H3MP_Server.clientsWaitingUpDate.ContainsKey(otherPlayers[i]))
                            {
                                H3MP_Server.clientsWaitingUpDate[otherPlayers[i]].Add(ID);
                            }
                            else
                            {
                                H3MP_Server.clientsWaitingUpDate.Add(otherPlayers[i], new List<int> { ID });
                            }
                            H3MP_ServerSend.RequestUpToDateObjects(otherPlayers[i], false, ID);
                        }
                    }

                    // Send relevant trackedObjects specifically from us too
                    SendRelevantTrackedObjects(0);
                }
                else // No other players in the client's scene/instance 
                {
                    SendRelevantTrackedObjects();
                }

                // Tell the client to sync its items
                H3MP_ServerSend.ConnectSync(ID, inControl);
            }

            // Also send TNH instances
            H3MP_ServerSend.InitTNHInstances(ID);

            // Send custom connection data
            byte[] initData = null;
            SendInitConnectionData(ID, initData);
        }

        // MOD: This will get called when the server sends all the data a newly connected client connect needs
        //      A mod that wants to send its own initial data to process can prefix this to modify data before it gets sent
        private void SendInitConnectionData(int ID, byte[] data)
        {
            if (data != null)
            {
                H3MP_ServerSend.InitConnectionData(ID, data);
            }
        }

        public void SendRelevantTrackedObjects(int fromClient = -1)
        {
            // Send to the client all items that are already synced and controlled by clients in the same scene and instance
            for (int i = 0; i < H3MP_Server.items.Length; ++i)
            {
                // TODO: Optimization: In client handle for trackedItem we already check if this item is in our scene before instantiating
                //       Here we could then ommit this step, but that would mean sending a packet for every item in the game even the 
                //       the ones from other scenes, which will be useless to the client
                //       Need to check which one would be more efficient, more packets or checking scene twice
                //       Could also pass a bool telling the client not to check the scene because its already been checked?
                H3MP_TrackedItemData trackedItem = H3MP_Server.items[i];
                if (trackedItem != null)
                {
                    if(fromClient == -1)
                    {
                        if (trackedItem.controller == 0)
                        {
                            if (player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                            {
                                // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                                // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                                // nullable are still null, which is problematic
                                trackedItem.Update();
                                H3MP_ServerSend.TrackedItemSpecific(trackedItem, ID);
                            }
                        }
                        else if(trackedItem.controller != ID &&
                                player.scene.Equals(H3MP_Server.clients[trackedItem.controller].player.scene) &&
                                player.instance == H3MP_Server.clients[trackedItem.controller].player.instance)
                        {
                            H3MP_ServerSend.TrackedItemSpecific(trackedItem, ID);
                        }
                    }
                    else if(fromClient == 0)
                    {
                        if (trackedItem.controller == 0 && player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                        {
                            // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                            // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                            // nullable are still null, which is problematic
                            trackedItem.Update();
                            H3MP_ServerSend.TrackedItemSpecific(trackedItem, ID);
                        }
                    }
                    else if (trackedItem.controller == fromClient &&
                            player.scene.Equals(H3MP_Server.clients[fromClient].player.scene) &&
                            player.instance == H3MP_Server.clients[fromClient].player.instance)
                    {
                        H3MP_ServerSend.TrackedItemSpecific(trackedItem, ID);
                    }
                }
            }
            // Send to the client all sosigs that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.sosigs.Length; ++i)
            {
                H3MP_TrackedSosigData trackedSosig = H3MP_Server.sosigs[i];
                if (trackedSosig != null)
                {
                    if (fromClient == -1)
                    {
                        if (trackedSosig.controller == 0)
                        {
                            if (player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                            {
                                // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                                // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                                // nullable are still null, which is problematic
                                trackedSosig.Update();
                                H3MP_ServerSend.TrackedSosigSpecific(trackedSosig, ID);
                            }
                        }
                        else if (trackedSosig.controller != ID &&
                                player.scene.Equals(H3MP_Server.clients[trackedSosig.controller].player.scene) &&
                                player.instance == H3MP_Server.clients[trackedSosig.controller].player.instance)
                        {
                            H3MP_ServerSend.TrackedSosigSpecific(trackedSosig, ID);
                        }
                    }
                    else if (fromClient == 0)
                    {
                        if (trackedSosig.controller == 0 && player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                        {
                            // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                            // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                            // nullable are still null, which is problematic
                            trackedSosig.Update();
                            H3MP_ServerSend.TrackedSosigSpecific(trackedSosig, ID);
                        }
                    }
                    else if (trackedSosig.controller == fromClient &&
                            player.scene.Equals(H3MP_Server.clients[fromClient].player.scene) &&
                            player.instance == H3MP_Server.clients[fromClient].player.instance)
                    {
                        H3MP_ServerSend.TrackedSosigSpecific(trackedSosig, ID);
                    }
                }
            }
            // Send to the client all AutoMeaters that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.autoMeaters.Length; ++i)
            {
                H3MP_TrackedAutoMeaterData trackedAutoMeater = H3MP_Server.autoMeaters[i];
                if (trackedAutoMeater != null)
                {
                    if (fromClient == -1)
                    {
                        if (trackedAutoMeater.controller == 0)
                        {
                            if (player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                            {
                                // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                                // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                                // nullable are still null, which is problematic
                                trackedAutoMeater.Update();
                                H3MP_ServerSend.TrackedAutoMeaterSpecific(trackedAutoMeater, ID);
                            }
                        }
                        else if (trackedAutoMeater.controller != ID &&
                                player.scene.Equals(H3MP_Server.clients[trackedAutoMeater.controller].player.scene) &&
                                player.instance == H3MP_Server.clients[trackedAutoMeater.controller].player.instance)
                        {
                            H3MP_ServerSend.TrackedAutoMeaterSpecific(trackedAutoMeater, ID);
                        }
                    }
                    else if (fromClient == 0)
                    {
                        if (trackedAutoMeater.controller == 0 && player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                        {
                            // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                            // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                            // nullable are still null, which is problematic
                            trackedAutoMeater.Update();
                            H3MP_ServerSend.TrackedAutoMeaterSpecific(trackedAutoMeater, ID);
                        }
                    }
                    else if (trackedAutoMeater.controller == fromClient &&
                            player.scene.Equals(H3MP_Server.clients[fromClient].player.scene) &&
                            player.instance == H3MP_Server.clients[fromClient].player.instance)
                    {
                        H3MP_ServerSend.TrackedAutoMeaterSpecific(trackedAutoMeater, ID);
                    }
                }
            }
            // Send to the client all Encryptions that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.encryptions.Length; ++i)
            {
                H3MP_TrackedEncryptionData trackedEncryption = H3MP_Server.encryptions[i];
                if (trackedEncryption != null)
                {
                    if (fromClient == -1)
                    {
                        if (trackedEncryption.controller == 0)
                        {
                            if (player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                            {
                                // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                                // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                                // nullable are still null, which is problematic
                                trackedEncryption.Update();
                                H3MP_ServerSend.TrackedEncryptionSpecific(trackedEncryption, ID);
                            }
                        }
                        else if (trackedEncryption.controller != ID &&
                                player.scene.Equals(H3MP_Server.clients[trackedEncryption.controller].player.scene) &&
                                player.instance == H3MP_Server.clients[trackedEncryption.controller].player.instance)
                        {
                            H3MP_ServerSend.TrackedEncryptionSpecific(trackedEncryption, ID);
                        }
                    }
                    else if (fromClient == 0)
                    {
                        if (trackedEncryption.controller == 0 && player.scene.Equals(H3MP_GameManager.scene) && player.instance == H3MP_GameManager.instance)
                        {
                            // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                            // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                            // nullable are still null, which is problematic
                            trackedEncryption.Update();
                            H3MP_ServerSend.TrackedEncryptionSpecific(trackedEncryption, ID);
                        }
                    }
                    else if (trackedEncryption.controller == fromClient &&
                            player.scene.Equals(H3MP_Server.clients[fromClient].player.scene) &&
                            player.instance == H3MP_Server.clients[fromClient].player.instance)
                    {
                        H3MP_ServerSend.TrackedEncryptionSpecific(trackedEncryption, ID);
                    }
                }
            }
        }

        public void Disconnect(int code, Exception ex = null)
        {
            connected = false;

            switch (code)
            {
                case 0:
                    Mod.LogInfo("Client "+ID+" : " + tcp.socket.Client.RemoteEndPoint + " disconnected, end of stream.");
                    break;
                case 1:
                    Mod.LogInfo("Client "+ID+" : " + tcp.socket.Client.RemoteEndPoint + " forcibly disconnected.");
                    Mod.LogWarning("Exception: " + ex.Message + "\n" + ex);
                    break;
                case 2:
                    Mod.LogInfo("Client "+ID+" : " + tcp.socket.Client.RemoteEndPoint + " disconnected.");
                    break;
            }

            tcp.Disconnect();
            udp.Disconnect();

            Mod.RemovePlayerFromLists(ID);
            H3MP_GameManager.DistributeAllControl(ID);
            SpecificDisconnect();
            H3MP_ServerSend.ClientDisconnect(ID);

            player = null;
        }

        // MOD: This will be called after disconnection to reset specific fields
        //      For example, here we deal with current TNH data
        //      If your mod has some H3MP dependent data that you want to get rid of when you disconnect from a server, do it here
        private void SpecificDisconnect()
        {
            if (H3MP_GameManager.TNHInstances.TryGetValue(player.instance, out H3MP_TNHInstance TNHInstance) && TNHInstance.currentlyPlaying.Contains(ID)) // TNH_Manager was set to null and we are currently playing
            {
                TNHInstance.RemoveCurrentlyPlaying(true, ID, true);
            }
        }
    }
}
