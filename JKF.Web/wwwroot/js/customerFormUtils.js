//渲染表单
function renderForm($formItem, controls ,creator) {

    for (var i in controls) {
        var control = controls[i].control;
        var controlType = control.controlType;
        renderTextControl($formItem, control, controlType, creator);
        renderTextareaControl($formItem, control, controlType, creator);
        renderDatePickerControl($formItem, control, controlType,creator);
        renderRadioControl($formItem, control, controlType, creator);
        renderSelectControl($formItem, control, controlType, creator);
        renderCheckBoxControl($formItem, control, controlType, creator);
        renderImageControl($formItem, control, controlType, creator);
        renderFileControl($formItem, control, controlType, creator);
    }

}
//渲染文本框
function renderTextControl($formItem, control, controlType, creator) {
    if (controlType == "文本框") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        //alert(id);
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }
        var validation = control.validation;

        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";
        contentHtml += "<input class='default-input' date-type='text' style='width:" + width + "px;height:" + height + "px' type='text' data-type='text' id='" + property + "'/>";
        $addonControl.append(contentHtml);
        var $ctrl = $addonControl.find("#" + property);
        if (validation == "1") {
            $ctrl.attr("data-required", "true");
        } else if (validation == "2") {
            $ctrl.attr("data-required", "true");
            $ctrl.attr("data-type", "integer");
        } else if (validation == "3") {

        } else if (validation == "4") {
            $ctrl.attr("data-required", "true");
            $ctrl.attr("data-type", "regex");
            var validationRegex = control.validationRegex;
            var validationErrorMsg = control.validationErrorMsg;
            $ctrl.attr("data-regex", validationRegex);
            $ctrl.attr("data-errorMsg", validationErrorMsg);
        }

        if (creator < 0)
            $("#" + property).attr("readonly", true);

    }
}
//渲染文本域
function renderTextareaControl($formItem, control, controlType, creator) {
    if (controlType == "文本域") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }
        var validation = control.validation;
        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";
        contentHtml += "<textarea class='default-input' date-type='text' style='width:" + width + "px;height:" + height + "px' type='text' id='" + property + "' data-type='textarea'></textarea>";
        $addonControl.append(contentHtml);
        var $ctrl = $addonControl.find("#" + property);
        if (validation == 1) {
            $ctrl.attr("data-required", "true");
        }

        if (creator < 0)
            $("#" + property).attr("readonly", true);

    }
}

//渲染日期
function renderDatePickerControl($formItem, control, controlType, creator) {
    if (controlType == "日期") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }
        var validation = control.validation;
        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";
        contentHtml += "<input  readonly='readonly' id='" + property + "' style='width:" + width + "px;height:" + height + "px' class='Wdate default-input' type='text' data-type='date' onclick='WdatePicker();' />";
        $addonControl.append(contentHtml);
        var $ctrl = $addonControl.find("#" + property);
        if (validation == 1) {
            $ctrl.attr("data-required", "true");
        }
        if (creator < 0)
            $("#" + property).attr("readonly", true);

    }
}

//渲染单选框
function renderRadioControl($formItem, control, controlType, creator) {
    if (controlType == "单选按钮") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        var selectType = control.selectType;
        var selectLink = control.selectLink;
        var selectListBox = control.selectListBox;

        var selectItems = [];
        if (selectType == "1") {
            var res = $.httpGet({ url: selectLink });
            if (res.code == 200) {
                var datas = res.datas;
                for (var i in datas) {
                    selectItems.push({ itemValue: datas[i].ItemValue, itemString: datas[i].ItemName });
                }
            }
        } else if (selectType == "2") {
            var items = selectListBox.split("\n");
            for (var i in items) {
                selectItems.push({ itemValue: items[i].split(",")[0], itemString: items[i].split(",")[1] });
            }

        }

        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }

        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";
        //contentHtml += "<input class='default-input' date-type='text' style='width:" + width + "px;height:" + height + "px' type='text' id='" + property + "'/>";
        for (var i in selectItems) {
            contentHtml += "<input class=\"blue\" type=\"radio\" value=\"" + selectItems[i].itemValue + "\" name=\"" + property + "\" id=\"" + property + "_" + selectItems[i].itemValue + "\"/><label for=\"" + property + "_" + selectItems[i].itemValue + "\">" + selectItems[i].itemString + "</label>";
        }
        contentHtml += "<input id=\"" + property + "\" class=\"default-input\" type=\"hidden\" data-type=\"radio\" />";

        $addonControl.append(contentHtml);

        if (creator < 0)
            $("input[name=" + property + "]").attr("readonly", true);

    }
}

