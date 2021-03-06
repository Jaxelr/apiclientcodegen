﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public class RemoteLogger : IRemoteLogger
    {
        public List<IRemoteLogger> Loggers { get; } = new List<IRemoteLogger>();

        public List<string> DefaultTags { get; } = new List<string>();

        public RemoteLogger(params IRemoteLogger[] remoteLoggers)
        {
            Loggers.AddRange(
                new IRemoteLogger[]
                {
                    new ExceptionlessRemoteLogger()
                });

            if (remoteLoggers.Any())
            {
                Loggers.AddRange(remoteLoggers);
            }
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            foreach (var logger in Loggers)
                logger.TrackFeatureUsage(featureName, DefaultTags.Union(tags).ToArray());
        }

        public void TrackError(Exception exception)
        {
            foreach (var logger in Loggers)
                logger.TrackError(exception);
        }

        public void Disable() 
            => Loggers.ForEach(c=>c.Disable());
    }
}