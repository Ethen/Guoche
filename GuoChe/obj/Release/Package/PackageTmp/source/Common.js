Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
//扩展字符串处理函数
String.prototype.StringFormat = function () {
    if (arguments.length == 0) {
        return this;
    }
    for (var stringFormatS = this, stringFormatI = 0; stringFormatI < arguments.length; stringFormatI++) {
        stringFormatS = stringFormatS.replace(new RegExp("\\{" + stringFormatI + "\\}", "g"), arguments[stringFormatI]);
    }
    return stringFormatS;
};



$(document).ready(function () {
    var rule = "#sidebar a[href='{0}']".StringFormat(location.pathname);
    $(rule).parent().parent().show();

    $(".b_C1F8A9").click(function () {//处理左边栏菜单收缩放开
        event.stopPropagation();
        if ($(this).hasClass("collapsed")) {
            console.log(1);
            $(this).removeClass("collapsed").next(".collapse").css("height", "auto");
        }
        else {
            console.log(2);
            $(this).addClass("collapsed").next(".collapse").css("height", "0px");
        }
        
    })
})

var WebHelper = {
    getQueryStringByName: function (name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

        if (result == null || result.length < 1) {

            return "";

        }

        return result[1];
    }

}

var valid = {
    elemnets: {},
    init: function (el) {
        valid.elemnets = el;
        valid.registerEvent();
    },
    registerEvent: function () {
        if (!!valid.elemnets) {
            for (var j = 0; j < valid.elemnets.ids.length; j++) {
                var id = valid.elemnets.ids[j];
                var current = $("#" + id);
                if (current.is("input")) {
                    current.keyup(function () {
                        $(this).removeClass("error").next(".verror").remove();
                    }).blur(function () {
                        if ($(this).val().length > 0)
                        {
                            $(this).removeClass("error").next(".verror").remove();
                        }
                    });
                }
                if (current.is("select")) {
                    current.change(function () {
                        $(this).removeClass("error").next(".verror").remove();
                    });
                }
            }
        }
    },
    validate: function () {
        if (!!valid.elemnets) {
            var hasError = 0;
            for (var i = 0; i < valid.elemnets.ids.length; i++) {
                var obj = valid.elemnets.methods[i], id = valid.elemnets.ids[i];
                var current = $("#" + id), iserror = false;
                current.parent().find(".verror").remove();
                if (!!obj.required && !$.trim(current.val()))//必填
                {
                    current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[0] + '</span>');
                    iserror = true;
                }

                if (!iserror && !!obj.maxlength)//最大
                {
                    if (current.val().length > obj.maxlength) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[1] + '</span>');
                        iserror = true;
                    }
                }

                if (!iserror && (obj.minlength || 0) > 0) {//最小
                    if (current.val().length < obj.minlength) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[2] + '</span>');
                        iserror = true;
                    }
                }

                if (!iserror && !!obj.rule && $.trim(current.val()).length>0) {//正则表达式
                    if (!obj.rule.test(current.val())) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[3] + '</span>');
                        iserror = true;
                    }
                }

                if (iserror) { hasError++; }
            }

            return hasError < 1;
        }

        return true;
    }
}