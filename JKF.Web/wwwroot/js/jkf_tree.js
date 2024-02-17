(function ($, jkf) {

    $.fn.jkf_tree = function (options) {

        var $that = $($(this).get(0));

        var _default = {
            url: null,
            param: null,
            type: "post",
            async: false,
            datas:null,
            treeParentIdKey: 'ParentId',
            treeKeyKey: '',
            treeValueKey: '',
            iconValueKey: '',
            defaultIcon:'fa-leaf',
            hasCheckBox: true,
            checkEvent: null,
            selectEvent: null,
            initCheckBoxData: null,
            initSelectValue: null, 
            initCheckBoxByValues: null,
            noMatchUpLevelCheck: false

        }

        var configs = $.extend({}, _default, options);
        var datas = null;
        var treeDatas = null;
        var rootData = null;

        var renderTreeList = function () {
            if (!!configs.url && configs.url.length > 0) {
                if (configs.async == false) {
                    if (configs.type == "get") {
                        configs.datas = $.httpGet({ url: configs.url, param: configs.param }).datas;
                        renderTreeHtml();

                    } else {
                        configs.datas = $.httpPost({ url: configs.url, param: configs.param }).datas;
                        renderTreeHtml();
                    }
                } else {

                    if (configs.type == "get") {
                        $.httpGetAsync({ url: configs.url, param: configs.param }, function (res) {
                            configs.datas = res.datas;
                            renderTreeHtml();
                        });
                    } else {
                        $.httpPostAsync({ url: configs.url, param: configs.param }, function (res) {
                            configs.datas = res.datas;
                            renderTreeHtml();
                        });
                    }
                }

            } else if (!!configs.datas) {
                renderTreeHtml();
            }
        };

        var processChildrenTreeData = function (datas, parentId) {
            var treeData = [];

            if (configs.treeParentIdKey == '') {
                treeData = datas;
            } else {
                for (var i in datas) {
                    if (datas[i][configs.treeParentIdKey] == parentId) {
                        treeData.push(datas[i]);
                        var children = processChildrenTreeData(datas, datas[i][configs.treeKeyKey]);
                        datas[i].children = children;
                    }
                }
            }

            return treeData;
        };

        var renderChildrenTreeData = function (margin_left,parentId,data) {


            $that.append("<div id=" + parentId + "_" + data[configs.treeKeyKey]+ "  class='item'></div>");
            $item = $that.find("#" + parentId + "_" + data[configs.treeKeyKey]);

            $item.append("<span id=tree_" + parentId + "_" + data[configs.treeKeyKey]+ " class='treeItem'></span>");
            var $tree_item = null;

            $tree_item = $item.find("#tree_" + parentId + "_" + data[configs.treeKeyKey]);

            if (!!data.children && data.children.length > 0) {
                $tree_item.append("<i class='fold-click fa fa-caret-down'></i>&nbsp;");
            }

            $tree_item.append("<input data-value='" + data[configs.treeKeyKey]+"' type='checkbox' class='blue' id=check_" + parentId + "_" + data[configs.treeKeyKey] + " >");

            if (configs.iconValueKey != "") {
                $tree_item.append("&nbsp;<span class='itemName'><i class='fa " + data[configs.iconValueKey] + "'> " + data[configs.treeValueKey]+"</span>");
            } else {
                $tree_item.append("&nbsp;<span class='itemName'><i class='fa " + configs.defaultIcon + "'>" + data[configs.treeValueKey]+"</span>");
            }
            margin_left += 18;

            $item.css("margin-left", margin_left);
            
            for (var i in data.children) {
                var childrenData = data.children[i];
                renderChildrenTreeData(margin_left, parentId + "_" + data[configs.treeKeyKey],childrenData);
            }
        }

        var renderTreeHtml = function () {
            //if (!configs.datas || configs.datas.length <= 0) {
            $that.empty();
            //return;
            //}

            datas = configs.datas;
            // console.log(datas);

            treeDatas = processChildrenTreeData(datas, '0');

            //console.log(treeDatas);
            if (treeDatas == null || treeDatas.length <= 0) {
                return;
            }

            rootData = {};
            rootData.children = treeDatas;

            $that.append("<div id='root' class='item'></div>");
            $root = $that.find("#root");

            $root.append("<span id='tree_root' class='treeItem'></span>");
            var $tree_item_root = null;

            if (!!rootData.children && rootData.children.length > 0) {
                $tree_item_root = $root.find("#tree_root");
                $tree_item_root.append("<i class='fold-click fa fa-caret-down'></i>&nbsp;");
            }
            
            $tree_item_root.append("<input data-value='root' type='checkbox' class='blue' id='check_root'/>");           
            $tree_item_root.append("&nbsp;<span class='itemName'><i class='fa fa-tree'></i>根节点</span>");
            var margin_left = 0;
            for (var i in rootData.children) {
                renderChildrenTreeData(margin_left, 'root', rootData.children[i]);                           
            }

            if (configs.hasCheckBox == false) {
                $that.find(":checkbox").hide();
            }

        }

        //父亲节点的(一直向上找到根节点)
        var findAndCheckUpRootNode = function (id, checked) {
            var value = $that.find(":checkbox[id=" + id + "]").attr("data-value");
            var upId = id.replace("_"+ value, "");
            if (upId != "check") {
                $that.find(":checkbox[id=" + upId + "]").prop("checked", checked);
                value = $that.find(":checkbox[id=" + upId + "]").attr("data-value");
                if (!!configs.checkEvent && typeof (configs.checkEvent) == 'function') {
                    if (value == "root")
                        configs.checkEvent("0", checked, true);
                    else
                        configs.checkEvent(value, checked, true);
                }
                var attrId = $that.find(":checkbox[id=" + upId + "]").attr("id");
                findAndCheckUpRootNode(attrId, checked);
            }
        }

        var setEvent = function () {

            $that.find(".fold-click").click(function () {

                $fold = $(this);
                $divItem = $fold.parent().parent();

                var divId = $divItem.attr("id") + "_";

                if ($fold.hasClass('fa-caret-down')) {
                    $that.find("div[id*=" + divId + "]").hide();
                    $fold.removeClass("fa-caret-down").addClass("fa-caret-right");
                } else {
                    $that.find("div[id*=" + divId + "]").show();
                    $fold.removeClass("fa-caret-right").addClass("fa-caret-down");
                }              
            })

            $that.find(":checkbox").click(function () {
                var value = $(this).attr("data-value");
                var checked = $(this).prop("checked");
                if (!!configs.checkEvent && typeof(configs.checkEvent) == 'function') {
                    if (value == "root")
                        configs.checkEvent("0", checked, false);
                    else
                        configs.checkEvent(value, checked, false);
                }

                //处理孩子节点
                var id = $(this).attr("id") + "_";
                $that.find(":checkbox[id*=" + id + "]").prop("checked", checked);


                //alert(configs.noMatchUpLevelCheck);

                if (configs.noMatchUpLevelCheck == false && checked == true)
                    //处理父亲节点
                    findAndCheckUpRootNode($(this).attr("id"),checked);

            });


            $that.find("span.itemName").click(function () {
                if (configs.hasCheckBox == true) {
                    var $checkbox = $(this).siblings("input[type=checkbox]");

                    var checked = $checkbox.prop("checked");
                    var value = $checkbox.attr("data-value");

                    if (checked) {
                        $(this).addClass("selected");
                        $checkbox.parent().parent().siblings().find("span.itemName").removeClass("selected");
                    } else {
                        $(this).removeClass("selected");
                    }

                    if (!!configs.selectEvent && typeof (configs.selectEvent) == 'function') {
                        configs.selectEvent(value, checked);
                    }
                } else {                    
                    $that.find("span.itemName").removeClass("selected");
                    $(this).addClass("selected");

                    if (!!configs.selectEvent && typeof (configs.selectEvent) == 'function') {
                        var $checkbox = $(this).siblings("input[type=checkbox]");
                        var value = $checkbox.attr("data-value");

                        configs.selectEvent(value, true);
                    }
                }
                
            })


        }
        var initCheckBox = function () {
            if (configs.initCheckBoxData != null && configs.initCheckBoxData.length > 0) {
                for (var i in configs.initCheckBoxData) {
                    var data = configs.initCheckBoxData[i];
                    $that.find(":checkbox[id$=" + data + "]").prop("checked", true);
                }
            }
        }
        var initSelectValue = function () {
            if (configs.initSelectValue != null) {
                
                $that.find("span.itemName").removeClass("selected");

                var $items = $that.find("span.itemName");
                $items.each(function (i, elem) {
                    if ($(this).html().indexOf(configs.initSelectValue) >= 0) {
                        $(this).addClass("selected");
                    }
                })               
            }
        }
        var initCheckBoxByValues = function () {
            if (configs.initCheckBoxByValues != null && configs.initCheckBoxByValues.length > 0) {
                var $items = $that.find("span.itemName");
                
                for (var i in configs.initCheckBoxByValues) {

                    var data = configs.initCheckBoxByValues[i];
                    
                    $items.each(function (i, elem) {
                        //console.log($(this).text());
                        if ($(this).text() == data ) {
                            $(this).siblings(":checkbox").prop("checked", true);
                        }
                    })
                }
            }
        }


        renderTreeList();    
        setEvent();
        initCheckBox();
        initSelectValue();
        initCheckBoxByValues();
        


        setTimeout(function () {
            $that.parent().getNiceScroll().remove();
            $that.parent().niceScroll({
                'cursorcolor': '#999999',
                'cursorborder': 'none',
                'smoothscroll': 'true',
                'autohidemode': true,//"hidden",
                'horizrailenabled': true,
                'enablemousewheel': true,
                'oneaxismousemode': "auto",
            });
        }, 50);

        var ret = {

            getDatas: function () {
                return configs.datas;
            }

        }
        return ret;

    }

})(jQuery,top.jkf)