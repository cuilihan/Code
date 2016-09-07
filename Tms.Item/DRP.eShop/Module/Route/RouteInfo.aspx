<%@ Page Title=""  EnableViewState="false"  Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="RouteInfo.aspx.cs" Inherits="DRP.eShop.Module.Route.RouteInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../UI/routeinfo.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $(".r_routeinfo_subject").click(function () {
                var onClsName = $(this).children("span").attr("class");
                var blurClsName = onClsName == "r_routeinfo_arrow_up" ? "r_routeinfo_arrow_down" : "r_routeinfo_arrow_up";
                $(this).children("span").removeClass(onClsName).addClass(blurClsName);
                var clsName = $(this).next().attr("class");
                if (clsName.indexOf("hiden") > -1) $(this).next().removeClass("hiden")
                else $(this).next().addClass("hiden");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 0px 10px 0px 10px;">
        <div style="text-align: center; margin-top:5px;">
            <img style="width: 100%; border:1px solid #eee; padding:1px;" src="../../Files/Img/r.jpg"  />
        </div>
        <div class="r_title">
            新年巨献-【风花雪月之大理】昆明金殿、楚雄恐龙谷、大理苍山索道、洱海游船、三塔寺、同江千古情双飞六日特价游
        </div>
        <ul class="r_summary">
            <li>出发城市：苏州
            </li>
            <li>出游天数：6天
            </li>
            <li>去程交通：上海虹桥-昆明（飞机）
            </li>
            <li>返程交通：昆明-上海虹桥（飞机）
            </li>
            <li>集合地点：苏州会议中心散客集散中心
            </li>
            <li>集合时间：早上7：30
            </li>
            <li>成人价：<span>2090</span><em>起</em>
                儿童价：<span>2000</span><em>起</em>
            </li>
        </ul>
        <div>
            分享到：<a class="r_shared">一键分享</a>
        </div>
        <div class="r_routeinfo">
            <div class="r_routeinfo_subject">
                行程安排<span class="r_routeinfo_arrow_up"></span>
            </div>
            <div class="r_routeinfo_c">
                参考行程内容
            </div>
            <div class="r_routeinfo_subject">
                线路特色<span class="r_routeinfo_arrow_down"></span>
            </div>
            <div class="r_routeinfo_c hiden">
                特色
            </div>
            <div class="r_routeinfo_subject">
                费用说明<span class="r_routeinfo_arrow_down"></span>
            </div>
            <div class="r_routeinfo_c hiden">
                费用包含：
                费用不含：
            </div>
            <div class="r_routeinfo_subject">
                注意事项<span class="r_routeinfo_arrow_down"></span>
            </div>
            <div class="r_routeinfo_c hiden">
                注意内容
            </div>
            <div class="r_routeinfo_subject">
                出发班期<span class="r_routeinfo_arrow_down"></span>
            </div>
            <div class="r_routeinfo_c hiden">
                出团日期
            </div>
        </div>
        <div class="r_mobile">报名电话 <a href="tel:400-0512-8899">400-0512-8899</a></div>
    </div>

</asp:Content>
