namespace WWServer.Interpritators
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using WWServer.LocalModels;
    using WWServer.Packets.ClientPackets;

    public class DelayManager : IDisposable
    {

        Stopwatch timer = new Stopwatch();
        public DelayManager(int millisecond, IncomingData data)
        {

            timer.Start();
            var halfaSecond = 0;
            while (timer.IsRunning)
            {
                if (timer.ElapsedMilliseconds >= millisecond)
                {
                    data.CurrentData.Add(false);
                    //InternalDataHandlers.HandleDataPackets(data);
                    CombatSystemPackets.CollissionExpired(data.CurrentData.ElementAt(1).ToString());
                    timer.Stop();
                    Dispose();


                    return;
                }
                if (halfaSecond == 60)
                {
                    halfaSecond = 0;
                    data.CurrentData.Add(false);
                    InternalDataHandlers.HandleDataPackets(data);
                }
                halfaSecond++;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
