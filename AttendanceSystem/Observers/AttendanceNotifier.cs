using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class AttendanceNotifier : ISubject
    {
        private List<IObserver> observers;

        public AttendanceNotifier()
        {
            observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string message)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].Update(message);
            }
        }
    }
}
