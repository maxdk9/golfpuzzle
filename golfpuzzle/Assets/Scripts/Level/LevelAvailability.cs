
    public class LevelAvailability
    {
        public static bool LevelPurchased(int levelNumber)
        {
            if (levelNumber <= 50)
            {
                return true;
            }

            if (EasyMobileManager.Instance.AdvancedUnlocked)
            {
                if (levelNumber <= 100)
                {
                    return true;
                }
            }
            return false;
        }
    }
