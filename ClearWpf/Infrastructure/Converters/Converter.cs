﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClearWpf.Infrastructure.Converters
{
    public abstract class Converter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;

        public abstract object Convert(object v, Type t, object p, CultureInfo c);

        public virtual object ConvertBack(object v, Type t, object p, CultureInfo c) =>
            throw new NotSupportedException("Обратное преобразование не поддерживается");
    }
}
