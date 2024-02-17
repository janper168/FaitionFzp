
var lineMode = false;
var startPoint = null;
var endPoint = null;
var startObjectPointPos = "";
var endObjectPointPos = "";
var $startObject = null;
var $endObject = null;
var startTuxingRet = null;
var endTuxingRet = null;

var lineObject = [];
var currentLineCount = 0;

var tuxings = [];
var zhexianWidth = 100;

var objectCanMove = true;

function LineProcess(lineId) {
    if (!workFlowDesignId) {
        top.jkf.showTips('异常：没有流程设计id', 3);
        return;
    }

    top.jkf.openFrame({
        parentFrameId: $openIframe.attr("id"),
        title: "流程线条设置：" + lineId,
        width: 1000,
        height: 600,
        level: 2,
        url: '/WorkFlow/WorkFlowDesign/DesignFormLine?workFlowDesignId=' + workFlowDesignId + "&lineId=" + lineId,
        data: null,
        hasMinButton: false,
        hasMaxButton: true,
        hasCloseButton: true,
        isWazard: true,
        buttonGroup: [
            {
                buttonId: "button_save",
                buttonText: "保存",
                buttonClickAction: function () {
                    var openWin = top.jkf.flowDesignFormLineIframe.contentWindow;
                    openWin.saveForm();

                }
            }
        ]
    });
}

function deleteLineObject($lineObject) {
    var deleteId = -1;
    for (var i in lineObject) {
        var item = lineObject[i];
        if (item.id == $lineObject.attr("id")) {
            for (var j in tuxings) {
                if (tuxings[j].id == item.$startTuxing.attr("id")) {
                    tuxings[j].ret.removeTuxingchangeFunc(item.id);
                } else if (tuxings[j].id == item.$endTuxing.attr("id")) {
                    tuxings[j].ret.removeTuxingchangeFunc(item.id);
                } 
            }     
            deleteId = i;
            break;
        }
    }
    if (deleteId != -1) {
        lineIndex = 0;//重置
        lineObject.splice(deleteId, 1);//从联线的数组里面删除
        $lineObject.remove();//删除线条
    }

} 

function deleteTuxing($tuxing) {
    if ($tuxing.attr("id") == "startEndon" ||
        $tuxing.attr("id") == "endEndon") {
        alert("起点和终点不能删除！");
        return;
    } 
    var deleteLineObjectIds = [];

    for (var i in lineObject) {
        var item = lineObject[i];
        //console.log("lineObject[" + i + "]:item.$startTuxing:" + item.$startTuxing.attr("id") + ":item.$endTuxing:" + item.$endTuxing.attr("id"));
        for (var k in tuxings) {
            //console.log("tuxings:" + k + ":" + tuxings[k].id);
        }

        if (item.$startTuxing.attr("id") == $tuxing.attr("id")) {
            for (var j in tuxings) {
                if (tuxings[j].id == item.$endTuxing.attr("id")) {
                    tuxings[j].ret.removeTuxingchangeFunc(item.id);
                    break;
                }
            }
            deleteLineObjectIds.push(item.id);
        } else if (item.$endTuxing.attr("id") == $tuxing.attr("id")) {
            for (var j in tuxings) {
                if (tuxings[j].id == item.$startTuxing.attr("id")) {
                    tuxings[j].ret.removeTuxingchangeFunc(item.id);
                    break;
                }
            }
            deleteLineObjectIds.push(item.id);
        }
    }
    //先删除关联的线
    $.each(deleteLineObjectIds, function (i, item) {
        var $lineObject = $tuxing.parent().find("#" + item);
        if ($lineObject.length == 1) {          
            deleteLineObject($lineObject);           
        }
    });

    var deleteId = -1;
    for (var i in tuxings) {
        if (tuxings[i].id == $tuxing.attr("id")) {
            deleteId = i;
            break;
        }
    }
    //图形删除
    if (deleteId != -1) {
        lineIndex = 0;//重置
        tuxings.splice(deleteId, 1);
        $tuxing.remove();
    }

    //console.log("shengxia tuxings.length : " + tuxings.length);
    //console.log("shengxia lineObject.length : " + lineObject.length);

    for (var k in tuxings) {
        //console.log("----tuxings:" + k + ":" + tuxings[k].id);
    }
}


