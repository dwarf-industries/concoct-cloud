using System;
using System.Diagnostics;
using WWServer.LocalModels;
using WWServer.Packets.ClientPackets;

namespace WWServer.Interpritators
{
    class WeatherManager : IDisposable
    {

        Stopwatch timer = new Stopwatch();
        public WeatherManager(int millisecond, IncomingData data)
        {

            timer.Start();
            while (timer.IsRunning)
            {
                if (timer.ElapsedMilliseconds >= millisecond)
                {
                    timer.Stop();
                    Dispose();
                    WeatherPackets.UpdateWeatherSystem();
                }


            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
