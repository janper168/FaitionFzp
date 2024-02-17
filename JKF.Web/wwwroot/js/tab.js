(function ($, jkf){

	jkf.currentIframe = function() {
		var iframeId = jkf.iFrameId;
		//alert(iframeId);
		if (top.frames[iframeId].contentWindow != undefined) {
			return top.frames[iframeId].contentWindow;
		}
		else {
			return top.frames[iframeId];
		}
	};

	jkf.iFrameId = '';
	
	//触发点击tab项函数
	var trigerClickTab = function(target) {	

		var id = $(target).attr("id").replace('tab_','iframe_');
		var url = $(target).data("url");//获取url	
		var name = $(target).data("name");
		$existcontentframe = $(".content-frame").getFirstJqObj("content-frame").children("iframe[src='"+url+"']");//获取已存在的iframe

		//console.log("existcontentframe:" + $existcontentframe.length);

		if ($existcontentframe.length === 1) { //如果已经存在url的iframe页			
			$existcontentframe.fadeIn(500).siblings("iframe").hide();//其他的iframe页隐藏
			$(target).addClass("selected").siblings("span").removeClass("selected");//tab标签选中，其他tab标签不选中
		}else if ($existcontentframe.length === 0){
			//如果不存在iframe页，则创建新的iframe页并显示					
			$(".content-frame").getFirstJqObj("content-frame").children("iframe").hide();
			var htmlIframe = '<iframe id="' + id + '" src="'+url+'" frameborder=0 scrolling="auto" width="100%" height="88%" style="background-color:#eeeeee"></iframe>';
			var $jqHtml = $(".content-frame").getFirstJqObj("content-frame").append($(htmlIframe));
			$jqHtml.fadeIn(500);
			$(target).addClass("selected").siblings("span").removeClass("selected");


			$.httpPostAsync({
				url: "/Base/Log/VisitModule",
				param: { moduleName: name, moduleUrl: url }
			},
				function (res) {
					if (res.code != 200) {
						var _topUrl = '/Login/Index';
						top.window.location.href = _topUrl;
						return;
					}
				}			
			);

		}
		jkf.iFrameId = id;
	}
	
	//点击tab页事件
	var clickTab = function(e)  {

		var target = e.target;
	    var url = $(e.target).data("url");//target对象有url属性
		if (!url) {
			url = $(e.target).parent().data("url");
			target = $(e.target).parent().get(0);
		}
		
		trigerClickTab(target);
	}
	
	//关闭table页事件
	var closeTab = function (e) {
		e.preventDefault();
		e.stopPropagation();
		//console.log(e.target);
		var url = $(e.target).parent().data("url");
		$(".content-frame").getFirstJqObj("content-frame").children("iframe[src='"+url+"']").remove();

		$removeTab = $(e.target).parent();
		if ($removeTab.prev().length > 0) {
			trigerClickTab($removeTab.prev().get(0));
		}
		$removeTab.remove();

	}
	
	//tab项向左移动的按钮
	var leftTabClick = function(e){
		e.preventDefault();
		e.stopPropagation();
		var $group = $("span.tab-center-group").eq(0);
		
		//按钮组的长度
		var groupWidth = $group.width();
		
		//按钮组头尾的相对位置
		var groupLeftPos = parseInt($group.css("left"));
		var groupRightPos = parseInt($group.css("right"));
				
		if (groupRightPos < 40) { 
			if (groupLeftPos <= 40 ){
			
				var $childrenItems = $group.children("span.tab-item-to-frame");
				var groupMoveLength = 0;
				var groupMoveWidth = 0;
				var $item = null;

				var leftPos = 0;
				var rightPos = 0;	
				leftPos = groupLeftPos;
				for (var i = 0; i < $childrenItems.length; i++){
					$item = $childrenItems.eq(i);
					rightPos = leftPos + $item.outerWidth() + 4;//包含margin在内的宽度
					
					//如果这个tab按钮落在区间内
					if (leftPos < 40 && rightPos > 40) {										
						groupMoveLength = rightPos - 40;
						break;
					}else if (leftPos == 40){
						groupMoveLength = -($item.outerWidth() + 4); 
						break;
					}else{	
						leftPos = rightPos;//下一个
						continue;
					}
					
				}	
			    $group.animate({left:(groupLeftPos+groupMoveLength) + "px"},100);
			}
		}		
	}
	
	//tab项向右移动的按钮
	var rightTabClick = function(e){

		e.preventDefault();
		e.stopPropagation();
		var $group = $("span.tab-center-group").eq(0);
		//按钮组的长度
		var groupWidth = $group.width();
		
		//按钮组头尾的相对位置
		var groupLeftPos = parseInt($group.css("left"));
		var groupRightPos = parseInt($group.css("right"));

		if (groupLeftPos < 40 ){
					
			var $childrenItems = $group.children("span.tab-item-to-frame");
			var groupMoveLength = 0;
			var groupMoveWidth = 0;
			var $item = null;
			
			var leftPos = 0;
			var rightPos = 0;	
			leftPos = groupLeftPos;
			for (var i = 0; i < $childrenItems.length; i++){
				$item = $childrenItems.eq(i);
				rightPos = leftPos + $item.outerWidth() + 4;//包含margin在内的宽度
				
				
				//如果这个tab按钮落在区间内
				if (leftPos < 40 && rightPos > 40) {										
					groupMoveLength = $item.outerWidth() - rightPos + 40;
					break;
				}else if (rightPos == 40){
					groupMoveLength = $item.outerWidth() + 4; 
					break;
				}else{	
					leftPos = rightPos;//下一个
					continue;
				}
				
			}	

			$group.animate({left:(groupLeftPos+groupMoveLength) + "px"},100);
			
		}	
	}

	//在tab组中click tab项
	var clickTabInGroup = function($tabGroup, id, url, name) {
		
		var $exist = $tabGroup.find("span[data-url='" + url + "']");

		//console.log("data-url length:" + $exist.length);
		//console.log("id:" + id);
		//console.log("url:" + url);
		//console.log("name:" + name);

		if ($exist.length > 0) {
			
			var $curTab = null;
			var existTabName = '';
			for (var i = 0; i < $exist.length; i++){
				existTabName = $exist.eq(i).text();				
				if (existTabName === name) {
					$curTab = $exist.eq(i);
					break;
				}
			}
						
			if ($curTab) {
				$curTab.data("name", name);
				trigerClickTab($curTab.get(0));
				return true;
			}			
		}
		return false;
	}
		
	//tab按钮事件的设置
	$.fn.setMainFrame = function () {
		
		//解绑
		$("span.tab-item-to-frame").off("click",clickTab);
		$("span.tab-item-to-frame .fa-close").off("click",closeTab);
		$("span.tab-left-tap").off("click",leftTabClick);
		$("span.tab-right-tap").off("click",rightTabClick);
		
		//绑定
		$("span.tab-item-to-frame").click(clickTab);
		$("span.tab-item-to-frame .fa-close").click(closeTab);		
		$("span.tab-left-tap").click(leftTabClick);
		$("span.tab-right-tap").click(rightTabClick);
		return this;
	}
	
	//菜单项的点击函数
	$.fn.addOrShowTab = function(htmlText) {

		console.log("htmlText:" + htmlText);

		var idMatch = htmlText.match(/id=".+?"/g)
		//console.log("0:" + idMatch[0]);
		id = idMatch[0].replace(/id="/g, '').replace(/"/, '');

		var iconMatch = htmlText.match(/fa-([\w-]+)"/g);
	    var icon = iconMatch[0].replace(/"/g,'');
		//console.log(icon);
		
		var name = htmlText.match(/<\/i>(.+)/g)[0];
		name = name.replace(/<\/i>/g,'').replace('</span>','');
		//console.log(name);

		var urlMatch = htmlText.match(/data-url="(.+)"/g);
		//console.log(urlMatch);
		
		var url = '';
		if (urlMatch && urlMatch.length === 1) {
			url = urlMatch[0];
			//console.log(url);
			url = url.replace(/data-url="/g,'').replace(/"/g,'');
			if (!url || url === 'undefined' ) {
				//console.log('非叶子节点');
				return;
			}
			//console.log(url);
		}else{
			//console.log('非叶子节点');
			return false;
		}

		var $tabGroup = $(".tab-center-group",$(this));
		
		if (clickTabInGroup($tabGroup, id, url, name)) return true;
		
		var tabItemHtml = '<span id="tab_' + id + '" class="tab-item-to-frame" data-url="'+url+'"><i class="tab-icon fa ' + icon + '"></i>'+name+'<i class="fa fa-close"></i></span>';

		$tabGroup.append($(tabItemHtml));
		$(this).setMainFrame();
		
		return clickTabInGroup($tabGroup, id, url, name);
	}


})(window.jQuery,top.jkf);

