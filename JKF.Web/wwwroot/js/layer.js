(function ($, jkf) {

	$.extend(jkf, {

		

		addMaskLayer: function (boxId) {
			var htmlText = "<div class='maskLayer' id='maskLayer_" + boxId+"'></div>";

			jkf.$topBody.append(htmlText);
			var $maskLayer = jkf.$topBody.find("div[id=maskLayer_" + boxId+"]");
			$maskLayer.width(jkf.$topBody.width());
			$maskLayer.height(jkf.$topBody.height());
			$maskLayer.show();
			return $maskLayer; 
		},

		closeMaskLayer: function (boxId) {
			var $maskLayer = jkf.$topBody.find("div[id=maskLayer_" + boxId+"]");
			$maskLayer.remove();
		},
		GetIFrameWindowData :function($openFrame) {
			return $openFrame.data("data");
		},
		openFrame: function (options) {
			var boxId = jkf.newGuid();
			var $iFrame = jkf.GetIFrameWindow(boxId,
				options.parentFrameId,
				options.title,
				options.width,
				options.height,
				options.url,
				options.hasMinButton,
				options.hasMaxButton,
				options.hasCloseButton,
				options.isWazard,
				options.buttonGroup
			);

			$iFrame.data("data", options.data);


			var frameLevel = options.level;
			var addonZIndexValue = 0;
			for (var i = 0; i < frameLevel; i++) {
				addonZIndexValue += 100;
			}
				
			var iFrameZindex = parseInt($iFrame.css("z-index")) + addonZIndexValue;
			var maskLayerZindex = iFrameZindex - 1;

			$maskLayer = this.addMaskLayer(boxId);
			$maskLayer.css("z-index", maskLayerZindex);
			$iFrame.css("z-index", iFrameZindex);
			$iFrame.show();
			
		},

		confirmFrame: function (text, callback) {
			var frameHtmlText = "<div class='confirmFrame'></div>";
			$("body").append(frameHtmlText);

			var $confirmFrame = $("body").find("div[class=confirmFrame]");
			$confirmFrame.append(text);

			var bottomLineHtmlText = "<span class='bottomLine'></span>";
			$confirmFrame.append(bottomLineHtmlText);

			var $bottomLine  = $confirmFrame.find(".bottomLine");

			$bottomLine.append("<button class='btn blue' id='btn_yes'>是</button");
			$bottomLine.append("<button class='btn blue' id='btn_no'>否</button");

			$btn_yes = $bottomLine.find("#btn_yes");
			$btn_no = $bottomLine.find("#btn_no");

	
			var boxId = jkf.newGuid();
			var $maskLayer = jkf.addMaskLayer(boxId);

			var confirmFrameZindex = parseInt($confirmFrame.css("z-index"));
			$maskLayer.css("z-index", confirmFrameZindex - 1);


			$btn_yes.click(function () {
				callback();
				jkf.closeMaskLayer(boxId);
				$confirmFrame.remove();
			});

			$btn_no.click(function () {
				jkf.closeMaskLayer(boxId);
				$confirmFrame.remove();
			});

			var topPos = (jkf.$topBody.height() - 130) / 2;
			var leftPos = (jkf.$topBody.width() - 260) / 2;

			$confirmFrame.css('left', leftPos);
			$confirmFrame.css('top', topPos);

		},

		loadingFrame: function (isOpening,text,maskBoxId) {
			//alert(2);
			if (isOpening == false) {
				jkf.$topBody.find(".loadingFrame").remove();
				jkf.closeMaskLayer(maskBoxId);
				return;
			}

			var frameHtmlText = "<div class='loadingFrame'></div>";
			$("body").append(frameHtmlText);

			var $loadingFrame = $("body").find(".loadingFrame");

			$loadingFrame.append("<i class='fa fa-refresh rotate' style='margin-bottom:-3px;'></i>&nbsp;" + text);

			var boxId = jkf.newGuid();
			var $maskLayer = jkf.addMaskLayer(boxId);

			var loadingFrameZindex = parseInt($loadingFrame.css("z-index"));
			$maskLayer.css("z-index", loadingFrameZindex - 1);

			var topPos = (jkf.$topBody.height() - 80) / 2;
			var leftPos = (jkf.$topBody.width() - 260) / 2;

			$loadingFrame.css('left', leftPos);
			$loadingFrame.css('top', topPos);
			$loadingFrame.width(260);
			$loadingFrame.height(80);

			//if (isOpening == true) {
				//$loadingFrame.show();
			//}

			return boxId;
		},

		showTips : function (text,type) {
			var millSeconds = 4000

			var boxId = jkf.newGuid();
			var $tipBox = jkf.getTipsBox(boxId, text, type);
			//console.log(boxId);
			$tipBox.fadeIn();
			setInterval(function () {
				$tipBox.fadeOut();
				$tipBox.remove();
			}, millSeconds);
			

		},

		getTipsBox : function (boxId, text, type) {
			var colorCss = "";

			if (type == 1) {
				colorCss = "green";
			} else if (type == 2) {
				colorCss = "red";
			} else if (type == 3) {
				colorCss = "orange";
			}

			var htmlText = "<div class='" + colorCss + " tipBox' id='" + boxId + "'></div>";
			jkf.$topBody.append(htmlText);

			var $tipBox = jkf.$topBody.find("div[id='" + boxId + "']");
			//console.log(jkf.$topBody);
			$tipBox.hide();
			$tipBox.append("<div class='textContext'>" + text + "</div>");

			var topPos = 10;
			leftPos = (jkf.$topBody.width() - 260) / 2;

			$tipBox.css('left', leftPos);
			$tipBox.css('top', topPos);

			return $tipBox;
		},
		GetIFrameWindow: function (	
			boxId,
			parentFrameId,
			title,
			width,
			height,
			url,
			hasMinButton,
			hasMaxButton,
			hasCloseButton,
			isWazard,
			buttonGroup) {

			var htmlText = "<div class='frameWindow' id=frameWindow_" + boxId + "></div>";
			jkf.$topBody.append(htmlText);

			var $window = jkf.$topBody.find("div[id=frameWindow_" + boxId + "]");

			//设置窗体的宽度
			var topWidth = jkf.$topBody.width();
			var topHeight = jkf.$topBody.height();

			var winWidth = 0;
			var winHeight = 0;

			//console.log(width + "," + height);

			if (!width) {
				winWidth = topWidth - 10;
			} else if (width >= topWidth) {
				winWidth = topWidth - 10;
			} else if (width < topWidth) {
				winWidth = width;
			}

			if (!height) {
				winHeight = topHeight - 10;
			} else if (height >= topHeight) {
				winHeight = topHeight - 10;
			} else if (height < topHeight) {
				winHeight = height;
			}

			$window.width(winWidth);
			$window.height(winHeight);

			//设置窗体位置
			var topPos = (topHeight - winHeight) / 2;
			var leftPos = (topWidth - winWidth) / 2;

			$window.css('left', leftPos);
			$window.css('top', topPos);

			var titleBar = "<div class='titleBar'></div>";
			$window.append(titleBar);
			var $titleBar = $window.find(".titleBar");

			var titleText = "<span class='titleText'>" + title + "</span>";
			$titleBar.append(titleText);

			var buttonGroupText = "<span class='win_title_buttonGroup'></span>";
			$titleBar.append(buttonGroupText);
			var $buttonGroup = $titleBar.find(".win_title_buttonGroup");

			if (hasMaxButton) {
				$buttonGroup.append("<button class='btn' id='win_MaxButton'><i class='fa fa-window-maximize'></i></button")

				var $win_maxButton = $buttonGroup.find("#win_MaxButton");
				$win_maxButton.click(function (e) {

					if (winWidth == width) {
						winWidth = topWidth - 10;
					} else {
						winWidth = width;
					}

					if (winHeight == height) {
						winHeight = topHeight - 10;
					} else {
						winHeight = height;
					}

					$window.width(winWidth);
					$window.height(winHeight);

					//设置窗体位置
					var topPos = (topHeight - winHeight) / 2;
					var leftPos = (topWidth - winWidth) / 2;

					$window.css('left', leftPos);
					$window.css('top', topPos);

					$("#iframe_" + boxId).attr("height", $window.height() - 80 - 2);

				})
			}

			if (hasCloseButton) {
				$buttonGroup.append("<button class='btn' id='win_CloseButton'><i class='fa fa-window-close'></i></button")

				var $win_closeButton = $buttonGroup.find("#win_CloseButton");
				$win_closeButton.click(function (e) {
					$window.remove();
					jkf.closeMaskLayer(boxId);
				})
			}

			var bottomBar = "<div class='bottomBar'></div>";
			$window.append(bottomBar);
			var $bottomBar = $window.find(".bottomBar");

			if (!!buttonGroup && buttonGroup.length > 0) {
				var btnGroupText = "<span class='btn_group'></span>"
				$bottomBar.append(btnGroupText);
				var $btnGroup = $bottomBar.find(".btn_group");


				for (var i in buttonGroup) {
					var buttonInfo = buttonGroup[i];
					var btnId = buttonInfo.buttonId;
					var buttonText = buttonInfo.buttonText;
					var buttonAction = buttonInfo.buttonClickAction;
					$btnGroup.append("<button id='" + btnId + "' class='btn blue'>" + buttonText+"</button>");

					var $btn = $btnGroup.find("#" + btnId);
					$btn.click(buttonAction);

				}
			}

			var iframeText = "<iframe id='iframe_" + boxId + "' class='iframe_content' frameborder=0 scrolling='auto' width=100% src='" + url + "' data-parentId='" + parentFrameId + "'></iframe>";
			$window.append(iframeText);


			$("#iframe_" + boxId).attr("height", $window.height() - 80 - 2);
		
			return $window;
		}

	});

	
})(jQuery, top.jkf);