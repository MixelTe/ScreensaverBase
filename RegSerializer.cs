﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;

namespace ScreenSaverBase
{
	internal class RegSerializer
	{
		internal static void Save(string keyName, object settings)
		{
			var t = settings.GetType();
			foreach (var fi in t.GetFields())
			{
				var v = fi.GetValue(settings);
				var n = fi.Name;
				//var s = string.Format(CultureInfo.InvariantCulture, "{0}", v);
				string s;
				var ft = fi.FieldType;
				if (ft == typeof(Color))
				{
					var c = (Color)v;
					s = FormattableString.Invariant($"{c.A},{c.R},{c.G},{c.B}");
				}
				else if (v is IFormattable f)
				{
					s = f.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					s = v.ToString();
				}

				Registry.SetValue(keyName, n, s);
			}
		}
		internal static void Load(string keyName, object settings)
		{
			var t = settings.GetType();
			foreach (var fi in t.GetFields())
			{
				var n = fi.Name;
				var s = (string)Registry.GetValue(keyName, n, "");
				if (!string.IsNullOrWhiteSpace(s))
				{
					var ft = fi.FieldType;
					object v;
					if (ft == typeof(int))
					{
						v = int.Parse(s, CultureInfo.InvariantCulture);
					}
					else if (ft == typeof(float))
					{
						v = float.Parse(s, CultureInfo.InvariantCulture);
					}
					else if (ft == typeof(double))
					{
						v = double.Parse(s, CultureInfo.InvariantCulture);
					}
					else if (ft == typeof(bool))
					{
						v = bool.Parse(s);
					}
					else if (ft == typeof(string))
					{
						v = s;
					}
					else if (ft == typeof(Color))
					{
						var parts = s.Split(',');
						if (parts.Length != 4) throw new Exception($"type of field is not supported! Type: {n}");
						var a = int.Parse(parts[0], CultureInfo.InvariantCulture);
						var r = int.Parse(parts[1], CultureInfo.InvariantCulture);
						var g = int.Parse(parts[2], CultureInfo.InvariantCulture);
						var b = int.Parse(parts[3], CultureInfo.InvariantCulture);
						v = Color.FromArgb(a, r, g, b);
					}
					else
					{
						throw new Exception($"type of field is not supported! Type: {n}");
					}

					fi.SetValue(settings, v);
				}
			}
		}
	}
}