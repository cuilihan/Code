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

//创建模拟窗口
function openWindow(options, callback) {
    try {
        var defaults = { "width": 500, "height": 300, "url": "", "title": "窗口" };
        var opt = $.extend(defaults, options);
        var jq = top.jQuery;
        jq('#mPopWin').window({
            title: opt.title,
            width: opt.width,
            height: opt.height,
            modal: true,
            minimizable: false,
            top: (jq(window).height() - opt.height + 50) * 0.5,
            left: (jq(window).width() - opt.width) * 0.5,
            loadingMessage: "正在加载数据",
            onBeforeOpen: function () {
                jq('#mPopWin iframe').remove();
                jq("<iframe frameborder=\"0\" src='" + opt.url + "' style=\"width: " + (opt.width - 20) + "px; height: " + (opt.height - 42) + "px; overflow: auto;\"></iframe>").appendTo(jq('#mPopWin'));
            },
            onBeforeClose: function () {
                if (callback) {
                    callback.call();
                }
            },
            onClose: function () {
                jq('#mPopWin iframe').remove();
            }
        });
    } catch (e) { }
}

//打开标签窗口
function addTab(title, href, callback) {
    var tt = $('#tabs');
    var h = document.body.clientHeight - 35;
    var content = "<div><iframe frameborder=\"0\" src=\"" + href + "\" style=\"width:100%; height:" + h + "px; overflow:auto;\"></iframe></div>";
    if (tt.tabs('exists', title)) {//如果tab已经存在,则选中并刷新该tab           
        tt.tabs('select', title);
        var tab = tt.tabs('getSelected');
        tt.tabs('update', {
            tab: tab,
            options: {
                title: title,
                content: content,
                closable: true,
                selected: true,
                iconCls: 'icon-edit'
            }
        });
    }
    else {
        tt.tabs('add', {
            title: title,
            closable: true,
            content: content,
            iconCls: 'icon-edit'
        });
        tt.tabs({
            onClose: function (tt) {
                if (callback) {
                    callback.call();
                }
            }
        });
    }
    tt.tabs('select', title);
}

//关闭Tab窗口且右下角有消息提示
function closeTab(msg, tabTitle) {
    if (msg != "") {
        Notice(msg);
    }
    var jq = parent.jQuery;
    jq('#tabs').tabs('close', tabTitle);
    return false;
}

//关闭窗口且右下角有消息提示
function closeWindow(msg) {
    if (msg != "") {
        Notice(msg);
    }
    if (msg.indexOf('失败') == -1) {
        var jq = top.jQuery; //父窗口jQuery对象
        jq('#mPopWin').window("close");
    }
    return false;
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

        if (keyCode == 8 || keyCode == 46 || keyCode == 45) return true;
        else if (keyCode >= 48 && keyCode <= 57) return true;
        else return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    });
}

//提示
function Alert(msg) {
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.alert("提示", msg, "info");
    return false;
}

//异常
function Error(msg) {
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.alert("异常提示", msg, "error");
    return false;
}

//疑问
function Question(msg) {
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.alert("确认提示", msg, "question");
    return false;
}

//警告
function Warning(msg) {
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.alert("警告", msg, "warning");
    return false;
}

//消息提示
function Notice(msg) {
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.show({ title: "消息提醒", msg: msg, timeout: 3000 });
    return false;
}

//个人消息提醒
function messageRemind(msg) {
    
    var jq = top.jQuery; //父窗口jQuery对象
    jq.messager.show({ title: "消息提醒", msg: msg, timeout: 8000, width: 350, height: 200 });
    return false;
}

//datagrid已选择的表格
function getDataGridSelectedRow(tblID) {
    
    var arr = [];
    var rows = $('#' + tblID).datagrid('getSelections');
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        arr.push(row.ID);
    }
    if (arr.length == 0) return "";
    return arr.join(",");
}

//Checkbox 全选 
function selectAll(obj, targetID) {
    
    var isChked = $(obj).is(":checked");
    var ctrl = $("#" + targetID).find("input[type='checkbox']");
    isChked ? ctrl.prop("checked", "checked") : ctrl.removeAttr("checked");
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

//打印　 
function printScope() {
    
    var bdhtml = window.document.body.innerHTML; //获取当前页的html代码 
    var sprnstr = "<!--start print-->"; //设置打印开始区域 
    var eprnstr = "<!--end print-->"; //设置打印结束区域 
    var prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 18);
    prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
    window.document.body.innerHTML = prnhtml;
    window.print();
    window.document.body.innerHTML = bdhtml;
}

//关闭Window页面
function closeBlankWindow() {
    
    window.opener = null;
    window.open('', '_self');
    window.close();
}

//tab标签
//tabTitle:ID  tabBoxID:标签对应内容的ID
function navTab(tabTitleID, tabBoxID) {
    
    $("#" + tabBoxID).find("li").hide();
    $("#" + tabTitleID).find("li:first").addClass("thistab").show();
    $("#" + tabBoxID).find("li:first").show();

    $("#" + tabTitleID).find("li").bind("click", function () {
        $(this).addClass("thistab").siblings("li").removeClass("thistab");
        var activeindex = $("#" + tabTitleID).find("li").index(this);
        $("#" + tabBoxID).children().eq(activeindex).show().siblings().hide();
        return false;
    });
}

//常用的功能函数
var commData = {
    serverURL: "/Service/CommonData.ashx",//通用服务地址
    fnQueryUser: function (deptID) { //查询部门用户
        var u = commData.serverURL + "?action=1&deptID=" + deptID;
        var jsonData = "";
        dataService.ajaxGet(u, function (data) {
            jsonData = data;
        }, false);
        return jsonData;
    }
};
