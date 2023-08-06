using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Back : Card
    {
        // This can help prevent cheating on servers lol

        public Back(App app) : base(app)
        {
            Title = "Back";
            Description = "";
        }
    }
}