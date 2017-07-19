<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Menu.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu" class="easyui-layout">
    <form id="form1" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        document.onkeypress = check;
        document.onkeydown = check;
        function SelectMenu(num) {
            for (var i = 1; i <= 30; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="60000">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="技术管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                技术任务管理<asp:Label ID="lblTechAssign" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>我的任务</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink12" runat="server" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/TM_Data/TM_Paint_Task.aspx" Target="right">
                    <p>
                        我的油漆任务
                        <asp:Label runat="server" ID="lbYQRW" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                审核任务<asp:Label ID="task" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>新增采购申请</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                采购申请审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                需用计划管理<asp:Label ID="lb_XYplan" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                物料减少/变更审批<asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink18" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>工作量统计</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink19" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                需用计划驳回
                                <asp:Label ID="lblBack" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                不合格品通知单<asp:Label ID="lb_rejectPro" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/CM_Data/CM_CUSTOMER.aspx"><p>顾客财产</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink43" onClick="SelectMenu(43);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_SCDYTZSP.aspx" Target="right" runat="server">
                    <p>
                        生产代用通知审批
                        <asp:Label runat="server" ID="lbDYTZ" Text="" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    Target="_blank" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                代用单审核<asp:Label ID="lb_dydsh" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lb_mp_ck" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
                    <p>
                        计划单变更<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ServiceList.aspx?Dep=031" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                顾客服务通知<asp:Label runat="server" ID="lblFWTZ" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ServiceList.aspx?Dep=03" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                顾客服务处理<asp:Label runat="server" ID="lblFWCL" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>BOM原始数据查询</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FaHuoNotice.aspx" Target="right">
                    <p>
                        发货通知<asp:Label runat="server" ID="fhtz" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink24" runat="server" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHLXDGL.aspx" Target="right">
                    <p>
                        服务联系单
                        <asp:Label runat="server" ID="lbLXD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink23" runat="server" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLDGL.aspx" Target="right">
                    <p>
                        质量问题处理单
                        <asp:Label runat="server" ID="lbCLD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_TZTHTZDGL.aspx" Target="right" runat="server">
                    <p>
                        图纸替换通知单<asp:Label runat="server" ID="lbTZTHTZD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <%-- <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>涂装方案查询</p></asp:HyperLink>--%>
                <%--     <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>工程量管理</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected" 
                Target="right" runat="server">
                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>变更物料备库查看<asp:Label ID="lblBG_BK" runat="server" ForeColor="Red" Visible="false"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                
                </asp:HyperLink>    
                 
                 
                
                    
                    <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>采购申请审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:HyperLink>
                 <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">                    
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                        </Triggers>
                        <ContentTemplate>
                            <p>项目结转备库<asp:Label ID="LabelProjTemp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                   </asp:UpdatePanel>                   
                    
                </asp:HyperLink>
                
                 <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>进度管理-技术</p></asp:HyperLink>--%>
            </div>
            <div title="工艺工时管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                工艺类工艺卡片<asp:Label ID="lblProcessCard" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                通用类工艺卡片<asp:Label ID="lblProcessCardGen" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink17" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                工时管理<asp:Label ID="lblGongShi" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                工时数据统计</p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
