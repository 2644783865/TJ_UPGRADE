﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_ZHAOBIAOLOGIN.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_ZHAOBIAOLOGIN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <!--网页缩放-->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <!--触摸问题-->
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <!--元素设置为块-->
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <!--网页缩放-->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <!--触摸问题-->
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <!--元素设置为块-->
    <title>中材（天津）重型机械有限公司</title>
    <link href="../Assets/WXPTcss/css.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/WXPTcss/index.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/WXPTcss/other.css" rel="stylesheet" type="text/css" />
    <link rel="apple-touch-icon-precomposed" href="./images/logo.png">
    <!--苹果手机添加快捷方式提醒-->
    <!--font awesome 图标插件-->
    <link href="../Assets/WXPTcss/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!--返回顶部-->

    <script src="../Assets/WXPTJS/smoothscroll.js" type="text/javascript"></script>

    <div class="head">
        <%--<a href="http://111.160.8.74:81/wap">--%>
        <img src="../Assets/WXPTcss/logo.png" />
    </div>
    <!--slider 代码 开始 -->
    <section class="slider">
  </section>
    <!--slider 代码 结束 -->
    <section>
    <h3 class="link_nav_con">物料招标</h3>
</section>
    <div class="clear">
        <br />
    </div>
    <div align="center" style="width: 100%">
        <table width="80%">
            <tr>
                <td align="center">
                    <strong style="font-size: medium">账号：</strong>
                    <asp:TextBox ID="txtusername" runat="server" Width="150px" Style="right" Height="30px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <strong style="font-size: medium">密码：</strong>
                    <asp:TextBox ID="txtpassword" runat="server" Width="150px" Style="right" Height="30px"
                        Font-Size="Medium" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                     <asp:Button ID="btnlogin" runat="server" Text="登录" OnClick="btnlogin_click" Height="30px"
                Font-Size="Medium" />
                </td>
            </tr>
            <br>
            <br>
            <br>
            <tr>
                <td align="center" class="foot">
                    <section class="foot_mian">
		            <p>服务热线：022-86890161</p>
		            <p>版权所有 2010-2015 中材(天津)重型机械有限公司</p>
		            </section>
                </td>
            </tr>
        </table>
    </div>

    

    <style type="text/css">
        body
        {
            margin-bottom: 60px !important;
        }
        a, button, input
        {
            -webkit-tap-highlight-color: rgba(255, 0, 0, 0);
        }
        ul, li
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }
        .top_bar
        {
            position: fixed;
            z-index: 900;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            font-family: Helvetica, Tahoma, Arial, Microsoft YaHei, sans-serif;
        }
        .top_menu
        {
            display: -webkit-box;
            border-top: 1px solid #3D3D46;
            display: block;
            width: 100%;
            background: rgba(255, 255, 255, 0.7);
            height: 48px;
            display: -webkit-box; <%--display:box;--%>margin:0;padding:0;-webkit-box-orient:horizontal;background:-webkit-gradient(linear, 0 0, 0 100%, from(#524945), to(#48403c), color-stop(60%, #524945));<%--box-shadow:01px00rgba(255, 255, 255, 0.1)inset;--%>}
        .top_bar .top_menu > li
        {
            -webkit-box-flex: 1;
            position: relative;
            text-align: center;
        }
        .top_menu li:first-child
        {
            background: none;
        }
        .top_bar .top_menu > li > a
        {
            height: 48px;
            margin-right: 1px;
            display: block;
            text-align: center;
            color: #FFF;
            text-decoration: none;
            text-shadow: 0 1px rgba(0, 0, 0, 0.3);
            -webkit-box-flex: 1;
        }
        .top_bar .top_menu > li.home
        {
            max-width: 70px;
        }
        .top_bar .top_menu > li.home a
        {
            height: 66px;
            width: 66px;
            margin: auto; <%--border-radius:60px;--%>position:relative;top:-22px;left:2px;background:url(             '../Assets/WXPTcss/home.png')no-repeatcentercenter;<%--background-size:100%100%;--%>}
        .top_bar .top_menu > li > a label
        {
            overflow: hidden;
            margin: 0 0 0 0;
            font-size: 12px;
            display: block !important;
            line-height: 18px;
            text-align: center;
        }
        .top_bar .top_menu > li > a img
        {
            padding: 3px 0 0 0;
            height: 24px;
            width: 24px;
            color: #fff;
            line-height: 48px;
            vertical-align: middle;
        }
        .top_bar li:first-child a
        {
            display: block;
        }
        .menu_font
        {
            text-align: left;
            position: absolute;
            right: 10px;
            z-index: 500;
            background: -webkit-gradient(linear, 0 0, 0 100%, from(#524945), to(#48403c), color-stop(60%, #524945)); <%--border-radius:5px;--%>width:120px;margin-top:10px;padding:0;<%--box-shadow:01px5pxrgba(0, 0, 0, 0.3);--%>}
        .menu_font.hidden
        {
            display: none;
        }
        .menu_font
        {
            top: inherit !important;
            bottom: 60px;
        }
        .menu_font li a
        {
            height: 40px;
            margin-right: 1px;
            display: block;
            text-align: center;
            color: #FFF;
            text-decoration: none;
            text-shadow: 0 1px rgba(0, 0, 0, 0.3);
            -webkit-box-flex: 1;
        }
        .menu_font li a
        {
            text-align: left !important;
        }
        .top_menu li:last-of-type a
        {
            background: none;
            overflow: hidden;
        }
        .menu_font:after
        {
            top: inherit !important;
            bottom: -6px; <%--border-color:#48403crgba(0, 0, 0, 0)rgba(0, 0, 0, 0);--%>border-width:6px6px0;position:absolute;content:"";<%--display:inline-block;--%>width:0;height:0;border-style:solid;left:80%;}
        .menu_font li
        {
            border-top: 1px solid rgba(255, 255, 255, 0.1);
            border-bottom: 1px solid rgba(0, 0, 0, 0.2);
        }
        .menu_font li:first-of-type
        {
            border-top: 0;
        }
        .menu_font li:last-of-type
        {
            border-bottom: 0;
        }
        .menu_font li a
        {
            height: 40px;
            line-height: 40px !important;
            position: relative;
            color: #fff;
            display: block;
            width: 100%;
            text-indent: 10px;
            white-space: nowrap; <%--text-overflow:ellipsis;--%>overflow:hidden;}
        .menu_font li a img
        {
            width: 20px;
            height: 20px; <%--display:inline-block;--%>margin-top:-2px;color:#fff;line-height:40px;vertical-align:middle;}
        .menu_font > li > a label
        {
            padding: 3px 0 0 3px;
            font-size: 14px;
            overflow: hidden;
            margin: 0;
        }
        #menu_list0
        {
            right: 0;
            left: 10px;
        }
        #menu_list0:after
        {
            left: 20%;
        }
        #sharemcover
        {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.7);
            display: none;
            z-index: 20000;
        }
        #sharemcover img
        {
            position: fixed;
            right: 18px;
            top: 5px;
            width: 260px;
            height: 180px;
            z-index: 20001;
            border: 0;
        }
        .top_bar .top_menu > li > a:hover, .top_bar .top_menu > li > a:active
        {
            background-color: #333;
        }
        .menu_font li a:hover, .menu_font li a:active
        {
            background-color: #333;
        }
        .menu_font li:first-of-type a
        { <%--border-radius:5px5px00;--%>}
        .menu_font li:last-of-type a
        { <%--border-radius:005px5px;--%>}
        #plug-wrap
        {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0);
            z-index: 800;
        }
        #cate18 .device
        {
            bottom: 49px;
        }
        #cate18 #indicator
        {
            bottom: 240px;
        }
        #cate19 .device
        {
            bottom: 49px;
        }
        #cate19 #indicator
        {
            bottom: 330px;
        }
        #cate19 .pagination
        {
            bottom: 60px;
        }
    </style>
    <div class="top_bar" style="-webkit-transform: translate3d(0,0,0)">
        <nav>
    <ul id="top_menu" class="top_menu">
    <li><a href="http://111.160.8.74:81/"><img src="../Assets/WXPTcss/plugmenu6.png" /><label>首 页</label></a></li>
    <li><a href="http://map.baidu.com/mobile/webapp/search/search/qt=s&wd=中材(天津)重型机械有限公司&c=224&searchFlag=bigBox&version=5&exptype=dep/vt=map/?fromhash=1"><img src="../Assets/WXPTcss/plugmenu3.png" /><label>地 图</label></a>    </li>    
<li class="home"><a href="http://111.160.8.74:81/"></a></li>
<li><a href="tel:02286890155"><img src="../Assets/WXPTcss/plugmenu1.png" /><label>拨 号</label></a>    </li>
<li><a href="#top"><img src="../Assets/WXPTcss/plugmenu6.png" /><label>置 顶</label></a> </li></ul>
  </nav>
    </div>
    <div id="plug-wrap" onclick="closeall()" style="display: none;">
    </div>
 </form>
</body>
</html>