//渲染下拉框
function renderSelectControl($formItem, control, controlType, creator) {
    if (controlType == "下拉框") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        var selectType = control.selectType;
        var selectLink = control.selectLink;
        var selectListBox = control.selectListBox;

        var selectItems = [];
        if (selectType == "1") {
            var res = $.httpGet({ url: selectLink });
            if (res.code == 200) {
                var datas = res.datas;
                for (var i in datas) {
                    selectItems.push({ itemValue: datas[i].ItemValue, itemString: datas[i].ItemName });
                }
            }

        } else if (selectType == "2") {
            var items = selectListBox.split("\n");
            for (var i in items) {
                selectItems.push({ itemValue: items[i].split(",")[0], itemString: items[i].split(",")[1] });
            }

        }

        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }

        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";

        contentHtml += "<select style='width:" + width + "px;height:" + height + "px'   class=\"default-input\" id='" + property + "' data-type='select'>";
        for (var i in selectItems) {
            contentHtml += "<option value=\"" + selectItems[i].itemValue + "\">" + selectItems[i].itemString + "</option>";
        }
        contentHtml += "</select>";

        $addonControl.append(contentHtml);

        if (creator < 0)
            $("select[id=" + property + "]").attr("readonly", true);

    }
}

//渲染复选框
function renderCheckBoxControl($formItem, control, controlType, creator) {
    if (controlType == "复选框") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        var selectType = control.selectType;
        var selectLink = control.selectLink;
        var selectListBox = control.selectListBox;

        var selectItems = [];
        if (selectType == "1") {
            var res = $.httpGet({ url: selectLink });
            if (res.code == 200) {
                var datas = res.datas;
                for (var i in datas) {
                    selectItems.push({ itemValue: datas[i].ItemValue, itemString: datas[i].ItemName });
                }
            }

        } else if (selectType == "2") {
            var items = selectListBox.split("\n");
            for (var i in items) {
                selectItems.push({ itemValue: items[i].split(",")[0], itemString: items[i].split(",")[1] });
            }

        }

        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }

        controlHtml = "<div style='height:" + height + "px;' class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";

        for (var i in selectItems) {
            contentHtml += "<input type='checkbox' id='" + property + "_" + selectItems[i].itemValue + "'  class='blue' value=\"" + selectItems[i].itemValue + "\" /><label for='" + property + "_" + selectItems[i].itemValue + "'/>" + selectItems[i].itemString + "</label>";
        }
        contentHtml += "<input type='hidden' id='" + property + "' data-type='checkbox'/>";
        $addonControl.append(contentHtml);


        if (creator < 0)
            $("input[id^=" + property + "_]").attr("readonly", true);
    }
}

//渲染图片
function renderImageControl($formItem, control, controlType, creator) {
    if (controlType == "图片") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        var selectType = control.selectType;
        var selectLink = control.selectLink;
        var selectListBox = control.selectListBox;
        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }

        controlHtml = "<div height:" + height + "px class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";

        contentHtml += '<div id="' + property + 'Wrapper" style="display:inline-block;position:relative;width:' + width + 'px;line-height:' + height + 'px;border:1px solid #ccc;background-color:#eee;"><img style="vertical-align:middle;max-width:' + width + 'px;max-height:' + height + 'px;" id="' + property + '_image" /><div id="' + property + '_select" style="width:35px;height:20px;line-height:20px;position:absolute;left:0;top:0;background:lightblue;color:#fff;box-shadow:0px 0px 3px #ccc;">选择</div></div>';
        contentHtml += '<input id="' + property + '" class="width_' + width + ' default-input" type="hidden" data-type="images" />';
        contentHtml += '<input id="' + property + '_Upload" type="file" style="display:none;" name="' + property + '_Upload" multiple="multiple"/>';
        contentHtml += '&nbsp;<button id="btn_' + property + '" class="btn blue">浏览图片</btn>'
        $addonControl.append(contentHtml);

        $("#" + property + "_select").click(function () {
            $("#" + property + "_Upload").trigger("click");

        })

        $("#" + property + "_Upload").change(function () {
            if ($(this).val() != "") {
                imagesLoad(this);
            }
        })

        $("#btn_" + property).click(function (e) {
            top.jkf.openFrame({
                parentFrameId: $openIframe.attr("id"),
                title: "浏览图片",
                width: '',
                height: '',
                level: 2,
                url: '/WorkFlow/CustomerForm/WatchImagesForm',
                data: { urls: $("#" + property).val() },
                hasMinButton: false,
                hasMaxButton: false,
                hasCloseButton: true,
                isWazard: false,
            })
        });
        if (creator < 0)
            $('#' + property + 'Wrapper').hide();


    }
}

