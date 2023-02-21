﻿using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using FistVR;
using UnityEngine.SceneManagement;

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

                H3MP_ServerSend.Welcome(ID, "Welcome to the server");
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

        public void SendIntoGame(string playerName, string scene, int instance, int IFF)
        {
            player = new H3MP_Player(ID, playerName, Vector3.zero, IFF);
            player.scene = scene;
            player.instance = instance;

            // Spawn this client's player in all connected client but itself
            foreach(H3MP_ServerClient client in H3MP_Server.clients.Values)
            {
                if(client.player != null)
                {
                    if(client.ID != ID)
                    {
                        H3MP_ServerSend.SpawnPlayer(ID, client.player, scene, instance, IFF);
                    }
                }
            }

            // Also spawn player for host
            H3MP_GameManager.singleton.SpawnPlayer(player.ID, player.username, scene, instance, player.position, player.rotation, IFF);

            // Spawn all clients' players in this client
            bool inControl = true;
            foreach (H3MP_ServerClient client in H3MP_Server.clients.Values)
            {
                if(client.player != null && client.ID != ID)
                {
                    H3MP_ServerSend.SpawnPlayer(client.ID, player, client.player.scene, client.player.instance, IFF, true);
                    inControl &= !scene.Equals(client.player.scene);
                }
            }

            // Also spawn host player in this client
            H3MP_ServerSend.SpawnPlayer(ID, 0, Mod.config["Username"].ToString(), H3MP_GameManager.sceneLoading ? LoadLevelBeginPatch.loadingLevel : SceneManager.GetActiveScene().name, H3MP_GameManager.instance, GM.CurrentPlayerBody.transform.position, GM.CurrentPlayerBody.transform.rotation, IFF, true);
            inControl &= !scene.Equals(H3MP_GameManager.sceneLoading ? LoadLevelBeginPatch.loadingLevel : SceneManager.GetActiveScene().name);

            if (H3MP_GameManager.synchronizedScenes.ContainsKey(scene))
            {
                Mod.LogInfo("Player " + ID + " join server in scene " + scene);
                // Send to the clients all items that are already synced and controlled by clients in the same scene
                TODO: Review: SHouldn't we request up to date objects here instad of sending directly when they join? because server may not have the most up to date
                      So we might be sending out of date item data
                SendRelevantTrackedObjects();

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

        public void SendRelevantTrackedObjects()
        {
            // Send to the client all items that are already synced and controlled by clients in the same scene and instance
            for (int i = 0; i < H3MP_Server.items.Length; ++i)
            {
                // TODO: Optimization: In client handle for trackedItem we already check if this item is in our scene before instantiating
                //       Here we could then ommit this step, but that would mean sending a packet for every item in the game even the 
                //       the ones from other scenes, which will be useless to the client
                //       Need to check which one would be more efficient, more packets or checking scene twice
                //       Could also pass a bool telling the client not to check the scene because its already been checked?
                if (H3MP_Server.items[i] != null)
                {
                    if ((H3MP_Server.items[i].controller == 0 && player.scene.Equals(SceneManager.GetActiveScene().name) && player.instance == H3MP_GameManager.instance) ||
                        (H3MP_Server.items[i].controller != 0 && H3MP_Server.items[i].controller != ID && player.scene.Equals(H3MP_Server.clients[H3MP_Server.items[i].controller].player.scene) && player.instance == H3MP_Server.clients[H3MP_Server.items[i].controller].player.instance))
                    {
                        // Ensure it is up to date before sending because an item may not have been updated at all since there might not have
                        // been anyone in the scene/instance with the controller. Then when someone else joins the scene, we send relevent items but
                        // nullable are still null, which is problematic
                        if (H3MP_Server.items[i].controller == 0)
                        {
                            H3MP_Server.items[i].Update();
                        }
                        H3MP_ServerSend.TrackedItemSpecific(H3MP_Server.items[i], ID);
                    }
                }
            }
            // Send to the client all sosigs that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.sosigs.Length; ++i)
            {
                if (H3MP_Server.sosigs[i] != null)
                {
                    if ((H3MP_Server.sosigs[i].controller == 0 && player.scene.Equals(SceneManager.GetActiveScene().name) && player.instance == H3MP_GameManager.instance) ||
                        (H3MP_Server.sosigs[i].controller != 0 && H3MP_Server.sosigs[i].controller != ID && player.scene.Equals(H3MP_Server.clients[H3MP_Server.sosigs[i].controller].player.scene) && player.instance == H3MP_Server.clients[H3MP_Server.sosigs[i].controller].player.instance))
                    {
                        if (H3MP_Server.sosigs[i].controller == 0)
                        {
                            H3MP_Server.sosigs[i].Update();
                        }
                        H3MP_ServerSend.TrackedSosigSpecific(H3MP_Server.sosigs[i], ID);
                    }
                }
            }
            // Send to the client all AutoMeaters that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.autoMeaters.Length; ++i)
            {
                if (H3MP_Server.autoMeaters[i] != null)
                {
                    if ((H3MP_Server.autoMeaters[i].controller == 0 && player.scene.Equals(SceneManager.GetActiveScene().name) && player.instance == H3MP_GameManager.instance) ||
                        (H3MP_Server.autoMeaters[i].controller != 0 && H3MP_Server.autoMeaters[i].controller != ID && player.scene.Equals(H3MP_Server.clients[H3MP_Server.autoMeaters[i].controller].player.scene) && player.instance == H3MP_Server.clients[H3MP_Server.autoMeaters[i].controller].player.instance))
                    {
                        if (H3MP_Server.autoMeaters[i].controller == 0)
                        {
                            H3MP_Server.autoMeaters[i].Update();
                        }
                        H3MP_ServerSend.TrackedAutoMeaterSpecific(H3MP_Server.autoMeaters[i], ID);
                    }
                }
            }
            // Send to the client all Encryptions that are already synced and controlled by clients in the same scene
            for (int i = 0; i < H3MP_Server.encryptions.Length; ++i)
            {
                if (H3MP_Server.encryptions[i] != null)
                {
                    if ((H3MP_Server.encryptions[i].controller == 0 && player.scene.Equals(SceneManager.GetActiveScene().name) && player.instance == H3MP_GameManager.instance) ||
                        (H3MP_Server.encryptions[i].controller != 0 && H3MP_Server.encryptions[i].controller != ID && player.scene.Equals(H3MP_Server.clients[H3MP_Server.encryptions[i].controller].player.scene) && player.instance == H3MP_Server.clients[H3MP_Server.encryptions[i].controller].player.instance))
                    {
                        if (H3MP_Server.encryptions[i].controller == 0)
                        {
                            H3MP_Server.encryptions[i].Update();
                        }
                        H3MP_ServerSend.TrackedEncryptionSpecific(H3MP_Server.encryptions[i], ID);
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

            Mod.RemovePlayerFromLists(ID);
            SpecificDisconnect();
            H3MP_ServerSend.ClientDisconnect(ID);

            player = null;
            tcp.Disconnect();
            udp.Disconnect();
        }

        // MOD: This will be called after disconnection to reset specific fields
        //      For example, here we deal with current TNH data
        //      If your mod has some H3MP dependent data that you want to get rid of when you disconnect from a server, do it here
        private void SpecificDisconnect()
        {
            if (H3MP_GameManager.TNHInstances.TryGetValue(player.instance, out H3MP_TNHInstance TNHInstance) && TNHInstance.currentlyPlaying.Contains(ID)) // TNH_Manager was set to null and we are currently playing
            {
                TNHInstance.RemoveCurrentlyPlaying(true, ID, true);

                // If was manager controller, give manager control to next currently playing
                // NOW HANDLED BY SERVER
                //if (TNHInstance.controller == ID && TNHInstance.currentlyPlaying.Count > 0)
                //{
                //    TNHInstance.controller = TNHInstance.currentlyPlaying[0];
                //    if (H3MP_ThreadManager.host)
                //    {
                //        H3MP_ServerSend.SetTNHController(TNHInstance.instance, TNHInstance.currentlyPlaying[0]);
                //    }
                //    else
                //    {
                //        H3MP_ClientSend.SetTNHController(TNHInstance.instance, TNHInstance.currentlyPlaying[0]);
                //    }
                //}
            }
        }
    }
}
