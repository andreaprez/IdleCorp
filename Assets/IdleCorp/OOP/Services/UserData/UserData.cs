namespace IdleCorp.OOP.Services.UserData
{
    public abstract class UserData : IUserData
    {
        public abstract IUserData SetDefaultValues();

        public void SaveData()
        {
            ServiceLocator.GetService<UserDataService>().SaveData(this);
        }
    }
}