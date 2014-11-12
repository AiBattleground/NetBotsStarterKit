using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using NetBotsClient.Ai;

namespace NetBotsClient.Host.Models
{
    public sealed class BotRepository
    {
        private static readonly Lazy<BotRepository> lazy = new Lazy<BotRepository>(() => new BotRepository());

        public static BotRepository Instance { get { return lazy.Value;  } }

        private BotRepository() { }

        private readonly Dictionary<string, IRobot> _botRepo = new Dictionary<string, IRobot>(); 

        public IRobot GetBot(string botName)
        {
            botName = botName.ToLower();
            if (_botRepo.ContainsKey(botName))
            {
                return _botRepo[botName];
            }
            else
            {
                var type = typeof (IRobot);
                var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(x => x.GetTypes())
                            .Where(type.IsAssignableFrom);
                var robot = types.FirstOrDefault(r => r.Name.ToLower() == botName);
                
                if (robot != null)
                {
                    IRobot instance = (IRobot) Activator.CreateInstance(robot);
                    _botRepo[botName] = instance;
                    return instance;
                }
            }
            return null;
        } 
    }
}