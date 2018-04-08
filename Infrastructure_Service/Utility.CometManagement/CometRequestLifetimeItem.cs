using System;

namespace JIT.Utility.CometManagement
{
    public class CometRequestLifetimeItem
    {
        public DateTime LifeTime { get; private set; }
        public string UserId { get; private set; }
        public CometResult CometResult { get; private set; }

        public CometRequestLifetimeItem(string userId, DateTime lifeTime, CometResult result)
        {
            UserId = userId;
            LifeTime = lifeTime;
            CometResult = result;
        }
    }
}
