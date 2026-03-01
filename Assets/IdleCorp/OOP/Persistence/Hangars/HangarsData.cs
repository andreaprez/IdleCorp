using System.Collections.Generic;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Hangars
{
    public class HangarsData : UserData
    {
        public int TotalRobotCount;
        public List<HangarData> Hangars;

        public HangarsData()
        {
            SetDefaultValues();
        }

        public sealed override IUserData SetDefaultValues()
        {
            Hangars = new List<HangarData>();
            return this;
        }

        public void SetTotalRobotCount(int robotCount)
        {
            TotalRobotCount = robotCount;
            SaveData();
        }

        public void AddHangar(HangarData hangarData)
        {
            Hangars.Add(hangarData);
            SaveData();
        }

        public void ModifyHangarId(int positionId, int hangarId)
        {
            var hangar = Hangars.Find(h => h.PositionId == positionId);
            hangar.SetId(hangarId);
            SaveData();
        }

        public void ModifyHangarRobotCount(int positionId, int robotCount)
        {
            var hangar = Hangars.Find(h => h.PositionId == positionId);
            hangar.SetCurrentRobotCount(robotCount);
            SaveData();
        }
    }
}