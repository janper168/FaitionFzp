

(function ($) {

    $.fn.formValidator = function () {

        var $form = $($(this)[0]);//取第一个

        //获取表单的所有元素
  
        var $formItem = $form.find("input,select,textarea");

        $formItem.each(function () {

            $item = $(this);
            $item.on("change", function () {
                //alert($(this).attr("id"));
                //alert($(this).attr("data-type"));
                //alert($(this).val());
                if (checkItem($(this)) == true) {
                    if ($(this).attr("data-type") == "image") {
                        $("#" + $(this).attr("id") + "Wrapper_error").remove();
                    } else {
                        $("#" + $(this).attr("id") + "_error").remove();
                    }
                    
                }
            })
            
        })

        $(window).resize(function () {
            ret.valid();
        })

        function checkItem($item) {
            var errorMsg = "";
            //检查必填
            var dataRequired = $item.attr("data-required");
            if (!!dataRequired) {
                if (dataRequired == "true") {
                    if ($item.val() == "" && $item.attr("data-type") != "image") {
                        errorMsg = "该项必填！";
                    } else if ($item.val() == "" && ($item.attr("data-type") == "image" || $item.attr("data-type") == "images")) {
                        errorMsg = "请选择图片！"
                    }
                } 
            }
            //检查类型
            if (errorMsg == "") {
                var dataType = $item.attr("data-type");
                if (!!dataType) {
                    if (dataType == "text") {
                    } else if (dataType == "integer") {
                        if (isNaN($item.val())) {
                            errorMsg = "必须为数字！";
                        }
                    } else if (dataType == "regex") {
                        var regexString = $item.attr("data-regex");
                        if (!!$item.val()) {//如果有值才判断
                            var pattern = new RegExp(regexString);
                            var b = pattern.test($item.val());
                            if (b == false) {
                                errorMsg = $item.attr("data-errorMsg");
                            }
                        }
                    }
                }
            }
            //字段是否重复
            if (errorMsg == "") {
                var checkExistUrl = $item.attr("data-check-exists-url");
                if (!!checkExistUrl) {
                    var res = $.httpPost({ url: checkExistUrl, param: { paramValue : $item.val()}});
                    if (res.code != 200) {
                        errorMsg = "已存在！";
                    }
                }
            }

            var $validItem = null;
            if ($item.attr("data-type") == "image") {
                $validItem = $item.siblings("#" + $item.attr("id") + "Wrapper");
                //alert(1);
            } else {
                $validItem = $item;
            }
    
            $("body").find("#" + $validItem.attr("id") + "_error").remove();
            if (errorMsg != "") {

                $("body").append("<div id='" + $validItem.attr("id") + "_error' class='error' ><i class='fa fa-exclamation-circle'>" + errorMsg + "</div>");

                var pos = $.getAbsPos($validItem);

                var $error = $("body").find("#" + $validItem.attr("id") + "_error");

                $error.css("top", pos.y);
                $error.css("left", parseInt(pos.x + $validItem.width() - $error.width()));               

                $validItem.addClass("errorBorder");

                return false;
            }
            $validItem.removeClass("errorBorder");
            return true;
        }

        var ret = {
            valid: function () {
                var totalResult = true;
                $formItem.each(function () {
                    var result = checkItem($(this));
                    if (result == false) {
                        if (totalResult == true) {
                            totalResult = false;
                        } 
                    }
                })
                return totalResult;
            }
        }

        return ret;
    }

})(jQuery);