# texture2dUtils
Utility to enlarge / reduce / rotate / border Unity's Texture2D

# UnityのTexture2Dを編集するUtilityです。


以下の機能があります。

## 1.縁取り
　元の画像にたいして指定したサイズで拡大します。
 （キャンパスサイズの変更イメージ）

```
    var tex = Texture2DUtils.expandTexture2D(srcTexture2D, srcTexture2D.width + 24 , srcTexture2D.height + 24 );
```
 
## 2.回転
　元の画像を中心点を軸に指定された角度で変換します。

```
    var tex = Texture2DUtils.tiltTexture2D(srcTexture2D, (double)( 45 * Math.PI / 180));
```
 
## ３.拡大・縮小
　元の画像を指定されたサイズで拡大縮小します。

```
    var tex = Texture2DUtils.resizeTexture(srcTexture2D, 1024 , 1024 );
```

## 注意事項

　・ Textureのinspectorでread/write Enabled は　チェックしておいてください。

　・ サイズの小さい画像を回転すると、ジャギーがでます。この場合、resizeTextureメソッドで拡大した画像を回転させて元のサイズにresizeTextureメソッドで縮小するとジャギーが抑えられます。
