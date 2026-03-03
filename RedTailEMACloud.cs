#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This code is subject to the terms of the Mozilla Public License 2.0 at https://mozilla.org/MPL/2.0/
//Converted from TradingView Pine Script - HawkEye EMA Cloud
//NinjaTrader version: RedTail EMA Cloud

namespace NinjaTrader.NinjaScript.Indicators
{
	public class RedTailEMACloud : Indicator
	{
		private EMA ema1Short;
		private EMA ema1Long;
		private EMA ema2Short;
		private EMA ema2Long;
		private EMA ema3Short;
		private EMA ema3Long;
		private EMA ema4Short;
		private EMA ema4Long;
		private EMA ema5Short;
		private EMA ema5Long;

		private SMA sma1Short;
		private SMA sma1Long;
		private SMA sma2Short;
		private SMA sma2Long;
		private SMA sma3Short;
		private SMA sma3Long;
		private SMA sma4Short;
		private SMA sma4Long;
		private SMA sma5Short;
		private SMA sma5Long;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"RedTail EMA Cloud - Multiple EMA clouds for trend identification";
				Name										= "RedTail EMA Cloud";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				IsSuspendedWhileInactive					= true;

				// MA Type
				MAType										= MATypeOption.EMA;

				// EMA Lengths
				ShortEMA1Length								= 8;
				LongEMA1Length								= 9;
				ShortEMA2Length								= 5;
				LongEMA2Length								= 12;
				ShortEMA3Length								= 34;
				LongEMA3Length								= 50;
				ShortEMA4Length								= 72;
				LongEMA4Length								= 89;
				ShortEMA5Length								= 180;
				LongEMA5Length								= 200;

				// Display Options
				ShowLongAlerts								= false;
				ShowShortAlerts								= false;
				ShowLine									= false;
				ShowEMACloud1								= true;
				ShowEMACloud2								= true;
				ShowEMACloud3								= true;
				ShowEMACloud4								= false;
				ShowEMACloud5								= false;
				EMACloudLeading								= 0;

				// Cloud 1 Colors
				Cloud1BullColor								= Brushes.DarkGreen;
				Cloud1BearColor								= Brushes.DarkMagenta;
				Cloud1Opacity								= 45;

				// Cloud 2 Colors
				Cloud2BullColor								= Brushes.LimeGreen;
				Cloud2BearColor								= Brushes.Red;
				Cloud2Opacity								= 65;

				// Cloud 3 Colors
				Cloud3BullColor								= Brushes.DodgerBlue;
				Cloud3BearColor								= Brushes.Orange;
				Cloud3Opacity								= 70;

				// Cloud 4 Colors
				Cloud4BullColor								= Brushes.Teal;
				Cloud4BearColor								= Brushes.HotPink;
				Cloud4Opacity								= 65;

				// Cloud 5 Colors
				Cloud5BullColor								= Brushes.Cyan;
				Cloud5BearColor								= Brushes.OrangeRed;
				Cloud5Opacity								= 65;

