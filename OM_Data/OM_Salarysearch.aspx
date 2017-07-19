<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_Salarysearch.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Salarysearch"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager runat="server" AsyncPostBackTimeout="10" ID="ToolkitScriptManager1">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <table style="width: 100%">
            <tr>
                <td style="width: 29%;">
                    日期从：<input type="text" style="width: 90px" id="startdate" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;},editable:false"
                        runat="server" class="easyui-datebox" />&nbsp;&nbsp;&nbsp; 到：<input type="text" style="width: 90px"
                            id="enddate" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;},editable:false"
                            runat="server" class="easyui-datebox" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnchaxun1" runat="server" UseSubmitBehavior="false" Text="查询" OnClick="btnQuery_OnClick">
                    </asp:Button>
                </td>
                <td width="10%">
                    <asp:HyperLink ID="hylbmbzgw" CssClass="hand" runat="server">
                        <asp:Image ID="Image1" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />多选查询</asp:HyperLink>
                    <asp:PopupControlExtender ID="pcebmbzgw" CacheDynamicResults="false" Position="Bottom"
                        Enabled="true" runat="server" OffsetX="-500" OffsetY="8" TargetControlID="hylbmbzgw"
                        PopupControlID="panlbmbzgw">
                    </asp:PopupControlExtender>
                    <asp:Panel ID="panlbmbzgw" Width="820px" Style="display: none; visibility: hidden;
                        border-style: solid; border-width: 1px; border-color: blue; background-color: Menu;"
                        runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                            title="关闭">X</a>
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    部门
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="listdepartment" runat="server" RepeatColumns="8">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    班组
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="listbanzu" runat="server" RepeatColumns="8">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    岗位序列
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="listposition" runat="server" RepeatColumns="8">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    合同主体
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="listhetongzhuti" runat="server" RepeatColumns="5">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btn_confirm1" runat="server" Text="确定" UseSubmitBehavior="false"
                                        OnClick="btn_confirm1_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_clear1" runat="server" Text="清空" UseSubmitBehavior="false" OnClick="btn_clear1_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td width="10%">
                    <asp:HyperLink ID="hplhtzt" CssClass="hand" runat="server">
                        <asp:Image ID="Image2" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />需合计项勾选</asp:HyperLink>
                    <asp:PopupControlExtender ID="pcehtzt" CacheDynamicResults="false" Position="Bottom"
                        Enabled="true" runat="server" OffsetX="-500" OffsetY="8" TargetControlID="hplhtzt"
                        PopupControlID="panelhtzt">
                    </asp:PopupControlExtender>
                    <asp:Panel ID="panelhtzt" Width="600px" Style="display: none; visibility: hidden;
                        border-style: solid; border-width: 1px; border-color: blue; background-color: Menu;"
                        runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                            title="关闭">X</a>
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    需合计项
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="CheckBoxListhj" runat="server" RepeatColumns="6">
                                        <asp:ListItem Text="基础工资" Value="QD_JCGZ"></asp:ListItem>
                                        <asp:ListItem Text="工龄工资" Value="QD_GZGL"></asp:ListItem>
                                        <asp:ListItem Text="固定工资" Value="QD_GDGZ"></asp:ListItem>
                                        <asp:ListItem Text="绩效工资" Value="QD_JXGZ"></asp:ListItem>
                                        <asp:ListItem Text="奖励" Value="QD_JiangLi"></asp:ListItem>
                                        <asp:ListItem Text="病假工资" Value="QD_BingJiaGZ"></asp:ListItem>
                                        <asp:ListItem Text="加班工资" Value="QD_JiaBanGZ"></asp:ListItem>
                                        <asp:ListItem Text="补发加班" Value="QD_BFJB"></asp:ListItem>
                                        <asp:ListItem Text="中夜班费" Value="QD_ZYBF"></asp:ListItem>
                                        <asp:ListItem Text="补发中夜班" Value="QD_BFZYB"></asp:ListItem>
                                        <asp:ListItem Text="年假工资" Value="QD_NianJiaGZ"></asp:ListItem>
                                        <asp:ListItem Text="应扣岗位" Value="QD_YKGW"></asp:ListItem>
                                        <asp:ListItem Text="调整补发" Value="QD_TZBF"></asp:ListItem>
                                        <asp:ListItem Text="调整补扣" Value="QD_TZBK"></asp:ListItem>
                                        <asp:ListItem Text="其他" Value="QD_QTFY"></asp:ListItem>
                                        <asp:ListItem Text="应付合计" Value="(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)"></asp:ListItem>
                                        <asp:ListItem Text="交通补贴" Value="QD_JTBT"></asp:ListItem>
                                        <asp:ListItem Text="防暑降温费" Value="QD_FSJW"></asp:ListItem>
                                        <asp:ListItem Text="采暖补贴" Value="QD_CLBT"></asp:ListItem>
                                        <asp:ListItem Text="养老保险" Value="QD_YLBX"></asp:ListItem>
                                        <asp:ListItem Text="失业保险" Value="QD_SYBX"></asp:ListItem>
                                        <asp:ListItem Text="医疗保险" Value="QD_YiLiaoBX"></asp:ListItem>
                                        <asp:ListItem Text="大额救助" Value="QD_DEJZ"></asp:ListItem>
                                        <asp:ListItem Text="补保险" Value="QD_BuBX"></asp:ListItem>
                                        <asp:ListItem Text="公积金" Value="QD_GJJ"></asp:ListItem>
                                        <asp:ListItem Text="补公积金" Value="QD_BGJJ"></asp:ListItem>
                                        <asp:ListItem Text="水电费" Value="QD_ShuiDian"></asp:ListItem>
                                        <asp:ListItem Text="扣项" Value="QD_KOUXIANG"></asp:ListItem>
                                        <asp:ListItem Text="个税" Value="QD_GeShui"></asp:ListItem>
                                        <asp:ListItem Text="代扣小计" Value="(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)"></asp:ListItem>
                                        <asp:ListItem Text="实发金额" Value="((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG))"></asp:ListItem>
                                        <asp:ListItem Text="扣税基数" Value="((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG)"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btn_confirm2" runat="server" Text="确定" UseSubmitBehavior="false"
                                        OnClick="btn_confirm2_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_clear2" runat="server" Text="清空" UseSubmitBehavior="false" OnClick="btn_clear2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td width="10%">
                    <asp:HyperLink ID="HyperLinksx" CssClass="hand" runat="server">
                        <asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />其他筛选</asp:HyperLink>
                    <asp:PopupControlExtender ID="PopupControlExtendersx" CacheDynamicResults="false"
                        Position="Bottom" Enabled="true" runat="server" OffsetX="-300" OffsetY="8" TargetControlID="HyperLinksx"
                        PopupControlID="palORG">
                    </asp:PopupControlExtender>
                    <asp:Panel ID="palORG" Width="500px" Style="display: none; visibility: hidden; border-style: solid;
                        border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="chaxuntishi" runat="server" ForeColor="Red">查询多人请用逗号隔开姓名!</asp:Label>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                            title="关闭">X</a>
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    姓名：
                                </td>
                                <td width="100px">
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_name"
                                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtName" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td align="left">
                                    工号：
                                </td>
                                <td width="100px">
                                    <asp:TextBox ID="txtworkno" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen1" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation1" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt1" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen2" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation2" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt2" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen3" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation3" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt3" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen4" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation4" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt4" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen5" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation5" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt5" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen6" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation6" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt6" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen7" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation7" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt7" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic7" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen8" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation8" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt8" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic8" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen9" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation9" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt9" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLogic9" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="60px">
                                    <asp:DropDownList ID="screen10" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" width="100px">
                                    <asp:DropDownList ID="ddlRelation10" BackColor="AliceBlue" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem>
                                        <asp:ListItem Value="2">等于</asp:ListItem>
                                        <asp:ListItem Value="3">不等于</asp:ListItem>
                                        <asp:ListItem Value="4">大于</asp:ListItem>
                                        <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="6">小于</asp:ListItem>
                                        <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt10" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnQuery" runat="server" UseSubmitBehavior="false" Text="查询" OnClick="btnQuery_OnClick">
                                    </asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbtips" runat="server" Text="使用提示：合同主体 包含 天津重机 -可以筛选公司内部合同主体为天津重机的员工工资信息；合同主体 包含 天津重机 与  出勤 等于 20 -可以筛选公司内部合同主体为天津重机的出勤天数为20的员工工资信息。其中，'不包含'指排除该特征的员工工资信息，'并且'指上下两行条件同时成立，'或者'指上下两行条件满足其一即可"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td align="right">
                    <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
            border="1" width="80%">
            <tr>
                <td align="center" width="30px">
                    <strong>序号</strong>
                </td>
                <%for (int h = 1; h < dt.Columns.Count; h++)
                  {
                %>
                <td align="center" style="border-style: solid; font-weight: bold">
                    <%=dt.Columns[h].ColumnName.ToString().Trim()%>
                </td>
                <%	   
                    }
                %>
            </tr>
            <%
                int num = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    num++;
            %>
            <tr>
                <td>
                    <%=num %>
                </td>
                <%for (int j = 1; j < dt.Columns.Count; j++)
                  {
                %>
                <td align="center">
                    <%=dt.Rows[i][j].ToString().Trim()%>
                </td>
                <% 
                    } %>
            </tr>
            <% } %>
        </table>
    </div>
</asp:Content>
