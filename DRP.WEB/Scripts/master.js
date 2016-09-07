var master = {
    init: function () {
        master.fnInitNavigate();
        master.fnPushMessage(false);
        $("#btnUserInfo").click(function () {
            master.fnUserRoleSelect();
        });
        $("#btnService").hover(function () { //在线客服
            $(this).addClass("hover").find("div.jq_hidebox").show();
        }, function () {
            $(this).removeClass("hover").find("div.jq_hidebox").hide();
        });
        $("#btnSkin").hover(function () { //皮肤
            $(this).addClass("hover").find("div.skin_hidebox").show();
        }, function () {
            $(this).removeClass("hover").find("div.skin_hidebox").hide();
        });
        $("#btnServiceCenter").hover(function () { //服务中心 
            $(this).addClass("hover").find("div.my_hidebox").show();
        }, function () {
            $(this).removeClass("hover").find("div.my_hidebox").hide();
        });
        master.fnLoadHelp();
        $(window).resize(function () {
            $("#frmBench").location.reload();
        });
    },
    fnPushMessage: function (isClose) { //查询运维中心推送的消息
        try {
            $(window).scroll(function () {
                if ($.browser.msie && $.browser.version == "6.0")
                    $(".pop_notice_panel").css("top", $(window).height() - $(".pop_notice_panel").height() + $(document).scrollTop());
            });
            var isDisplay = $('.pop_notice_panel').attr("isDisplay");
            var cookieString = new String(document.cookie);
            var cookieHeader = '360ly_com_notice_switch=';
            var beginPosition = cookieString.indexOf(cookieHeader);
            if (isClose) {
                $(".pop_notice_panel").css("display", "none");
                var refrushTime = new Date();
                refrushTime.setTime(refrushTime.getTime() + 1000 * 60 * 60 * 12) //同一ip设置过期时间，此处设置为12小时只显示一次
                document.cookie = '360ly_com_notice_switch=yes;expires=' + refrushTime.toGMTString();
            }
            else if (isDisplay == "" && beginPosition < 0) {
                var url = "/Service/Index.ashx?action=3&r=" + getRand();
                dataService.ajaxGet(url, function (data) {
                    if (data != "" && typeof (data) != "undefined") {
                        $("#360ly_notice").html(data);
                        $(".pop_notice_panel").css("display", "block");
                    } else {
                        $(".pop_notice_panel").css("display", "none");
                    }
                });
            }
        } catch (e) { }
    },
    //#region 导航菜单
    fnInitNavigate: function () { //初始化导航菜单
        try {
            master.fnAutoSetNavWidth();
            master.fnDefaultNav();
            $(window).resize(function () {
                master.fnAutoSetNavWidth();
            });
            $("#navRollWrap").find("li>a").click(function () {
                var id = $(this).prop("id");
                var pageID = $(this).attr("pageID");
                $("#navRollWrap").find("li").removeClass("layout_menu_nav_on");
                $(this).parent().addClass("layout_menu_nav_on");
                if (pageID != "productbook" && pageID != "tmssite") {
                    master.fnLoadAccordionNav(id);
                    //master.fnLoadTreeMenu(id);
                }
            });
        } catch (e) { }
    },
    fnDefaultNav: function () { //默认显示第一个导航
        var o = $("#navRollWrap").find("li>a:first");
        if (o) {
            var id = o.prop("id");
            master.fnLoadAccordionNav(id);
            // master.fnLoadTreeMenu(id);
            o.parent().addClass("layout_menu_nav_on");
        }
    },
    fnLoadTreeMenu: function (navID) { //左侧树形菜单
        var setting = {
            data: {
                simpleData: {
                    enable: true
                }
            }
        };
        var url = "/Service/Index.ashx?action=2&id=" + navID;
        dataService.ajaxGet(url, function (data) {
            var zNodes = [];
            if (data != "") {
                $(eval(data)).each(function () {
                    var json = { id: this.ID, pId: this.ParentID, name: this.NavName, open: true, url: this.NavUrl, target: 'frmBench' };
                    zNodes.push(json);
                });
            }
            $.fn.zTree.init($("#navTreeMenu"), setting, zNodes);
        });
    },
    fnLoadAccordionNav: function (navID) { //左侧手风琴式菜单
        var url = "/Service/Index.ashx?action=1&id=" + navID + "&r=" + getRand();
        dataService.ajaxGet(url, function (data) {
            var sb = [];
            sb.push("<div id=\"drp_left_navigate\" class=\"easyui-accordion\"  data-options=\"border:false,fit:false\">");
            $(eval(data)).each(function () {
                sb.push("<div title=\"" + this.NavName + "\" data-options=\"iconCls:'icon-folder'\" class='drp_left_menu'>");
                sb.push("<ul class=\"navigate_left\">");

                $(this.NavData).each(function (idx) {
                    var clsName = this.NavCls;
                    if (clsName == "") clsName = "nav_default";
                    sb.push("<li>");
                    sb.push("<a pageID='" + this.PageID + "' class='" + clsName + "' href='" + this.NavUrl + "' target='frmBench'>" + this.NavName + "</a>");
                    sb.push("</li>")

                });
                sb.push("</ul>");
                sb.push("</div>");
            });
            sb.push("</div>");
            $("#navigate").html(sb.join(""));
            $.parser.parse('#navigate');
            $(".navigate_left").find("a").click(function () {
                $(".navigate_left").find("a").removeClass("navigate_left_li_on");
                $(this).addClass("navigate_left_li_on");
            });
        });
    },
    fnAutoSetNavWidth: function () { //自适应屏幕宽度
        var clientWidth = $(document.body).width();
        var navNum = $("#navRollWrap").find("ul>li").size();
        var navWidth = navNum * 80;//菜单宽度
        var userWidth = $(".layout_menu_user").width();//用户信息宽度
        var btnWidth = $(".layout_menu_nav_btn_l").width() + $(".layout_menu_nav_btn_r").width(); //左右Button宽度
        var navWrapWidth = clientWidth - userWidth - btnWidth - navWidth;
        $("#layout_menu_right").css("width", (clientWidth - userWidth) + "px");
        var __w = Math.abs(clientWidth - userWidth - btnWidth);
        $("#layout_menu_nav").css("width", Math.abs(__w) + "px");
        $("#navRollWrap").css("width", (navWidth + 10) + "px");
        if (navWrapWidth > 0) {
            $(".layout_menu_nav_btn_l").hide();
            $(".layout_menu_nav_btn_r").hide();
        }
        else {
            $(".layout_menu_nav_btn_l").show();
            $(".layout_menu_nav_btn_r").show();
        }
    },
    fnMoveToLeft: function () { //向右移动菜单 
        $("#navRollWrap").animate({ left: "+=80px" }, 100, "", function () { });
    },
    fnMoveToRight: function () { //向右移动菜单  
        $("#navRollWrap").animate({ left: "-=80px" }, 100, "", function () { });
    },
    //#endregion
    fnUserRoleSelect: function () { //角色切换
        $('#UserRoleList').dialog({
            title: '角色切换',
            width: 400,
            height: 150,
            closed: false,
            cache: false,
            modal: true
        });
    },
    fnAddFavorites: function () { //加为收藏夹
        $('#btnFavorites').addFavorite('旅管家管理系统', location.href);
    },
    fnLoadHelp: function () { //在线帮助
        $("#btnJITHelp").click(function () {
            var pageID = $(".navigate_left").find(".navigate_left_li_on").eq(0).attr("pageID");
            var url = "/Service/CommonData.ashx?action=2&pageID=" + pageID;
            if (pageID == "") pageID = "index";
            dataService.ajaxGet(url, function (data) {
                if (data == "") data = "<div style='font-size:15px;font-weight:bold;padding:10px 20px;'>未查询到本节帮助内容，请参考 <a style='font-size:15px; color:red;' href='http://www.360ly.com/help/doc.aspx' target='_blank'>旅管家使用说明书</a></div>";
                $("#helpData").html(data);
            });
            $("#help-window").removeClass("hide");
            $('#help-window').window({
                title: "在线帮助文档",
                width: 800,
                height: 500,
                modal: true,
                minimizable: false,
                loadingMessage: "正在加载数据"
            });
        });
    }
};

function doSearch(value, name) { //查询 
    var url = "";
    var key = encodeURI(value);
    switch (name) {
        case "customer": url = "/Module/Search/CustomerSearch.aspx?key=" + key; break;
        case "order": url = "/Module/Search/OrderSearch.aspx?key=" + key; break;
        case "resource": url = "/Module/Search/ResourceSearch.aspx?key=" + key; break;
    }
    $("#frmBench").attr("src", url);
}
jQuery.fn.addFavorite = function (l, h) {
    return this.click(function () {
        var t = jQuery(this);
        if (jQuery.browser.msie) {
            window.external.addFavorite(h, l);
        } else if (jQuery.browser.mozilla || jQuery.browser.opera) {
            t.attr("rel", "sidebar");
            t.attr("title", l);
            t.attr("href", h);
        } else {
            alert("请使用Ctrl+D将本页加入收藏夹！");
        }
    });
};



