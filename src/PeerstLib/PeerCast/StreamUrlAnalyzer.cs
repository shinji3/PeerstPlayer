﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PeerstLib.PeerCast
{
	// ストリームURLから抽出した情報
	public class StreamUrlInfo
	{
		public string Host { get; set; }		// PeerCastのアドレス
		public string PortNo { get; set; }		// PeerCastのポート番号
		public string StreamId { get; set; }	// ストリームID
	}

	// ストリームURL解析クラス
	public static class StreamUrlAnalyzer
	{
		const string StreamUrlPattern = @"ttp://(.*):(.*)/pls/(.*)?tip=";
		const int GroupCount = 4;

		// ストリームURL情報の取得
		public static StreamUrlInfo GetUrlInfo(string streamUrl)
		{
			StreamUrlInfo info = new StreamUrlInfo();
			Regex regex = new Regex(StreamUrlPattern);
			Match match = regex.Match(streamUrl);

			if (match.Groups.Count == GroupCount)
			{
				info.Host = match.Groups[1].Value;
				info.PortNo = match.Groups[2].Value;
				info.StreamId = match.Groups[3].Value.Substring(0, match.Groups[3].Value.Length - 1);
			}

			return info;
		}
	}
}
