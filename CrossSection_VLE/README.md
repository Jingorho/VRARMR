# Cross-Section Virtual Learning Environment
図形の断面図をデモできるVLE．

<p align="center">
  <img src="/img/mainImg.png">
</p>




## Features:
<p>仮想空間内で板を自由に動かすことで，3次元図形を板で切ったときの断面図を観察できる．</p> 
<p>開発環境: Unity2018.1.1f1 / htc VIVE / macOS High SierraとWindows10</p>


### [操作説明](/img/usersGuide.png)
- VIVEのコントローラで  
	コントローラが板に触れると板の色が少し濃くなる -> 人差し指部分のトリガーを引くと掴んで動かせる
	[コツ]  
	トリガーが効かなくなったら，いったんコントローラを板の外に外して衝突判定を外してから，再度コントローラを板の中に入れて衝突判定を生じさせると改善する場合が多い．

- キーボード操作で  
	↑↓キーと←→キーとz,xキーで回転．Shiftキーを押しながらだと移動．

- その他主なキーボード操作  
	- よく使う
		- D: デフォルトモード
		- S: 軸固定モード(軸は決まった位置に固定)
		- O: 軸移動モード
		- P: 移動した軸での固定モード
		- R: 板の位置リセット
		- 1: 立方体の表示/非表示の切り替え
		- 2: 正四角錐の表示/非表示の切り替え
		- 3: 正四面体の表示/非表示の切り替え

	- あんまり使わない
		- W: 切り口表示/非表示切り替え //開発版．不完全 (2018.07.24)
		- C: 図形文字の表示/非表示切り替え
		- V: 中点の表示/非表示切り替え
		- G: ガイド文字の表示/非表示切り替え	
		- 4: 円錐の表示/非表示の切り替え
		- 5: 円柱の表示/非表示の切り替え
		- 6: 直方体の表示/非表示の切り替え
		- 7: 球の表示/非表示の切り替え




## [デモ](https://youtu.be/oMZO4tNokEQ)
//開発版．不完全 (2018.07.24)




## 参考，メモ
- 断面図のレンダリング: [断面図シェーダー](https://github.com/Dandarawy/Unity3DCrossSectionShader)
- プリミティブ図形のモデリング: [Blender](https://www.blender.org/)
- 図形のテクスチャ: [透過テクスチャ](http://neareal.net/index.php?ComputerGraphics%2FUnity%2FTips%2FEnableTransparentTexture)
- 軸固定: [射影ベクトル](https://docs.unity3d.com/ja/current/ScriptReference/Vector3.Project.html)
- 断面図カメラ: [Render Texture](http://mikasa.hatenablog.jp/entry/2015/05/05/051008)

とか．随時更新...


