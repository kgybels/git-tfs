﻿using System.Collections.Generic;
using System.ComponentModel;
using NDesk.Options;
using Sep.Git.Tfs.Util;
using System;

namespace Sep.Git.Tfs.Commands
{
    [StructureMapSingleton]
    public class CheckinOptions
    {
        public OptionSet OptionSet
        {
            get
            {
                return new OptionSet
                {
                    { "m|comment=", "A comment for the changeset",
                        v => CheckinComment = v },
                    { "no-build-default-comment", "Do not concatenate commit comments for the changeset comment.",
                        v => NoGenerateCheckinComment = v != null },
                    { "no-merge", "Omits setting commit being checked in as parent, thus allowing to rebase remaining onto TFS changeset without exceeding merge commits.",
                        v => NoMerge = v != null },
                    { "f|force=", "The policy override reason.",
                        v => { Force = true; OverrideReason = v; } },
                    { "w|work-item=:", "Associated work items\ne.g. -w12345 to associate with 12345\nor -w12345:resolve to resolve 12345",
                        (n, opt) => { if(n == null) throw new OptionException("Missing work item number for option -w.", "-w");
                            (opt == "resolve" ? WorkItemsToResolve : WorkItemsToAssociate).Add(n); } },
                    { "no-gate", "Disables gated checkin.",
                        v => { OverrideGatedCheckIn = true; } },
                };
            }
        }

        private List<string> _workItemsToAssociate = new List<string>();
        private List<string> _workItemsToResolve = new List<string>();
        private Dictionary<string, string> _checkinNotes = new Dictionary<string,string>();

        public string CheckinComment { get; set; }
        // This can be extended to checkin when the $EDITOR is invoked.
        public bool NoGenerateCheckinComment { get; set; }
        public bool NoMerge { get; set; }
        public string OverrideReason { get; set; }
        public bool Force { get; set; }
        public bool OverrideGatedCheckIn { get; set; }
        public List<string> WorkItemsToAssociate { get { return _workItemsToAssociate; } }
        public List<string> WorkItemsToResolve { get { return _workItemsToResolve; } }
        public Dictionary<string, string> CheckinNotes { get { return _checkinNotes; } }
    }
}
