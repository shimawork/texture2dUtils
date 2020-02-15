# texture2dUtils
Utility to enlarge / reduce / rotate / border Unity's Texture2D

# UnityのTexture2Dを編集するUtilityです。


以下の機能があります。

## 1.縁取り
　元の画像にたいして指定したサイズで拡大します。
 （キャンパスサイズの変更イメージ） 

```
//128ピクセルの枠で囲う
var dstTex = Texture2DUtils.expandTexture2D(srcTex, srcTex.width + 128 , srcTex.height + 128 );
```


## 2.回転
　元の画像を中心点を軸に指定された角度で変換します。

```
//４５度傾ける
var dstTex = Texture2DUtils.tiltTexture2D(srcTex, (double)( 45 * Math.PI / 180));
```


## ３.拡大・縮小
　元の画像を指定されたサイズで拡大縮小します。

```
//256*256 に リサイズ
var dstTex = Texture2DUtils.resizeTexture(srcTex, ２５６ , ２５６ );
```

## 注意事項

　・ Textureのinspectorでread/write Enabled は　チェックしておいてください。

　・ サイズの小さい画像を回転すると、ジャギーがでます。この場合、resizeTextureメソッドで拡大した画像を回転させて元のサイズにresizeTextureメソッドで縮小するとジャギーが抑えられます。
``` 
//ちいさい画像のジャギーの抑え方
var dstTex = Texture2DUtils.resizeTexture(srcTex, 1024 , 1024 ); //大きくして
dstTex = Texture2DUtils.tiltTexture2D(dstTex, (double)( 45 * Math.PI / 180)); //まわして
dstTex = Texture2DUtils.resizeTexture(dstTex, srcTex.width , srcTex.height ); //元の大きさに戻す。
```
