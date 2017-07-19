<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QC_Menu.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Menu" %>

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

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 30; i++) {
                if (document.getElementById("HyperLink" + i) != null) {
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
                }

            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="5000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;功能选项" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="质量管理" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                厂内任务管理<asp:Label ID="lb_task_fengong" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                报检任务管理<asp:Label ID="lb_task_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                我的报检任务<asp:Label ID="lb_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>新增采购申请</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                采购申请审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
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
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>质检人员设置</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>--%>
                    <p>
                        成品入库审批<asp:Label ID="lbl_finish" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FaHuoNotice.aspx" Target="right">
                    <p>
                        发货通知<asp:Label runat="server" ID="fhtz" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink29" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/TM_Data/TM_MP_Back.aspx">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Conditional">
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
                <asp:HyperLink ID="HyperLink13" runat="server" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_CUSTOMER.aspx?dep=03" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel123" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                顾客财产质检<asp:Label ID="lbGKCC" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                目标分解<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/QC_Data/QC_Internal_Audit.aspx">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                内审<asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/QC_Data/QC_External_Audit.aspx">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                外审<asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink18" runat="server" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHLXDGL.aspx" Target="right" Visible="false">
                    <p>
                        服务联系单
                        <asp:Label runat="server" ID="lbLXD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink19" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_SHCLDGL.aspx" Target="right" Visible="false">
                    <p>
                        质量问题处理单
                        <asp:Label runat="server" ID="lbCLD" ForeColor="Red" Visible="true"></asp:Label>
                    </p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_GZLXDGL.aspx" Target="right" runat="server">
                    <p>
                        工作联系单<asp:Label runat="server" ID="lbGZLXD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_TZTHTZDGL.aspx" Target="right" runat="server">
                    <p>
                        图纸替换通知单<asp:Label runat="server" ID="lbTZTHTZD" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/QC_Data/QC_Management_Audit.aspx">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                管理评审<asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                 <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                计划单变更评审<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" runat="server" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ZengBuPs.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                计划单增补评审<asp:Label runat="server" ID="zbtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_JHDQXSP.aspx" Target="right" runat="server">
                    <p>
                        计划单取消评审
                        <asp:Label runat="server" ID="lbQXPS" ForeColor="Red"></asp:Label>
                    </p>
                </asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
