﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	partial class MainForm
	{
		#region 表示メニュー

		private void 表示ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			最前列表示ToolStripMenuItem.Checked = TopMost;
			タイトルバーToolStripMenuItem.Checked = TitleBar;
			レスボックスToolStripMenuItem.Checked = panelResBox.Visible;
			ステータスバーToolStripMenuItem.Checked = panelStatusLabel.Visible;
			フレームToolStripMenuItem.Checked = Frame;
		}

		private void 最前列表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("TopMost");
		}

		private void 最小化ミュートToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Mini&Mute");
		}
		
		private void フルスクリーンToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("WMPFullScreen");
		}
		
		private void 動画サイズに合わせるToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "FitSizeMovie", (!動画サイズに合わせるToolStripMenuItem1.Checked).ToString());
		}

		private void タイトルバーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("TitleBar");
		}

		private void レスボックスToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("ResBox");
		}

		private void ステータスバーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("StatusLabel");
		}

		private void フレームToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Frame");
		}

		#endregion

		#region 機能メニュー

		private void リトライToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Retry");
		}

		private void bump再接続ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Bump");
		}

		private void チャンネル情報を更新するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("ChannelInfoUpdate");
		}

		private void スレッド一覧を更新するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("ThreadListUpdate");
		}

		private void スレッドビューワを開くToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenThreadViewer");
		}
		
		private void スクリーンショットToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("ScreenShot");
		}

		private void スクリーンショットフォルダを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenScreenShotFolder");
		}

		#endregion

		#region 音量メニュー

		private void 上げるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Volume+10");
		}

		private void 下げるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Volume-10");
		}

		private void ミュートToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Mute");
		}

		private void 中央ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("VolumeBalanceMiddle");
		}

		private void 左ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("VolumeBalanceLeft");
		}

		private void 右ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("VolumeBalanceRight");
		}

		#endregion

		#region ファイルメニュー

		private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenFile");
		}

		private void クリップボードから開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenClipBoard");
		}

		private void 終了するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Close");
		}

		private void リレーを切断して終了するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("Close&RelayCut");
		}

		#endregion

		/// <summary>
		/// ミュート切り替え
		/// </summary>
		private void 音量ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			ミュートToolStripMenuItem.Checked = wmp.Mute;
		}

		#region 画面分割メニュー

		#region 画面分割メニュー（幅）

		private void 幅5分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitWidth(5);
		}

		private void 幅4分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitWidth(4);
		}

		private void 幅3分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitWidth(3);
		}

		private void 幅2分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitWidth(2);
		}

		private void 幅1分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitWidth(1);
		}

		#endregion

		#region 画面分割メニュー（高さ）

		private void 高さ5分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitHeight(5);
		}

		private void 高さ4分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitHeight(4);
		}

		private void 高さ3分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitHeight(3);
		}

		private void 高さ2分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitHeight(2);
		}

		private void 高さ1分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplitHeight(1);
		}

		#endregion

		#region 画面分割メニュー（縦×横）

		private void 分割5X5ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplit(5);
		}

		private void 分割4X4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplit(4);
		}

		private void 分割3X3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplit(3);
		}

		private void 分割2X2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplit(2);
		}

		private void 分割1X1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenSplit(1);
		}

		#endregion

		#endregion

		#region サイズ指定

		#region サイズ指定（拡大率）

		private void toolStripMenuItem50Per_Click(object sender, EventArgs e)
		{
			SetScale(50);
		}

		private void toolStripMenuItem75Per_Click(object sender, EventArgs e)
		{
			SetScale(75);
		}

		private void toolStripMenuItem100Per_Click(object sender, EventArgs e)
		{
			SetScale(100);
		}

		private void toolStripMenuItem150_Click(object sender, EventArgs e)
		{
			SetScale(150);
		}

		private void toolStripMenuItem200Per_Click(object sender, EventArgs e)
		{
			SetScale(200);
		}

		#endregion

		#region サイズ指定（幅×高さ）

		private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetSize(160, 120);
		}

		private void x240ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetSize(320, 240);
		}

		private void x360ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetSize(480, 360);
		}

		private void x480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetSize(640, 480);
		}

		private void x600ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetSize(800, 600);
		}

		#endregion

		#region サイズ指定（幅）

		private void 幅160ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetWidth(160);
		}

		private void 幅320ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetWidth(320);
		}

		private void 幅480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetWidth(480);
		}

		private void 幅640ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetWidth(640);
		}

		private void 幅800ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetWidth(800);
		}

		#endregion

		#region サイズ指定（高さ）

		private void 高さ120ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetHeight(120);
		}

		private void 高さ240ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetHeight(240);
		}

		private void 高さ360ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetHeight(360);
		}

		private void 高さ480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetHeight(480);
		}

		private void 高さ600ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetHeight(600);
		}

		#endregion

		#endregion

		/// <summary>
		/// バランス切り替え
		/// </summary>
		private void バランスToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			中央ToolStripMenuItem.Checked = false;
			左ToolStripMenuItem.Checked = false;
			右ToolStripMenuItem.Checked = false;

			switch (wmp.settings.balance)
			{
				case 0:
					中央ToolStripMenuItem.Checked = true;
					break;
				case -100:
					左ToolStripMenuItem.Checked = true;
					break;
				case 100:
					右ToolStripMenuItem.Checked = true;
					break;
			}
		}

		/// <summary>
		/// 終了時
		/// </summary>
		private void 終了時ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "SaveLocationOnClose":
						位置を保存するToolStripMenuItem.Checked = (data == "True");
						break;
					case "SaveSizeOnClose":
						サイズを保存するToolStripMenuItem.Checked = (data == "True");
						break;
					case "SaveVolumeOnClose":
						音量を保存するToolStripMenuItem.Checked = (data == "True");
						break;
					case "RlayCutOnClose":
						リレーを切断するToolStripMenuItem.Checked = (data == "True");
						break;
					case "CloseViewerOnClose":
						スレッドビューワを終了するToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
			/*
			位置を保存するToolStripMenuItem.Checked = SaveLocationOnClose;
			サイズを保存するToolStripMenuItem.Checked = SaveSizeOnClose;
			音量を保存するToolStripMenuItem.Checked = SaveVolumeOnClose;
			リレーを切断するToolStripMenuItem.Checked = RlayCutOnClose;
			スレッドビューワを終了するToolStripMenuItem.Checked = CloseViewerOnClose;
			 */
		}

		private void 位置を保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveLocationOnClose = !SaveLocationOnClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "SaveLocationOnClose", SaveLocationOnClose.ToString());
		}

		private void サイズを保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSizeOnClose = !SaveSizeOnClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "SaveSizeOnClose", SaveSizeOnClose.ToString());
		}

		private void リレーを切断するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RlayCutOnClose = !RlayCutOnClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "RlayCutOnClose", RlayCutOnClose.ToString());
		}

		private void 音量を保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveVolumeOnClose = !SaveVolumeOnClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "SaveVolumeOnClose", SaveVolumeOnClose.ToString());
		}

		private void スレッドビューワを終了するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseViewerOnClose = !CloseViewerOnClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "CloseViewerOnClose", CloseViewerOnClose.ToString());
		}


		/// <summary>
		/// デフォルト
		/// </summary>
		private void デフォルトToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "TopMost":
						最前列表示ToolStripMenuItem2.Checked = (data == "True");
						break;
					case "TitleBar":
						タイトルバーToolStripMenuItem2.Checked = (data == "True");
						break;
					case "ResBox":
						レスボックスToolStripMenuItem2.Checked = (data == "True");
						break;
					case "StatusLabel":
						ステータスバーToolStripMenuItem2.Checked = (data == "True");
						break;
					case "Frame":
						フレームToolStripMenuItem2.Checked = (data == "True");
						break;
					case "FitSizeMovie":
						動画サイズに合わせるToolStripMenuItem1.Checked = (data == "True");
						break;
					case "ThreadCaption":
						字幕表示ToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
			/*
			最前列表示ToolStripMenuItem2.Checked = TopMost;
			タイトルバーToolStripMenuItem2.Checked = TitleBar;
			レスボックスToolStripMenuItem2.Checked = panelResBox.Visible;
			ステータスバーToolStripMenuItem2.Checked = panelStatusLabel.Visible;
			フレームToolStripMenuItem2.Checked = Frame;
			 */
		}

		private void 最前列表示ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "TopMost", (!最前列表示ToolStripMenuItem2.Checked).ToString());
		}

		private void タイトルバーToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "TitleBar", (!タイトルバーToolStripMenuItem2.Checked).ToString());
			OnPanelSizeChange();
		}

		private void レスボックスToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ResBox", (!レスボックスToolStripMenuItem2.Checked).ToString());
			OnPanelSizeChange();
		}

		private void ステータスバーToolStripMenuItem2_Click(object sender, EventArgs e)
		{

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "StatusLabel", (!ステータスバーToolStripMenuItem2.Checked).ToString());
			OnPanelSizeChange();
		}

		private void フレームToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "Frame", (!フレームToolStripMenuItem2.Checked).ToString());
			OnPanelSizeChange();
		}

		private void 字幕表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ThreadCaption", (!字幕表示ToolStripMenuItem.Checked).ToString());
		}

		/// <summary>
		/// レスボックスの操作
		/// </summary>
		private void レスボックスの操作ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "ResBoxType":
						enter改行ShiftEnter書き込みToolStripMenuItem.Checked = (data == "True");
						enter書き込みShiftEnter改行ToolStripMenuItem.Checked = !(data == "True");
						break;
				}
			}

			/*
			enter改行ShiftEnter書き込みToolStripMenuItem.Checked = ResBoxType;
			enter書き込みShiftEnter改行ToolStripMenuItem.Checked = !ResBoxType;
			 */
		}

		private void enter改行ShiftEnter書き込みToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResBoxType = true;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ResBoxType", ResBoxType.ToString());
		}

		private void enter書き込みShiftEnter改行ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResBoxType = false;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ResBoxType", ResBoxType.ToString());
		}

		/// <summary>
		/// 設定
		/// </summary>
		private void 設定ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "UseScreenMagnet":
						スクリーン吸着ToolStripMenuItem.Checked = (data == "True");
						break;
					case "UseMouseGesture":
						マウスジェスチャーToolStripMenuItem.Checked = (data == "True");
						break;
					case "CloseResBoxOnWrite":
						書き込み時にレスボックスを非表示ToolStripMenuItem.Checked = (data == "True");
						break;
					case "ResBoxAutoVisible":
						レスボックスを自動的に隠すToolStripMenuItem.Checked = (data == "True");
						break;
					case "AspectRate":
						アスペクト比を維持ToolStripMenuItem.Checked = (data == "True");
						break;
					case "CloseResBoxOnBackSpace":
						bSでレスボックスを閉じるToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
			/*
			スクリーン吸着ToolStripMenuItem.Checked = UseScreenMagnet;
			マウスジェスチャーToolStripMenuItem.Checked = UseMouseGesture;
			書き込み時にレスボックスを非表示ToolStripMenuItem.Checked = CloseResBoxOnWrite;
			レスボックスを自動的に隠すToolStripMenuItem.Checked = ResBoxAutoVisible;
			アスペクト比を維持ToolStripMenuItem.Checked = AspectRate;
			 */
		}

		private void スクリーン吸着ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UseScreenMagnet = !UseScreenMagnet;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "UseScreenMagnet", UseScreenMagnet.ToString());
		}

		private void マウスジェスチャーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UseMouseGesture = !UseMouseGesture;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "UseMouseGesture", UseMouseGesture.ToString());
		}

		private void 書き込み時にレスボックスを非表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseResBoxOnWrite = !CloseResBoxOnWrite;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "CloseResBoxOnWrite", CloseResBoxOnWrite.ToString());
		}

		private void bSでレスボックスを閉じるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseResBoxOnBackSpace = !CloseResBoxOnBackSpace;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "CloseResBoxOnBackSpace", CloseResBoxOnBackSpace.ToString());
		}

		private void レスボックスを自動的に隠すToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResBoxAutoVisible = !ResBoxAutoVisible;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ResBoxAutoVisible", ResBoxAutoVisible.ToString());
		}

		private void アスペクト比を維持ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AspectRate = !AspectRate;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "AspectRate", AspectRate.ToString());
		}

		private void クリック時にレスボックスを非表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClickToResBoxClose = !ClickToResBoxClose;

			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "ClickToResBoxClose", ClickToResBoxClose.ToString());
		}

		/// <summary>
		/// 機能
		/// </summary>
		private void 現在の状態を保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteIniFile();
		}

		private void 位置を保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

			// 位置を保存
			iniFile.Write("Player", "X", Left.ToString());
			iniFile.Write("Player", "Y", Top.ToString());
		}

		private void サイズを保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

			// サイズを保存
			if (WindowState != FormWindowState.Maximized)
			{
				iniFile.Write("Player", "Width", Width.ToString());
				iniFile.Write("Player", "Height", Height.ToString());
			}
			else
			{
				System.Drawing.Size frame = Size - ClientSize;

				int height = 0;
				height += (panelResBox.Visible ? panelResBox.Height : 0);
				height += (panelStatusLabel.Visible ? panelStatusLabel.Height : 0);

				iniFile.Write("Player", "Width", (PanelWMPSize.Width + frame.Width).ToString());
				iniFile.Write("Player", "Height", (PanelWMPSize.Height + height).ToString());
			}
		}

		private void 音量を保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

			// 音量を保存
			iniFile.Write("Player", "Volume", wmp.Volume.ToString());
		}

		/// <summary>
		/// ファイル
		/// </summary>

		private void コンタクトＵＲＬをブラウザで開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// ブラウザを開く
			if (File.Exists(BrowserAddress))
			{
				Process.Start(BrowserAddress, GetThreadUrl());
			}
			else
			{
				try
				{
					string browserPath = GetDefaultBrowserExePath();
					Process.Start(browserPath, GetThreadUrl());
				}
				catch
				{
				}
			}
		}

		private void wMPメニューToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenContextMenu");
		}

		private void toolStripTextBoxURL_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				wmp.URL = toolStripTextBoxURL.Text;
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// 動画サイズに合わせる（黒枠をなくす）
		/// </summary>
		private void 動画サイズに合わせるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("FitSizeMovie");
		}


		/// <summary>
		/// デフォルト：X
		/// </summary>
		private void xToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "X":
						toolStripTextBoxX.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：Y
		/// </summary>
		private void yToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "Y":
						toolStripTextBoxY.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：幅
		/// </summary>
		private void 幅ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "Width":
						toolStripTextBox幅.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：高さ
		/// </summary>
		private void 高さToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "Height":
						toolStripTextBox高さ.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：拡大率
		/// </summary>
		private void 拡大率ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "Scale":
						toolStripTextBoxScale.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：音量
		/// </summary>
		private void 音量ToolStripMenuItem1_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "Volume":
						toolStripTextBox音量.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// デフォルト：X 入力
		/// </summary>
		private void toolStripTextBoxX_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "X", toolStripTextBoxX.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// デフォルト：Y 入力
		/// </summary>
		private void toolStripTextBoxY_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "Y", toolStripTextBoxY.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// デフォルト：拡大率 入力
		/// </summary>
		private void toolStripTextBoxScale_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "Scale", toolStripTextBoxScale.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// デフォルト：音量 入力
		/// </summary>
		private void toolStripTextBox音量_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "Volume", toolStripTextBox音量.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// デフォルト：高さ 入力
		/// </summary>
		private void toolStripTextBox高さ_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "Height", toolStripTextBox高さ.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// デフォルト：幅 入力
		/// </summary>
		private void toolStripTextBox幅_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "Width", toolStripTextBox幅.Text);
				contextMenuStripWMP.Close();
			}
		}

		/// <summary>
		/// 設定->フォント->色
		/// </summary>
		private void 色ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// カラーダイアログを表示
			ColorDialog cd = new ColorDialog();
			cd.Color = labelDetail.ForeColor;
			cd.ShowDialog();

			// iniに書きだし
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "FontColorR", cd.Color.R.ToString());
			iniFile.Write("Player", "FontColorG", cd.Color.G.ToString());
			iniFile.Write("Player", "FontColorB", cd.Color.B.ToString());

			// 適応
			labelDetail.ForeColor = Color.FromArgb(255, cd.Color);
			labelDuration.ForeColor = Color.FromArgb(255, cd.Color);
			labelVolume.ForeColor = Color.FromArgb(255, cd.Color);
		}

		/// <summary>
		/// 設定->フォント->フォント
		/// </summary>
		private void フォントToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.Font = new Font(labelDetail.Font.Name, labelDetail.Font.Size);
			fd.ShowDialog();

			// iniに書きだし
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Player", "FontSize", fd.Font.Size.ToString());
			iniFile.Write("Player", "FontName", fd.Font.Name);

			// 適応
			SetFont(fd.Font.Name, fd.Font.Size);
		}

		/// <summary>
		/// スクリーン吸着範囲
		/// </summary>
		private void toolStripTextBoxScreenMagnetDockDist_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
					iniFile.Write("Player", "ScreenMagnetDockDist", toolStripTextBoxScreenMagnetDockDist.Text);
					ScreenMagnetDockDist = int.Parse(toolStripTextBoxScreenMagnetDockDist.Text);
					contextMenuStripWMP.Close();
				}
			}
			catch
			{
			}
		}

		private void スクリーン吸着範囲ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "ScreenMagnetDockDist":
						toolStripTextBoxScreenMagnetDockDist.Text = data;
						break;
				}
			}
		}

		private void toolStripTextBoxマウスジェスチャー感度_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Player", "MouseGestureInterval", toolStripTextBoxマウスジェスチャー感度.Text);
				try
				{
					wmp.mouseGesture.Interval = int.Parse(toolStripTextBoxマウスジェスチャー感度.Text);
				}
				catch
				{
				}
				contextMenuStripWMP.Close();
			}
		}

		private void マウスジェスチャー感度ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					case "MouseGestureInterval":
						toolStripTextBoxマウスジェスチャー感度.Text = data;
						break;
				}
			}
		}

		string ResBoxText = "";
		/// <summary>
		/// スレッド一覧を更新
		/// </summary>
		private void スレッド一覧を更新ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExeCommand("ThreadListUpdate");
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		private void スレッドビューワを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = "";
			string threadUrl = "";
			try
			{
				if (!ChannelInfo.IsInfo)
				{
					DialogResult result = MessageBox.Show("コンタクトURLが取得できていませんが\nスレッドビューワを開きますか？", "Message", MessageBoxButtons.YesNo);
					if (result == DialogResult.No)
					{
						return;
					}
				}

				// スレッドブラウザが指定してあるか
				if (File.Exists(ThreadBrowserAddress))
				{
					Process.Start(ThreadBrowserAddress, GetThreadUrl());
				}
				else
				{
					// 起動していたら終了
					if (ThreadViewerProcess != null && !ThreadViewerProcess.HasExited)
					{
						// 終了
						ThreadViewerProcess.CloseMainWindow();
					}

					// スレビューワを開く
					fileName = GetCurrentDirectory() + "\\PeerstViewer.exe";
					threadUrl = GetThreadUrl();

					ThreadViewerProcess = System.Diagnostics.Process.Start(fileName, threadUrl + " " + ChannelInfo.Name);
				}
			}
			catch
			{
				MessageBox.Show(fileName + "を開けませんでした。\nコマンドライン：" + threadUrl);
			}
		}

		/// <summary>
		/// スレッドURLをコピー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void スレッドURLをコピーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(GetThreadUrl(), true);
		}

		/// <summary>
		/// 最小化ボタン
		/// </summary>
		private void toolStripButtonMin_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		/// <summary>
		/// 最大化ボタン
		/// </summary>
		private void toolStripButtonMax_Click(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				WindowState = FormWindowState.Normal;
				OnPanelSizeChange(PanelWMPSize);
			}
			else
			{
				PanelWMPSize = WMPSize;
				WindowState = FormWindowState.Maximized;
			}

			OnPanelSizeChange();
		}

		/// <summary>
		/// 閉じるボタン
		/// </summary>
		private void toolStripButtonClose_Click(object sender, EventArgs e)
		{
			try
			{
				Visible = false;
				Close();
				//Application.Exit();
			}
			catch
			{
			}
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ExeCommand("OpenThreadViewer");
		}

		/// <summary>
		/// URL入力後Enter
		/// </summary>
		private void toolStripTextBoxThreadURL_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				BBS.GetDataFromUrl(toolStripTextBoxThreadURL.Text, out BoardName, out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo);

				if ((KindOfBBS != KindOfBBS.None) && (BoadGenre != "") && (BoadNo != ""))
				{
					// スレッド一覧を取得(スレッド)
					ThreadList = BBS.GetThreadList(KindOfBBS, BoadGenre, BoadNo);
				}

				if (comboBoxThreadList.InvokeRequired)
				{
					Invoke(new HttpGetThreadListDelegate(HttpGetThreadListMethod));
				}
				else
				{
					HttpGetThreadListMethod();
				}
			}
		}
	}
}