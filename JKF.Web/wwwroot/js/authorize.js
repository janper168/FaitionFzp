
(function ($, jkf) {

    $.extend(jkf, {

        processAuthorizedButtons: function (buttonGroupId, authorizedButtons) {
            var $buttonGroup = $(buttonGroupId);

            if ($buttonGroup.length == 1) {

                if (authorizedButtons != null && authorizedButtons.length > 0) {

                    var $buttons = $buttonGroup.find("button");

                    $buttons.each(function (i, elem) {
                        var $button = $(this);
                        var buttonId = $button.attr("Id");
                        var isShow = false;
                        for (var j in authorizedButtons) {
                            var authButton = authorizedButtons[j];
                            if (authButton.EnCode == buttonId) {
                                $button.html("<i class='" + authButton.Icon + "'></i>" + authButton.Name);
                                $button.show();
                                isShow = true;
                                break;
                            }
                        }
                        if (isShow == false)
                            $button.hide();

                    })
                }
            }

        },

        processAuthorizedColumns: function (columnHeaders, authorizedColumns) {

            for (var i in columnHeaders) {
                var header = columnHeaders[i];
                var isHidden = true;
                for (var j in authorizedColumns) {
                    var authColumn = authorizedColumns[j];
                    if (authColumn.EnCode == header.columnName) {
                        header.showName = authColumn.Name;
                        isHidden = false;
                        break;
                    }
                }
                if (isHidden == true) {
                    header.hidden = true;
                }

            }
        }
        ,
        processAuthorizedForms: function ($form, authorizedForms) {
            var $inputs = $form.find(":input");

            for (var j in authorizedForms) {
               var authForm = authorizedForms[j];
            }
            $inputs.each(function (i, elem) {
                var $input = $(this);
                var id = $input.attr("id");
                var isShow = false;

                for (var j in authorizedForms) {
                    var authForm = authorizedForms[j];
                    if (authForm.EnCode == id) {
                        isShow = true;
                        break;
                    }
                }
                if (isShow == false) {
                    if (!!id) {
                    } 
                    $input.parent().css('visibility', 'hidden');

                }

            });

            var $radios = $form.find(":radio");
            $radios.each(function (i, elem) {
                var $input = $(this);
                var id = $input.attr("id")
                var name = $input.attr("name");

                var isShow = false;
                for (var j in authorizedForms) {
                    var authForm = authorizedForms[j];
                    if (authForm.EnCode == name) {
                        isShow = true;
                        break;
                    }
                }
                if (isShow == false) {
                    if (!!id) {
                        alert(id);
                    } 
                    $input.parent().css('visibility', 'hidden');

                } else {
                    $input.parent().css('visibility', 'visible');
                }
            });

        }

    })
})(jQuery, top.jkf);




