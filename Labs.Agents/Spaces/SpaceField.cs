using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class SpaceField<TAgent> : ISpaceField
    {
        public static readonly SpaceField<TAgent> Outside = new SpaceField<TAgent>(false);

        public ISpace Space { get; }
        public bool IsOutside { get; }
        public bool IsEmpty => IsOutside == false && isObstacle == false && IsDestroyed == false && anchors.Count == 0;
        public bool IsAgent => anchors.Count > 0;
        public int X { get; }
        public int Y { get; }
        List<AgentAnchor<TAgent>> anchors = new List<AgentAnchor<TAgent>>();
        bool isObstacle;
        internal bool IsDestroyed;
        readonly bool MultipleAnchorsAllowed;

        public SpaceField(ISpace space, int x, int y, bool multipleAnchorsAllowed)
        {
            Space = space;
            X = x;
            Y = y;
            MultipleAnchorsAllowed = multipleAnchorsAllowed;
        }

        private SpaceField(bool multipleAnchorsAllowed)
        {
            IsOutside = true;
            MultipleAnchorsAllowed = multipleAnchorsAllowed;
        }

        public bool IsObstacle 
        {
            get
            {
                return isObstacle;
            }
            internal set
            {
                if (IsAgent)
                {
                    throw new Exception();
                }
                else
                {
                    isObstacle = value;
                }
            }
        }

        internal void AddAnchor(AgentAnchor<TAgent> anchor)
        {
            if (isObstacle || (MultipleAnchorsAllowed == false && anchors.Count > 0))
            {
                throw new Exception();
            }
            else
            {
                anchors.Add(anchor);
            }
        }

        internal void RemoveAnchors()
        {
            anchors.Clear();
        }

        //internal AgentAnchor<TAgent> Anchor
        //{
        //    set
        //    {
        //        if (value == null)
        //        {
        //            anchor = value;
        //        }
        //        else
        //        {
        //            if (isObstacle || anchor != null)
        //            {
        //                throw new Exception();
        //            }
        //            else
        //            {
        //                anchor = value;
        //            }
        //        }
        //    }
        //}

        public IEnumerable<TAgent> Agents
        {
            get
            {
                return anchors.Select(anchor => anchor.Agent);
            }
        }
    }
}
