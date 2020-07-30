
namespace WWServer.Interpritators
{
    using DbScaffold.Models;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using WWServer.LocalModels;
    using WWServer.Packets.ClientPackets;

    class DelayHandlerUnified : IDisposable
    {
        Stopwatch timer = new Stopwatch();
        public DelayHandlerUnified(int millisecond, IncomingData data)
        {

            timer.Start();
#pragma warning disable CS0219 // The variable 'halfaSecond' is assigned but its value is never used
            var halfaSecond = 0;
#pragma warning restore CS0219 // The variable 'halfaSecond' is assigned but its value is never used
            while (timer.IsRunning)
            {
                if (timer.ElapsedMilliseconds >= millisecond)
                {
                    data.CurrentData.Add(false);
                    //InternalDataHandlers.HandleDataPackets(data);
                    CharacterPackets.CharacerMounted((int)data.CurrentData.ElementAt(1), (Mounts)data.CurrentData.ElementAt(2), (int)data.CurrentData.ElementAt(3));
                    timer.Stop();
                    Dispose();


                    return;
                }

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
