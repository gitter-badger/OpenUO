﻿using System;

namespace OpenUO.Core.PresentationFramework.ComponentModel.Design
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RaisePropertyChangedAttribute : Attribute
    {
        public RaisePropertyChangedAttribute(params string[] properties)
        {
            Properties = properties;
        }

        public string[] Properties
        {
            get;
            private set;
        }
    }
}