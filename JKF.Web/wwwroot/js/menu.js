(function($){
	
	
	var setDynamicMenuUpEvent = function(e) {

		e.preventDefault();
		e.stopPropagation();
		
		var $menuItems = $(this).parent().children(".custom-submenu-dynamic-item");

		var length = $menuItems.length; //所有item项的长度
		var topPos = 0;

		//找到第一个显示item的index为止
		var firstShowIndex = 0;
		for (var i = 0; i < length; i++) {
			if ($($menuItems[i]).is(":hidden")) {
				firstShowIndex++;
			}else {
				break;
			}	
		}
		
		var downLeftLength = length - 10 - firstShowIndex; //隐藏在底部不可见的item的长度
		if(downLeftLength > 0){
			$($menuItems[firstShowIndex]).hide(100);
			$($menuItems[firstShowIndex + 10]).show(100);
			//$menuItems.each(function (index) {		
				//console.log($(this).css("top"));
				//$(this).animate({ top: (parseInt($(this).css("top")) - 30) + "px" }, 100);
			//});
		}
			
	};
				
	var setDynamicMenuDownEvent = function(e) {
			
		e.preventDefault();
		e.stopPropagation();
			
		var $menuItems = $(this).parent().children(".custom-submenu-dynamic-item");
		var length = $menuItems.length; //所有item项的长度

		var topPos = 0;

		//找到第一个显示item的index为止
		var firstShowIndex = 0;
		for (var i = 0; i < length; i++) {
			if ($($menuItems[i]).is(":hidden")) {
				firstShowIndex++;
			}else {
				break;
			}	
		}

		if (firstShowIndex > 0 && length - 10 - firstShowIndex >= 0){
			$($menuItems[firstShowIndex - 1]).show(100);
			$($menuItems[firstShowIndex + 9]).hide(100);
			//$menuItems.each(function (index) {
				//console.log($(this).css("top"));
				//$(this).animate({ top: (parseInt($(this).css("top")) + 30) + "px"},100)
			//});
		}
			
	};
	
	//设置弹性菜单的开关功能
	$.fn.setFlexMenuTap = function(options){	
			
		var $onlyFirstObj = $(this).getFirstJqObj('flex-menu-tap');

		//默认的配置属性
		var _default = {
			menuData:null,
			menu:".custom-menu-group",
			mainframe:".main-frame"
		}
		var configs = $.extend({}, _default, options);
		
		var $menu = $(configs.menu).getFirstJqObj('custom-menu-group'); //菜单
		var $mainframe = $(configs.mainframe).getFirstJqObj('main-frame'); //主框架
		var menuData = configs.menuData; //菜单数据
		
		var frameWidth = 0; //主框架宽度
		var menuWidth = 0;  //菜单宽度
		
		//主窗体尺寸重绘回调
		configs.bodyResizeCallBack = function($that) {
			if ($that.attr("data-content-before") === "«"){
				menuWidth = $('body').width() * 0.13;
				frameWidth = $('body').width() * 0.87 - 1; //减1是因为menu右边框为1个像素
				$that.parent().parent().width(menuWidth);
				$mainframe.width(frameWidth);
				$menu.setCustomMenuContent({menuType:'normal',menuData:menuData,mainframe:configs.mainframe});
				
			}else{
				menuWidth = $('body').width() * 0.05;
				frameWidth = $('body').width() * 0.95 - 1;
				$that.parent().parent().width(menuWidth);
				$mainframe.width(frameWidth);
				$menu.setCustomMenuContent({menuType:'flow',menuData:menuData,mainframe:configs.mainframe});			
			}
		}
		
		$(window).resize(function(){
			configs.bodyResizeCallBack($onlyFirstObj);
		});

		$onlyFirstObj.on("click", function(){
			 if ($(this).attr("data-content-before") === "«"){				
				menuWidth = $('body').width() * 0.05;
				frameWidth = $('body').width() * 0.95 - 1;
				$(this).attr("data-content-before","»");
				$(this).parent().parent().animate({width: menuWidth},100,function(){
					$(this).find(".flex-menu-tap").removeClass("menu-right-align").addClass("menu-center-align").attr('title','展开');										
				});
				$mainframe.animate({width: frameWidth},100);
				
				$menu.setCustomMenuContent({menuType:'flow',menuData:menuData,mainframe:configs.mainframe});	
				
			 } else {
				 menuWidth = $('body').width() * 0.13;
				 frameWidth = $('body').width() * 0.87 - 1;
				 $(this).attr("data-content-before","«");
				 $(this).parent().parent().animate({width: menuWidth},100,function(){
					 $(this).find(".flex-menu-tap").removeClass("menu-center-align").addClass("menu-right-align").attr('title','收缩');

				 });
				 $mainframe.animate({width: frameWidth},100);

				 $menu.setCustomMenuContent({menuType:'normal',menuData:menuData,mainframe:configs.mainframe});
				 
			 }
		});
		
		configs.bodyResizeCallBack($onlyFirstObj);
		
		return this;
	};
	
	//设置菜单的内容功能
	$.fn.setCustomMenuContent = function(options) {
		
		if ($(this).length !== 1){
			$.consoleAndAlertLog("menu菜单只能是单例！");
			return false;
		}
		
		var $mainframe = $(options.mainframe);
		var _default = {
			//isInDynamicMenu : false,
			//设置动态菜单的项的click事件
			setMenuItemDynamicOnClick: function(e){
				e.preventDefault();
				e.stopPropagation();
				if ($(this).children().eq(0).hasClass("left")){
								
					var $parent = $(this).children().eq(0).parent();
					if ($parent.find(".right").length === 0) { //如果是叶子节点触发菜单项点击
						var htmlText = $(this).children().eq(0).parent().html();
						$mainframe.addOrShowTab(htmlText);
					}
				}
			},
			//设置普通菜单项的click事件
			setMenuItemOnClick: function(){
				if ($(this).hasClass("left")) {

					var $parent = $(this).parent();
					if ($parent.find(".right").length == 0) { //如果是叶子节点触发菜单项点击
						var htmlText = $(this).parent().html();
						$mainframe.addOrShowTab(htmlText);	
					}
				}
				
				if ($(this).hasClass("hide")){	
					$(this).parent().addClass("expand");
					$(this).parent().siblings(".custom-menu-item").removeClass("expand");
					$(this).siblings(".custom-submenu-group").slideDown(200).end().removeClass("hide").addClass("show").parent().siblings().find(".custom-submenu-group").hide(200).siblings("span").removeClass("show").addClass("hide");
					if ($(this).hasClass("right")){
						$(this).html("<i class='fa fa-chevron-up'></i>").attr('title','收起');
					}else{
						$(this).siblings(".right").html("<i class='fa fa-chevron-up'></i>").attr('title','收起');
					}
					$(this).parent().siblings().find("span.right").attr("title",'展开').html("<i class='fa fa-chevron-down'></i>");

				}else if ($(this).hasClass("show")){
					$(this).parent().removeClass("expand");
					$(this).parent().siblings(".custom-menu-item").removeClass("expand");
					$(this).siblings(".custom-submenu-group").hide(200).siblings("span").removeClass("show").addClass("hide").parent().siblings().find(".custom-submenu-group").hide(200).end().removeClass("show").addClass("hide");
					if ($(this).hasClass("right")){
						$(this).html("<i class='fa fa-chevron-down'></i>").attr('title','展开');
					}else{
						$(this).siblings(".right").html("<i class='fa fa-chevron-down'></i>").attr('title','展开');
					}
					$(this).parent().siblings().find("span.right").attr("title",'展开').html("<i class='fa fa-chevron-down'></i>");
				}					
			},
			//创建普通菜单的子菜单
			createSubMenu:function(children, recursionOrderNumber){
				var retVal = '';
				if (children && children.length > 0){
					retVal = '<ul class="custom-submenu-group">';
					for (var i = 0; i < children.length; i++){
						var subMenuData = children[i];
						retVal += '<li class="custom-submenu-item"><span id="' + subMenuData.menuId + '" class="hide left" style="text-indent:' + recursionOrderNumber * 10 + 'px"><i class="fa ' + subMenuData.icon + '" data-url="' + subMenuData.url + '"></i>' + subMenuData.menuName + '</span>';
						if (subMenuData.children && subMenuData.children.length > 0) {
							retVal += '<span class="hide right" title="展开"><i class="fa fa-chevron-down"></i></span>';
						}
								
						retVal+= this.createSubMenu(subMenuData.children,recursionOrderNumber + 1);
						
						retVal += '</li>';
					}
					retVal+="</ul>"
				}
				return retVal;
			},
			//创建普通菜单
			createMenu:function($that, menuData){
				var strVal = '';
				$that.html('');
				for (var i = 0; i < menuData.length; i++){
					var itemData = menuData[i];

					strVal += '<li class="custom-menu-item">';
					strVal += '<span id="' + itemData.menuId + '" class="hide left"><i class="fa ' + itemData.icon + '"></i>' + itemData.menuName + '</span>';
					strVal += '<span class="hide right" title="展开"><i class="fa fa-chevron-down"></i></span>';
					strVal += this.createSubMenu(itemData.children, 1);
					strVal+='</li>';
				}
				$that.append($(strVal));
			},
			
			//设置顶部动态菜单的菜单项事件
			setTopDynamicMenuItemEvent:function(e){
				//e.preventDefault();
				//e.stopPropagation();
				//console.log(2);
				var bodyHeight = $('body').height();
				//console.log("bodyHeight:" + bodyHeight);
				 
				var top = $(this).offset().top;
				var left = $(this).offset().left + 50;
				
				//console.log("top:" + top);
				//console.log("left:" + left);
				
				var topToBottomHeight = bodyHeight - top;
				//console.log("topToBottomHeight:" + topToBottomHeight);				

				var $triangle = $(this).parent().siblings("div.triangle");
				
				$(this).parent().parent().siblings().children('.custom-submenu-dynamic-group').hide();
				
				var $contextMenu = $(this).parent().parent().children('.custom-submenu-dynamic-group');
				if ($contextMenu.length <= 0) {
					return false;
				}
				var isDynamicMenuShow = $contextMenu.data("isDynamicMenuShow");
				if (!isDynamicMenuShow) { 					
					$contextMenu.data("isDynamicMenuShow", true);					
				}else{
					$contextMenu.show();
					return true;
				}
				
				//$contextMenu = $($contextMenu[0]);
				
				var $upMenuItem = $contextMenu.find("li.custom-submenu-dynamic-direction>span.up");
				var $downMenuItem = $contextMenu.find("li.custom-submenu-dynamic-direction>span.down");	
								
				var $menuItems = $contextMenu.children(".custom-submenu-dynamic-item");
		
				if ($menuItems.length > 10) {
					$upMenuItem.parent().show();
					$downMenuItem.parent().show();

					var contextHeight = $contextMenu.height();

					$upMenuItem.parent().off("click",setDynamicMenuUpEvent);
					$upMenuItem.parent().click(setDynamicMenuUpEvent);
					
					$downMenuItem.parent().off("click",setDynamicMenuDownEvent);					
					$downMenuItem.parent().click(setDynamicMenuDownEvent);
					
					$menuItems.each(function(index){
						if (index > 10) {
							$(this).hide();
						}else{
							$(this).show();
						}
					});
												
				    //console.log("contextHeight:" + contextHeight);
					
					if (contextHeight <= topToBottomHeight) {
						$contextMenu.css('left',left);
						$contextMenu.css('top', top);

						$triangle.css('left', left - 18);
						$triangle.css('top', top + 5);

						//console.log("1");
					} else {
						$contextMenu.css('left',left);
						$contextMenu.css('top', top - contextHeight + + 20);

						$triangle.css('left', left - 18);
						$triangle.css('top', top - 10);
						//console.log("2");
					}

				}else{
				    //console.log("contextHeight:" + contextHeight);
					$upMenuItem.parent().hide();
					$downMenuItem.parent().hide();

					var contextHeight = $contextMenu.height();

					if (contextHeight <= topToBottomHeight) {
						$contextMenu.css('left',left);
						$contextMenu.css('top', top);

						$triangle.css('left', left - 18);
						$triangle.css('top', top + 5);
						//console.log("1");
					} else {
						$contextMenu.css('left',left);
						$contextMenu.css('top', top - contextHeight + 20);

						console.log("left1:" + left + ",top1:" + top);

						//console.log("2");
						$triangle.css('left', left - 18);
						$triangle.css('top', top - 10);
					}
					
				}
				$triangle.show(50);
				$contextMenu.show(50);
			},
			//设置上下文菜单的事件
			setContextMenuEvent:function(e){
				//e.preventDefault();
				//e.stopPropagation();
				$(this).hide();	
				$(this).data("isDynamicMenuShow",false);
			},
			setContextMenuItemEvent:function(e){
				//e.preventDefault();
				//e.stopPropagation();
				var $contextMenu = $(this).children('.custom-submenu-dynamic-group');
				if ($contextMenu.length > 0) {				
					$contextMenu.hide();
					$contextMenu.data("isDynamicMenuShow",false);
				}
				var $triangle = $(this).find("div.triangle");
				if ($triangle.length > 0) {
					$triangle.hide();
				}

			},
			//设置动态菜单项事件
			setDynamicMenuItemEvent:function(e){
				//e.preventDefault();
				//e.stopPropagation();	
				//console.log(2);
				var top = $(this).offset().top;
				var left = $(this).offset().left;

				var bodyHeight = $('body').height();

				var topToBottomHeight = bodyHeight - top;
								
				var $nextContextMenu = $(this).children(".custom-submenu-dynamic-group");
				if ($nextContextMenu.length <= 0) {
					return false;
				}

				var isDynamicMenuShow = $nextContextMenu.data("isDynamicMenuShow");
				if (!isDynamicMenuShow) { 					
					$nextContextMenu.data("isDynamicMenuShow", true);					
				}else{
					$nextContextMenu.show();
					return true;
				}
					
				$(this).siblings(".custom-submenu-dynamic-item").children(".custom-submenu-dynamic-group").hide(100);
				
				var $upMenuItem = $nextContextMenu.find("li.custom-submenu-dynamic-direction>span.up");
				var $downMenuItem = $nextContextMenu.find("li.custom-submenu-dynamic-direction>span.down");			
				$menuItems = $nextContextMenu.children(".custom-submenu-dynamic-item");

				if ($menuItems.length > 10) {
					$upMenuItem.parent().show();
					$downMenuItem.parent().show();
					$upMenuItem.parent().off("click",setDynamicMenuUpEvent);
					$upMenuItem.parent().click(setDynamicMenuUpEvent);
					
					$downMenuItem.parent().off("click",setDynamicMenuDownEvent);					
					$downMenuItem.parent().click(setDynamicMenuDownEvent);
					$menuItems.each(function(index){
						if (index > 10) {
							$(this).hide();
						}else{
							$(this).show();
						}
					});

					var contextHeight = $contextMenu.height();

					if (contextHeight <= topToBottomHeight) {
						$nextContextMenu.css('left', 178);
						$nextContextMenu.css('top', 0);
					} else {
						$nextContextMenu.css('left', 178);
						$nextContextMenu.css('bottom', 0);
					}

					$nextContextMenu.show(100);

				}else{
					$upMenuItem.parent().hide();
					$downMenuItem.parent().hide();

					var contextHeight = $nextContextMenu.height();

					if (contextHeight <= topToBottomHeight) {
						$nextContextMenu.css('left', 178);
						$nextContextMenu.css('top', 0);
					} else {
						$nextContextMenu.css('left', 178);
						$nextContextMenu.css('bottom', 0);						
					}
					
					$nextContextMenu.show(100);
				}
				
								
			},
			//创建动态子菜单
			createSubDynamicMenu:function(children, recursionOrderNumber){
				var retVal = '';				
				if (children && children.length > 0) {
					retVal = '<ul class="custom-submenu-dynamic-group">';

					retVal += '<li class="custom-submenu-dynamic-direction"><span class="up"><i class="fa fa-chevron-up"></i></span></li>';
			
					for (var i = 0; i < children.length; i++){
						var subMenuData = children[i];
						retVal += '<li class="custom-submenu-dynamic-item"><span id="' + subMenuData.menuId + '" class="left" ><i class="fa ' + subMenuData.icon + '" data-url="' + subMenuData.url + '"></i>' + subMenuData.menuName + '</span>';
						if (subMenuData.children && subMenuData.children.length > 0) {
							retVal += '<span class="right"><i class="fa fa-chevron-right"></i></span>';
						}
								
						retVal+= this.createSubDynamicMenu(subMenuData.children,recursionOrderNumber + 1);
						
						retVal += '</li>';
					}

					retVal += '<li class="custom-submenu-dynamic-direction"><span class="down"><i class="fa fa-chevron-down"></i></span></li>';
															
					retVal+="</ul>";
				}
				return retVal;
			},
			//创建动态菜单
			createDynamicMenu:function($that,menuData){
				$that.html('');
				var strVal = '';				
				for (var i = 0; i < menuData.length; i++){
					var itemData = menuData[i];

					strVal += '<li class="custom-menu-dynamic-item">';
					strVal += '<span id="' + itemData.menuId + '" class="left"><i class="fa fa-3x ' + itemData.icon + '"></i></span><div class="text">' + itemData.menuName + '</div><div class="triangle"></div>';
					
					strVal += this.createSubDynamicMenu(itemData.children, 1);
					strVal +='</li>';
				}
								
				$that.append($(strVal));
			}
			
		};
		
		var configs = $.extend(_default, options);
		//console.log(configs.menuType);
		//普通菜单
		if (configs.menuType === 'normal') {		
			configs.createMenu($(this), configs.menuData);
			$(".custom-menu-item > span", $(this)).click(configs.setMenuItemOnClick);		
			$(".custom-submenu-item > span", $(this)).click(configs.setMenuItemOnClick);	
		}else if (configs.menuType === 'flow') {//流动菜单
			configs.createDynamicMenu($(this),configs.menuData);	
			$(".custom-menu-dynamic-item i").on("mouseover", configs.setTopDynamicMenuItemEvent);
			$(".custom-menu-dynamic-item").on("mouseleave", configs.setContextMenuItemEvent);
			$(".custom-menu-dynamic-item > div.text").on("mouseover", function (e) {
				$(this).prev().children("i").trigger("mouseover");
			});
			$(".custom-submenu-dynamic-group").on("mouseleave",configs.setContextMenuEvent)				
			$(".custom-submenu-dynamic-item").on("mouseover",configs.setDynamicMenuItemEvent);
			$(".custom-submenu-dynamic-item").on("mouseleave",configs.setContextMenuItemEvent);
			$(".custom-submenu-dynamic-item").click(configs.setMenuItemDynamicOnClick)		
		}
		$mainframe.setMainFrame();

		return this;
	}

})(jQuery);

