<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jiPiaomanagementaspx.aspx.cs"
    Inherits="ZCZJ_DPF.OM_Data.jiPiaomanagementaspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机票管理</title>
    <base id="goDownload" target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/EasyUICommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="height: 25px">
        </div>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="box-wrapper">
            <div class="box-outer">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="audittitle" runat="server" Text="机票申请"></asp:Label><%--审批类型，每次添加新的审批时，必须修改--%>
                            &nbsp;&nbsp;&nbsp;<strong>部门：</strong>&nbsp;
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;<strong>制单人：</strong><asp:TextBox ID="txtName" runat="server" Width="90px"></asp:TextBox>
                            申请日期从：<input type="text" id="startdate" style="width: 100px" onfocus="this.blur()"
                                runat="server" class="easyui-datebox" />&nbsp;&nbsp;&nbsp; 到：<input type="text" id="enddate"
                                    onfocus="this.blur()" runat="server" style="width: 100px" class="easyui-datebox" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLinkAdd" runat="server" Target="_blank" NavigateUrl="~/OM_Data/jiPiaodetail.aspx?action=add&auditno=">
                                <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="35%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="shstate" runat="server" OnSelectedIndexChanged="shstate_CheckedChanged"
                                        RepeatDirection="Horizontal" AutoPostBack="true">
                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="0">初始化</asp:ListItem>
                                        <asp:ListItem Value="1">审核中</asp:ListItem>
                                        <asp:ListItem Value="2">已通过</asp:ListItem>
                                        <asp:ListItem Value="3">已驳回</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="task" runat="server" OnSelectedIndexChanged="shstate_CheckedChanged"
                                        RepeatDirection="Horizontal" AutoPostBack="true">
                                        <asp:ListItem Value="">全部</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">我的任务</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Depart" />
                <asp:AsyncPostBackTrigger ControlID="btncx" />
                <asp:AsyncPostBackTrigger ControlID="shstate" />
                <asp:AsyncPostBackTrigger ControlID="task" />
            </Triggers>
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                                border="1" width="100%">
                                <asp:Repeater ID="rptjipiao" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;">
                                            <td align="center">
                                                序号
                                            </td>
                                            <td align="center">
                                                单号
                                            </td>
                                            <td align="center">
                                                部门
                                            </td>
                                            <td align="center">
                                                制单人
                                            </td>
                                            <td align="center">
                                                制单时间
                                            </td>
                                            <td>
                                                审核状态
                                            </td>
                                            <td>
                                                反馈状态
                                            </td>
                                            <td align="center">
                                                编辑
                                            </td>
                                            <td align="center">
                                                删除
                                            </td>
                                            <td align="center">
                                                查看
                                            </td>
                                            <td>
                                                审核
                                            </td>
                                            <td>
                                                制单人反馈
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                            <td align="center">
                                                <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="auditno" runat="server" Text='<%#Eval("auditno")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="sqdepartmentname" runat="server" Text='<%#Eval("sqdepartmentname")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="addpername" runat="server" Text='<%#Eval("addpername")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="addtime" runat="server" Text='<%#Eval("addtime")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="totalstate" runat="server" Text='<%#Eval("totalstate").ToString()=="0"?"初始化":Eval("totalstate").ToString()=="1"?"审核中":Eval("totalstate").ToString()=="2"?"已通过":"驳回"%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="fankuistate" runat="server" Text='<%#Eval("fankuistate").ToString()=="0"?"未反馈":"已反馈"%>'></asp:Label>
                                                <asp:Label ID="fankui" runat="server" Visible="false" Text='<%#Eval("fankui")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLinkXG" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=edit&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_binaji" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />编辑</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="HyperLinkSC" OnClick="hlDelete_OnClick" CommandArgument='<%# Eval("auditno")%>'  OnClientClick="return confirm('确认删除吗?')" runat="server">
                                                <asp:Image ID="Image_shanchu" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />删除</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkCK" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=view&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_chakan" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />查看</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkSH" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=audit&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_shenhe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />审核</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkFK" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=fankui&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_fankui" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />反馈</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                没有记录!<br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
