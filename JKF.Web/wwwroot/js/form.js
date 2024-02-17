(function ($, jkf) {

    $.extend({

        setSelect: function(options) {
            var datas = null;
            if (!!options.data) {
                datas = options.data;
            } else {
                var res = $.httpPost({ url: options.url });
                datas = res.datas;
            }

            $("#" + options.ctrlId).remove(); //先删除

            renderSelection(
                options.textCtrlId,
                options.ctrlId,
                options.idKey,
                options.valueKey,
                options.width,
                options.height,
                datas);
        },
        filterSelection: function (textCtrlId, ctrlId) {
            var filterVal = $("#" + textCtrlId).val();
            var $selection = $("body").find("#" + ctrlId);
            var $ul = $selection.find("ul");
            var items = $ul.find("li");
            //var noneSelect = false;
            items.each(function (index, elem) {
                if (!filterVal && $(this).attr("data-value").indexOf(filterVal) >= 0) {
                    $(this).show();
                    //noneSelect = true;
                } else {
                    $(this).hide();
                }
            })
            //if (noneSelect == false) {
            //    $("#" + textCtrlId).data("value", "");
            //}
        },
        setTreeSelect: function (options) {

            var datas = null;
            if (!!options.data ) {
                datas = options.data;
            } else {
                var res = $.httpPost({ url: options.url });
                datas = res.datas;
            }

            //alert(datas.length);
            //console.log("111"+datas);

            var treeData = getTreeSelectData(datas,                   
                options.parentIdKey,
                options.topParentIdValue,
                options.idKey,
                options.targetKey,
                options.targetValue,
                options.hideDownLevelKeyValue
            );

            //alert(treeData.length);

            $("#" + options.ctrlId).remove(); //先删除

            renderTreeSelection(
                options.textCtrlId,
                options.ctrlId,
                options.idKey,
                options.valueKey,
                options.idValue,
                options.width,
                options.height,
                options.topParentIdValue,
                treeData,
                0);
            
        },
        getSelectValue: function (ctrlId,$body,idValue) {
            var value = "";
            var $lis = $(ctrlId, $body).find("ul li");
            for (var i = 0; i < $lis.length;i++) {
                if ($($lis.get(i)).attr("data-id") == idValue) {
                    value = $($lis.get(i)).attr("data-value");
                    return value;
                }
            }
            return value;
        },
        noPermitInput :function(e) {
            var evt = window.event || e;
            if(isIE()) {
            evt.returnValue = false; //ie 禁止键盘输入    
            } else {
                evt.preventDefault(); //fire fox 禁止键盘输入    
            }
        }   , 
        getFormData: function (data, $form) {

            if ($form.length <= 0) {
                jkf.showTips("表单对象不存在！",2);
                return;
            }

            //var $form = $("#" + divFormId);
            //console.log($form);

            //先遍历类型为text的控件
            var $textItems = $form.find("input[data-type=text]");
            
            $textItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                data[itemName] = $item.val();
            });

            var $textItems = $form.find("input[data-type=regex]");

            $textItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                data[itemName] = $item.val();
            });


            //先遍历类型为text的控件
            var $integerItems = $form.find("input[data-type=integer]");

            $integerItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                data[itemName] = $item.val();
            });

            //遍历text_select的控件
            var $selectItems = $form.find("input[data-type=text_select]");
            
            $selectItems.each(function (i,elem) {
               
                //alert($item.attr("id"));
                var itemName = $(this).attr("id");
                data[itemName] = $(this).data("value");

                //console.log(itemName + ":" + data[itemName]);
                
            })

            //遍历radio group
            var $radioItems = $form.find("input[data-type=radio]");

            $radioItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                var value = $form.find("input[name='" + itemName + "']:checked").val();
                data[itemName] = value;
            })

            //遍历textArea
            var $textAreaItems = $form.find("textarea");
            $textAreaItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                var value = $item.val();
                data[itemName] = value;
            })

            //日期
            var $dateItems = $form.find("input[data-type=date]");

            $dateItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                if (!!$item.val())
                    data[itemName] = $item.val().substr(0, 10);

            });

            //遍历类型为image的控件
            var $imageItems = $form.find("input[data-type=image]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                if (!!$item.val())
                    data[itemName] = $item.val();

            });

            //遍历类型为image的控件(多图片)
            var $imageItems = $form.find("input[data-type=images]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                if (!!$item.val())
                    data[itemName] = $item.val();

            });

            //遍历类型为image的控件(多图片)
            var $imageItems = $form.find("input[data-type=files]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                if (!!$item.val())
                    data[itemName] = $item.val();

            });

            //先遍历类型为text的控件
            var $textItems = $form.find("select[data-type=select]");

            $textItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                data[itemName] = $item.find("option:selected").val();
            });

            //遍历checkbox的控件
            var $checkBoxItems = $form.find("input[data-type=checkbox]");
            // alert("text_select.count:" + $selectItems.length);
            $checkBoxItems.each(function () {
                var $item = $(this);
                var itemName = $item.attr("id");
                var itemVal = "";

                $items = $("input[id^=" + itemName + "_]");
                $items.each(function (i, elem) {
                    if ($(this).prop("checked") == true) {

                        //alert($(this).attr("id"));
                        itemVal += $(this).val() + ";";
                    }
                        
                });
                data[itemName] = itemVal;

            })


        },
        setFormData: function (data, $form) {

            //alert(1);
            if ($form.length <= 0) {
                jkf.showTips("表单对象不存在！",2);
                return;
            }

            //var $form = $("#" + divFormId);
            //console.log($form);

            //先遍历类型为text的控件
            var $textItems = $form.find("input[data-type=text]");

            $textItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);
               
            });

            var $textItems = $form.find("input[data-type=regex]");

            $textItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);

            });

            //先遍历类型为text的控件
            var $integerItems = $form.find("input[data-type=integer]");

            $integerItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);

            });

            //遍历text_select的控件
            var $selectItems = $form.find("input[data-type=text_select]");
           // alert("text_select.count:" + $selectItems.length);
            $selectItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                //alert("1:"+itemName);
                //alert("2:"+data[itemName]);
                //alert("3:" + top.jkf.getSelectValue("#" + itemName + "_select", $('body'), data[itemName]));
                
                $item.data("value", data[itemName]);
                $item.val($.getSelectValue("#" + itemName + "_select", $("body"), data[itemName]));

            })

            //遍历radio group
            var $radioItems = $form.find("input[data-type=radio]");

            $radioItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");
                $form.find("input[name='" + itemName + "'][value=" + data[itemName] + "]").attr("checked", "checked");
                
            })

            //遍历textArea
            var $textAreaItems = $form.find("textarea");
            $textAreaItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);

            })
            //日期
            var $dateItems = $form.find("input[data-type=date]");

            $dateItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                if (!!data[itemName])
                $item.val(data[itemName].substr(0,10));

            });

            //遍历类型为image的控件
            var $imageItems = $form.find("input[data-type=image]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);
                $("#" + itemName + "_image").attr("src", data[itemName]);

            });

            //遍历类型为image的控件
            var $imageItems = $form.find("input[data-type=images]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);
                //alert(data[itemName]);
                var firstImage = (!!data[itemName] ? data[itemName].split(';')[0] : "");
                $("#" + itemName + "_image").attr("src", firstImage);

            });

            //遍历类型为image的控件
            var $imageItems = $form.find("input[data-type=files]");

            $imageItems.each(function () {
                var $item = $(this);
                //alert($item.attr("id"));
                var itemName = $item.attr("id");

                $item.val(data[itemName]);
                //alert(data[itemName]);
                //var firstImage = (!!data[itemName] ? data[itemName].split(';')[0] : "");
                //$("#" + itemName + "_image").attr("src", firstImage);

            });

            //遍历select的控件
            var $selectItems = $form.find("select[data-type=select]");
            // alert("text_select.count:" + $selectItems.length);
            $selectItems.each(function () {
                var $item = $(this);
                var itemName = $item.attr("id");
                //alert(itemName);
                //alert(data[itemName]);
                $item.val(data[itemName]);
                
               
            })

            //遍历checkbox的控件
            var $checkBoxItems = $form.find("input[data-type=checkbox]");
            // alert("text_select.count:" + $selectItems.length);
            $checkBoxItems.each(function () {
                var $item = $(this);
                var itemName = $item.attr("id");
                var itemVal = data[itemName];
                //alert(itemVal);
                var itemVals = itemVal.split(';');
                for (var i in itemVals) {
                    $("input[id=" + itemName + "_" + itemVals[i] + "]").prop("checked", true); 
                }
                
                //alert(itemName);
                //alert(data[itemName]);
                //$item.val(data[itemName]);


            })


           
        },
        getAbsPos: function ($target) {
            return absPos($target.get(0));
        }
    });

  

    function renderSelection(textCtrlId, ctrlId, idKey, valueKey, width, height, datas) {
        var htmlText = "<div id='" + ctrlId + "' style='width:" + width + ";height:" + height + "' class='form_selection'></div>"

        $("body").append(htmlText);
        var $selection = $("body").find("#" + ctrlId);
        $selection.hide();
        var $text = $("body").find("#" + textCtrlId);

        $selection.append("<ul></ul>");
        var $ul = $selection.find("ul");

        renderDataItem($ul, idKey, valueKey, datas);

        $ul.find("li").click(function (e) {
            $(this).addClass("selected").siblings("li").removeClass("selected");
            $text.val($(this).data("value"));

            $selection.data("selectValue", $(this).data("id"));
            //alert($selection.data("selectValue"));
            $text.data("value", $(this).data("id"));
            //alert($text.data("value"));

            $selection.hide();
            $text.trigger("change");
        })

        $text.hover(function () {

        }, function () {
            $text.blur();
            //$selection.hide();

        })

        $text.focusin(function (e) {
            //alert("focusin1");
            var pos = absPos($text.get(0));

            var textCtrlLeft = pos.x;
            var textCtrlTop = pos.y;

            //console.log(textCtrlLeft);
            //console.log(textCtrlTop);

            var topToWindow = $("body").height() - textCtrlTop;
            var selectionHeight = $selection.height();

            if (topToWindow >= selectionHeight - $text.height()) {
                $selection.css("top", textCtrlTop + $text.height())
            } else {

                $selection.css("bottom", textCtrlTop)
            }
            $selection.css("left", textCtrlLeft);
            $selection.show();
           
        });
        $(window).resize(function () {
            $selection.hide();
        })

        $text.focusout(function () {
            //$selection.hide();
        });

        $selection.hover(function () {

        }, function () {
            $(this).hide();
        })

        $selection.width(width);
        $selection.height(height);
        setTimeout(function () {
            $selection.getNiceScroll().remove();
            $selection.niceScroll({
                'cursorcolor': '#999999',
                'cursorborder': 'none',
                'smoothscroll': 'true',
                'autohidemode': true,
                'horizrailenabled': true,
                'enablemousewheel': true,
                'oneaxismousemode': "auto",
            });
        }, 50);

    }
    function renderTreeSelection(textCtrlId, ctrlId, idKey, valueKey, idValue, width, height, topParentIdValue,treeData, level) {

        var htmlText = "<div id='" + ctrlId + "' style='width:" + width + ";height:" + height + "' class='form_selection'></div>"

        $("body").append(htmlText);
        var $selection = $("body").find("#" + ctrlId);
        $selection.hide();
        var $text = $("body").find("#" + textCtrlId);

        $selection.append("<ul></ul>");
        var $ul = $selection.find("ul");

        //console.log("11"+treeData);

        var liText = "<li data-id='" + topParentIdValue + "' data-value='根节点'>根节点</li>";
        $ul.append(liText);

        renderTreeDataItem($ul, idKey, valueKey, idValue, treeData, level);

        $ul.find("li").click(function (e) {
            $(this).addClass("selected").siblings("li").removeClass("selected");
            $text.val($(this).data("value"));
            
            $selection.data("selectValue", $(this).data("id"));
            //alert($selection.data("selectValue"));
            $text.data("value", $(this).data("id"));
            //alert($text.data("value"));

            $selection.hide();
            $text.trigger("change");
        })

        $text.hover(function () {

        }, function () {
                $text.blur();
                //$selection.hide();

        })

        $text.focusin(function (e) {
            //alert("focusin1");
            var pos = absPos($text.get(0));

            var textCtrlLeft = pos.x;
            var textCtrlTop = pos.y;

            //console.log(textCtrlLeft);
            //console.log(textCtrlTop);

            var topToWindow = $("body").height() - textCtrlTop;
            var selectionHeight = $selection.height();

            if (topToWindow >= selectionHeight - $text.height()) {
                $selection.css("top", textCtrlTop + $text.height())
            } else {

                $selection.css("bottom", textCtrlTop)
            }
            $selection.css("left", textCtrlLeft);
           
            $selection.show();

        });

        $(window).resize(function(){
            $selection.hide();
        })

        $text.focusout(function () {
            //$selection.hide();
        });

        $selection.hover(function () {

        }, function () {
                $(this).hide();
        })

        $selection.width(width);
        $selection.height(height);
        setTimeout(function () {
            $selection.getNiceScroll().remove();
            $selection.niceScroll({
                'cursorcolor': '#999999',
                'cursorborder': 'none',
                'smoothscroll': 'true',
                'autohidemode': true,
                'horizrailenabled': true,
                'enablemousewheel': true,
                'oneaxismousemode': "auto",
            });
        }, 50);


    }
    function absPos(node) {
        var x = y = 0;
        do {
            x += node.offsetLeft;
            y += node.offsetTop;
        } while (node = node.offsetParent);
        return {
            'x': x,
            'y': y
        };
    }
    function isIE() {
        if (window.navigator.userAgent.toLowerCase().indexOf("msie") >= 1)
            return true;
        else
            return false;
    }     
    function renderDataItem($ul, idKey, valueKey, datas) {
        for (var i in datas) {

            var data = datas[i];
            var text = data[valueKey];
            
            text = "&nbsp;" + text;
            
            var liText = "<li data-id='" + data[idKey] + "' data-value='" + data[valueKey] + "'>" + text + "</li>";
            $ul.append(liText);
        }
    }
    function renderTreeDataItem($ul, idKey, valueKey, idValue, treeData,  level) {

        for (var i in treeData) {
            var data = treeData[i];
            var text = "|-" + data[valueKey];
            for (var i = 0; i < level; i++) {
                text = "&nbsp;&nbsp;&nbsp;" + text;
            }
            var liText = "<li data-id='" + data[idKey] + "' data-value='" + data[valueKey] + "'>" + text+"</li>";
            $ul.append(liText);
            if (!!data.children && data.children.length > 0) {              
               renderTreeDataItem($ul, idKey, valueKey, idValue, data.children, level+1)             
            }
        }
    }
    function getTreeSelectData(datas, parentIdKey, parentIdVal, IdKey, targetKey, targetValue, hideDownLevelKeyValue) {
        var treeData = [];

        for (var i in datas) {

            if (datas[i][parentIdKey] == parentIdVal) {
                //console.log(targetKey);
                //console.log(targetValue);

                //alert("1:" + datas[i][IdKey]);
                //alert("2:" + hideDownLevelKeyValue);

                if (!!hideDownLevelKeyValue && datas[i][IdKey] == hideDownLevelKeyValue) {
                    continue;
                }

                //有没有过滤字段
                if (targetKey) {
                    if (!(datas[i][targetKey] == targetValue)) {
                        continue;
                    }
                }
                //console.log(2);
                treeData.push(datas[i]);
                //alert(datas[i][IdKey]);
                var children = getTreeSelectData(datas, parentIdKey, datas[i][IdKey], IdKey, targetKey, targetValue, hideDownLevelKeyValue);
                //alert("children:" + children.length)
                datas[i].children = children;
            }
        }
        return treeData;
    }

})(jQuery,top.jkf)