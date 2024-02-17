(function ($) {

    $.extend({

        httpGet: function (options) {
            //alert(1);
            //默认的配置属性
            var _default = {
                url: null,
                param: null,
                cache: false,
                dataType: 'json',
            };
            var resultData = null;
            var configs = $.extend({}, _default, options);
            var boxId = top.jkf.loadingFrame(true, "正在处理,请稍候...");
            $.ajax({
                url: configs.url,
                dataType: configs.dataType,
                type: 'get',
                data: configs.param,
                cache: configs.cache,
                async: false,
                success: function (res) {
                    resultData = res;
                },
                complete: function () {
                    top.jkf.loadingFrame(false, "", boxId);
                }
            })
            return resultData;
        },
        httpGetAsync: function (options, callback) {
            //alert(1);
            //默认的配置属性
            var _default = {
                url: null,
                param: null,
                cache: false,
                dataType: 'json',
            };

            var configs = $.extend({}, _default, options);
            var boxId = top.jkf.loadingFrame(true, "正在处理,请稍候...");
            $.ajax({
                url: configs.url,
                dataType: configs.dataType,
                type: 'get',
                data: configs.param,
                cache: configs.cache,
                async: true,
                success: function (res) {
                    callback(res);
                }, complete: function () {
                    top.jkf.loadingFrame(false, "", boxId);
                }
            })

        },
        httpPost: function (options) {
            //alert(1);
            //默认的配置属性
            var _default = {
                url: null,
                param: null,
                cache: false,
                dataType: 'json',
            };
            var resultData = null;
            var configs = $.extend({}, _default, options);
            var boxId = top.jkf.loadingFrame(true, "正在处理,请稍候...");
            $.ajax({
                url: configs.url,
                dataType: configs.dataType,
                type: 'post',
                data: configs.param,
                cache: configs.cache,
                async: false,
                success: function (res) {
                    resultData = res;
                }, complete: function () {
                    top.jkf.loadingFrame(false, "", boxId);
                }
            });
            return resultData;

        },
        httpPostAsync: function (options, callback) {
            //alert(1);
            //默认的配置属性
            var _default = {
                url: null,
                param: null,
                cache: false,
                dataType: 'json',
            };

            var configs = $.extend({}, _default, options);
            var boxId = top.jkf.loadingFrame(true, "正在处理,请稍候...");
            $.ajax({
                url: configs.url,
                dataType: configs.dataType,
                type: 'post',
                data: configs.param,
                cache: configs.cache,
                async: true,
                success: function (res) {
                    callback(res);
                },complete: function () {
                    top.jkf.loadingFrame(false, "", boxId);
                }
            })

        }
    });

})(jQuery);