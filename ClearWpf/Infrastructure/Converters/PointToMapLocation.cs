﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClearWpf.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocation))]
    [ValueConversion(typeof(Point), typeof(Location))]
    public class PointToMapLocation : Converter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is Point point)) return null;
            return new Location(point.X, point.Y);
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is Location location)) return null;
            return new Point(location.Latitude, location.Longitude);
        }
    }
}
