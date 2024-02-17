
history.pushState(null, null, document.URL);
window.addEventListener('popstate', function () {
    history.pushState(null, null, document.URL);
});

top.jkf = (function ($) {

    var jkf = {

        topFrame: top,

        $topBody: $('body'),

        //同时在控制台和弹窗输出
        consoleAndAlertLog: function (txt) {
            console.log(txt);
            alert(txt);
        },
        newGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
            }
            return guid;
        },
        getIframeByElement: function (element) {
            var iframe;
            $("iframe", window.parent.document).each(function () {
                if (element.ownerDocument === this.contentWindow.document) {
                    iframe = this;
                }
                return !iframe;
            });
            return iframe;
        },
        //处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外
        banBackSpace :function (e){
            var ev = e || window.event;//获取event对象
            var obj = ev.target || ev.srcElement;//获取事件源

            var t = obj.type || obj.getAttribute('type');//获取事件源类型

            //获取作为判断条件的事件类型
            var vReadOnly = obj.getAttribute('readonly');
            var vEnabled = obj.getAttribute('enabled');
            //处理null值情况
            vReadOnly = (vReadOnly == null) ? false : vReadOnly;
            vEnabled = (vEnabled == null) ? true : vEnabled;

            //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
            //并且readonly属性为true或enabled属性为false的，则退格键失效
            var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
                && (vReadOnly == true || vEnabled != true)) ? true : false;

            //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
            var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
                ? true : false;

            //判断
            if (flag2) {
                return false;
            }
            if (flag1) {
                return false;
            }
        }

	}
    return jkf;

}) (window.jQuery);

//所有的主题颜色定义
var themeColorCssNameArray =
[
    "bg-color-lightblue",
    "bg-color-lightgreen",
    "bg-color-darkblue"
];