(function ($) {
    startLineMode = function () {
        lineMode = true;
    }
    stopLineMode = function () {
        lineMode = false;
    }
    $.fn.lineObject = function (options) {

        var $current = $(this);

        //连线的起点坐标
        var startPointX = options.startPoint.x;
        var startPointY = options.startPoint.y;

        //console.log("startPointX, startPointY:"+ startPointX+":"+ startPointY)

        //连线的终点坐标
        var endPointX = options.endPoint.x;
        var endPointY = options.endPoint.y;

        //console.log("endPointX, endPointY:" + endPointX + ":" + endPointY)

        //起点关联的图形选择器
        var $startPointObject = options.startPointObject;
        //终点关联的图形选择器
        var $endPointObject = options.endPointObject;

        var lineObjectItem = {
            id: $current.attr("id"),
            $startTuxing: $startPointObject.parent().parent(),
            $endTuxing: $endPointObject.parent().parent()
        }

        lineObject.push(lineObjectItem);

        for (var i in lineObject) {
            var item = lineObject[i];
            //console.log("item,id:" + item.id);
            //console.log("item,$startTuxing:" + item.$startTuxing.attr("id"));
            //console.log("item,$endTuxing:" + item.$endTuxing.attr("id"));
        }

        //点类型：上下左右
        var startPosType = options.startObjectPointPos;
        var endPosType = options.endObjectPointPos;

        $current.attr("startPosType", startPosType);
        $current.attr("endPosType", endPosType);

        //console.log("startPosType:" + startPosType);
        //console.log("endPosType:" + endPosType);

        var retc0 = {};
        var retc1 = {};
        var retc2 = {};

        //图形1的down点，图形2的up点
        if (startPosType == "down" && endPosType == "up") {
            if (startPointX <= endPointX && startPointY <= endPointY) {
                //console.log("7777777")
                //console.log("tuxing id:" + $startPointObject.parent().parent().attr("id"));
                retc0.left = startPointX;
                retc0.top = startPointY;
                retc0.width = endPointX - startPointX;
                retc0.height = endPointY - startPointY;

                retc1.left = startPointX;
                retc1.top = startPointY;
                retc1.width = endPointX - startPointX;
                retc1.height = (endPointY - startPointY) / 2;

                retc2.width = endPointX - startPointX;
                retc2.height = (endPointY - startPointY) / 2;
                retc2.left = startPointX;
                retc2.top = startPointY + retc2.height;

                var retc0Html = "<div class='a2B rightAndBottom'>";
                retc0Html += "<div class='a2BInner rightAndBottom1'></div>";
                retc0Html += "<div class='a2BInner rightAndBottom2'></div><div class='rightBottomArrow'></div>";
                retc0Html += "</div>"

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.rightAndBottom');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.a2BInner.rightAndBottom1');
                var $retc2 = $(this).find('.a2BInner.rightAndBottom2');

                $retc1.css({ left: 0, top: 0 });
                $retc2.css({ left: 0, top: retc2.height });

                $retc1.width(retc1.width);
                $retc2.width(retc2.width);

                $retc1.height(retc1.height);
                $retc2.height(retc2.height);

            }
            else if (startPointX >= endPointX && startPointY <= endPointY) {
                //.log("88888888888888")
                //console.log("tuxing id:" + $startPointObject.parent().parent().attr("id"));
                //console.log("startPointY:" + startPointY + ",startPointX:" + startPointX);
                retc0.left = endPointX;
                retc0.top = startPointY;
                retc0.width = startPointX - endPointX;
                retc0.height = endPointY - startPointY;

                retc1.left = endPointX;
                retc1.top = startPointY;
                retc1.width = startPointX - endPointX;
                retc1.height = (endPointY - startPointY) / 2;

                retc2.width = startPointX - endPointX;
                retc2.height = (endPointY - startPointY) / 2;
                retc2.left = endPointX;
                retc2.top = startPointY + retc2.height;

                var retc0Html = "<div class='a2B leftAndBottom'>";
                retc0Html += "<div class='a2BInner leftAndBottom1'></div>";
                retc0Html += "<div class='a2BInner leftAndBottom2'></div><div class='leftBottomArrow'></div>";
                retc0Html += "</div>"

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.leftAndBottom');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.a2BInner.leftAndBottom1');
                var $retc2 = $(this).find('.a2BInner.leftAndBottom2');

                $retc1.css({ left: 0, top: 0 });
                $retc2.css({ left: 0, top: retc2.height });

                $retc1.width(retc1.width);
                $retc2.width(retc2.width);

                $retc1.height(retc1.height);
                $retc2.height(retc2.height);

            }
        }
        else if (startPosType == "left" && endPosType == "left") {

            if (startPointX <= endPointX && startPointY > endPointY) {
                //console.log("5555")
                retc0.width = endPointX - startPointX + zhexianWidth;
                retc0.height = startPointY - endPointY;
                retc0.left = startPointX - zhexianWidth;
                retc0.top = endPointY;

                var retc0Html = "<div class='a2B leftAndLeft'>" +
                    "<div class='hideBlock left' ></div >" +
                    "<div class='leftAndLeftArrow right'></div >";
                retc0Html += "</div>"

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.leftAndLeft');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.hideBlock');
                $retc1.css({
                    left: zhexianWidth + $startPointObject.parent().parent().width(), top: startPointY - endPointY - 5
                });

                $retc1.width(endPointX - startPointX - $startPointObject.parent().parent().width());
            }
            else if (startPointX >= endPointX && startPointY > endPointY) {
                //console.log("666")
                retc0.left = endPointX - zhexianWidth;
                retc0.top = endPointY;
                retc0.width = startPointX - endPointX + zhexianWidth;
                retc0.height = startPointY - endPointY;

                retc1.left = endPointX;
                retc1.top = startPointY;
                retc1.width = startPointX - endPointX;
                retc1.height = (endPointY - startPointY) / 2;

                var retc0Html = "<div class='a2B leftAndLeft'>"
                    + "<div class='hideBlock left'></div>"
                    + "<div class='leftAndLeftArrow left'></div>";
                retc0Html += "</div>"

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.leftAndLeft');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.hideBlock');

                $retc1.css({ left: zhexianWidth + $endPointObject.parent().parent().width(), top: -5 });

                $retc1.width(startPointX - endPointX - $endPointObject.parent().parent().width());

            }
        }
        else if (startPosType == "right" && endPosType == "right") {
            if (startPointX <= endPointX && startPointY > endPointY) {
                //console.log("333")
                retc0.width = endPointX - startPointX + zhexianWidth;
                retc0.height = startPointY - endPointY;
                retc0.left = startPointX;
                retc0.top = endPointY;

                var retc0Html = "<div class='a2B rightAndRight'>" +
                    "<div class='hideBlock left' ></div >" +
                    "<div class='rightAndRightArrow right'></div >";
                retc0Html += "</div>";

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.leftAndLeft');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.hideBlock');
                $retc1.css({
                    left: 0, top: -5
                });

                $retc1.width(endPointX - $endPointObject.parent().parent().width() - startPointX);
            }
            else if (startPointX >= endPointX && startPointY > endPointY) {
                //console.log("444")
                retc0.left = endPointX;
                retc0.top = endPointY;
                retc0.width = startPointX - endPointX + zhexianWidth;
                retc0.height = startPointY - endPointY;

                var retc0Html = "<div class='a2B rightAndRight'>"
                    + "<div class='hideBlock left'></div>"
                    + "<div class='rightAndRightArrow left'></div>";
                retc0Html += "</div>";

                $(this).append(retc0Html);

                //var $retc0 = $(this).find('.a2B.leftAndLeft');
                var $retc0 = $(this);
                $retc0.css({ left: retc0.left, top: retc0.top });
                $retc0.width(retc0.width);
                $retc0.height(retc0.height);

                var $retc1 = $(this).find('.hideBlock');

                $retc1.css({ left: 0, bottom: -5 });

                $retc1.width(startPointX - $startPointObject.parent().parent().width() - endPointX);

            }

        }

        var changeMyLineObject = function ($tuxing, offsetLeft, offsetTop) {

            var $startTuxing = $startPointObject.parent().parent();
            var $endTuxing = $endPointObject.parent().parent();

            if ($tuxing.attr("id") == $startTuxing.attr("id")) {
                if (options.endObjectPointPos == "up" &&
                    options.startObjectPointPos == "down") {
                    //console.log("1111111111")
                    var $retc0 = null;
                    var $retc1 = null;
                    var $retc2 = null;

                    var $retTempRight = $current.find('.a2B.rightAndBottom');
                    var $retTempLeft = $current.find('.a2B.leftAndBottom');

                    var label = 0;

                    if ($retTempRight.length == 1 && $startTuxing.offset().left <= $endTuxing.offset().left) {
                        ;//end还在右下角
                        $retc0 = $current.find('.a2B.rightAndBottom');
                        $retc1 = $current.find('.a2BInner.rightAndBottom1');
                        $retc2 = $current.find('.a2BInner.rightAndBottom2');

                        label = 1;
                    } else if ($retTempRight.length == 1 && $startTuxing.offset().left > $endTuxing.offset().left) {
                        //end变为左下角
                        $retc0 = $current.find('.a2B.rightAndBottom');
                        $retc1 = $current.find('.a2BInner.rightAndBottom1');
                        $retc2 = $current.find('.a2BInner.rightAndBottom2');

                        $retc0.removeClass('rightAndBottom').addClass('leftAndBottom');
                        $retc1.removeClass('rightAndBottom1').addClass('leftAndBottom1');
                        $retc2.removeClass('rightAndBottom2').addClass('leftAndBottom2');

                        $current.find("div.rightBottomArrow").removeClass("rightBottomArrow").addClass("leftBottomArrow");

                        label = 2;
                    }
                    else if ($retTempLeft.length == 1 && $startTuxing.offset().left >= $endTuxing.offset().left) {
                        ;//end还在左下角
                        $retc0 = $current.find('.a2B.leftAndBottom');
                        $retc1 = $current.find('.a2BInner.leftAndBottom1');
                        $retc2 = $current.find('.a2BInner.leftAndBottom2');
                        label = 3;
                    } else if ($retTempLeft.length == 1 && $startTuxing.offset().left < $endTuxing.offset().left) {
                        //end变为右下角
                        $retc0 = $current.find('.a2B.leftAndBottom');
                        $retc1 = $current.find('.a2BInner.leftAndBottom1');
                        $retc2 = $current.find('.a2BInner.leftAndBottom2');

                        $retc0.removeClass('leftAndBottom').addClass('rightAndBottom');
                        $retc1.removeClass('leftAndBottom1').addClass('rightAndBottom1');
                        $retc2.removeClass('leftAndBottom2').addClass('rightAndBottom2');
                        $current.find("div.leftBottomArrow").removeClass("leftBottomArrow").addClass("rightBottomArrow");
                        label = 4;
                    }
                    //console.log("111:" + label)
                    //console.log("222:" + $retTempRight.length + ";" + $retTempLeft.length);
                    if (label == 1) {
                        var posLeft = $startTuxing.width() / 2 + offsetLeft;
                        var posTop = $startTuxing.height() + offsetTop;

                        if (posTop <= $endTuxing.offset().top) {

                            $retc0.parent().css({ left: posLeft, top: posTop });

                            $retc0.parent().width($endTuxing.width() / 2 + $endTuxing.offset().left - posLeft);
                            $retc0.parent().height($endTuxing.offset().top - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $endTuxing.offset().top) {
                                return false;
                            }
                            else return true;
                        }
                    }
                    else if (label == 2) {
                        //console.log("2");
                        var posLeft = $startTuxing.width() / 2 + offsetLeft;
                        var posTop = $startTuxing.height() + offsetTop;

                        if (posTop <= $endTuxing.offset().top) {
                            $retc0.parent().css({
                                left: $endTuxing.offset().left + $endTuxing.width() / 2,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width(posLeft - $endTuxing.offset().left + $endTuxing.width() / 2);
                            $retc0.parent().height($endTuxing.offset().top - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $endTuxing.offset().top) {
                                return false;
                            }
                            else return true;
                        }
                    } else if (label == 3) {
                        var posLeft = $startTuxing.width() / 2 + offsetLeft;
                        var posTop = $startTuxing.height() + offsetTop;

                        if (posTop <= $endTuxing.offset().top) {
                            $retc0.parent().css({
                                left: $endTuxing.offset().left + $endTuxing.width() / 2,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width(posLeft - $endTuxing.offset().left - $endTuxing.width() / 2);
                            $retc0.parent().height($endTuxing.offset().top - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $endTuxing.offset().top) {
                                return false;
                            }
                            else return true;
                        }
                    } else if (label == 4) {
                        var posLeft = $startTuxing.width() / 2 + offsetLeft;
                        var posTop = $startTuxing.height() + offsetTop;

                        if (posTop <= $endTuxing.offset().top) {
                            $retc0.parent().css({ left: posLeft, top: posTop });

                            $retc0.parent().width($endTuxing.width() / 2 + $endTuxing.offset().left - posLeft);
                            $retc0.parent().height($endTuxing.offset().top - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $endTuxing.offset().top) {
                                return false;
                            }
                            else return true;
                        }
                    }

                }
                else if (options.endObjectPointPos == "left" &&
                    options.startObjectPointPos == "left") {
                    //console.log(5555);
                    //console.log("22222222222222")
                    var $retc0 = null;
                    var $retc1 = null;

                    var $retc0 = $current.find('.a2B.leftAndLeft');

                    if (offsetLeft >= $endTuxing.offset().left && $endTuxing.offset().left + $endTuxing.width() >= offsetLeft) {
                        //console.log(5);
                        $retc0.parent().css({ left: $endTuxing.offset().left - zhexianWidth, top: $endTuxing.offset().top + $endTuxing.height() / 2 });
                        $retc0.parent().width(offsetLeft - $endTuxing.offset().left + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $current.find('div.leftAndLeftArrow').removeClass('right').addClass('left');
                    }
                    else if (offsetLeft + $startTuxing.width() >= $endTuxing.offset().left && offsetLeft <= $endTuxing.offset().left + $endTuxing.width()) {
                        //console.log(6);
                        $retc0.parent().css({ left: offsetLeft - zhexianWidth, top: $endTuxing.offset().top + $endTuxing.height() / 2 });
                        $retc0.parent().width($endTuxing.offset().left - offsetLeft + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $current.find('div.leftAndLeftArrow').removeClass('left').addClass('right');

                    } else if (offsetLeft >= $endTuxing.offset().left + $endTuxing.width()) {
                        //console.log(7);
                        $retc0.parent().css({ left: $endTuxing.offset().left - zhexianWidth, top: $endTuxing.offset().top + $endTuxing.height() / 2 });
                        $retc0.parent().width(offsetLeft - $endTuxing.offset().left + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);

                        $retc1 = $current.find('.hideBlock');
                        $retc1.css({ left: zhexianWidth + $endTuxing.width(), top: -5 });
                        $retc1.width(offsetLeft - $endTuxing.offset().left - $endTuxing.width());
                        $current.find('div.leftAndLeftArrow').removeClass('right').addClass('left');

                    } else if (offsetLeft + $startTuxing.width() <= $endTuxing.offset().left) {
                        //console.log(8);
                        $retc0.parent().css({ left: offsetLeft - zhexianWidth, top: $endTuxing.offset().top + $endTuxing.height() / 2 });
                        $retc0.parent().width($endTuxing.offset().left - offsetLeft + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $retc1 = $current.find('.hideBlock');

                        $retc1.css({
                            left: zhexianWidth + $startTuxing.width(), top: offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2 - 5
                        });

                        $retc1.width($endTuxing.offset().left - offsetLeft - $startTuxing.width());
                        $current.find('div.leftAndLeftArrow').removeClass('left').addClass('right');

                    }

                    if (offsetTop <= $endTuxing.height() / 2 + $endTuxing.offset().top) {
                        return false;
                    }
                    else return true;

                }
                else if (options.endObjectPointPos == "right" &&
                    options.startObjectPointPos == "right") {
                    //console.log(9999);
                    //console.log("33333333333333")
                    var $retc0 = null;
                    var $retc1 = null;

                    var $retc0 = $current.find('.a2B.rightAndRight');

                    if (offsetLeft >= $endTuxing.offset().left && $endTuxing.offset().left + $endTuxing.width() >= offsetLeft) {
                        //console.log(5);
                        $retc0.parent().css({
                            left: $endTuxing.offset().left + $endTuxing.width(),
                            top: $endTuxing.offset().top + $endTuxing.height() / 2
                        });
                        $retc0.parent().width(offsetLeft + $startTuxing.width() - $endTuxing.offset().left - $endTuxing.width() + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $current.find('div.rightAndRightArrow').removeClass('right').addClass('left');
                    }
                    else if (offsetLeft + $startTuxing.width() >= $endTuxing.offset().left && offsetLeft <= $endTuxing.offset().left + $endTuxing.width()) {
                        //console.log(6);
                        $retc0.parent().css({
                            left: offsetLeft + $startTuxing.width(),
                            top: $endTuxing.offset().top + $endTuxing.height() / 2
                        });
                        $retc0.parent().width($endTuxing.offset().left + $endTuxing.width() - offsetLeft - $startTuxing.width() + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $current.find('div.rightAndRightArrow').removeClass('left').addClass('right');

                    } else if (offsetLeft >= $endTuxing.offset().left + $endTuxing.width()) {
                        //console.log(7);
                        $retc0.parent().css({
                            left: $endTuxing.offset().left + $endTuxing.width(),
                            top: $endTuxing.offset().top + $endTuxing.height() / 2
                        });
                        $retc0.parent().width(offsetLeft + $startTuxing.width() - $endTuxing.offset().left - $endTuxing.width() + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);

                        $retc1 = $current.find('.hideBlock');
                        $retc1.width(offsetLeft - $endTuxing.offset().left - $endTuxing.width());
                        $retc1.css({ left: 0, top: offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2 - 5 });
                        $current.find('div.rightAndRightArrow').removeClass('right').addClass('left');

                    } else if (offsetLeft + $startTuxing.width() <= $endTuxing.offset().left) {
                        //console.log(8);
                        $retc0.parent().css({
                            left: offsetLeft + $startTuxing.width(),
                            top: $endTuxing.offset().top + $endTuxing.height() / 2
                        });
                        $retc0.parent().width($endTuxing.offset().left + $endTuxing.width() - offsetLeft - $startTuxing.width() + zhexianWidth);
                        $retc0.parent().height(offsetTop + $startTuxing.height() / 2 - $endTuxing.offset().top - $endTuxing.height() / 2);
                        $retc1 = $current.find('.hideBlock');

                        $retc1.css({
                            left: 0, top: - 5
                        });

                        $retc1.width($endTuxing.offset().left - offsetLeft - $startTuxing.width());
                        $current.find('div.rightAndRightArrow').removeClass('left').addClass('right');

                    }

                    if (offsetTop <= $endTuxing.height() / 2 + $endTuxing.offset().top) {
                        return false;
                    }
                    else return true;

                }
            }
            else if ($tuxing.attr("id") == $endTuxing.attr("id")) {
                if (options.endObjectPointPos == "up" &&
                    options.startObjectPointPos == "down") {
                    //console.log("444444444444444")
                    var $retc0 = null;
                    var $retc1 = null;
                    var $retc2 = null;

                    var $retTempRight = $current.find('.a2B.rightAndBottom');
                    var $retTempLeft = $current.find('.a2B.leftAndBottom');

                    var label = 0;

                    if ($retTempRight.length == 1 && $startTuxing.offset().left <= $endTuxing.offset().left) {
                        ;//end还在右下角
                        $retc0 = $current.find('.a2B.rightAndBottom');
                        $retc1 = $current.find('.a2BInner.rightAndBottom1');
                        $retc2 = $current.find('.a2BInner.rightAndBottom2');

                        label = 1;
                    } else if ($retTempRight.length == 1 && $startTuxing.offset().left > $endTuxing.offset().left) {
                        //end变为左下角
                        $retc0 = $current.find('.a2B.rightAndBottom');
                        $retc1 = $current.find('.a2BInner.rightAndBottom1');
                        $retc2 = $current.find('.a2BInner.rightAndBottom2');

                        $retc0.removeClass('rightAndBottom').addClass('leftAndBottom');
                        $retc1.removeClass('rightAndBottom1').addClass('leftAndBottom1');
                        $retc2.removeClass('rightAndBottom2').addClass('leftAndBottom2');

                        $current.find("div.rightBottomArrow").removeClass("rightBottomArrow").addClass("leftBottomArrow");

                        label = 2;
                    }
                    else if ($retTempLeft.length == 1 && $startTuxing.offset().left >= $endTuxing.offset().left) {
                        ;//end还在左下角
                        $retc0 = $current.find('.a2B.leftAndBottom');
                        $retc1 = $current.find('.a2BInner.leftAndBottom1');
                        $retc2 = $current.find('.a2BInner.leftAndBottom2');
                        label = 3;
                    } else if ($retTempLeft.length == 1 && $startTuxing.offset().left < $endTuxing.offset().left) {
                        //end变为右下角
                        $retc0 = $current.find('.a2B.leftAndBottom');
                        $retc1 = $current.find('.a2BInner.leftAndBottom1');
                        $retc2 = $current.find('.a2BInner.leftAndBottom2');

                        $retc0.removeClass('leftAndBottom').addClass('rightAndBottom');
                        $retc1.removeClass('leftAndBottom1').addClass('rightAndBottom1');
                        $retc2.removeClass('leftAndBottom2').addClass('rightAndBottom2');
                        $current.find("div.leftBottomArrow").removeClass("leftBottomArrow").addClass("rightBottomArrow");
                        label = 4;
                    }

                    if (label == 1) {//右下角

                        if ($startTuxing.offset().top + $startTuxing.height() <= offsetTop) {

                            $retc0.parent().css({
                                left: $startTuxing.offset().left + $startTuxing.width() / 2,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width(offsetLeft - $retc0.offset().left + $endTuxing.width() / 2);
                            $retc0.parent().height(offsetTop - $retc0.offset().top);

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', (offsetTop - $retc0.offset().top) / 2);

                            if ($startTuxing.offset().top + $startTuxing.height() == offsetTop) {
                                return false;
                            } else {
                                return true;
                            }
                        }

                    }
                    else if (label == 2) {//左下角
                        //console.log("2");

                        var posLeft = $endTuxing.width() / 2 + offsetLeft;
                        var posTop = offsetTop;

                        if (posTop >= $startTuxing.offset().top + $startTuxing.height()) {
                            $retc0.parent().css({
                                left: posLeft,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width($startTuxing.offset().left + $startTuxing.width() / 2 - posLeft);
                            $retc0.parent().height(posTop - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $startTuxing.offset().top + $startTuxing.height()) {
                                return false;
                            }
                            else return true;
                        }
                    }
                    else if (label == 3) { //左下角
                        //console.log("3");

                        var posLeft = $endTuxing.width() / 2 + offsetLeft;
                        var posTop = offsetTop;

                        if (posTop >= $startTuxing.offset().top + $startTuxing.height()) {
                            $retc0.parent().css({
                                left: posLeft,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width($startTuxing.offset().left + $startTuxing.width() / 2 - posLeft);
                            $retc0.parent().height(posTop - $startTuxing.offset().top - $startTuxing.height());

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', $retc0.height() / 2);

                            if (posTop == $startTuxing.offset().top + $startTuxing.height()) {
                                return false;
                            }
                            else return true;
                        }
                    }
                    else if (label == 4) { //右下角

                        if ($startTuxing.offset().top + $startTuxing.height() <= offsetTop) {
                            $retc0.parent().css({
                                left: $startTuxing.offset().left + $startTuxing.width() / 2,
                                top: $startTuxing.offset().top + $startTuxing.height()
                            });

                            $retc0.parent().width(offsetLeft - $retc0.offset().left + $endTuxing.width() / 2);
                            $retc0.parent().height(offsetTop - $retc0.offset().top);

                            $retc1.width($retc0.width());
                            $retc2.width($retc0.width());

                            $retc1.height($retc0.height() / 2);
                            $retc2.height($retc0.height() / 2);

                            $retc2.css('top', (offsetTop - $retc0.offset().top) / 2);

                            if ($startTuxing.offset().top + $startTuxing.height() == offsetTop) {
                                return false;
                            } else {
                                return true;
                            }
                        }
                    }
                }
                else if (options.endObjectPointPos == "left" &&
                    options.startObjectPointPos == "left") {
                    //console.log(1111);
                    //console.log("555555555555555555555")
                    var $retc0 = null;
                    var $retc1 = null;

                    var $retc0 = $current.find('.a2B.leftAndLeft');

                    if (offsetLeft - zhexianWidth <= $startTuxing.offset().left && offsetLeft + $endTuxing.width() >= $startTuxing.offset().left) {
                        //console.log(1);
                        $retc0.parent().css({ left: offsetLeft - zhexianWidth, top: offsetTop + $endTuxing.height() / 2 });
                        $retc0.parent().width($startTuxing.offset().left - offsetLeft + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $current.find('div.leftAndLeftArrow').removeClass('right').addClass('left');

                    } else if (offsetLeft - zhexianWidth <= $startTuxing.offset().left && offsetLeft + $endTuxing.width() < $startTuxing.offset().left) {
                        //console.log(2);
                        $retc0.parent().css({ left: offsetLeft - zhexianWidth, top: offsetTop + $endTuxing.height() / 2 });
                        $retc0.parent().width($startTuxing.offset().left - offsetLeft + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);

                        $retc1 = $current.find('.hideBlock');
                        $retc1.css({ left: zhexianWidth + $endTuxing.width(), top: -5 });
                        $retc1.width($startTuxing.offset().left - offsetLeft - $endTuxing.width());
                        $current.find('div.leftAndLeftArrow').removeClass('right').addClass('left');

                    } else if (offsetLeft - zhexianWidth > $startTuxing.offset().left && $startTuxing.offset().left + $startTuxing.width() >= offsetLeft) {
                        //console.log(3);
                        $retc0.parent().css({ left: $startTuxing.offset().left - zhexianWidth, top: offsetTop + $endTuxing.height() / 2 });
                        $retc0.parent().width(offsetLeft - $startTuxing.offset().left + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $current.find('div.leftAndLeftArrow').removeClass('left').addClass('right');

                    } else if (offsetLeft - zhexianWidth > $startTuxing.offset().left && $startTuxing.offset().left + $startTuxing.width() < offsetLeft) {
                        //console.log(4);
                        $retc0.parent().css({ left: $startTuxing.offset().left - zhexianWidth, top: offsetTop + $endTuxing.height() / 2 });
                        $retc0.parent().width(offsetLeft - $startTuxing.offset().left + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $retc1 = $current.find('.hideBlock');

                        $retc1.css({
                            left: zhexianWidth + $startTuxing.width(), top: $startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2 - 5
                        });

                        $retc1.width(offsetLeft - $startTuxing.offset().left - $startTuxing.width());
                        $current.find('div.leftAndLeftArrow').removeClass('left').addClass('right');

                    }
                    if ($startTuxing.offset().top <= offsetTop + $endTuxing.height() / 2) {
                        return false;
                    }
                    else return true;
                }
                else if (options.endObjectPointPos == "right" &&
                    options.startObjectPointPos == "right") {
                    //console.log("666666666666666666666666666666")
                    //console.log(99990);
                    var $retc0 = null;
                    var $retc1 = null;

                    var $retc0 = $current.find('.a2B.rightAndRight');

                    if (offsetLeft > $startTuxing.offset().left && $startTuxing.offset().left + $startTuxing.width() >= offsetLeft) {
                        //console.log(51);
                        $retc0.parent().css({
                            left: $startTuxing.offset().left + $startTuxing.width(),
                            top: offsetTop + $endTuxing.height() / 2
                        });
                        $retc0.parent().width(offsetLeft + $endTuxing.width() - $startTuxing.offset().left - $startTuxing.width() + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $current.find('div.rightAndRightArrow').removeClass('left').addClass('right');
                    }
                    else if (offsetLeft + $endTuxing.width() >= $startTuxing.offset().left && offsetLeft <= $startTuxing.offset().left + $startTuxing.width()) {
                        //console.log(61);
                        $retc0.parent().css({
                            left: offsetLeft + $endTuxing.width(),
                            top: offsetTop + $endTuxing.height() / 2
                        });
                        $retc0.parent().width($startTuxing.offset().left + $startTuxing.width() - offsetLeft - $endTuxing.width() + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $current.find('div.rightAndRightArrow').removeClass('right').addClass('left');

                    } else if (offsetLeft >= $startTuxing.offset().left + $startTuxing.width()) {
                        //console.log(71);
                        $retc0.parent().css({
                            left: $startTuxing.offset().left + $startTuxing.width(),
                            top: offsetTop + $endTuxing.height() / 2
                        });
                        $retc0.parent().width(offsetLeft + $endTuxing.width() - $startTuxing.offset().left - $startTuxing.width() + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);

                        $retc1 = $current.find('.hideBlock');
                        $retc1.width(offsetLeft - $startTuxing.offset().left - $startTuxing.width());
                        $retc1.css({ left: 0, top: offsetTop - $endTuxing.offset().top - 5 });
                        $current.find('div.rightAndRightArrow').removeClass('left').addClass('right');

                    } else if (offsetLeft + $endTuxing.width() <= $startTuxing.offset().left) {
                       // console.log(81);
                        $retc0.parent().css({
                            left: offsetLeft + $endTuxing.width(),
                            top: offsetTop + $endTuxing.height() / 2
                        });
                        $retc0.parent().width($startTuxing.offset().left + $startTuxing.width() - offsetLeft - $endTuxing.width() + zhexianWidth);
                        $retc0.parent().height($startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2);
                        $retc1 = $current.find('.hideBlock');

                        $retc1.css({
                            left: 0, top: $startTuxing.offset().top + $startTuxing.height() / 2 - offsetTop - $endTuxing.height() / 2 - 5
                        });

                        $retc1.width($startTuxing.offset().left - offsetLeft - $endTuxing.width());
                        $current.find('div.rightAndRightArrow').removeClass('right').addClass('left');

                    }

                    if (offsetTop >= $startTuxing.offset().top - $startTuxing.height() / 2) {
                        return false;
                    }
                    else return true;

                }
            }
        }

        options.startTuxingRet.setTuxingchangeFunc($current.attr("id"),changeMyLineObject);
        options.endTuxingRet.setTuxingchangeFunc($current.attr("id") ,changeMyLineObject);

        $current.dblclick(function () {

            $(this).toggleClass('deleteMode');
            $(this).find("div.a2B.leftAndLeft").toggleClass('deleteMode');
            $(this).find("div.a2B.rightAndRight").toggleClass('deleteMode');
            $(this).find("div.a2BInner.rightAndBottom1").toggleClass('deleteMode');
            $(this).find("div.a2BInner.rightAndBottom2").toggleClass('deleteMode');
            $(this).find("div.a2BInner.leftAndBottom1").toggleClass('deleteMode');
            $(this).find("div.a2BInner.leftAndBottom2").toggleClass('deleteMode');

            if ($(this).hasClass("deleteMode")==true) {
                $(this).siblings().removeClass("deleteMode");
                $(this).siblings().find("div.a2B.leftAndLeft").removeClass('deleteMode');
                $(this).siblings().find("div.a2B.rightAndRight").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom2").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom2").removeClass('deleteMode');
            }

        });
        var deleted = false;

        $current.parent().parent().keyup(function (e) {
            
            //e.preventDefault();
            //e.stopPropagation();

            if (deleted == true) return;
            if ($current.hasClass("deleteMode")==true) {

                if (e.keyCode == 46) { //删除
                    deleteLineObject($current);
                    deleted = true;
                } else if (e.keyCode == 13 && e.ctrlKey == false) {
                    //alert("编辑");
                    if (options.editFunc) {
                        options.editFunc($current.attr("id"));
                    }
                }
            }
            
        });

        lineIndex = 0;//重置

        if (options.editFunc && objectCanMove == true && options.notPopUp != true) {
            options.editFunc($current.attr("id"));
        }
        return $(this);
    }

    //起点和终点
    $.fn.endonTuxing = function (options) {
        var $current = $(this);

        var width = options.width ? options.width : 50;
        var height = options.height ? options.height : 50;
        var offsetLeft = options.offsetLeft ? options.offsetLeft : 0;
        var offsetTop = options.offsetTop ? options.offsetTop : 0;
        var endonType = options.type;
        var color = endonType == "startEndon" ? 'green' : 'orange';
        if (options.bgColor) color = bgColor;
        //var canMove = options.canMove;
      
        if ($current.find("#" + endonType).length == 1) {
            $current.remove(); // 删掉
            alert("起点和终点只能有一个!");
            return;
        }

        var html =
            '<div class="endonInner">' +
            '<div class="point up"></div>' +
            '<div class="point down"></div>' +
            '<div class="point left"></div>' +
            '<div class="point right"></div>' + "</div>";

        $current.append(html);

        var $pUp = $current.find("div.point.up");
        var $pDown = $current.find("div.point.down");
        var $pLeft = $current.find("div.point.left");
        var $pRight = $current.find("div.point.right");
        var $point = $current.find("div.point");

        $current.width(2*width);
        $current.height(2*height);

        $current.css('left', offsetLeft);
        $current.css('top', offsetTop);
        $current.css('border-radius', width);
        $current.css('background-color', color);


        $pUp.css({ 'left': (width - 4) + 'px', 'top': '-4px' });
        $pDown.css({ 'left': (width - 4) + 'px', 'bottom': '-4px' });
        $pLeft.css({ 'left': '-4px', 'top': (height - 4) + 'px' });
        $pRight.css({ 'right': '-4px', 'top': (height - 4) + 'px' });

        var ret = $current.moveable();

        var upPoint = {};
        var downPoint = {};
        var leftPoint = {};
        var rightPoint = {};

        if (objectCanMove == false) {
            $point.hide();//不能移动   
        }

        $point.click(function (e) {
            if (lineMode == false) {
                alert("ctrl+enter切换连线模式！");
                return;
            }

            $(this).addClass("clicked");

            var offsetLeft = parseInt($current.css('left'));
            var offsetTop = parseInt($current.css('top'));

            var clickedPoint = null;
            var objectPointPos = "";

            if ($(this).hasClass("up") == true) {
                upPoint = { x: width + offsetLeft, y: 0 + offsetTop };
                clickedPoint = upPoint;
                objectPointPos = "up";
            } else if ($(this).hasClass("down") == true) {
                downPoint = { x: width + offsetLeft, y: 2 * height + offsetTop };
                clickedPoint = downPoint;
                objectPointPos = "down";
            } else if ($(this).hasClass("left") == true) {
                leftPoint = { x: 0 + offsetLeft, y: height + offsetTop };
                clickedPoint = leftPoint;
                objectPointPos = "left";
            } else if ($(this).hasClass("right") == true) {
                rightPoint = { x: 2 * width + offsetLeft, y: height + offsetTop };
                clickedPoint = rightPoint;
                objectPointPos = "right";
            }


            if (lineMode == true) {
                if (!startPoint && !endPoint) {
                    if (endonType != "startEndon") {
                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;
                        $(this).removeClass("clicked");
                        alert("只能从开始节点开始！");
                        return;
                    }

                    startPoint = clickedPoint;
                    $startObject = $(this);
                    startObjectPointPos = objectPointPos;
                    startTuxingRet = ret;

                    return;
                }
                if (startPoint && !endPoint) {
                    if ($startObject.parent().parent().attr("id") == "startEndon" &&
                        endonType != "endEndon") {
                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;
                        $(this).removeClass("clicked");
                        alert("只能到结尾节点结束！");
                        return;
                    }
                    endPoint = clickedPoint;
                    $endObject = $(this);
                    endObjectPointPos = objectPointPos;
                    endTuxingRet = ret;
                }
                if (startPoint && endPoint) {
                    var exist = false; //形同的连线不重复
                    $.each(lineObject, function (i, elem) {
                        if ($startObject.parent().parent().attr("id") == elem.$startTuxing.attr("id") &&
                            $endObject.parent().parent().attr("id") == elem.$endTuxing.attr("id")) {
                            exist = true;
                        }
                    });

                    if (exist == true) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线请不要重复！");
                        return;
                    }

                    //先在创建线条之前判断起点和终点的位置是否合适
                    if (!(
                        (endObjectPointPos == "up" && startObjectPointPos == "down")
                        ||
                        (startObjectPointPos == "left" && endObjectPointPos == "left" && startPoint.y > endPoint.y)
                        ||
                        (startObjectPointPos == "right" && endObjectPointPos == "right" && startPoint.y > endPoint.y)
                    )) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线的位置不合适！");
                        return;
                    }

                    var lineHtml = "<div class='lineObject' id=lineObject_" + (currentLineCount) + "></div>";
                    var $father = $point.parent().parent().parent();
                    $father.append(lineHtml);

                    var $lineObject = $father.find("#lineObject_" + currentLineCount);
                    $lineObject.lineObject({
                        startPoint: startPoint,
                        endPoint: endPoint,
                        startPointObject: $startObject,
                        endPointObject: $endObject,
                        startObjectPointPos: startObjectPointPos,
                        endObjectPointPos: endObjectPointPos,
                        startTuxingRet: startTuxingRet,
                        endTuxingRet: endTuxingRet,
                        editFunc: LineProcess
                    })

                    $startObject.removeClass("clicked");
                    $endObject.removeClass("clicked");

                    currentLineCount++;

                    startPoint = null;
                    endPoint = null;
                    startObjectPointPos = "";
                    endObjectPointPos = "";
                    $startObject = null;
                    $endObject = null;
                    startTuxingRet = null;
                    endTuxingRet = null;
                }
            }
            else { return; }

        });

        $current.dblclick(function (e) {
            $(this).toggleClass("deleteMode");

            if ($(this).hasClass("deleteMode") == true) {
                $(this).siblings().removeClass("deleteMode");
                $(this).siblings().find("div.a2B.leftAndLeft").removeClass('deleteMode');
                $(this).siblings().find("div.a2B.rightAndRight").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom2").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom2").removeClass('deleteMode');
            }
        })
        $current.parent().parent().keyup(function (e) {
           // e.preventDefault();
            //e.stopPropagation();

            if ($current.hasClass("deleteMode") == true) {

                if (e.keyCode == 46) { //删除
                    deleteTuxing($current);
                    console.log("deleteTuxing:endon");
                } else if (e.keyCode == 13 && e.ctrlKey == false) {
                    //alert("编辑");
                }
            }

        });

        ret.upPoint = upPoint;
        ret.downPoint = downPoint;
        ret.leftPoint = leftPoint;
        ret.rightPoint = rightPoint;
        ret.$pUp = $pUp;
        ret.$pDown = $pDown;
        ret.$pLeft = $pLeft;
        ret.$pRight = $pRight;

        tuxings.push({ id: $current.attr("id"), ret });

        return $current;
    }

    $.fn.lingxing = function (options) {

        var $current = $(this);

        var width = options.width ? options.width : 100;
        var height = options.height ? options.height : 100;
        var offsetLeft = options.offsetLeft ? options.offsetLeft : 0;
        var offsetTop = options.offsetTop ? options.offsetTop : 0;
        var color = options.color ? options.color : 'blue';
        //var canMove = options.canMove;

        var html =
            '<div class="lingxingInner"><div class="triangle up" ></div>' +
            '<div class="triangle down"></div>' +
            '<div class="point up"></div>' +
            '<div class="point down"></div>' +
            '<div class="point left"></div>' +
            '<div class="point right"></div>' + "</div>";

        $current.append(html);

        var $tUp = $current.find("div.triangle.up");
        var $tDown = $current.find("div.triangle.down");
        var $pUp = $current.find("div.point.up");
        var $pDown = $current.find("div.point.down");
        var $pLeft = $current.find("div.point.left");
        var $pRight = $current.find("div.point.right");
        var $point = $current.find("div.point");


        if (objectCanMove == false) {
            $point.hide();//不能移动   
        }

        $current.width(2 * width);
        $current.height(2 * height);

        $current.css('left', offsetLeft);
        $current.css('top', offsetTop);

        $tUp.css({ 'left': '0px', 'top': '0px', 'width': '0px', 'height': '0px' });
        $tUp.css({
            'border-left': width + 'px solid transparent',
            'border-right': width + 'px solid transparent',
            'border-bottom': height + 'px solid ' + color
        });

        $tDown.css({ 'left': '0px', 'top': height + 'px', 'width': '0px', 'height': '0px' });
        $tDown.css({
            'border-left': width + 'px solid transparent',
            'border-right': width + 'px solid transparent',
            'border-top': height + 'px solid ' + color
        });

        $pUp.css({ 'left': (width - 4) + 'px', 'top': '-4px' });
        $pDown.css({ 'left': (width - 4) + 'px', 'bottom': '-4px' });
        $pLeft.css({ 'left': '-4px', 'top': (height - 4) + 'px' });
        $pRight.css({ 'right': '-4px', 'top': (height - 4) + 'px' });


        var ret = $current.moveable();

        var upPoint = {};
        var downPoint = {};
        var leftPoint = {};
        var rightPoint = {};

        $point.click(function (e) {
            if (lineMode == false) {
                alert("ctrl+enter切换连线模式！");
                return;
            }

            $(this).addClass("clicked");

            var offsetLeft = parseInt($current.css('left'));
            var offsetTop = parseInt($current.css('top'));

            var clickedPoint = null;
            var objectPointPos = "up";

            if ($(this).hasClass("up") == true) {
                upPoint = { x: width + offsetLeft, y: 0 + offsetTop };
                clickedPoint = upPoint;
                objectPointPos = "up";
            } else if ($(this).hasClass("down") == true) {
                downPoint = { x: width + offsetLeft, y: 2 * height + offsetTop };
                clickedPoint = downPoint;
                objectPointPos = "down";
            } else if ($(this).hasClass("left") == true) {
                leftPoint = { x: 0 + offsetLeft, y: height + offsetTop };
                clickedPoint = leftPoint;
                objectPointPos = "left";
            } else if ($(this).hasClass("right") == true) {
                rightPoint = { x: 2 * width+ offsetLeft ,y:  height +offsetTop };
                clickedPoint = rightPoint;
                objectPointPos = "right";
            }

            if (lineMode == true) {
                if (!startPoint && !endPoint) {
                    startPoint = clickedPoint;
                    $startObject = $(this);
                    startObjectPointPos = objectPointPos;
                    startTuxingRet = ret;
                    return;
                }
                if (startPoint && !endPoint) {
                    endPoint = clickedPoint;
                    $endObject = $(this);
                    endObjectPointPos = objectPointPos;
                    endTuxingRet = ret;
                }
                if (startPoint && endPoint) {
                    var exist = false; //形同的连线不重复
                    $.each(lineObject, function (i, elem) {
                        if ($startObject.parent().parent().attr("id") == elem.$startTuxing.attr("id") &&
                            $endObject.parent().parent().attr("id") == elem.$endTuxing.attr("id")) {
                            exist = true;
                        }
                    });

                    if (exist == true) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线请不要重复！");
                        return;
                    }

                    //先在创建线条之前判断起点和终点的位置是否合适
                    if (!(
                        (endObjectPointPos == "up" && startObjectPointPos == "down")
                        ||
                        (startObjectPointPos == "left" && endObjectPointPos == "left" && startPoint.y > endPoint.y)
                        ||
                        (startObjectPointPos == "right" && endObjectPointPos == "right" && startPoint.y > endPoint.y)
                    )) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线的位置不合适！");
                        return;
                    }

                    var lineHtml = "<div class='lineObject' id=lineObject_" + (currentLineCount) + "></div>";
                    var $father = $point.parent().parent().parent();
                    $father.append(lineHtml);

                    var $lineObject = $father.find("#lineObject_" + currentLineCount);
                    $lineObject.lineObject({
                        startPoint: startPoint,
                        endPoint: endPoint,
                        startPointObject: $startObject,
                        endPointObject: $endObject,
                        startObjectPointPos: startObjectPointPos,
                        endObjectPointPos: endObjectPointPos,
                        startTuxingRet: startTuxingRet,
                        endTuxingRet: endTuxingRet,
                        editFunc: LineProcess
                    })

                    $startObject.removeClass("clicked");
                    $endObject.removeClass("clicked");

                    currentLineCount++;

                    startPoint = null;
                    endPoint = null;
                    startObjectPointPos = "";
                    endObjectPointPos = "";
                    $startObject = null;
                    $endObject = null;
                    startTuxingRet = null;
                    endTuxingRet = null;
                }
            }
            else { return; }

        });

        $current.dblclick(function () {
            $(this).toggleClass("deleteMode");
     
            if ($(this).hasClass("deleteMode")==true) {
                $(this).siblings().removeClass("deleteMode");
                $(this).siblings().find("div.a2B.leftAndLeft").removeClass('deleteMode');
                $(this).siblings().find("div.a2B.rightAndRight").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom2").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom2").removeClass('deleteMode');
            }
        });

        var isDeleted = false;
        $current.parent().parent().keyup(function (e) {
            //e.preventDefault();
            //e.stopPropagation();

            if (isDeleted == true) return;
            if ($current.hasClass("deleteMode")==true) {

                if (e.keyCode == 46) { //删除
                    if (!!$current) {
                        deleteTuxing($current);
                        //console.log("deleteTuxing:lingxing:" + $current.attr("id"));
                        isDeleted = true;
                    }           
                } else if (e.keyCode == 13 && e.ctrlKey == false) {
                    //alert("编辑");
                    if (options.editFunc) {
                        options.editFunc($current.attr("id"));
                    }
                }
            }
        });

        ret.upPoint = upPoint;
        ret.downPoint = downPoint;
        ret.leftPoint = leftPoint;
        ret.rightPoint = rightPoint;
        ret.$pUp = $pUp;
        ret.$pDown = $pDown;
        ret.$pLeft = $pLeft;
        ret.$pRight = $pRight;

        tuxings.push({ id: $current.attr("id"), ret });

        return $current;
    }

    $.fn.fangxing = function (options) {

        var $current = $(this);

        var width = options.width ? options.width : 100;
        var height = options.height ? options.height : 100;
        var offsetLeft = options.offsetLeft ? options.offsetLeft : 0;
        var offsetTop = options.offsetTop ? options.offsetTop : 0;
        var color = options.color ? options.color : 'blue';
        //var canMove = options.canMove;

        var html =
            '<div class="fangxingInner">' +
            '<div class="point up"></div>' +
            '<div class="point down"></div>' +
            '<div class="point left"></div>' +
            '<div class="point right"></div>' + "</div>";

        $current.append(html);

        var $pUp = $current.find("div.point.up");
        var $pDown = $current.find("div.point.down");
        var $pLeft = $current.find("div.point.left");
        var $pRight = $current.find("div.point.right");
        var $point = $current.find("div.point");

        if (objectCanMove == false) {
            $point.hide();//不能移动   
        }

        $current.width(2 * width);
        $current.height(2 * height);

        $current.css('left', offsetLeft);
        $current.css('top', offsetTop);
        $current.css('border-radius', 10);
        $current.css('background-color', color);

        $pUp.css({ 'left': (width - 4) + 'px', 'top': '-4px' });
        $pDown.css({ 'left': (width - 4) + 'px', 'bottom': '-4px' });
        $pLeft.css({ 'left': '-4px', 'top': (height - 4) + 'px' });
        $pRight.css({ 'right': '-4px', 'top': (height - 4) + 'px' });


        var ret = $current.moveable();
        
        var upPoint = {};
        var downPoint = {};
        var leftPoint = {};
        var rightPoint = {};

        $point.click(function (e) {
            if (lineMode == false) {
                alert("ctrl+enter切换连线模式！");
                return;
            }

            $(this).addClass("clicked");

            var offsetLeft = parseInt($current.css('left'));
            var offsetTop = parseInt($current.css('top'));

            var clickedPoint = null;
            var objectPointPos = "up";

            if ($(this).hasClass("up") == true) {
                upPoint = { x: width + offsetLeft, y: 0 + offsetTop };
                clickedPoint = upPoint;
                objectPointPos = "up";
            } else if ($(this).hasClass("down") == true) {
                downPoint = { x: width + offsetLeft, y: 2 * height + offsetTop };
                clickedPoint = downPoint;
                objectPointPos = "down";
            } else if ($(this).hasClass("left") == true) {
                leftPoint = { x: 0 + offsetLeft, y: height + offsetTop };
                clickedPoint = leftPoint;
                objectPointPos = "left";
            } else if ($(this).hasClass("right") == true) {
                rightPoint = { x: 2 * width + offsetLeft, y: height + offsetTop };
                clickedPoint = rightPoint;
                objectPointPos = "right";
            }

            if (lineMode == true) {
                if (!startPoint && !endPoint) {
                    startPoint = clickedPoint;
                    $startObject = $(this);
                    startObjectPointPos = objectPointPos;
                    startTuxingRet = ret;
                    return;
                }
                if (startPoint && !endPoint) {
                    endPoint = clickedPoint;
                    $endObject = $(this);
                    endObjectPointPos = objectPointPos;
                    endTuxingRet = ret;
                }
                if (startPoint && endPoint) {
                    var exist = false; //形同的连线不重复
                    $.each(lineObject, function (i, elem) {
                        if ($startObject.parent().parent().attr("id") == elem.$startTuxing.attr("id") &&
                            $endObject.parent().parent().attr("id") == elem.$endTuxing.attr("id")) {
                            exist = true;
                        }
                    });

                    if (exist == true) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线请不要重复！");
                        return;
                    }

                    //先在创建线条之前判断起点和终点的位置是否合适
                    if (!(
                        (endObjectPointPos == "up" && startObjectPointPos == "down")
                        ||
                        (startObjectPointPos == "left" && endObjectPointPos == "left" && startPoint.y > endPoint.y)
                        ||
                        (startObjectPointPos == "right" && endObjectPointPos == "right" && startPoint.y > endPoint.y)
                    )) {
                        $startObject.removeClass("clicked");
                        $endObject.removeClass("clicked");

                        startPoint = null;
                        endPoint = null;
                        startObjectPointPos = "";
                        endObjectPointPos = "";
                        $startObject = null;
                        $endObject = null;
                        startTuxingRet = null;
                        endTuxingRet = null;

                        alert("连线的位置不合适！");
                        return;
                    }

                    var lineHtml = "<div class='lineObject' id=lineObject_" + (currentLineCount) + "></div>";
                    var $father = $point.parent().parent().parent();
                    $father.append(lineHtml);

                    var $lineObject = $father.find("#lineObject_" + currentLineCount);
                    $lineObject.lineObject({
                        startPoint: startPoint,
                        endPoint: endPoint,
                        startPointObject: $startObject,
                        endPointObject: $endObject,
                        startObjectPointPos: startObjectPointPos,
                        endObjectPointPos: endObjectPointPos,
                        startTuxingRet: startTuxingRet,
                        endTuxingRet: endTuxingRet,
                        editFunc: LineProcess
                    })

                    $startObject.removeClass("clicked");
                    $endObject.removeClass("clicked");

                    if (!options.editFunc) {
    
                       options.editFunc($current.attr("id"));
                    }

                    currentLineCount++;

                    startPoint = null;
                    endPoint = null;
                    startObjectPointPos = "";
                    endObjectPointPos = "";
                    $startObject = null;
                    $endObject = null;
                    startTuxingRet = null;
                    endTuxingRet = null;
                }
            }
            else { return; }

        });
        $current.dblclick(function (e) {

            $(this).toggleClass("deleteMode");
   
            if ($(this).hasClass("deleteMode")) {
                $(this).siblings().removeClass("deleteMode");
                $(this).siblings().find("div.a2B.leftAndLeft").removeClass('deleteMode');
                $(this).siblings().find("div.a2B.rightAndRight").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.rightAndBottom2").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom1").removeClass('deleteMode');
                $(this).siblings().find("div.a2BInner.leftAndBottom2").removeClass('deleteMode');
            }
        })

        var isDeleted = false;
        $current.parent().parent().keyup(function (e) {
            //e.preventDefault();
            //e.stopPropagation();

            if (isDeleted == true) return;
           
            if ($current.hasClass("deleteMode")==true) {
      
                if (e.keyCode == 46) { //删除
                    if (!!$current) {
                        //console.log("deleteTuxing:fangxing:" + $current.attr("id"));
                        deleteTuxing($current);
                        isDeleted = true;
                    }

                } else if (e.keyCode == 13 && e.ctrlKey == false) {
                    //alert("编辑");
                    if (options.editFunc) {
                        options.editFunc($current.attr("id"));
                    }

                }
            }
        });

        ret.upPoint = upPoint;
        ret.downPoint = downPoint;
        ret.leftPoint = leftPoint;
        ret.rightPoint = rightPoint;
        ret.$pUp = $pUp;
        ret.$pDown = $pDown;
        ret.$pLeft = $pLeft;
        ret.$pRight = $pRight;

        tuxings.push({ id: $current.attr("id"), ret });

        return ret;
    }

    $.fn.moveable = function (options) {

        var $current = $(this);

        var offsetX = 0;
        var offsetY = 0;
        var down = false;

        var moveTop = true;

        $current.mousedown(function (e) {
            
            var origLeft = $current.offset().left;
            var origTop = $current.offset().top;

            var downX = e.clientX;
            var downY = e.clientY;

            offsetX = downX - origLeft;
            offsetY = downY - origTop;
            
            down = true;
            
        }).mouseup(function (e) {           
            down = false;          
        });

        $(".contentWrapper").mousemove(function (e) {
            if (down == true && objectCanMove ==true) {

                var downX = e.clientX;
                var downY = e.clientY;

                if (moveTop == true) {
                    $current.css('left', downX - offsetX);
                    $current.css('top', downY - offsetY);
                }

                moveTop = reCalled($current, downX - offsetX, downY - offsetY);
            }
           
        });
      
        var changeLineObjectCalled = [];
        //图形对应线条的注册回调
        var reCalled = function ($tuxing, offsetLeft, offsetTop) {

            var anyCallFalse = true;
            var callRet = true;
            $.each(changeLineObjectCalled, function (i, elem) {
                //console.log("$tuxing id:" + $tuxing.attr("id"));
                if (elem.callFunc) {
                    callRet = elem.callFunc($tuxing, offsetLeft, offsetTop);
                    if (callRet == false) anyCallFalse = false;//有一个返回false,整体返回false
                }
            });

            return anyCallFalse;
        }

        var ret = {
            'setTuxingchangeFunc': function (lineId, callFunc) {
                //console.log("setTuxingchangeFunc lineId:" + lineId);
                changeLineObjectCalled.push({ lineId, callFunc });
            }
            ,
            'removeTuxingchangeFunc': function (lineId) {
                //console.log("removeTuxingchangeFunc: lineId:" + lineId);
                var deleteId = -1;
                for (var i in changeLineObjectCalled) {
                    if (changeLineObjectCalled[i].lineId == lineId) {
                        deleteId = i;
                        break;
                    }
                }
                //console.log("removeTuxingchangeFunc:deleteId:" + deleteId);
                if (deleteId >= 0) {
                    //console.log("removeTuxingchangeFunc : " + deleteId);
                    changeLineObjectCalled.splice(deleteId, 1);
                }
            }
        };

        return ret;
    }

})(jQuery);



