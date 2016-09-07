<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DRP.WeChat.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <link href="/UI/index.css" rel="stylesheet" />
    <script src="/Script/WeChatIndex.js"></script>
    <script type="text/javascript">
        $(function () {
            c.init();
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-container" id="main-container"> 
        <div class="page-content">
            <div class="infobox-container">
                <h4>欢迎您登录旅管家：<asp:Literal runat="server" ID="lblUserName"></asp:Literal></h4>
                <div class="infobox">
                    <div class="infobox-icon">
                        <i class="icon-customer"></i>
                    </div>

                    <div class="infobox-data">
                        <span class="infobox-data-number" id="CustomerNum">0</span>
                        <div class="infobox-content">所有客户，今日增长量</div>
                    </div>
                    <div class="stat stat-success" id="CustomerNumDay">0</div>
                </div>

                <div class="infobox infobox-blue  ">
                    <div class="infobox-icon">
                        <i class="icon-order"></i>
                    </div>

                    <div class="infobox-data">
                        <span class="infobox-data-number" id="OrderNum">0</span>
                        <div class="infobox-content">本月订单，今日增长量</div>
                    </div>

                    <div class="badge badge-success" id="OrderNumDay">
                        0
												
                    </div>
                </div>

                <div class="infobox infobox-pink  ">
                    <div class="infobox-icon">
                        <i class="icon-income"></i>
                    </div>

                    <div class="infobox-data">
                        <span class="infobox-data-number" id="OrdeAmt">0</span>
                        <div class="infobox-content">本月应收，今日增长量</div>
                    </div>
                    <div class="stat stat-success" id="OrderAmtDay">0</div>
                </div>

                <div class="infobox infobox-red  ">
                    <div class="infobox-icon">
                        <i class="icon-outpay"></i>
                    </div>

                    <div class="infobox-data">
                        <span class="infobox-data-number" id="OrderCost">0</span>
                        <div class="infobox-content">本月应付，今日增长量</div>

                    </div>
                    <div class="stat stat-success" id="OrderCostDay">0</div>
                </div>

                <div class="infobox infobox-orange2  ">
                    <div class="infobox-icon">
                        <i class="icon-profit"></i>
                    </div>

                    <div class="infobox-data">
                        <span class="infobox-data-number" id="total">0</span>
                        <div class="infobox-content">本月利润，今日增长量</div>
                    </div>

                    <div class="badge badge-success" id="totalPert">
                        0
                    </div>
                </div>



                <div class="space-6"></div>

                <div class="infobox infobox-green infobox-small infobox-dark">
                    <a href="LoginOut.aspx" style="line-height: 43px; text-align: center; display: block; color: #fff !important; width: 100%; height: 100%; font-size: 20px">退出</a>
                </div>

            </div>

            <div class="vspace-sm"></div>
        </div>
    </div>

</asp:Content>
