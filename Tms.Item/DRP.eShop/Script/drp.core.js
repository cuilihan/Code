/*
Function:基于jQuery EasyUI常用方法扩展及通用功能集合
Author:Kimlee
Date:2014-10-19
*/

//随机数
function getRand() {
    var sb = [];
    while (sb.length < 4)
        sb.push(Math.floor(Math.random() * 9 + 1));
    return sb.join("");
}

//解析URL传递的参数
function request(paras) {
    var url = location.href;   //url
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}   //参数组
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}
 
//控制class为checkNum的文本框只能输入数字。
function checkNumber() {
    $(".checkNum").keypress(function (event) {
        event = (event) ? event : ((window.event) ? window.event : "");
        var keyCode = event.keyCode ? event.keyCode : event.which;
        if (keyCode == 8 || keyCode == 45 || keyCode == 46) return true;
        else if (keyCode >= 48 && keyCode <= 57) return true;
        else return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    });
}

//控制class为checkNum的文本框只能输入数字（不可输入负数）
function checkInt() {
    $(".checkInt").keypress(function (event) {
        event = (event) ? event : ((window.event) ? window.event : "");
        var keyCode = event.keyCode ? event.keyCode : event.which;
        if (keyCode == 8 || keyCode == 46) return true;
        else if (keyCode >= 48 && keyCode <= 57) return true;
        else return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    });
}
 

//ajax数据
var dataService = {
    ajaxGet: function (url, callback, isAsync) {
        if (typeof (isAsync) == "undefined")
            isAsync = true;
        $.ajax({
            type: "get",
            async: isAsync,
            url: url,
            success: function (data, textStatus) {
                callback(data);
            }
        });
    },
    ajaxPost: function (url, arrData, callback) {
        $.ajax({
            type: "post",
            url: url,
            data: arrData,
            success: function (data, textStatus) {
                callback(data);
            }
        });
    }
};
 