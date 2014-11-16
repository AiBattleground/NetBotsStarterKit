using System;
using System.Collections.Generic;
using System.Linq;
using NetBotsClient.Ai;

namespace NetBotsClient.Host
{
    public static class BotRegistry
    {
        private static readonly Dictionary<string, IRobot> InstanceRepo = new Dictionary<string, IRobot>();
        private static readonly Dictionary<string, Type> TypeRepo = new Dictionary<string, Type>();


        public static IRobot GetBot(string botName, string botKey)
        {
            if (InstanceRepo.ContainsKey(botKey))
            {
                return InstanceRepo[botKey];
            }
            if (TypeRepo.ContainsKey(botName))
            {
                var myType = TypeRepo[botName.ToLower()];
                IRobot instance = (IRobot)Activator.CreateInstance(myType);
                InstanceRepo[botKey] = instance;
                return instance;
            }
            return null;
        }

        //    botName = botName.ToLower();
        //    if (BotRepo.ContainsKey(botName))
        //    {
        //        return BotRepo[botName];
        //    }
        //    else
        //    {
        //        var type = typeof (IRobot);
        //        var types = AppDomain.CurrentDomain.GetAssemblies()
        //                    .SelectMany(x => x.GetTypes())
        //                    .Where(type.IsAssignableFrom);
        //        var robot = types.FirstOrDefault(r => r.Name.ToLower() == botName);
                
        //        if (robot != null)
        //        {
        //            IRobot instance = (IRobot) Activator.CreateInstance(robot);
        //            BotRepo[botName] = instance;
        //            return instance;
        //        }
        //    }
        //    return null;
        //}

        public static void RegisterAllBots()
        {
            var type = typeof(IRobot);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(type.IsAssignableFrom);
            foreach (var t in types)
            {
                TypeRepo.Add(t.Name.ToLower(), t);
            }
        }
    }
}