//渲染文件
function renderFileControl($formItem, control, controlType, creator) {
    if (controlType == "附件") {
        var fillLineSpace = control.fillLineSpace;
        var id = control.controlId;
        var label = control.label;
        var property = control.property;
        var width = control.width;
        var height = control.height;
        var selectType = control.selectType;
        var selectLink = control.selectLink;
        var selectListBox = control.selectListBox;
        if (!property) return;
        var controlHtml = "";
        var contentHtml = "";
        var lineType = "";
        if (fillLineSpace == "1") {
            lineType = "fullRowLine";
        } else if (fillLineSpace == "2") {
            lineType = "oneInTwoRowLine";
        } else if (fillLineSpace == "3") {
            lineType = "oneInThreeRowLine";
        }

        controlHtml = "<div height:" + height + "px class='controlBlock " + lineType + "' id='" + id + "'></div>";
        $formItem.append(controlHtml);
        var $addonControl = $formItem.find(".controlBlock").last();
        contentHtml = "<div class='labelBlock'><label class='controllabel' for='" + property + "'>" + label + "</label></div>";
        contentHtml += '<input id="' + property + '_Upload" type="file" style="display:none;" name="' + property + '_Upload" multiple="multiple"/>';
        contentHtml += '<input id="' + property + '" class="width_' + width + ' default-input" type="hidden" data-type="files" />';
        contentHtml += '&nbsp;&nbsp;<button id="btnUpload_' + property + '" class="btn blue">上传附件</button>'
        contentHtml += '&nbsp;&nbsp;<button id="btnWatch_' + property + '" class="btn blue">浏览附件</button>'
        $addonControl.append(contentHtml);

        $("#btnUpload_" + property).click(function () {
            $("#" + property + "_Upload").trigger("click");

        })

        $("#" + property + "_Upload").change(function () {
            if ($(this).val() != "") {
                filesLoad(this);
            }
        })

        $("#btnWatch_" + property).click(function (e) {
            top.jkf.openFrame({
                parentFrameId: $openIframe.attr("id"),
                title: "浏览附件",
                width: '',
                height: '',
                level: 2,
                url: '/WorkFlow/CustomerForm/WatchFilesForm',
                data: { urls: $("#" + property).val() },
                hasMinButton: false,
                hasMaxButton: false,
                hasCloseButton: true,
                isWazard: false,
            })
        });

        if (creator < 0)
            $('#btnUpload_' + property).hide();
    }
}

function imagesLoad(ele) {

    var formData = new FormData();
    var files = $(ele)[0].files;

    var propertyName = $($(ele)[0]).attr("id");
    for (var i = 0; i < files.length; i++) {
        formData.append("attachment" + i, files[i]);
    }

    $.ajax({
        url: "/workflow/customerForm/onPostUpload",
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        beforeSend: function () {
        },
        success: function (res) {
            var srcList = res.datas.split(';');
            var src0 = "";
            for (var i in srcList) {
                src0 = srcList[i];
                break;
            }
            
            var imgPropName = propertyName.replace("_Upload", '_image');
            var propName = propertyName.replace("_Upload", '');
            $("#" + imgPropName).attr("src", src0);
            $("#" + propName).val(res.datas);
            top.jkf.showTips("上传图片成功！", 1);

        }
        ,
        error: function (res) {
            top.jkf.showTips("上传图片出错！", 2);
        }
    });

}


function filesLoad(ele) {

    var formData = new FormData();
    var files = $(ele)[0].files;
    var propertyName = $($(ele)[0]).attr("id");
    for (var i = 0; i < files.length; i++) {
        formData.append("attachment" + i, files[i]);
    }

    $.ajax({
        url: "/workflow/customerForm/onPostUpload",
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        beforeSend: function () {
        },
        success: function (res) {          
            var propName = propertyName.replace("_Upload", '');            
            $("#" + propName).val(res.datas);
            top.jkf.showTips("上传文件成功！", 1);
        }
        ,
        error: function (res) {
            top.jkf.showTips("上传文件出错！", 2);
        }
    });

}