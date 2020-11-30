using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public enum XfsSceneType
    {
        Process = 0,
        Manager = 1,
        Realm = 2,
        Gate = 3,
        Http = 4,
        Location = 5,
        Map = 6,

        // 客户端Model层
        Client = 30,
        Zone = 31,
        Login = 32, 
        Chat = 33,
        War = 34,

    }
}
