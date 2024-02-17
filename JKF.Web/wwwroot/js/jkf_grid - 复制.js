(function ($, jkf) {


    $.fn.jkfGrid = function (options) {

        //确保是jkf_grid_tbl这个类选择器，唯一一个元素
        var $onlyFirstObj = $(this).getFirstJqObj('jkf_grid_tbl');
        //alert($onlyFirstObj.width() + "," + $onlyFirstObj.height());

        //默认的配置属性
        var _default = {
            columnHeaders: null,
            datas: null,
            remoteAddress: null,
            hasPager: false,
            mainId: "Id",
            treeMode: false,
            treeModeParentId: 'ParentId',
            treeModeLevelColumn: '',
            rowSelectColor: "lightblue",
            multiSelect: false,
            showNumber: false,
            hasPager: false,
            pageSize: 20,
        };


        var configs = $.extend({}, _default, options);

        var hiddenColumn = [];
        var showColumn = [];
        var mainTableId = "mainTbl";
        var isMouseDown = false;
        var currentTh = null;
        var treeData = [];
        var rootData = {};
        var selectedRow = null;
        var lineDbclickEvent = null;
        var clickInterval = 600;
        var clickTimes = 0;
        var clickTimer = null;
        var rowNumber = 0; //初始行号
        var lastParam = null;
        var pagination = {
            PageIndex: 0,
            PageSize: configs.pageSize,
            SkipCount : 0,
            TotalCout: 0,
            PageCount: 0,
        }

        $onlyFirstObj.html('<div id="gridTable"></div><div id="pager"></div>');

        var renderHeader = function () {

            $("#gridTable", $onlyFirstObj).append("<table></table>");
            $("#gridTable > table", $onlyFirstObj).attr("id", mainTableId);

            $("#" + mainTableId, $onlyFirstObj).append("<thead></thead>");
            var $thead = $("#" + mainTableId, $onlyFirstObj).find("thead");
            $thead.append("<tr></tr>");
            var $tr = $thead.find("tr").eq(0);
            $tr.attr("id", "thead_tr_id_0");

            //行号
            $tr.append("<th id='thead_tr_th_id_0_$lineNumber' class='NumberColumn'>NO.</th>");
            var $columnH0 = $tr.find("th").eq(0);
            $columnH0.show();

            if (configs.showNumber == false) {
                $columnH0.hide();
            }

            for (var id in configs.columnHeaders) {

                var colItem = configs.columnHeaders[id];

                $tr.append("<th></th>");
                var columnId = "thead_tr_th_id_0_" + colItem["columnName"];
                var $columnH = $tr.find("th").last();
                $columnH.attr("id", columnId);
                $columnH.show();

                if (!!colItem["hidden"] && colItem["hidden"] === true) {
                    hiddenColumn.push(colItem["columnName"]);
                    $columnH.hide();
                } else {
                    showColumn.push(colItem["columnName"]);
                }

                //alert(colItem["columnName"] +"," +colItem["showName"]);
                $columnH.html(colItem["showName"]);

                $columnH.css("width", colItem["width"]);
                $columnH.css("text-align", colItem["align"])

            }
            //$thead.append("<tr style='border:1px solid black;height:26px;'></tr>");
        };

        var renderData = function () {

            if (!configs.datas) {
                jkf.consoleAndAlertLog("表格数据为空，请检查！");
                return;
            }

            var datas = configs.datas;
            clearData();
            $("#" + mainTableId, $onlyFirstObj).append("<tbody></tbody>");

            var $tbody = $("#" + mainTableId, $onlyFirstObj).find("tbody");

            for (var row in datas) {
                data = datas[row];

                var rowId = "tr_id_" + row;
                $tbody.append("<tr></tr>");
                var $row = $tbody.find("tr").eq(row);
                $row.attr("id", rowId);


                //行号
                $row.append("<td class='NumberColumn'></td>");
                var $column = $row.find("td").eq(0);
                var columnId = rowId + "_td_0" + "_$lineNumber";
                $column.attr("id", columnId);
                $column.text(++rowNumber);
                $column.show();

                if (configs.showNumber == false) {
                    $column.hide();
                }

                for (var id in configs.columnHeaders) {

                    var colItem = configs.columnHeaders[id];

                    $row.append("<td></td>");
                    var realId = id + 1;
                    var columnId = rowId + "td_" + realId + "_" + colItem["columnName"];
                    var $column = $row.find("td").last();
                    $column.attr("id", columnId);
                    $column.show();
                    if (!!colItem["hidden"] && colItem["hidden"] === true) {
                        $column.hide();
                    }
                    var htmlText = data[colItem["columnName"]];

                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText);
                    }
                    $column.append(htmlText);

                    $column.css("width", colItem["width"]);
                    $column.css("text-align", colItem["align"])

                }
            }

        }

        var processChildrenTreeData = function (datas, parentId) {
            var treeData = [];

            for (var i in datas) {
                if (datas[i][configs.treeModeParentId] == parentId) {
                    treeData.push(datas[i]);
                    var children = processChildrenTreeData(datas, datas[i][configs.mainId]);
                    datas[i].children = children;
                }
            }
            return treeData;
        };

        var renderChildrenTreeData = function (parentGroup, level, childrenData, treeFolderArray) {


            $tbody = $("#mainTbl tbody", $onlyFirstObj);

            var groupId = 1;
            var group = "";

            for (var row in childrenData) {
                var data = childrenData[row];
                group = parentGroup + groupId;
                var rowId = "tree_" + group + "_tr_" + row;
                $tbody.append("<tr></tr>");
                var $row = $tbody.find("tr").last();
                $row.attr("id", rowId);

                treeFolderArray.push({ treeRowId: rowId, isShow: true });


                //行号
                $row.append("<td class='NumberColumn'></td>");
                var $column = $row.find("td").eq(0);
                var columnId = rowId + "_td_0" + "_$lineNumber";
                $column.attr("id", columnId);
                $column.text(++rowNumber);
                $column.show();

                if (!configs.showNumber) {
                    $column.hide();
                }

                for (var id in configs.columnHeaders) {

                    var colItem = configs.columnHeaders[id];

                    $row.append("<td></td>");
                    var realId = id + 1;
                    var columnId = rowId + "_td_" + realId + "_" + colItem["columnName"];
                    $column = $row.find("td").last();
                    $column.attr("id", columnId);
                    $column.show();
                    if (!!colItem["hidden"] && colItem["hidden"] === true) {
                        $column.hide();
                    }
                    var htmlText = data[colItem["columnName"]];

                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText, data);
                    }

                    var paddingLeft = 0;
                    if (colItem["columnName"] == configs.treeModeLevelColumn) {

                        for (var i = 0; i < level; i++) {
                            paddingLeft += 13;
                        }

                        if (!!data.children && data.children.length > 0) {
                            htmlText = "<i class='fold-click fa fa-minus-square-o'></i>&nbsp;" + htmlText;
                        }
                        $column.css("padding-left", paddingLeft + 5);
                    }

                    $column.append(htmlText);


                    $column.css("width", colItem["width"]);
                    $column.css("text-align", colItem["align"]);

                }

                groupId++;
                data.treeFolderArray = [];
                renderChildrenTreeData(group + "_", level + 1, data.children, data.treeFolderArray);
            }
        }

        var renderTreeData = function () {
            if (!configs.datas) {
                //jkf.consoleAndAlertLog("表格数据为空，请检查！");
                return;
            }

            var datas = configs.datas;

            clearData();
            treeData = processChildrenTreeData(datas, '0');

            console.log(treeData);

            rootData = {};
            rootData.treeFolderArray = [];
            rootData.children = treeData;

            $("#" + mainTableId, $onlyFirstObj).append("<tbody></tbody>");

            var $tbody = $("#" + mainTableId, $onlyFirstObj).find("tbody");
            var groupId = 1;

            for (var row in treeData) {
                var data = treeData[row];
                var level = 0;
                group = groupId + "";
                var rowId = "tree_" + group + "_tr_" + row;
                $tbody.append("<tr></tr>");
                var $row = $tbody.find("tr").last();
                $row.attr("id", rowId);

                rootData.treeFolderArray.push({ treeRowId: rowId, isShow: true });

                //行号
                $row.append("<td class='NumberColumn'></td>");
                var $column = $row.find("td").eq(0);
                var columnId = rowId + "_td_0" + "_$lineNumber";
                $column.attr("id", columnId);
                $column.text(++rowNumber);
                $column.show();

                if (configs.showNumber == false) {
                    $column.hide();
                }

                for (var id in configs.columnHeaders) {

                    var colItem = configs.columnHeaders[id];
                    var realId = id + 1;
                    $row.append("<td></td>");
                    var columnId = rowId + "_td_" + realId + "_" + colItem["columnName"];
                    $column = $row.find("td").last();
                    $column.attr("id", columnId);
                    $column.show();
                    if (!!colItem["hidden"] && colItem["hidden"] === true) {
                        $column.hide();
                    }
                    var htmlText = data[colItem["columnName"]];

                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText, data);
                    }

                    if (colItem["columnName"] == configs.treeModeLevelColumn) {

                        if (!!data.children && data.children.length > 0) {
                            htmlText = "<i class='fold-click fa fa-minus-square-o'></i>&nbsp;" + htmlText;
                        }
                    }

                    $column.append(htmlText);

                    $column.css("width", colItem["width"]);
                    $column.css("text-align", colItem["align"]);


                }
                groupId++;
                data.treeFolderArray = [];
                var parentGroup = group + "_";

                renderChildrenTreeData(parentGroup, level + 1, data.children, data.treeFolderArray);
                console.log(rootData);

            }

            $('i.fold-click').click(function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $tr = $(this).parent().parent();
                var rowId = $tr.attr("id");

                var isShow = false;

                if ($(this).hasClass("fa-plus-square-o")) {
                    $(this).removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
                    isShow = true;
                } else if ($(this).hasClass("fa-minus-square-o")) {
                    $(this).removeClass("a-minus-square-o").addClass("fa-plus-square-o");
                    isShow = false;
                }

                var found = foundDataAction(rootData, rowId, isShow);

                if (found != null) {
                    if (found.foldType == 1) {
                        showOrHideTreeRow(found.foundData, true);
                    } else if (found.foldType == 2) {
                        showOrHideTreeRow(found.foundData, false);
                    }

                    setNiceSroll();
                }
                console.log(rootData);
            })
        };

        function showOrHideTreeRow(rowData, isShow) {
            for (var i in rowData.treeFolderArray) {
                var folder = rowData.treeFolderArray[i];
                folder.isShow = isShow;
                var rowId = folder.treeRowId;

                if (isShow) {
                    $("#mainTbl tbody tr#" + rowId, $onlyFirstObj).show();
                } else {
                    $("#mainTbl tbody tr#" + rowId, $onlyFirstObj).hide();
                }
            }

            for (var j in rowData.children) {
                processSubDataShowOrHide(rowData.children[j], isShow);
            }

        }

        function processSubDataShowOrHide(rowData, isShow) {
            if (isShow == false) {
                for (var i in rowData.treeFolderArray) {
                    var folder = rowData.treeFolderArray[i];
                    var rowId = folder.treeRowId;
                    $("#mainTbl tbody tr#" + rowId, $onlyFirstObj).hide();
                }
                if (!!rowData.children && rowData.children.length > 0) {
                    for (var i in rowData.children) {
                        processSubDataShowOrHide(rowData.children[i], isShow);
                    }
                }
            } else {
                for (var i in rowData.treeFolderArray) {
                    var folder = rowData.treeFolderArray[i];
                    var rowId = folder.treeRowId;
                    var orginalIsSHow = folder.isShow;
                    if (orginalIsSHow == true)
                        $("#mainTbl tbody tr#" + rowId, $onlyFirstObj).show();
                }
                if (!!rowData.children && rowData.children.length > 0) {
                    for (var i in rowData.children) {
                        processSubDataShowOrHide(rowData.children[i], isShow);
                    }
                }
            }

        }

        function foundDataAction(topData, rowId, isShow) {
            var foldType = -1;
            var foundData = null;

            var found = false;
            for (var i in topData.treeFolderArray) {

                var folder = topData.treeFolderArray[i];

                if (folder.treeRowId == rowId) {

                    if (isShow == true) {
                        foldType = 1;//展开
                    } else {
                        foldType = 2;//折叠
                    }

                    foundData = topData.children[i];
                    found = true;
                }
                if (found) {
                    return { foldType: foldType, foundData: foundData };

                } else {
                    if (!!topData.children && topData.children.length > 0) {
                        for (var j in topData.children) {
                            found = foundDataAction(topData.children[j], rowId, isShow);
                            if (found != null) {
                                return found;
                            }
                        }
                    }
                }
            }
            return null;

        }

        var clearData = function () {
            $("#" + mainTableId, $onlyFirstObj).find("tbody").remove();
        }

        renderHeader();

        var bindingHeaderEvent = function () {
            $('#mainTbl th', $onlyFirstObj).bind({
                mousedown: function (e) {
                    var $th = $(this);
                    var left = $th.offset().left; //元素距左
                    var rightPos = left + $th.outerWidth();
                    if (rightPos - 4 <= e.pageX && e.pageX <= rightPos) {
                        isMouseDown = true;
                        currentTh = $th;
                        $('#mainTbl th', $onlyFirstObj).css('cursor', 'col-resize');

                        //创建遮罩层，防止mouseup事件被其它元素阻止冒泡，导致mouseup事件无法被body捕获，导致拖动不能停止
                        var bodyWidth = $('body').width();
                        var bodyHeight = $('body').height();
                        $('body').append('<div id="mask" style="opacity:0;top:0px;left:0px;cursor:ew-resize;background-color:green;position:absolute;z-index:9999;width:' + bodyWidth + 'px;height:' + bodyHeight + 'px;"></div>');
                    }
                }

            });

            $('body').bind({
                mousemove: function (e) {
                    //移动到column右边缘提示
                    $('#mainTbl th', $onlyFirstObj).each(function (index, eleDom) {
                        var ele = $(eleDom);
                        var left = ele.offset().left; //元素距左
                        var rightPos = left + ele.outerWidth();
                        if (rightPos - 4 <= e.pageX && e.pageX <= rightPos) { //移到列右边缘
                            ele.css('cursor', 'col-resize');
                        } else {
                            if (!isMouseDown) { //不是鼠标按下的时候取消特殊鼠标样式
                                ele.css("cursor", "auto");
                            }
                        }
                    });

                    //改变大小
                    if (currentTh != null) {
                        if (isMouseDown) { //鼠标按下了，开始移动
                            var left = currentTh.offset().left;
                            var paddingBorderLen = currentTh.outerWidth() - currentTh.width();
                            currentTh.width((e.pageX - left - paddingBorderLen) + 'px');
                        }
                    }
                },
                mouseup: function (e) {
                    isMouseDown = false;
                    currentTh = null;
                    $('#mainTbl th', $onlyFirstObj).css('cursor', 'auto');
                    $('#mask').remove();
                }
            });
        }

        bindingHeaderEvent();

        var bindingBodyEvent = function () {
            //alert(1);
            //alert($onlyFirstObj.find('#mainTbl tbody tr').length);
            $onlyFirstObj.find('#mainTbl tbody tr').click(function () {
                //alert(3);
                $(this).css("background-color", configs.rowSelectColor);
                $(this).siblings("tr").css("background-color", "#fafafa");
                selectedRow = $(this);

                clickTimes++;
                if (clickTimes == 1) {
                    clearTimeout(clickTimer);
                    clickTimer = setTimeout(function () {
                        clickTimes = 0;
                    }, clickInterval);
                }

                if (clickTimes == 2) {
                    if (!!lineDbclickEvent && typeof (lineDbclickEvent) == 'function') {
                        lineDbclickEvent(selectedRow);
                    }
                }
            });

        }

        var resizeEvent = function () {

            //var tblWidth = $("table#mainTbl",$onlyFirstObj).width();
            //var containerWidth = $onlyFirstObj.width();
            //if (tblWidth < containerWidth) {
            //    var colItem = configs.columnHeaders[configs.columnHeaders.length - 1];
            //    var colName = colItem.columnName;
            //    var lastColWidth = $("#thead_tr_th_id_0_" + colName).width();

            //    $("#thead_tr_th_id_0_" + colName).width(lastColWidth + containerWidth - tblWidth - 2);
            //}
        }

        resizeEvent();

        var showPager = function () {

            $pager = $onlyFirstObj.find("#pager");

            $pager.html("");

            if (configs.hasPager) {
                var htmlText = "<span>&nbsp;&nbsp;总共查询到 " + pagination.TotalCount + " 条记录，当前显示 第 " + (pagination.PageIndex + 1) + " 页,总共 " + pagination.PageCount + " 页， 每页 " + pagination.PageSize + " 条记录。 <span>&nbsp;&nbsp;"

                $pager.append(htmlText);

                var buttonFirstText = "<button id='first_btn' class='navigation_btn btn blue'><i class='fa fa-angle-double-left'></button>";
                $pager.append(buttonFirstText);

                var buttonPrevText = "<button id='prev_btn' class='navigation_btn btn blue'><i class='fa fa-angle-left'></button>";
                $pager.append(buttonPrevText);

                var buttonNextText = "<button id='next_btn' class='navigation_btn btn blue'><i class='fa fa-angle-right'></button>";
                $pager.append(buttonNextText);

                var buttonLastText = "<button id='last_btn' class='navigation_btn btn blue'><i class='fa fa-angle-double-right'></button>";
                $pager.append(buttonLastText);

                var searchPageIuputText = "<input id='customPageNumber' type='text' class='width_50 default-input'/>"
                $pager.append(searchPageIuputText);

                var buttonGoText = "<button id='go_btn' class='navigation_btn btn blue'><i class='fa fa-rotate-right'></button>";
                $pager.append(buttonGoText);

                $pager.find("#first_btn").click(function () {
                    pagination.PageIndex = 0;
                    ret.getRemoteData(lastParam);
                });
                $pager.find("#prev_btn").click(function () {
                    if (pagination.PageIndex > 0) {
                        pagination.PageIndex--;
                    } else {
                        return;
                    }
                    ret.getRemoteData(lastParam);
                });
                $pager.find("#next_btn").click(function () {
                    if (pagination.PageIndex < pagination.PageCount - 1) {
                        pagination.PageIndex++;
                    } else {
                        return;
                    }
                    ret.getRemoteData(lastParam);
                });
                $pager.find("#last_btn").click(function () {
                    pagination.PageIndex = pagination.PageCount - 1;
                    ret.getRemoteData(lastParam);
                });
                $pager.find("#go_btn").click(function () {
                    var goPage = parseInt($("#customPageNumber", $pager).val());
                    if (goPage < 1 || goPage > pagination.PageCount) {
                        return;
                    }
                    pagination.PageIndex = goPage - 1;
                    ret.getRemoteData(lastParam);
                });
            }

 
            //console.log($onlyFirstObj);
            if (configs.hasPager == true) {
                $pager.show();
                $onlyFirstObj.find("#gridTable").height($onlyFirstObj.height() - $onlyFirstObj.find("#pager").height());
            } else {
                $pager.hide();
                $onlyFirstObj.find("#gridTable").height($onlyFirstObj.height());
            }

        }
        showPager();

        $(window).resize(function () {
            resizeEvent();
        })



        var setTableLineDbclickEvent = function (callback) {

            lineDbclickEvent = callback;
        };

        var setNiceSroll = function () {

            setTimeout(function () {
                $("#gridTable", $onlyFirstObj).getNiceScroll().remove();
                $("#gridTable", $onlyFirstObj).niceScroll({
                    'cursorcolor': '#999999',
                    'cursorborder': 'none',
                    'smoothscroll': 'true',
                    'autohidemode': true,//"hidden",
                    'horizrailenabled': true,
                    'enablemousewheel': true,
                    'oneaxismousemode': "auto",
                });
            }, 50);
        }
        setNiceSroll();
        
        var ret = {

            'getRemoteData': function (param, isAsync = true) {
                rowNumber = 0;
                if (!param) {
                    param = {};
                }
                if (configs.hasPager == true) {

                    if (pagination.PageIndex == 0) {
                        lastParam = param;
                    } 

                    param.pagination = pagination;
                }
                if (!!configs.remoteAddress) {
                    if (isAsync) {
                        $.httpPostAsync({
                            url: configs.remoteAddress,
                            param: param,
                            dataType: "json"
                        }, function (res) {
                            console.log("res: " + res.code)
                            if (res.code == 200) {
                                if (configs.hasPager == true) {
                                    pagination = res.datas.pagination;                               
                                    configs.datas = res.datas.datas;
                                } else {
                                    configs.datas = res.datas;
                                }
                            }
                            if (configs.treeMode)
                                renderTreeData();
                            else
                                renderData();
                            bindingBodyEvent();
                            resizeEvent();
                            showPager();
                            setNiceSroll();
                        })
                    }
                } else {
                    var res = $.httpPost({
                        url: configs.remoteAddress,
                        param: param,
                        dataType: "json"
                    });
                    if (res.code == 200) {
                        
                        if (configs.hasPager == true) {
                            pagination = res.pagination;
                            
                            configs.datas = res.datas.datas;
                        } else {
                            configs.datas = res.datas;
                        }

                        if (configs.treeMode)
                            renderTreeData();
                        else
                            renderData();
                        bindingBodyEvent();
                        resizeEvent();
                        showPager();
                        setNiceSroll();
                    }
                  
                }
            },

            'setAndFlushData': function (datas) {
                rowNumber = 0;
                configs.datas = datas;
                $("#" + mainTableId, $onlyFirstObj).find("tbody").remove();
                if (configs.treeMode)
                    renderTreeData();
                else
                    renderData();
                bindingBodyEvent();
                resizeEvent();
                showPager();
                setNiceSroll();
            },
            'openFrameWindow': function (options) {

                jkf.openFrame({
                    parentFrameId: options.parentFrameId,
                    title: options.title,
                    width: options.width,
                    height: options.height,
                    level: options.level,
                    url: options.url,
                    data: options.data,
                    hasMinButton: options.hasMinButton,
                    hasMaxButton: options.hasMaxButton,
                    hasCloseButton: options.hasCloseButton,
                    isWazard: options.isWazard,
                    buttonGroup: options.buttonGroup
                })
            },
            'getSelectRowValue': function (columnName) {
                console.log("selectRow:" + selectedRow);
                if (selectedRow != null) {
                    var $val = selectedRow.find("td[id*=" + columnName + "]");
                    return $val.html();
                }
            },
            'setTableLineDbclick': function (callback) {
                //alert(1);
                setTableLineDbclickEvent(callback);
            },
            'getSelectedRowData': function (mainIdValue) {
                for (var i in configs.datas) {
                    var data = configs.datas[i];
                    if (mainIdValue == data[configs.mainId]) {
                        return data;
                    }
                }
                return null;
            }

        }

        return ret;
    }

})(jQuery, top.jkf);