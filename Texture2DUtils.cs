using UnityEngine;
using System;

/// <summary>
/// Texture2Dの回転・拡大。縮小・拡大（縁で補完してサイズ変更）
/// </summary>
public class Texture2DUtils
{

    /// <summary>
    /// 回転したときや縁取りに補完する色
    /// </summary>
	public static Color fillColor = Color.black;

    /// <summary>
    /// 拡大（縁取りを補完してサイズ変更）
    /// </summary>
    /// <param name="srcTexture">元画像</param>
    /// <param name="dstWidth">拡大後の幅</param>
    /// <param name="destHeight">拡大後の高さ</param>
    /// <returns>枠つきTexture2D</returns>
	public static Texture2D expandTexture2D(Texture2D srcTexture, int dstWidth, int destHeight)
	{
		Texture2D dstTexture = new Texture2D(dstWidth, destHeight);
		Color[] dstPixcels = new Color[dstTexture.GetPixels().Length];
		int startW = (dstTexture.width - srcTexture.width) / 2;
		int endW = startW + srcTexture.width;
		int startH = (dstTexture.height - srcTexture.height) / 2;
		int endH = startH + srcTexture.height;		
		Color[] srcPixcel = srcTexture.GetPixels();

		int h = 0; //画像の高さの位置
		int w = 0; //画像のよこの位置
		int srcIndex = 0;
		for (int i = 0; i < dstPixcels.Length; i++)
		{
			//枠の中なら、Pixelかきこみ
			if ((w > startW && w <= endW)
			&& (h > startH && h <= endH))
			{
				dstPixcels[i] = srcPixcel[srcIndex];
				srcIndex++;
			}
			else
			{
				dstPixcels[i] = fillColor;
			}
			//横幅加算
			w++;
			if (w >= dstWidth)
			{
				h++; //高さ加算
				w = 0;
			}
		}
		dstTexture.SetPixels(dstPixcels);
		dstTexture.Apply();
		return dstTexture;
	}

	/// <summary>
	/// 指定した角度でTextureを回転する。
	/// </summary>
	/// <param name="srcTexture">元画像</param>
	/// <param name="rad">角度（ラジアン）</param>
	/// <returns>回転したTexture2D</returns>
	public static Texture2D tiltTexture2D(Texture2D srcTexture, double rad)
	{

		Texture2D dstTexture = new Texture2D(srcTexture.width, srcTexture.height);
		int dstWidth = dstTexture.width;
		int cx = dstWidth / 2;
		int destHeight = dstTexture.height;
		int cy = destHeight / 2;
		Color[] srcPxcel = srcTexture.GetPixels();
		Color[] dstPxcel = srcTexture.GetPixels();

		Vector2 DST = new Vector2();
		int srcIDX = 0;

		for (int i = 0; i < dstPxcel.Length; i++)
		{
			calcDstXY(i); //pixcelの位置をXY座標へ変換
			// こちらのサイトのアルゴを参考にしています。
			// http://yaju3d.hatenablog.jp/entry/2013/04/30/235007
			// x1 = (x2-cx) * cos(A) - (y2-cy) * sin(A) + cx 
			// y1 = (x2-cx) * sin(A) + (y2-cy) * cos(A) + cy
			int srcX = (int)((DST.x - cx) * Math.Cos(rad) - (DST.y - cy) * Math.Sin(rad)) + cx;
			int srcY = (int)((DST.x - cx) * Math.Sin(rad) + (DST.y - cy) * Math.Cos(rad)) + cy;
			calcSrcIDX(srcX, srcY);//XY座標をindexへ変換
			try
			{
				dstPxcel[i] = srcPxcel[srcIDX];
			}
			catch
			{
				dstPxcel[i] = fillColor;
			}
		}
		dstTexture.SetPixels(dstPxcel);
		dstTexture.Apply();
		return dstTexture;

		//変換前画像のピクセル位置をXY座標へ変換
		void calcDstXY(int idx)
		{
			var h = idx / dstWidth;
			var w = idx % dstWidth;
			DST.x = w;
			DST.y = h;
		}

		//元画像のXY座標をピクセル位置へ変換
		void calcSrcIDX(int x, int y)
		{
			srcIDX = dstWidth * y + x;
		}

	}

	/// <summary>
	/// 
	/// こちらのサイトのコードを借用させていただいてます。
	/// http://makegamemo.seesaa.net/article/448091338.html
	/// </summary>
	/// <param name="src">元画像</param>
	/// <param name="dst_w">変換後の幅</param>
	/// <param name="dst_h">変換後の高さ</param>
	/// <returns>リサイズされたTexyure2D</returns>
	public static Texture2D resizeTexture(Texture2D src, int dst_w, int dst_h)
	{
		Texture2D dst = new Texture2D(dst_w, dst_h, src.format, false);
		float inv_w = 1f / dst_w;
		float inv_h = 1f / dst_h;
		for (int y = 0; y < dst_h; ++y)
			for (int x = 0; x < dst_w; ++x)
				dst.SetPixel(x, y, src.GetPixelBilinear((float)x * inv_w, (float)y * inv_h));
        dst.Apply();
		return dst;
	}
}
