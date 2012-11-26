﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	partial class MainForm
	{
		/// <summary>
		/// Load
		/// </summary>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 初期化ファイルを読み込む
			LoadInitFile();

			// WMP初期化
			panelWMP.Controls.Add(wmp);
			wmp.uiMode = "none";
			wmp.stretchToFit = true;
			wmp.Location = new Point(0, 0);
			wmp.Dock = DockStyle.Fill;
			wmp.Volume = 50;
			wmp.enableContextMenu = false;

			OnPanelSizeChange();

			// コマンドラインから再生
			wmp.LoadCommandLine();

			// コマンドラインからチャンネル名を取得
			ChannelDetail = GetChannelName();
		}

		/// <summary>
		/// ステータスラベル：MouseHover
		/// </summary>
		private void panelStatusLabel_MouseHover(object sender, EventArgs e)
		{
			ShowToolTipDetail(labelDetail);

			// レスボックスを表示 / 非表示
			if (ResBoxAutoVisible)
			{
				panelResBox.Visible = true;
				OnPanelSizeChange();
			}
		}

		/// <summary>
		/// レスボックス：MouseLeave
		/// </summary>
		private void comboBoxThreadList_MouseLeave(object sender, EventArgs e)
		{
			if (ResBoxAutoVisible)
			{
				// レスボックスを表示 / 非表示
				if (!resBox.Selected)
				{
					panelResBox.Visible = false;
					OnPanelSizeChange();
					resBox.Selected = false;
					panelResBox.Height = resBox.Height;
				}
			}
		}

		/// <summary>
		/// レスボックス:MouseDown
		/// </summary>
		private void comboBoxThreadList_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				// レスボックスを選択する
				if (!resBox.AutoEnable(e.X))
				{
					// スレッド選択 → レスボックスから選択をはずす
					resBox.Selected = false;
				}
			}
		}

		/// <summary>
		/// レスボックス：SelectedIndexChange
		/// </summary>
		private void comboBoxThreadList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxThreadList.SelectedIndex != -1 && ThreadList.Count > comboBoxThreadList.SelectedIndex)
			{
				Application.DoEvents();

				// スレタイ変更
				resBox.ThreadTitle = comboBoxThreadList.Items[comboBoxThreadList.SelectedIndex].ToString();

				// スレッドを変更
				ThreadNo = ThreadList[comboBoxThreadList.SelectedIndex][0];
			}
		}

		/// <summary>
		/// レスボックス：KeyDown
		/// </summary>
		private void resBox_KeyDown(object sender, KeyEventArgs e)
		{
			// レスボックスをバックスペースで閉じる
			if (CloseResBoxOnBackSpace && e.KeyCode == Keys.Back)
			{
				if (resBox.Text == "")
				{
					panelResBox.Visible = !panelResBox.Visible;
					resBox.Selected = false;
					OnPanelSizeChange();
					wmp.Select();
				}
			}

			if (e.Control && e.KeyCode == Keys.A)
			{
				resBox.SelectAll();
			}

			if (e.KeyCode == Keys.Enter)
			{
				if (ResBoxType)
				{
					if (e.Shift)
					{
						// 書き込み確認
						if (!CheckWrite())
						{
							return;
						}

						ResBoxText = resBox.Text;
						if (BBS.Write(KindOfBBS, BoadGenre, BoadNo, ThreadNo, "", "sage", resBox.Text))
						{
							BeforeWriteThreadNo = ThreadNo;
							resBox.Text = "";
							if (CloseResBoxOnWrite)
							{
								panelResBox.Visible = false;
							}
							IsWriting = true;
						}
						else
						{
							resBox.Text = ResBoxText;
						}
					}
				}
				else
				{
					if (!e.Shift)
					{
						// 書き込み確認
						if (!CheckWrite())
						{
							return;
						}

						if (BBS.Write(KindOfBBS, BoadGenre, BoadNo, ThreadNo, "", "sage", resBox.Text))
						{
							BeforeWriteThreadNo = ThreadNo;
							resBox.Text = "";
						}
						IsWriting = true;
					}
				}
			}
		}

		/// <summary>
		/// レスボックス：TextChange
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resBox_TextChanged(object sender, EventArgs e)
		{
			// 書き込み完了
			if (IsWriting)
			{
				resBox.Text = "";
				IsWriting = false;
			}

			// スクロールバーを表示
			if (resBox.Width < resBox.PreferredSize.Width && resBox.Selected)
			{
				resBox.ScrollBars = ScrollBars.Horizontal;
			}
			else
			{
				resBox.ScrollBars = ScrollBars.None;
			}

			// レスボックスの大きさを変更
			resBox.Height = resBox.PreferredSize.Height;
			panelResBox.Height = resBox.Height;
			OnPanelSizeChange();
		}

		/// <summary>
		/// TimerDetail
		/// </summary>
		private void timerDetail_Tick(object sender, EventArgs e)
		{
			// タイマーの間隔を設定
			if (timerDetail.Interval == 1)
			{
				timerDetail.Interval = 200;
			}

			// 最小化ミュート解除
			if (MiniMute && WindowState != FormWindowState.Minimized)
			{
				wmp.Mute = false;
				MiniMute = false;
			}

			/*
			// ToolStripの非表示
			if (MousePosition.Y - Top >= toolStrip.Height)
			{
				toolStrip.Visible = false;
			}
			 */

			// ステータスバーにチャンネル詳細を表示
			if (CommandShowCount <= 0)
			{
				labelDetail.Text = ChannelDetail + " " + wmp.FPS + "fps " + wmp.BandWidth + "kbps パケット:" + wmp.ReceivedPackets + " (" + wmp.Width + "x" + wmp.Height + ")";
				CommandShowCount = 0;
			}

			CommandShowCount--;
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			wmp.Focus();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// フォーム非表示 → 終了処理をユーザに速く見せる.
			Visible = false;

			// 停止
			wmp.Ctlcontrols.stop();

			// INI用
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

			// 位置を保存
			if (SaveLocationOnClose)
			{
				iniFile.Write("Player", "X", Left.ToString());
				iniFile.Write("Player", "Y", Top.ToString());
			}

			// サイズを保存
			if (SaveSizeOnClose)
			{
				if (WindowState != FormWindowState.Maximized)
				{
					iniFile.Write("Player", "Width", Width.ToString());
					iniFile.Write("Player", "Height", Height.ToString());
				}
				else
				{
					Size frame = Size - ClientSize;

					int height = 0;
					height += (panelResBox.Visible ? panelResBox.Height : 0);
					height += (panelStatusLabel.Visible ? panelStatusLabel.Height : 0);

					iniFile.Write("Player", "Width", (PanelWMPSize.Width + frame.Width).ToString());
					iniFile.Write("Player", "Height", (PanelWMPSize.Height + height).ToString());
				}
			}

			// ボリュームを保存
			if (SaveVolumeOnClose)
			{
				iniFile.Write("Player", "Volume", wmp.Volume.ToString());
			}

			if (RlayCutOnClose)
			{
				// リレーを切断
				wmp.RelayCut();
			}

			if (CloseViewerOnClose)
			{
				// ビューワを終了
				try
				{
					if (ThreadViewerProcess != null)
					{
						ThreadViewerProcess.CloseMainWindow();
					}
				}
				catch
				{
				}
			}
		}

		private void timerUpdate_Tick(object sender, EventArgs e)
		{
			// スレ一覧更新
			ExeCommand("ThreadListUpdate");

			// チャンネル情報更新
			ExeCommand("ChannelInfoUpdate");
		}

		#region ステータスバーコンテキストメニュー

		/// <summary>
		/// コンテキストメニューを表示
		/// </summary>
		private void contextMenuStripResBox_Opened(object sender, EventArgs e)
		{
			toolStripTextBoxThreadURL.Text = GetThreadUrl();
		}

		#endregion

		#region ファイルのドラッグ＆ドロップ

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
				{
					wmp.URL = fileName;
					break;
				}
			}
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			//ドラッグされているデータがstring型か調べ、
			//そうであればドロップ効果をMoveにする
			//if (e.Data.GetDataPresent(typeof(string)))
			e.Effect = DragDropEffects.Move;
			//else
			//string型でなければ受け入れない
			//	e.Effect = DragDropEffects.None;
		}

		#endregion

		private void timerLoadIni_Tick(object sender, EventArgs e)
		{
			// 初期化ファイルを読み込む
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

			#region デフォルト設定
			{
				// デフォルト
				string[] keys = iniFile.GetKeys("Player");

				for (int i = 0; i < keys.Length; i++)
				{
					string data = iniFile.ReadString("Player", keys[i]);
					switch (keys[i])
					{
						// タイトルバー
						case "Volume":
							try
							{
								wmp.Volume = int.Parse(data);
							}
							catch
							{
							}
							break;


						// フレーム
						case "Frame":
							if (data == "False")
							{
								Frame = false;
							}
							break;
					}
				}
			}
			#endregion

			// ウィンドウ表示
			Opacity = 100;

			// 初期化終了
			timerLoadIni.Enabled = false;
		}
		#region ウィンドウプロシージャ

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			const int WM_MOVING = 0x0216;

			// フレーム無し時にシステムメニューを表示
			if (!Frame && m.Msg == 787)
			{
				contextMenuStripWMP.Show(MousePosition);
				/*
				Marshal.WriteInt32(m.LParam, 0, MousePosition.X);
				Marshal.WriteInt32(m.LParam, 8, MousePosition.Y);

				if (!IsSystemMenu)
				{

					// マウス座標を取得し、lParamに加工 
					// int x = System.Windows.Forms.Control.MousePosition.X;
					// int y = System.Windows.Forms.Control.MousePosition.Y;
					int x = 0;
					int y = 0;
					IntPtr lParam = new IntPtr(x | y << 16);

					// Windowハンドルを取得し、0x313メッセージを送信 
					Win32API.PostMessage(wmp.Handle, 0x313, IntPtr.Zero, lParam);

					IsSystemMenu = true;
				}
				else
				{
					IsSystemMenu = false;
				}
				 */
			}

			if (UseScreenMagnet && m.Msg == WM_MOVING)
			{
				#region 吸いつき

				int left = Marshal.ReadInt32(m.LParam, 0);
				int top = Marshal.ReadInt32(m.LParam, 4);

				bool IsSetLeft = false;
				bool IsSetTop = false;

				#region ウィンドウに張り付く

				// 各辺に対する座標を取得
				POINT p1, p2, p3, p4;

				// 上
				p1.x = left + Width / 2;
				p1.y = top - ScreenMagnetDockDist;

				// 右
				p2.x = left + Width + ScreenMagnetDockDist;
				p2.y = top + Height / 2;

				// 下
				p3.x = p1.x;
				p3.y = top + Height + ScreenMagnetDockDist;

				// 左
				p4.x = left - ScreenMagnetDockDist;
				p4.y = p2.y;

				IntPtr h1 = WindowFromPoint(p1);
				IntPtr h2 = WindowFromPoint(p2);
				IntPtr h3 = WindowFromPoint(p3);
				IntPtr h4 = WindowFromPoint(p4);

				// 上
				if (h1 != IntPtr.Zero)
				{
					RECT rect;
					IntPtr anc1 = GetAncestor(h1, GA_ROOT);

					if (GetWindowRect(anc1, out rect))
					{
						if (Math.Abs(MousePosition.Y - wmp.ClickPoint.Y - rect.bottom) <= ScreenMagnetDockDist)
						{
							top = rect.bottom;
							IsSetTop = true;
						}
					}
				}

				// 右
				if (h2 != IntPtr.Zero)
				{
					RECT rect;
					IntPtr anc2 = GetAncestor(h2, GA_ROOT);

					if (GetWindowRect(anc2, out rect))
					{
						if (Math.Abs(MousePosition.X - wmp.ClickPoint.X + Width - rect.left) <= ScreenMagnetDockDist)
						{
							left = rect.left - Width;
							IsSetLeft = true;
						}
					}
				}

				// 下
				if (!IsSetTop && h3 != IntPtr.Zero)
				{
					RECT rect;
					IntPtr anc3 = GetAncestor(h3, GA_ROOT);

					if (GetWindowRect(anc3, out rect))
					{
						if (Math.Abs(MousePosition.Y - wmp.ClickPoint.Y + Height - rect.top) <= ScreenMagnetDockDist)
						{
							top = rect.top - Height;
							IsSetTop = true;
						}
					}
				}

				// 左
				if (!IsSetLeft && h4 != IntPtr.Zero)
				{
					RECT rect;
					IntPtr anc4 = GetAncestor(h4, GA_ROOT);

					if (GetWindowRect(anc4, out rect))
					{
						if (Math.Abs(MousePosition.X - wmp.ClickPoint.X - rect.right) <= ScreenMagnetDockDist)
						{
							left = rect.right;
							IsSetLeft = true;
						}
					}
				}

				Marshal.WriteInt32(m.LParam, 0, left);
				Marshal.WriteInt32(m.LParam, 4, top);
				Marshal.WriteInt32(m.LParam, 8, left + Width);
				Marshal.WriteInt32(m.LParam, 12, top + Height);

				#endregion

				#region  スクリーンに張り付く

				Rectangle scr = System.Windows.Forms.Screen.GetBounds(this);

				if (!IsSetTop && Math.Abs(MousePosition.Y - wmp.ClickPoint.Y) <= ScreenMagnetDockDist)
				{
					top = scr.Top;
					IsSetTop = true;
				}

				if (!IsSetLeft && Math.Abs(MousePosition.X - wmp.ClickPoint.X) <= ScreenMagnetDockDist)
				{
					left = scr.Left;
					IsSetLeft = true;
				}

				if (!IsSetTop && Math.Abs(MousePosition.Y + Height - wmp.ClickPoint.Y - scr.Height) <= ScreenMagnetDockDist)
				{
					top = scr.Bottom - Height;
					IsSetTop = true;
				}


				// TODO 以下の処理があると、マルチディスプレイをまたぐとウィンドウが飛んでしまう
				/*
				if (!IsSetLeft && Math.Abs(MousePosition.X + Width - wmp.ClickPoint.X - scr.Width) <= ScreenMagnetDockDist)
				{
					left = scr.Right - Width;
					IsSetLeft = true;
				}
				*/

				if (IsSetTop || IsSetLeft)
				{
					Marshal.WriteInt32(m.LParam, 0, left);
					Marshal.WriteInt32(m.LParam, 4, top);
					Marshal.WriteInt32(m.LParam, 8, left + Width);
					Marshal.WriteInt32(m.LParam, 12, top + Height);
					return;
				}

				#endregion

				#endregion
			}

			if (m.Msg == Win32API.WM_SIZING)
			{
				#region アスペクト比維持

				if (AspectRate)
				{
					//各辺の座標を取得
					int L = Marshal.ReadInt32(m.LParam, 0);
					int T = Marshal.ReadInt32(m.LParam, 4);
					int R = Marshal.ReadInt32(m.LParam, 8);
					int B = Marshal.ReadInt32(m.LParam, 12);

					int width = R - L;
					int height = B - T;

					Size dif = Size - panelWMP.Size;

					//ドラッグされている辺に応じて、新たなサイズを指定
					switch (m.WParam.ToInt32())
					{
						// 左
						case Win32API.WMSZ_LEFT:
						// 右
						case Win32API.WMSZ_RIGHT:
							{
								int panelWidth = width - dif.Width;
								B = T + (int)(panelWidth * wmp.AspectRate) + dif.Height;
							}
							break;
						// 上
						case Win32API.WMSZ_TOP:
						// 下
						case Win32API.WMSZ_BOTTOM:
							{
								int panelHeight = height - (panelStatusLabel.Visible ? panelStatusLabel.Height : 0) - (panelResBox.Visible ? panelResBox.Height : 0) - (Size - ClientSize).Height;
								R = L + (int)(panelHeight * (1 / wmp.AspectRate)) + dif.Width;
								// R = L + (int)((height - (panelStatusLabel.Visible ? panelStatusLabel.Height : 0) - (panelResBox.Visible ? panelResBox.Height : 0)) * (1 / wmp.AspectRate));
							}
							break;
					}

					// 新しいサイズで上書き
					Marshal.WriteInt32(m.LParam, 0, L);
					Marshal.WriteInt32(m.LParam, 4, T);
					Marshal.WriteInt32(m.LParam, 8, R);
					Marshal.WriteInt32(m.LParam, 12, B);
				}

				#endregion
			}
		}

		#endregion
		/// <summary>
		/// ステータスラベル：MouseDown
		/// </summary>
		private void panelStatusLabel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				// レスボックスを表示 / 非表示
				if (!ResBoxAutoVisible || ClickToResBoxClose)
				{
					panelResBox.Visible = !panelResBox.Visible;
					resBox.Selected = false;
					OnPanelSizeChange();
				}
				else
				{
					if (resBox.Text == "")
					{
						resBox.Selected = false;
					}
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				// コンテキストメニューを表示
				contextMenuStripResBox.Show(MousePosition);
			}
		}

		/// <summary>
		/// 音量ラベル:MouseDown
		/// </summary>
		private void labelVolume_MouseDown(object sender, MouseEventArgs e)
		{
			// ミュート
			wmp.Mute = !wmp.Mute;
		}

		/// <summary>
		/// チャンネル詳細をツールチップで表示
		/// </summary>
		public void ShowToolTipDetail(Control control)
		{
			toolTipDetail.SetToolTip(control, labelDetail.Text);
		}

	}
}