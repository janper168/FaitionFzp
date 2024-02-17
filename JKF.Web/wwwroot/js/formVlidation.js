/**
 * Created by joe on 17-11-28.
 */
(function ($) {
    var settings = {
        before: function () {
        },
        success: function () {
        },
        error: function () {
        },
        timeout: 600,
        language: 'cn'
    };

    var languages = {
        'en': {
            'required': "The following fields need to be filled:\n",
            'invalid': "The values of the following fields are invalid:\n",
            'matching': "The fields must match:\n",
            'character': "- Input name must be at least min Characters characters.\n",
            'error': "Oops, there were some errors:\n"
        },
        'cn': {
            'required': "- 字段的值必须填写。",
            'invalid': "- 字段的值无效",
            'matching': "- 字段必须匹配:",
            'character': "- 输入字段至少为 min 字符.",
            'character_max': "",
            'error': "发生错误"
        }
    };

    var $error = {
        has: false,
        required: [],
        character: [],
        matching: []
    };
    var $form;

    var check = function (input) {
        $('.error').tooltip('hide');
        input.nextAll('.error').remove();

        if (input.data('required') && !required(input)) {
            return false;
        }
        if (input.data('length') && !length(input)) {
            return false;
        }
        if (input.data('regular') && !regular(input)) {
            return false;
        }
        var $type = input.data('type') ? input.data('type') : input.attr('type');
        if ($type) {
            if ($type === 'checkbox' || $type === 'hidden' || $type === 'text') return true;
            regular(input, $type);
        }
        return true;
    };

    var required = function (input) {
        if (!input.val()) {
            $error.has = true;
            $error.required.push(input.attr('title') ? input.attr('title') : input.attr('name'));
            input.focus()
                .after(
                    $('<span>', {
                        'class': 'input-group-addon error',
                        'data-toggle': "tooltip",
                        'data-placement': "top",
                        'title': languages[settings.language]['required']
                    }).html('<i class="fa fa-warning"></i>')
                );
            return false;
        }
        return true;
    };

    var length = function (input) {
        var $length = input.data('length');
        var $regular = new RegExp("^[a-zA-Z0-9_-]{" + $length + "}$");
        var value = input.val();
        if ($regular.test(value) === false) {
            $error.has = true;
            $error.character.push(input.attr('title') ? input.attr('title') : input.attr('name'));
            var language = languages[settings.language]['character'].replace('min', $length);
            input.focus()
                .after(
                    $('<span>', {
                        'class': 'input-group-addon error',
                        'data-toggle': "tooltip",
                        'data-placement': "top",
                        'title': language
                    }).html('<i class="fa fa-warning"></i>')
                );
            return false;
        }
        return true;
    };

    var regular = function (input, type) {
        type = type || null;
        var $regulars = type ? type : input.data('regular');
        switch ($regulars) {
            case 'cn':
                $regular = /^[\u4e00-\u9fa5]+$/;
                break;
            case 'en':
                $regular = /^[a-zA-Z]+$/;
                break;
            case 'num':
                $regular = /^[0-9]+$/;
                break;
            case 'char':
                $regular = /^[\w\u4e00-\u9fa5]+/;
                break;
            case 'password':
                $regular = /^([a-zA-Z0-9!@#$%^&*()_?<>{}]){6,}$/;
                break;
            case 'url':
                $regular = /^(http|https|ftp):\/\/(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?(\/.*)?$/;
                break;
            case 'email':
                $regular = /^([0-9A-Za-z\-_\.]+)@([0-9a-z]+\.[a-z]{2,3}(\.[a-z]{2})?)$/;
                break;
            case 'ip':
                $regular = /^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$/;
                break;
            case 'qq':
                $regular = /^[1-9]*[1-9][0-9]*$/;
                break;
            default:
                break;
        }

        var re = new RegExp($regular);

        if (re.test(input.val()) === false) {
            $error.has = true;
            $error.matching.push(input.attr('title') ? input.attr('title') : input.attr('name'));
            input.focus()
                .after(
                    $('<span>', {
                        'class': 'input-group-addon error',
                        'data-toggle': "tooltip",
                        'data-placement': "top",
                        'title': languages[settings.language]['matching'] + (type ? type : $regulars)
                    }).html('<i class="fa fa-warning"></i>')
                );
            return false;
        }
        return true;
    };

    var submit = function (form) {
        $.ajax({
            url: form.attr('action'),
            type: form.attr('method') ? form.attr('method') : 'POST',
            timeout: settings.timeout,
            data: form.serialize(),
            beforeSend: function () {
                settings.before();
            },
            error: function () {
                settings.error();
            },
            success: function (data) {
                data = data ? $.parseJSON(data) : '';
                settings.success(data);
            }
        })
    };

    var listen = function (form) {
        form.on('change', 'input,select,textarea', function () {
            check($(this));
        });
        if ($error.has) {
            $('.error').tooltip('show');
        }
    };

    var methods = {
        init: function (options) {
            settings = $.extend(true, settings, options || {});
            return this.each(function () {
                $form = $(this);
                listen($form);
                $form.on('submit', function (e) {
                    e.preventDefault();
                    var $input = $(this).find('input,select,textarea');
                    $input.each(function () {
                        check($(this));
                    });
                    if ($form.find('.error').length > 0) {
                        $('.error').tooltip('show');
                    } else {
                        submit($form);
                    }
                })
            });
        },
        check: function (input) {
            if (typeof input === 'string')
                input = $(input);
            check(input);
        }
    };

    $.fn.validator = function () {
        var method = arguments[0];

        if (methods[method]) {
            method = methods[method];
            arguments = Array.prototype.slice.call(arguments, 1);
        } else if (typeof (method) == 'object' || !method) {
            method = methods.init;
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.validator');
            return this;
        }

        return method.apply(this, arguments);
    }

})(jQuery);
