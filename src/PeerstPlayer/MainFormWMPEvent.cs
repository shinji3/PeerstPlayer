﻿using System;
namespace PeerstPlayer
{
	partial class MainForm
	{
		/// <summary>
		/// WMP:MouseDown
		/// </summary>
		void wmp_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
		{
			if (resBox.Text == "")
			{
				resBox.Selected = false;
			}
		}

		/// <summary>
		/// MouseUp
		/// </summary>
		void wmp_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
		{
			if ((IsOpenContextMenu) && (e.nButton == 2) && (wmp.MouseGesture == ""))
			{
				contextMenuStripWMP.Show(MousePosition);
			}
		}

		/// <summary>
		/// WMP:MouseMove
		/// </summary>
		void wmp_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
		{
			// 右クリックしている時
			if (e.nButton == 2)
			{
				// マウスジェスチャ
				string gesture = wmp.MouseGesture;

				if (UseMouseGesture && gesture != "")
				{
					for (int i = 0; i < ShortcutList.Count; i++)
					{
						if (gesture == ShortcutList[i][0])
						{
							gesture += " (" + CommandToString(ShortcutList[i][1]) + ")";
							break;
						}
					}

					labelDetail.Text = "ジェスチャ：" + gesture;
				}
			}

			// ToolStripの表示
			if (e.fX > toolStrip.Left - 15 && e.fY < toolStrip.Height + 15)
			{
				toolStrip.Visible = true;
			}
			else
			{
				toolStrip.Visible = false;
			}
		}

		/// <summary>
		/// ジェスチャー
		/// </summary>
		void wmp_Gesture(object sender, string gesture)
		{
			for (int i = 0; i < ShortcutList.Count; i++)
			{
				if (gesture == ShortcutList[i][0])
				{
					ExeCommand(ShortcutList[i][1]);
				}
			}
		}

		/// <summary>
		/// 音量を変更
		/// </summary>
		void wmp_VolumeChange(object sender, EventArgs e)
		{
			if (wmp.Mute)
			{
				labelVolume.Text = "-";
			}
			else
			{
				labelVolume.Text = wmp.Volume.ToString();
			}
		}

		/// <summary>
		/// 再生時間が変更された
		/// </summary>
		void wmp_DurationChange(object sender, EventArgs e)
		{
			labelDuration.Text = wmp.Duration;
		}

		/// <summary>
		/// 動画を再生した
		/// </summary>
		void wmp_MovieStart(object sender, EventArgs e)
		{
			InitThreadSelected = false;

			// 動画サイズを合わせる
			if (FitSizeMovie)
			{
				ExeCommand("FitSizeMovie");
			}

			if (labelVolume.Text == "-")
			{
				wmp.Mute = true;
			}

			// チャンネル情報を更新
			ChannelInfoUpdate();
		}
	}
}