				// EMA Line Colors
				ShortEMARisingColor							= Brushes.Olive;
				ShortEMAFallingColor						= Brushes.Maroon;
				LongEMARisingColor							= Brushes.Green;
				LongEMAFallingColor							= Brushes.Red;
			}
			else if (State == State.Configure)
			{
			}
			else if (State == State.DataLoaded)
			{
				// Initialize EMAs or SMAs based on selection
				if (MAType == MATypeOption.EMA)
				{
					ema1Short = EMA(ShortEMA1Length);
					ema1Long = EMA(LongEMA1Length);
					ema2Short = EMA(ShortEMA2Length);
					ema2Long = EMA(LongEMA2Length);
					ema3Short = EMA(ShortEMA3Length);
					ema3Long = EMA(LongEMA3Length);
					ema4Short = EMA(ShortEMA4Length);
					ema4Long = EMA(LongEMA4Length);
					ema5Short = EMA(ShortEMA5Length);
					ema5Long = EMA(LongEMA5Length);
				}
				else
				{
					sma1Short = SMA(ShortEMA1Length);
					sma1Long = SMA(LongEMA1Length);
					sma2Short = SMA(ShortEMA2Length);
					sma2Long = SMA(LongEMA2Length);
					sma3Short = SMA(ShortEMA3Length);
					sma3Long = SMA(LongEMA3Length);
					sma4Short = SMA(ShortEMA4Length);
					sma4Long = SMA(LongEMA4Length);
					sma5Short = SMA(ShortEMA5Length);
					sma5Long = SMA(LongEMA5Length);
				}
			}
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar < Math.Max(Math.Max(Math.Max(LongEMA1Length, LongEMA2Length), Math.Max(LongEMA3Length, LongEMA4Length)), LongEMA5Length))
				return;

			// Get MA values based on type
			double ma1Short = MAType == MATypeOption.EMA ? ema1Short[0] : sma1Short[0];
			double ma1Long = MAType == MATypeOption.EMA ? ema1Long[0] : sma1Long[0];
			double ma2Short = MAType == MATypeOption.EMA ? ema2Short[0] : sma2Short[0];
			double ma2Long = MAType == MATypeOption.EMA ? ema2Long[0] : sma2Long[0];
			double ma3Short = MAType == MATypeOption.EMA ? ema3Short[0] : sma3Short[0];
			double ma3Long = MAType == MATypeOption.EMA ? ema3Long[0] : sma3Long[0];
			double ma4Short = MAType == MATypeOption.EMA ? ema4Short[0] : sma4Short[0];
			double ma4Long = MAType == MATypeOption.EMA ? ema4Long[0] : sma4Long[0];
			double ma5Short = MAType == MATypeOption.EMA ? ema5Short[0] : sma5Short[0];
			double ma5Long = MAType == MATypeOption.EMA ? ema5Long[0] : sma5Long[0];

			// Draw EMA Cloud 1
			if (ShowEMACloud1)
			{
				Brush cloud1Color = ma1Short >= ma1Long ? GetBrushWithOpacity(Cloud1BullColor, Cloud1Opacity) : GetBrushWithOpacity(Cloud1BearColor, Cloud1Opacity);
				Draw.Region(this, "Cloud1_" + CurrentBar, CurrentBar - EMACloudLeading, 0, ema1Short, ema1Long, null, cloud1Color, cloud1Color, 50);
				
				if (ShowLine && CurrentBar > 0)
				{
					Brush shortColor1 = ma1Short >= (MAType == MATypeOption.EMA ? ema1Short[1] : sma1Short[1]) ? ShortEMARisingColor : ShortEMAFallingColor;
					Brush longColor1 = ma1Long >= (MAType == MATypeOption.EMA ? ema1Long[1] : sma1Long[1]) ? LongEMARisingColor : LongEMAFallingColor;
					Draw.Line(this, "Short1_" + CurrentBar, false, 1, ma1Short, 0, ma1Short, shortColor1, DashStyleHelper.Solid, 1);
					Draw.Line(this, "Long1_" + CurrentBar, false, 1, ma1Long, 0, ma1Long, longColor1, DashStyleHelper.Solid, 3);
				}
			}

			// Draw EMA Cloud 2
			if (ShowEMACloud2)
			{
				Brush cloud2Color = ma2Short >= ma2Long ? GetBrushWithOpacity(Cloud2BullColor, Cloud2Opacity) : GetBrushWithOpacity(Cloud2BearColor, Cloud2Opacity);
				Draw.Region(this, "Cloud2_" + CurrentBar, CurrentBar - EMACloudLeading, 0, ema2Short, ema2Long, null, cloud2Color, cloud2Color, 50);
				
				if (ShowLine && CurrentBar > 0)
				{
					Brush shortColor2 = ma2Short >= (MAType == MATypeOption.EMA ? ema2Short[1] : sma2Short[1]) ? ShortEMARisingColor : ShortEMAFallingColor;
					Brush longColor2 = ma2Long >= (MAType == MATypeOption.EMA ? ema2Long[1] : sma2Long[1]) ? LongEMARisingColor : LongEMAFallingColor;
					Draw.Line(this, "Short2_" + CurrentBar, false, 1, ma2Short, 0, ma2Short, shortColor2, DashStyleHelper.Solid, 1);
					Draw.Line(this, "Long2_" + CurrentBar, false, 1, ma2Long, 0, ma2Long, longColor2, DashStyleHelper.Solid, 3);
				}
			}

			// Draw EMA Cloud 3
			if (ShowEMACloud3)
			{
				Brush cloud3Color = ma3Short >= ma3Long ? GetBrushWithOpacity(Cloud3BullColor, Cloud3Opacity) : GetBrushWithOpacity(Cloud3BearColor, Cloud3Opacity);
				Draw.Region(this, "Cloud3_" + CurrentBar, CurrentBar - EMACloudLeading, 0, ema3Short, ema3Long, null, cloud3Color, cloud3Color, 50);
				
				if (ShowLine && CurrentBar > 0)
				{
					Brush shortColor3 = ma3Short >= (MAType == MATypeOption.EMA ? ema3Short[1] : sma3Short[1]) ? ShortEMARisingColor : ShortEMAFallingColor;
					Brush longColor3 = ma3Long >= (MAType == MATypeOption.EMA ? ema3Long[1] : sma3Long[1]) ? LongEMARisingColor : LongEMAFallingColor;
					Draw.Line(this, "Short3_" + CurrentBar, false, 1, ma3Short, 0, ma3Short, shortColor3, DashStyleHelper.Solid, 1);
					Draw.Line(this, "Long3_" + CurrentBar, false, 1, ma3Long, 0, ma3Long, longColor3, DashStyleHelper.Solid, 3);
				}
			}

			// Draw EMA Cloud 4
			if (ShowEMACloud4)
			{
				Brush cloud4Color = ma4Short >= ma4Long ? GetBrushWithOpacity(Cloud4BullColor, Cloud4Opacity) : GetBrushWithOpacity(Cloud4BearColor, Cloud4Opacity);
				Draw.Region(this, "Cloud4_" + CurrentBar, CurrentBar - EMACloudLeading, 0, ema4Short, ema4Long, null, cloud4Color, cloud4Color, 50);
				
				if (ShowLine && CurrentBar > 0)
				{
					Brush shortColor4 = ma4Short >= (MAType == MATypeOption.EMA ? ema4Short[1] : sma4Short[1]) ? ShortEMARisingColor : ShortEMAFallingColor;
					Brush longColor4 = ma4Long >= (MAType == MATypeOption.EMA ? ema4Long[1] : sma4Long[1]) ? LongEMARisingColor : LongEMAFallingColor;
					Draw.Line(this, "Short4_" + CurrentBar, false, 1, ma4Short, 0, ma4Short, shortColor4, DashStyleHelper.Solid, 1);
					Draw.Line(this, "Long4_" + CurrentBar, false, 1, ma4Long, 0, ma4Long, longColor4, DashStyleHelper.Solid, 3);
				}
			}

			// Draw EMA Cloud 5
			if (ShowEMACloud5)
			{
				Brush cloud5Color = ma5Short >= ma5Long ? GetBrushWithOpacity(Cloud5BullColor, Cloud5Opacity) : GetBrushWithOpacity(Cloud5BearColor, Cloud5Opacity);
				Draw.Region(this, "Cloud5_" + CurrentBar, CurrentBar - EMACloudLeading, 0, ema5Short, ema5Long, null, cloud5Color, cloud5Color, 50);
				
				if (ShowLine && CurrentBar > 0)
				{
					Brush shortColor5 = ma5Short >= (MAType == MATypeOption.EMA ? ema5Short[1] : sma5Short[1]) ? ShortEMARisingColor : ShortEMAFallingColor;
					Brush longColor5 = ma5Long >= (MAType == MATypeOption.EMA ? ema5Long[1] : sma5Long[1]) ? LongEMARisingColor : LongEMAFallingColor;
					Draw.Line(this, "Short5_" + CurrentBar, false, 1, ma5Short, 0, ma5Short, shortColor5, DashStyleHelper.Solid, 1);
					Draw.Line(this, "Long5_" + CurrentBar, false, 1, ma5Long, 0, ma5Long, longColor5, DashStyleHelper.Solid, 3);
				}
			}
		}

		private Brush GetBrushWithOpacity(Brush brush, int opacityPercent)
		{
			if (brush == null)
				return Brushes.Transparent;

			SolidColorBrush solidBrush = brush as SolidColorBrush;
			if (solidBrush != null)
			{
				byte alpha = (byte)(255 * (100 - opacityPercent) / 100.0);
				Color color = Color.FromArgb(alpha, solidBrush.Color.R, solidBrush.Color.G, solidBrush.Color.B);
				Brush newBrush = new SolidColorBrush(color);
				newBrush.Freeze();
				return newBrush;
			}

			return brush;
		}

		#region Properties

		[NinjaScriptProperty]
		[Display(Name="MA Type", Description="Moving Average Type", Order=1, GroupName="Parameters")]
		public MATypeOption MAType
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Short EMA1 Length", Description="Short EMA1 Length", Order=2, GroupName="Parameters")]
		public int ShortEMA1Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Long EMA1 Length", Description="Long EMA1 Length", Order=3, GroupName="Parameters")]
		public int LongEMA1Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Short EMA2 Length", Description="Short EMA2 Length", Order=4, GroupName="Parameters")]
		public int ShortEMA2Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Long EMA2 Length", Description="Long EMA2 Length", Order=5, GroupName="Parameters")]
		public int LongEMA2Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Short EMA3 Length", Description="Short EMA3 Length", Order=6, GroupName="Parameters")]
		public int ShortEMA3Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Long EMA3 Length", Description="Long EMA3 Length", Order=7, GroupName="Parameters")]
		public int LongEMA3Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Short EMA4 Length", Description="Short EMA4 Length", Order=8, GroupName="Parameters")]
		public int ShortEMA4Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Long EMA4 Length", Description="Long EMA4 Length", Order=9, GroupName="Parameters")]
		public int LongEMA4Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Short EMA5 Length", Description="Short EMA5 Length", Order=10, GroupName="Parameters")]
		public int ShortEMA5Length
		{ get; set; }

		[Range(1, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Long EMA5 Length", Description="Long EMA5 Length", Order=11, GroupName="Parameters")]
		public int LongEMA5Length
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show Long Alerts", Description="Show Long Alerts", Order=1, GroupName="Display Options")]
		public bool ShowLongAlerts
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show Short Alerts", Description="Show Short Alerts", Order=2, GroupName="Display Options")]
		public bool ShowShortAlerts
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Display EMA Line", Description="Display EMA Line", Order=3, GroupName="Display Options")]
		public bool ShowLine
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show EMA Cloud-1", Description="Show EMA Cloud-1", Order=4, GroupName="Display Options")]
		public bool ShowEMACloud1
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show EMA Cloud-2", Description="Show EMA Cloud-2", Order=5, GroupName="Display Options")]
		public bool ShowEMACloud2
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show EMA Cloud-3", Description="Show EMA Cloud-3", Order=6, GroupName="Display Options")]
		public bool ShowEMACloud3
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show EMA Cloud-4", Description="Show EMA Cloud-4", Order=7, GroupName="Display Options")]
		public bool ShowEMACloud4
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="Show EMA Cloud-5", Description="Show EMA Cloud-5", Order=8, GroupName="Display Options")]
		public bool ShowEMACloud5
		{ get; set; }

		[Range(0, int.MaxValue)]
		[NinjaScriptProperty]
		[Display(Name="Leading Period For EMA Cloud", Description="Leading Period For EMA Cloud", Order=9, GroupName="Display Options")]
		public int EMACloudLeading
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Cloud 1 - Bullish Color", Description="Cloud 1 Bullish Color", Order=1, GroupName="Cloud 1 Colors")]
		public Brush Cloud1BullColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud1BullColorSerializable
		{
			get { return Serialize.BrushToString(Cloud1BullColor); }
			set { Cloud1BullColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Cloud 1 - Bearish Color", Description="Cloud 1 Bearish Color", Order=2, GroupName="Cloud 1 Colors")]
		public Brush Cloud1BearColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud1BearColorSerializable
		{
			get { return Serialize.BrushToString(Cloud1BearColor); }
			set { Cloud1BearColor = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[NinjaScriptProperty]
		[Display(Name="Cloud 1 - Opacity", Description="Cloud 1 Opacity (0-100)", Order=3, GroupName="Cloud 1 Colors")]
		public int Cloud1Opacity
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Cloud 2 - Bullish Color", Description="Cloud 2 Bullish Color", Order=1, GroupName="Cloud 2 Colors")]
		public Brush Cloud2BullColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud2BullColorSerializable
		{
			get { return Serialize.BrushToString(Cloud2BullColor); }
			set { Cloud2BullColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Cloud 2 - Bearish Color", Description="Cloud 2 Bearish Color", Order=2, GroupName="Cloud 2 Colors")]
		public Brush Cloud2BearColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud2BearColorSerializable
		{
			get { return Serialize.BrushToString(Cloud2BearColor); }
			set { Cloud2BearColor = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[NinjaScriptProperty]
		[Display(Name="Cloud 2 - Opacity", Description="Cloud 2 Opacity (0-100)", Order=3, GroupName="Cloud 2 Colors")]
		public int Cloud2Opacity
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Cloud 3 - Bullish Color", Description="Cloud 3 Bullish Color", Order=1, GroupName="Cloud 3 Colors")]
		public Brush Cloud3BullColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud3BullColorSerializable
		{
			get { return Serialize.BrushToString(Cloud3BullColor); }
			set { Cloud3BullColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Cloud 3 - Bearish Color", Description="Cloud 3 Bearish Color", Order=2, GroupName="Cloud 3 Colors")]
		public Brush Cloud3BearColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud3BearColorSerializable
		{
			get { return Serialize.BrushToString(Cloud3BearColor); }
			set { Cloud3BearColor = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[NinjaScriptProperty]
		[Display(Name="Cloud 3 - Opacity", Description="Cloud 3 Opacity (0-100)", Order=3, GroupName="Cloud 3 Colors")]
		public int Cloud3Opacity
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Cloud 4 - Bullish Color", Description="Cloud 4 Bullish Color", Order=1, GroupName="Cloud 4 Colors")]
		public Brush Cloud4BullColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud4BullColorSerializable
		{
			get { return Serialize.BrushToString(Cloud4BullColor); }
			set { Cloud4BullColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Cloud 4 - Bearish Color", Description="Cloud 4 Bearish Color", Order=2, GroupName="Cloud 4 Colors")]
		public Brush Cloud4BearColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud4BearColorSerializable
		{
			get { return Serialize.BrushToString(Cloud4BearColor); }
			set { Cloud4BearColor = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[NinjaScriptProperty]
		[Display(Name="Cloud 4 - Opacity", Description="Cloud 4 Opacity (0-100)", Order=3, GroupName="Cloud 4 Colors")]
		public int Cloud4Opacity
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Cloud 5 - Bullish Color", Description="Cloud 5 Bullish Color", Order=1, GroupName="Cloud 5 Colors")]
		public Brush Cloud5BullColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud5BullColorSerializable
		{
			get { return Serialize.BrushToString(Cloud5BullColor); }
			set { Cloud5BullColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Cloud 5 - Bearish Color", Description="Cloud 5 Bearish Color", Order=2, GroupName="Cloud 5 Colors")]
		public Brush Cloud5BearColor
		{ get; set; }

		[Browsable(false)]
		public string Cloud5BearColorSerializable
		{
			get { return Serialize.BrushToString(Cloud5BearColor); }
			set { Cloud5BearColor = Serialize.StringToBrush(value); }
		}

		[Range(0, 100)]
		[NinjaScriptProperty]
		[Display(Name="Cloud 5 - Opacity", Description="Cloud 5 Opacity (0-100)", Order=3, GroupName="Cloud 5 Colors")]
		public int Cloud5Opacity
		{ get; set; }

		[XmlIgnore]
		[Display(Name="Short EMA - Rising Color", Description="Short EMA Rising Color", Order=1, GroupName="EMA Line Colors")]
		public Brush ShortEMARisingColor
		{ get; set; }

		[Browsable(false)]
		public string ShortEMARisingColorSerializable
		{
			get { return Serialize.BrushToString(ShortEMARisingColor); }
			set { ShortEMARisingColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Short EMA - Falling Color", Description="Short EMA Falling Color", Order=2, GroupName="EMA Line Colors")]
		public Brush ShortEMAFallingColor
		{ get; set; }

		[Browsable(false)]
		public string ShortEMAFallingColorSerializable
		{
			get { return Serialize.BrushToString(ShortEMAFallingColor); }
			set { ShortEMAFallingColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Long EMA - Rising Color", Description="Long EMA Rising Color", Order=3, GroupName="EMA Line Colors")]
		public Brush LongEMARisingColor
		{ get; set; }

		[Browsable(false)]
		public string LongEMARisingColorSerializable
		{
			get { return Serialize.BrushToString(LongEMARisingColor); }
			set { LongEMARisingColor = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name="Long EMA - Falling Color", Description="Long EMA Falling Color", Order=4, GroupName="EMA Line Colors")]
		public Brush LongEMAFallingColor
		{ get; set; }

		[Browsable(false)]
		public string LongEMAFallingColorSerializable
		{
			get { return Serialize.BrushToString(LongEMAFallingColor); }
			set { LongEMAFallingColor = Serialize.StringToBrush(value); }
		}

		#endregion
	}

	public enum MATypeOption
	{
		EMA,
		SMA
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private RedTailEMACloud[] cacheRedTailEMACloud;
		public RedTailEMACloud RedTailEMACloud(MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			return RedTailEMACloud(Input, mAType, shortEMA1Length, longEMA1Length, shortEMA2Length, longEMA2Length, shortEMA3Length, longEMA3Length, shortEMA4Length, longEMA4Length, shortEMA5Length, longEMA5Length, showLongAlerts, showShortAlerts, showLine, showEMACloud1, showEMACloud2, showEMACloud3, showEMACloud4, showEMACloud5, eMACloudLeading, cloud1Opacity, cloud2Opacity, cloud3Opacity, cloud4Opacity, cloud5Opacity);
		}

		public RedTailEMACloud RedTailEMACloud(ISeries<double> input, MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			if (cacheRedTailEMACloud != null)
				for (int idx = 0; idx < cacheRedTailEMACloud.Length; idx++)
					if (cacheRedTailEMACloud[idx] != null && cacheRedTailEMACloud[idx].MAType == mAType && cacheRedTailEMACloud[idx].ShortEMA1Length == shortEMA1Length && cacheRedTailEMACloud[idx].LongEMA1Length == longEMA1Length && cacheRedTailEMACloud[idx].ShortEMA2Length == shortEMA2Length && cacheRedTailEMACloud[idx].LongEMA2Length == longEMA2Length && cacheRedTailEMACloud[idx].ShortEMA3Length == shortEMA3Length && cacheRedTailEMACloud[idx].LongEMA3Length == longEMA3Length && cacheRedTailEMACloud[idx].ShortEMA4Length == shortEMA4Length && cacheRedTailEMACloud[idx].LongEMA4Length == longEMA4Length && cacheRedTailEMACloud[idx].ShortEMA5Length == shortEMA5Length && cacheRedTailEMACloud[idx].LongEMA5Length == longEMA5Length && cacheRedTailEMACloud[idx].ShowLongAlerts == showLongAlerts && cacheRedTailEMACloud[idx].ShowShortAlerts == showShortAlerts && cacheRedTailEMACloud[idx].ShowLine == showLine && cacheRedTailEMACloud[idx].ShowEMACloud1 == showEMACloud1 && cacheRedTailEMACloud[idx].ShowEMACloud2 == showEMACloud2 && cacheRedTailEMACloud[idx].ShowEMACloud3 == showEMACloud3 && cacheRedTailEMACloud[idx].ShowEMACloud4 == showEMACloud4 && cacheRedTailEMACloud[idx].ShowEMACloud5 == showEMACloud5 && cacheRedTailEMACloud[idx].EMACloudLeading == eMACloudLeading && cacheRedTailEMACloud[idx].Cloud1Opacity == cloud1Opacity && cacheRedTailEMACloud[idx].Cloud2Opacity == cloud2Opacity && cacheRedTailEMACloud[idx].Cloud3Opacity == cloud3Opacity && cacheRedTailEMACloud[idx].Cloud4Opacity == cloud4Opacity && cacheRedTailEMACloud[idx].Cloud5Opacity == cloud5Opacity && cacheRedTailEMACloud[idx].EqualsInput(input))
						return cacheRedTailEMACloud[idx];
			return CacheIndicator<RedTailEMACloud>(new RedTailEMACloud(){ MAType = mAType, ShortEMA1Length = shortEMA1Length, LongEMA1Length = longEMA1Length, ShortEMA2Length = shortEMA2Length, LongEMA2Length = longEMA2Length, ShortEMA3Length = shortEMA3Length, LongEMA3Length = longEMA3Length, ShortEMA4Length = shortEMA4Length, LongEMA4Length = longEMA4Length, ShortEMA5Length = shortEMA5Length, LongEMA5Length = longEMA5Length, ShowLongAlerts = showLongAlerts, ShowShortAlerts = showShortAlerts, ShowLine = showLine, ShowEMACloud1 = showEMACloud1, ShowEMACloud2 = showEMACloud2, ShowEMACloud3 = showEMACloud3, ShowEMACloud4 = showEMACloud4, ShowEMACloud5 = showEMACloud5, EMACloudLeading = eMACloudLeading, Cloud1Opacity = cloud1Opacity, Cloud2Opacity = cloud2Opacity, Cloud3Opacity = cloud3Opacity, Cloud4Opacity = cloud4Opacity, Cloud5Opacity = cloud5Opacity }, input, ref cacheRedTailEMACloud);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.RedTailEMACloud RedTailEMACloud(MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			return indicator.RedTailEMACloud(Input, mAType, shortEMA1Length, longEMA1Length, shortEMA2Length, longEMA2Length, shortEMA3Length, longEMA3Length, shortEMA4Length, longEMA4Length, shortEMA5Length, longEMA5Length, showLongAlerts, showShortAlerts, showLine, showEMACloud1, showEMACloud2, showEMACloud3, showEMACloud4, showEMACloud5, eMACloudLeading, cloud1Opacity, cloud2Opacity, cloud3Opacity, cloud4Opacity, cloud5Opacity);
		}

		public Indicators.RedTailEMACloud RedTailEMACloud(ISeries<double> input , MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			return indicator.RedTailEMACloud(input, mAType, shortEMA1Length, longEMA1Length, shortEMA2Length, longEMA2Length, shortEMA3Length, longEMA3Length, shortEMA4Length, longEMA4Length, shortEMA5Length, longEMA5Length, showLongAlerts, showShortAlerts, showLine, showEMACloud1, showEMACloud2, showEMACloud3, showEMACloud4, showEMACloud5, eMACloudLeading, cloud1Opacity, cloud2Opacity, cloud3Opacity, cloud4Opacity, cloud5Opacity);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.RedTailEMACloud RedTailEMACloud(MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			return indicator.RedTailEMACloud(Input, mAType, shortEMA1Length, longEMA1Length, shortEMA2Length, longEMA2Length, shortEMA3Length, longEMA3Length, shortEMA4Length, longEMA4Length, shortEMA5Length, longEMA5Length, showLongAlerts, showShortAlerts, showLine, showEMACloud1, showEMACloud2, showEMACloud3, showEMACloud4, showEMACloud5, eMACloudLeading, cloud1Opacity, cloud2Opacity, cloud3Opacity, cloud4Opacity, cloud5Opacity);
		}

		public Indicators.RedTailEMACloud RedTailEMACloud(ISeries<double> input , MATypeOption mAType, int shortEMA1Length, int longEMA1Length, int shortEMA2Length, int longEMA2Length, int shortEMA3Length, int longEMA3Length, int shortEMA4Length, int longEMA4Length, int shortEMA5Length, int longEMA5Length, bool showLongAlerts, bool showShortAlerts, bool showLine, bool showEMACloud1, bool showEMACloud2, bool showEMACloud3, bool showEMACloud4, bool showEMACloud5, int eMACloudLeading, int cloud1Opacity, int cloud2Opacity, int cloud3Opacity, int cloud4Opacity, int cloud5Opacity)
		{
			return indicator.RedTailEMACloud(input, mAType, shortEMA1Length, longEMA1Length, shortEMA2Length, longEMA2Length, shortEMA3Length, longEMA3Length, shortEMA4Length, longEMA4Length, shortEMA5Length, longEMA5Length, showLongAlerts, showShortAlerts, showLine, showEMACloud1, showEMACloud2, showEMACloud3, showEMACloud4, showEMACloud5, eMACloudLeading, cloud1Opacity, cloud2Opacity, cloud3Opacity, cloud4Opacity, cloud5Opacity);
		}
	}
}

#endregion
