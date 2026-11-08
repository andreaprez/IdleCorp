using System;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Player
{
    [Serializable]
    public class PlayerData : UserData
    {
        public long LastSessionTimeTicks;

        public PlayerData()
        {
            SetDefaultValues();
        }

        public sealed override IUserData SetDefaultValues()
        {
            LastSessionTimeTicks = DateTime.UtcNow.Ticks;
            return this;
        }

        public void SetLastSessionTime(long ticks)
        {
            LastSessionTimeTicks = ticks;
            SaveData();
        }
    }
}