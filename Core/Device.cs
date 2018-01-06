using System;
using dotNSGDX.Utility;

namespace NTP.Core
{
    public abstract class Device : IObject
    {
        public struct DeviceInfo
        {
            public int id;
            public string dev, key;
            public float x, y, r;
            public string input;
            public string output;
        }

        public DeviceInfo info;

        public Device()
        {
            info = new DeviceInfo()
            {
                id = -1,
                dev = "null", key = "null",
                x = 0, y = 0, r = 0,
                input = "null", output = "null"
            };
        }

        public abstract Result OnUpdate(int t);
        public abstract Result OnRender(IRenderer renderer);
    }
}
