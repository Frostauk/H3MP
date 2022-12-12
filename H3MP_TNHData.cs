﻿using FistVR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3MP
{
    public class H3MP_TNHData
    {
        public int levelIndex;
        public TNH_Phase phase;
        public int curHoldIndex;
        public int lastHoldIndex;
        public int sequenceIndex;
        public int charIndex;
        public int progressionIndex;
        public int progressionEndlessIndex;
        public TNH_Manager.SosigPatrolSquad[] patrols;
        public int[] activeSupplyIndices;
        public int[] activeHoldSosigIDs;
        public int[] activeHoldTurretIDs;
        public int[][] supplyPointsSosigIDs;
        public int[][] supplyPointsTurretIDs;
    }
}
