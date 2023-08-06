using System.Collections.Generic;

namespace InheritanceFiveCrowns
{
    public class Log : IModelComponent
    {
        protected LinkedList<string> List;

        internal int Size = 10000;

        internal void Add(string str)
        {
            List.AddLast(str);
            while (List.Count > Size) List.RemoveFirst();
        }

        public LogNode GetRecent() => new(List.Last);

        void IModelComponent.OnResetGame()
        {
            List = new LinkedList<string>();
        }

        void IModelComponent.OnResetRound()
        {
            // Do nothing
        }
    }
}