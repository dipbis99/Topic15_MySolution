﻿using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace MySolution.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask : Task
    {
        public DemoTask(Session session) : base(session) { }
        [Association("Contact-DemoTask")]
        public XPCollection<Contact> Contacts
        {
            get
            {
                return GetCollection<Contact>(nameof(Contacts));
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Priority = Priority.Normal;
        }

        private Priority priority;
        public Priority Priority
        {
            get { return priority; }
            set
            {
                SetPropertyValue(nameof(Priority), ref priority, value);
            }
        }

        [Action(ToolTip = "Postpone the task to the next day")]
        public void Postpone()
        {
            if (DueDate == DateTime.MinValue)
            {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }
    }

    public enum Priority
    {
        [ImageName("State_Priority_Low")]
        Low,
        [ImageName("State_Priority_Normal")]
        Normal,
        [ImageName("State_Priority_High")]
        High
    }
}