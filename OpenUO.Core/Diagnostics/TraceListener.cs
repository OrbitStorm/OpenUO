﻿#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   TraceListener.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

namespace OpenUO.Core.Diagnostics
{
    public abstract class TraceListener : ITraceListener
    {
        protected TraceListener()
        {
            Enabled = true;
            Tracer.TraceReceived += OnTraceReceived;
        }

        public virtual TraceLevels? TraceLevel
        {
            get;
            set;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public bool Enabled
        {
            get;
            set;
        }

        private void OnTraceReceived(TraceMessageEventArgs e)
        {
            if(!Enabled)
            {
                return;
            }

            var message = e.TraceMessage;

            if(!TraceLevel.HasValue && message.Type < Tracer.TraceLevel)
            {
                return;
            }

            if(TraceLevel.HasValue && message.Type < TraceLevel)
            {
                return;
            }

            OnTraceReceived(message);
        }

        protected abstract void OnTraceReceived(TraceMessage message);

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                Tracer.TraceReceived -= OnTraceReceived;
            }
        }
    }
}