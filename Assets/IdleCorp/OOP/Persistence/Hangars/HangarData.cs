using System;

namespace IdleCorp.OOP.Persistence.Hangars
{
    [Serializable]
    public class HangarData
    {
        public int Id;
        public int PositionId;
        public int CurrentRobotCount;

        public HangarData()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Id = -1;
            PositionId = -1;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetPositionId(int positionId)
        {
            PositionId = positionId;
        }
        
        public void SetCurrentRobotCount(int robotCount)
        {
            CurrentRobotCount = robotCount;
        }
    }
}