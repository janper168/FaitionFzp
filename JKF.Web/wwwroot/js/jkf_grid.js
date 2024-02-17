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
            hasCheckBox: false,
            sortField: '',
            sortOrder: 'desc'
        };

        var configs = $.extend({}, _default, options);

        var hiddenColumn = [];
        var showColumn = [];
        var mainTableId = "mainTbl";
        var isMouseDown = false;
        var currentTh = null;
        var currentTd = null;
        var currentTds = [];
        var treeData = [];
        var rootData = {};
        var selectedRow = null;
        var lineDbclickEvent = null;
        var clickInterval = 300;
        var clickTimes = 0;
        var clickTimer = null;
        var rowNumber = 0; //初始行号
        var lastParam = null;
        var pagination = null;
        var sort = null;
        var columnWidth = [];
        
        $onlyFirstObj.html('<div id="gridTable"></div><div id="pager"></div>');

        var setPagination = function(){
            pagination = {
                PageIndex: 0,
                PageSize: configs.pageSize,
                SkipCount: 0,
                TotalCount: 0,
                PageCount: 0
            }
        }
        setPagination();

        var setSort = function () {
            sort = {
                sortField: configs.sortField,
                sortOrder: configs.sortOrder
            }
        }
        setSort();

        //设置宽度
        var setColumnWidth = function (columnId, width) {
            for (var i in columnWidth) {
                var data = columnWidth[i];
                if (columnId.indexOf(data.columnName) >= 0) {
                    data.width = width;
                }
            }
        }
        //获取宽度
        var getColumnWidth = function (columnName) {
            for (var i in columnWidth) {
                var data = columnWidth[i];
                if (columnName == data.columnName) {
                    return data.width
                }
            }
            return 50;//最少宽度
        }

        var renderHeader = function () {

            $("#gridTable", $onlyFirstObj).append("<div id='headTblWrapper' style='overflow: hidden;'><table id='headTbl'></table></div>");

            $("#headTbl", $onlyFirstObj).append("<thead></thead>");
            var $thead = $("#headTbl", $onlyFirstObj).find("thead");
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

            //复选框
            $tr.append("<th id='thead_tr_th_id_0_$checkbox' class='checkboxHeader'><input style='' id='selectAllCb' class='blue' type='checkbox'/></th>");
            var $columnH1 = $tr.find("th").last();
            $columnH1.show();

            if (configs.hasCheckBox == false) {
                $columnH1.hide();
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

                $columnH.html(colItem["showName"]);

                $columnH.css("width", colItem["width"]);
                columnWidth.push({ columnName: colItem["columnName"],width: colItem["width"]});
                $columnH.css("text-align", colItem["align"])

                if (!!colItem["sort"] && colItem["sort"] == true) {
                    $columnH.attr("data-sort", true);
                    $columnH.attr("data-Field", colItem["columnName"]);
                    $columnH.attr("data-sortOrder", "desc");
                }

            }
        };

        var renderData = function () {
            clearData();
            if (!configs.datas) {
                return;
            }

            var datas = configs.datas;

            $("#gridTable", $onlyFirstObj).append("<div id='mainTblWrapper' style='overflow:auto; max-height: 500px'><table id='mainTbl'></table></div>");

            $("#mainTbl", $onlyFirstObj).append("<tbody></tbody>");

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

                $row.append("<td id='" + rowId + "_td_1_$checkbox' class='checkboxHeader'><input style='' id='selectCb_'" + (rowNumber -1)+ " class='blue' type='checkbox'/></td>");
                var $columnH1 = $row.find("td").last();
                $columnH1.show();

                if (configs.hasCheckBox == false) {
                    $columnH1.hide();
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
                    if (!htmlText) {
                        //alert(1);
                        htmlText = "";//置为空串
                    }                     
                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText,data);
                    }
                    $column.append("<div>" + htmlText + "</div>");

                    var curColumnWidth = getColumnWidth(colItem["columnName"]);

                    $column.css("width", curColumnWidth);
                    $column.css("maxWidth", curColumnWidth);
                    $column.css("text-align", colItem["align"])

                }
            }

        }
        //减集
        var reduceArrayMemberNotExist = function(fullDatas, alreadyDatas) {
            reduceDatas = [];
            for (var i in fullDatas) {
                var eData = fullDatas[i];
                var notExist = false;
                for (j in alreadyDatas) {
                    var aData = alreadyDatas[j];
                    if (eData[configs.mainId] == aData[configs.mainId]) {
                        notExist = true;
                        break;
                    }
                }
                if (notExist == false) {
                    reduceDatas.push(eData);
                }
            }
            return reduceDatas;
        }

        var alreadyProcessData = [];

        var processChildrenTreeData = function (datas, parentId) {
            var treeData = [];

            for (var i in datas) {
                if (datas[i][configs.treeModeParentId] == parentId) {
                    treeData.push(datas[i]);
                    alreadyProcessData.push(datas[i]);
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
                    if (!htmlText) {
                        //alert(1);
                        htmlText = "";//置为空串
                    }        
                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText, data);
                    }

                    var paddingLeft = 0;
                    if (colItem["columnName"] == configs.treeModeLevelColumn) {

                        for (var i = 0; i < level; i++) {
                            paddingLeft += 13;
                        }

                        if (!!data.children && data.children.length > 0) {
                            htmlText = "<i class='fold-click fa fa-caret-down'></i>&nbsp;" + htmlText;
                        }
                        $column.css("padding-left", paddingLeft + 5);
                    }
                    $column.append("<div>" + htmlText + "</div>");
                    //$column.append(htmlText);

                    var curColumnWidth = getColumnWidth(colItem["columnName"]);
                    $column.css("width", curColumnWidth);
                    $column.css("text-align", colItem["align"]);

                }

                groupId++;
                data.treeFolderArray = [];
                renderChildrenTreeData(group + "_", level + 1, data.children, data.treeFolderArray);
            }
        }

        var renderTreeData = function () {
            clearData();

            if (!configs.datas) {
                return;
            }

            var datas = configs.datas;

            rootData = {};
            rootData.treeFolderArray = [];

            alreadyProcessData = [];
            treeData = processChildrenTreeData(datas, '0');
            rootData.children = treeData;

            var reduceDatas = reduceArrayMemberNotExist(configs.datas, alreadyProcessData);

            while (reduceDatas.length > 0) {
                var parentId = reduceDatas[0][configs.treeModeParentId];

                var isExist = true;
                while (isExist) {
                    isExist = false;
                    for (var j in reduceDatas) {
                        if (reduceDatas[j][configs.mainId] == parentId) {
                            parentId = reduceDatas[j][configs.treeModeParentId];
                            isExist = true;
                            break;
                        }
                    }
                }

                if (!isExist && !!parentId) {
                    var newTreeData = processChildrenTreeData(reduceDatas, parentId);                  
                    for (var i in newTreeData) {
                        treeData.push(newTreeData[i]);
                    }
                    reduceDatas = reduceArrayMemberNotExist(configs.datas, alreadyProcessData);
                }
   
            }


            $("#gridTable", $onlyFirstObj).append("<div id='mainTblWrapper' style='overflow:auto; max-height: 500px'><table id='mainTbl'></table></div>");

            $("#mainTbl", $onlyFirstObj).append("<tbody></tbody>");


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
                    if (!htmlText) {
                        //alert(1);
                        htmlText = "";//置为空串
                    }        
                    if (colItem["formatter"] && typeof (colItem["formatter"]) == "function") {

                        htmlText = colItem["formatter"](htmlText, data);
                    }

                    if (colItem["columnName"] == configs.treeModeLevelColumn) {

                        if (!!data.children && data.children.length > 0) {
                            htmlText = "<i class='fold-click fa fa-caret-down'></i>&nbsp;" + htmlText;
                        }
                    }
                    $column.append("<div>" + htmlText + "</div>");
                    //$column.append(htmlText);
                    var curColumnWidth = getColumnWidth(colItem["columnName"]);
                    $column.css("width", curColumnWidth);
                    $column.css("text-align", colItem["align"]);


                }
                groupId++;
                data.treeFolderArray = [];
                var parentGroup = group + "_";

                renderChildrenTreeData(parentGroup, level + 1, data.children, data.treeFolderArray);

            }

            $('i.fold-click').click(function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $tr = $(this).parent().parent().parent();
                var rowId = $tr.attr("id");

                var isShow = false;

                if ($(this).hasClass("fa-caret-right")) {
                    $(this).removeClass("fa-caret-right").addClass("fa-caret-down");
                    isShow = true;
                } else if ($(this).hasClass("fa-caret-down")) {
                    $(this).removeClass("fa-caret-down").addClass("fa-caret-right");
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
            $("#" + mainTableId, $onlyFirstObj).parent().remove();
        }

        renderHeader();

        var bindingHeaderEvent = function () {
            $('#headTbl th', $onlyFirstObj).bind({
                mousedown: function (e) {
                    currentTds = [];
                    var $th = $(this);
                    var left = $th.offset().left; //元素距左
                    var rightPos = left + $th.outerWidth();
                    if (rightPos - 4 <= e.pageX && e.pageX <= rightPos) {
                        isMouseDown = true;

                        currentTh = $th;
                        var index = $th.index();
                        if (configs.treeMode == true) index--;
                        //currentTd = $("#mainTbl td:eq(" + index + ")", $onlyFirstObj);

                        var $rows = $("#mainTbl tr", $onlyFirstObj);
                        console.log($rows.length);
                        for (var i = 0; i < $rows.length; i++) {
                            currentTds.push($($rows.get(i)).find("td").eq(index));
                        }

                        $('#headTbl th', $onlyFirstObj).css('cursor', 'col-resize');

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
                    $('#headTbl th', $onlyFirstObj).each(function (index, eleDom) {
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
                    if (currentTh != null && currentTds != []) {
                        if (isMouseDown) { //鼠标按下了，开始移动
                            var left = currentTh.offset().left;
                            var paddingBorderLen = currentTh.outerWidth() - currentTh.width();
                            if (e.pageX - left - paddingBorderLen <= 50) return;

                            currentTh.width((e.pageX - left - paddingBorderLen) + 'px');
                            //currentTd.width((e.pageX - left - paddingBorderLen) + 'px');

                            for (var i = 0; i < currentTds.length; i++) {
                                currentTds[i].css("maxWidth", (e.pageX - left - paddingBorderLen));
                                currentTds[i].css("width", (e.pageX - left - paddingBorderLen));
                            }

                            setColumnWidth(currentTh.attr("id"), e.pageX - left - paddingBorderLen);
                        }
                    }
                },
                mouseup: function (e) {
                    isMouseDown = false;
                    currentTh = null;
                    $('#headTbl th', $onlyFirstObj).css('cursor', 'auto');
                    $('#mask').remove();
                }
            });

            var $header = $onlyFirstObj.find("#headTbl thead tr");
            $header.find("th[data-sort=true]").click(function () {
                var sortField = $(this).attr("data-Field");

                var sortOrder = sort.sortOrder;

                if (sortOrder == "asc") {
                    sortOrder = "desc";
                } else {
                    sortOrder = "asc";
                }

                sort = { sortField: sortField, sortOrder: sortOrder };
                lastParam.sort = sort;
                ret.getRemoteData(lastParam);
            });
        }

        bindingHeaderEvent();

        var bindingBodyEvent = function () {

            $onlyFirstObj.find('#mainTbl tbody tr').click(function () {

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

            $("#selectAllCb", $onlyFirstObj).change(function () {
                var check = $(this).prop("checked");
                $onlyFirstObj.find(":input[type=checkbox]").prop("checked", check);
            });

        }

        var resizeEvent = function () {

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
                    ret.getRemoteData(lastParam,true,false); 
                });
                $pager.find("#prev_btn").click(function () {
                    if (pagination.PageIndex > 0) {
                        pagination.PageIndex--;
                    } else {
                        return;
                    }
                    ret.getRemoteData(lastParam, true, false);
                });
                $pager.find("#next_btn").click(function () {
                    if (pagination.PageIndex < pagination.PageCount - 1) {
                        pagination.PageIndex++;
                    } else {
                        return;
                    }
                    ret.getRemoteData(lastParam, true, false);
                });
                $pager.find("#last_btn").click(function () {
                    pagination.PageIndex = pagination.PageCount - 1;
                    ret.getRemoteData(lastParam, true, false);
                });
                $pager.find("#go_btn").click(function () {
                    var goPage = parseInt($("#customPageNumber", $pager).val());
                    if (goPage < 1 || goPage > pagination.PageCount) {
                        return;
                    }
                    pagination.PageIndex = goPage - 1;
                    ret.getRemoteData(lastParam, true, false);
                });
            }

            if (configs.hasPager == true) {
                $pager.show();
                $onlyFirstObj.find("#gridTable").height($onlyFirstObj.height() - $onlyFirstObj.find("#pager").height());
                $onlyFirstObj.find("#mainTblWrapper").css("max-height", $onlyFirstObj.height() - $onlyFirstObj.find("#pager").height() - 26);
            } else {
                $pager.hide();
                $onlyFirstObj.find("#gridTable").height($onlyFirstObj.height());
                $onlyFirstObj.find("#mainTblWrapper").css("max-height", $onlyFirstObj.height() - 26);
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

            $("#mainTblWrapper", $onlyFirstObj).scroll(function () {//给table外面的div滚动事件绑定一个函数
                var left = $(this).scrollLeft(); //获取滚动的距离
                $("#headTblWrapper", $onlyFirstObj).scrollLeft(left);

            });

            setTimeout(function () {
                $("#mainTblWrapper", $onlyFirstObj).getNiceScroll().remove();
                $("#mainTblWrapper", $onlyFirstObj).niceScroll({
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

        var renderSort = function () {

            var $header = $onlyFirstObj.find("#headTbl thead tr");
 
            $header.find("th").each(function (i, elem) {
                if ($(this).find("span").hasClass("sortFlag")) {
                    $(this).find("span.sortFlag").remove();
                }
            })

            var sortField = sort.sortField;
            var sortOrder = sort.sortOrder;

            var $sortField = $header.find("th[id*=_" + sortField + "]");

            if ($sortField.length == 1) {

                if (sortOrder == "asc")
                    $sortField.append("<span class='sortFlag' style='float:right;margin-right:10px;'><i class='fa fa-caret-up'></i></span>");
                else
                    $sortField.append("<span class='sortFlag' style='float:right;margin-right:10px;'><i class='fa fa-caret-down'></i></span>");

            }
        };

        var dataLoadedCallBack = null;
        
        var ret = {

            'getRemoteData': function (param, isAsync = true, resetPage = true) {
                rowNumber = 0;
                if (!param) {
                    param = {};
                }
                if (configs.hasPager == true) {

                    if (resetPage == true) {
                        pagination.PageIndex = 0;
                    }

                    if (pagination.PageIndex == 0) {
                        lastParam = param;
                    } 

                    param.pagination = pagination;
                    param.sort = sort;
                }
                
                if (!!configs.remoteAddress) {
                    if (isAsync) {
                        $.httpPostAsync(
                        {
                            url: configs.remoteAddress,
                            param: param,
                            dataType: "json"
                        }, function (res) {
                                if (res.code == 200) {
                                    if (configs.hasPager == true) {
                                        pagination = res.datas.pagination;
                                        rowNumber = pagination.PageIndex * pagination.PageSize;
                                        configs.datas = res.datas.datas;
                                        sort = res.datas.sort;
                                        renderSort();
                                         
                                    } else {
                                        configs.datas = res.datas;
                                    }
                                   
                                } else {
                                    jkf.showTips(res.errMsg, 2);
                                    return;
                                }
                            if (configs.treeMode)
                                renderTreeData();
                            else
                                renderData();
                            bindingBodyEvent();
                            resizeEvent();
                            showPager();
 
                            if (!!dataLoadedCallBack) {

                                dataLoadedCallBack();
                            }
                            setNiceSroll();
                        })
                    }
                    else {
                        var res = $.httpPost({
                            url: configs.remoteAddress,
                            param: param,
                            dataType: "json"
                        });

                        if (res.code == 200) {
                            if (configs.hasPager == true) {
                                pagination = res.datas.pagination;
                                rowNumber = pagination.PageIndex * pagination.PageSize;
                                configs.datas = res.datas.datas;
                                sort = res.datas.sort;
                                renderSort();

                            } else {
                                configs.datas = res.datas;
                            }

                        } else {
                            jkf.showTips(res.errMsg, 2);
                            return;

                        }

                        if (configs.treeMode)
                            renderTreeData();
                        else
                            renderData();
                        bindingBodyEvent();
                        resizeEvent();
                        showPager();

                        if (!!dataLoadedCallBack) {

                            dataLoadedCallBack();
                        }
                        setNiceSroll();

                    }
                } 
            },

            'setAndFlushData': function (datas) {
                rowNumber = 0;
                configs.datas = datas;
                setSort();
                setPagination();
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
                var values = [];
                var $row = null;
                if (configs.hasCheckBox) {
                    $("#mainTbl tbody tr", $onlyFirstObj).each(function (i, elem) {
                        if ($(this).find(":input[type=checkbox]:checked").length == 1) {
                            $row = $(this);
                            values.push($row.find("td[id*=" + columnName + "] div").html())
                        }
                    });

                    return values.join(',');
                } else {
                    if (selectedRow != null) {
                        var $val = selectedRow.find("td[id*=" + columnName + "]  div");
                        return $val.html();
                    }
                }
                
            },

            'setTableLineDbclick': function (callback) {
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
            },

            'setRemoteAddress': function (url) {
                configs.remoteAddress = url;
            },

            'bizCallBack': function (callback) {
               
                dataLoadedCallBack = callback;
            },
            'getRowCount': function () {
                return configs.datas.length;
            }
        }

        return ret;
    }

})(jQuery, top.jkf);