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
        public bool IsEmpty => IsOutside == false && hasObstacle == false && IsDestroyed == false && anchors.Count == 0;
        public bool HasAgent => anchors.Count > 0;
        public int X { get; }
        public int Y { get; }
        List<AgentSpaceAnchor<TAgent>> anchors = new List<AgentSpaceAnchor<TAgent>>();
        bool hasObstacle;
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

        public bool HasObstacle 
        {
            get
            {
                return hasObstacle;
            }
            internal set
            {
                if (HasAgent)
                {
                    throw new Exception();
                }
                else
                {
                    hasObstacle = value;
                }
            }
        }

        internal void AddAnchor(AgentSpaceAnchor<TAgent> anchor)
        {
            if (hasObstacle || (MultipleAnchorsAllowed == false && anchors.Count > 0))
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

        internal bool RemoveAnchor(AgentSpaceAnchor<TAgent> anchor)
        {
            return anchors.Remove(anchor);
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
