#<p align="center">
  <img src="https://avatars.githubusercontent.com/u/209633456?v=4" width="160" alt="RedTail Indicators Logo"/>
</p>

<h1 align="center">RedTail EMA Cloud</h1>

<p align="center">
  <b>A multi-layered moving average cloud indicator for NinjaTrader 8.</b><br>
  Five independent clouds with EMA/SMA selection, directional color coding, and crossover alerts.
</p>

<p align="center">
  <a href="https://buymeacoffee.com/dmwyzlxstj">
    <img src="https://img.shields.io/badge/☕_Buy_Me_a_Coffee-FFDD00?style=flat-square&logo=buy-me-a-coffee&logoColor=black" alt="Buy Me a Coffee"/>
  </a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/3astbeast/RedTail-EMA-Cloud/refs/heads/main/Screenshot%202026-03-03%20133013.png" width="800" alt="RedTail EMA Cloud Screenshot"/>
</p>

---

## Overview

RedTail EMA Cloud draws up to 5 filled cloud regions between pairs of moving averages. Each cloud changes color based on whether the short MA is above or below the long MA — giving you an instant visual read on trend direction across multiple timeframe perspectives from a single indicator. Converted from the TradingView HawkEye EMA Cloud Pine Script.

---

## How It Works

Each cloud is defined by a short and long moving average pair. The area between the two MAs is filled with a bullish color when the short MA is above the long MA, and a bearish color when it's below. Layering multiple clouds with different period pairs creates a multi-timeframe trend picture — when all clouds agree on direction, you have strong trend confirmation.

---

## Five Independent Clouds

Each cloud can be independently enabled or disabled with its own short/long MA lengths.

| Cloud | Default Short | Default Long | Enabled | Default Colors |
|-------|:---:|:---:|:---:|---|
| Cloud 1 | 8 | 9 | Yes | Dark Green / Dark Magenta |
| Cloud 2 | 5 | 12 | Yes | Lime Green / Red |
| Cloud 3 | 34 | 50 | Yes | Dodger Blue / Orange |
| Cloud 4 | 72 | 89 | No | Teal / Hot Pink |
| Cloud 5 | 180 | 200 | No | Cyan / Orange Red |

The defaults progress from fast (8/9) to slow (180/200), covering scalping through swing timeframes.

---

## MA Type Selection

Switch between **EMA** (Exponential Moving Average) and **SMA** (Simple Moving Average) for all clouds simultaneously. EMA is the default and reacts faster to recent price changes; SMA gives smoother, more stable clouds.

---

## EMA Line Overlay

Optionally display the individual MA lines on top of the cloud fills. When enabled, each MA line is color-coded based on direction:

- **Short MA** — Rising color (default: Olive) when the current value is above the previous bar, falling color (default: Maroon) when below
- **Long MA** — Rising color (default: Green) when above the previous bar, falling color (default: Red) when below

The short MA lines render at width 1 and the long MA lines at width 3 for easy visual distinction.

---

## Cloud Leading Offset

Shift the cloud fill forward by a configurable number of bars. This projects the cloud ahead of current price, similar to how Ichimoku displaces the Kumo cloud. Default: 0 (no offset).

---

## Per-Cloud Colors

Each of the 5 clouds has fully independent color settings:

- **Bullish Color** — Fill color when the short MA is above the long MA
- **Bearish Color** — Fill color when the short MA is below the long MA
- **Opacity** — 0 (fully transparent) to 100 (fully opaque). Each cloud has its own opacity, letting you layer clouds with different transparency levels.

---

## Crossover Alerts

Optional long and short crossover alerts that fire when the short MA crosses above (long signal) or below (short signal) the long MA.

---

## Installation

1. Download the `.cs` file from this repository
2. Open NinjaTrader 8
3. Go to **Tools → Import → NinjaScript Add-On**
4. Select the downloaded file and click **OK**
5. The indicator will appear in your **Indicators** list — add it to any chart

---

## Part of the RedTail Indicators Suite

This indicator is part of the [RedTail Indicators](https://github.com/3astbeast/RedTailIndicators) collection — free NinjaTrader 8 tools built for futures traders who demand precision.

---

<p align="center">
  <a href="https://buymeacoffee.com/dmwyzlxstj">
    <img src="https://img.shields.io/badge/☕_Buy_Me_a_Coffee-Support_My_Work-FFDD00?style=for-the-badge&logo=buy-me-a-coffee&logoColor=black" alt="Buy Me a Coffee"/>
  </a>
</p>
