

namespace RegistroPersonas.Utilities
{
    public static class ConnectionDB
    {
        public static string ReturnPathDb(string nameDB)
        {
            string pathDb = string.Empty;
            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                pathDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                pathDb = Path.Combine(pathDb, nameDB);
            }else if(DeviceInfo.Platform == DevicePlatform.iOS)
            {
                pathDb = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                pathDb = Path.Combine(pathDb, "..","Library", nameDB);
            }
            return pathDb;  
        }
    }
}
