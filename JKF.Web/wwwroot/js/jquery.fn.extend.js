(function ($,jkf) {
	//获取选择器的第一个元素
	$.fn.getFirstJqObj = function (className) {

		var $onlyFirstObj = null; //只获取选择器的第一个元素
		if ($(this).length <= 0) {
			jkf.consoleAndAlertLog("非法元素");
			return null;
		} else if (!$(this).hasClass(className)) {
			jkf.consoleAndAlertLog("非法元素,不存在'" + className + "'类.");
			return null;
		}

		//如果是集合存在多个元素只取第一个元素
		$onlyFirstObj = $($(this)[0]);
		return $onlyFirstObj;
	}
})(window.jQuery, top.jkf);
