<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_GZQD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZQD" Title="工资清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工资清单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script language="javascript" type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 7,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    
                    }
            });
        }

        $(function() {
            sTable();
        });
    </script>

    <asp:ToolkitScriptManager runat="server" AsyncPostBackTimeout="10" ID="ToolkitScriptManager1">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <strong>时间：</strong>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;年&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;月&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <strong>部门：</strong>&nbsp;
                    <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                        OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
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
                                    姓名：
                                </td>
                                <td width="100px">
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_name"
                                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtName" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    工号：
                                </td>
                                <td width="100px">
                                    <asp:TextBox ID="txtworkno" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    班组：
                                </td>
                                <td width="100px">
                                    <asp:DropDownList ID="DropDownListbanzu" runat="server" Width="100px" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    岗位：
                                </td>
                                <td width="100px">
                                    <asp:DropDownList ID="DropDownListgw" runat="server" Width="100px" AutoPostBack="false">
                                    </asp:DropDownList>
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
                    &nbsp;&nbsp;&nbsp;
                    每页显示记录条数:
                    <asp:DropDownList ID="DropDownListCount" runat="server" Width="60px" OnSelectedIndexChanged="Count_Change"
                        AutoPostBack="true">
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="150">150</asp:ListItem>
                        <asp:ListItem Value="300" Selected="True">300</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;
                    隐藏列：
                    <asp:CheckBox ID="cbxBumen" runat="server" AutoPostBack="true" OnCheckedChanged="cbxBumen_CheckedChanged" />
                    部门 &nbsp;&nbsp;
                    <asp:CheckBox ID="cbxbanzu" runat="server" AutoPostBack="true" OnCheckedChanged="cbxbanzu_CheckedChanged" />
                    班组 &nbsp;&nbsp;
                    <asp:CheckBox ID="cbxgh" runat="server" AutoPostBack="true" OnCheckedChanged="cbxgh_CheckedChanged" />
                    工号 &nbsp;&nbsp;
                    <asp:CheckBox ID="cbxgw" runat="server" AutoPostBack="true" OnCheckedChanged="cbxgw_CheckedChanged" />
                    岗位 &nbsp;&nbsp;
                    <asp:CheckBox ID="cbxKaoqin" runat="server" AutoPostBack="true" OnCheckedChanged="cbxKaoqin_CheckedChanged" />
                    考勤 &nbsp;&nbsp;
                    <asp:CheckBox ID="cbxWXYJ" runat="server" AutoPostBack="true" OnCheckedChanged="cbxWXYJ_CheckedChanged" />
                    五险一金
                </td>
                <td align="right">
                    <asp:Button ID="btnDaochu" runat="server" Text="导出" OnClick="btnDaochu_OnClick" />
                    <asp:FileUpload ID="FileUpload1" Width="130px" runat="server" ToolTip="导 入" Visible="false" />
                    <asp:Button ID="btnimport" runat="server" Text="导入" Visible="false" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnimport"
                        PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="80" X="900">
                    </asp:ModalPopupExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/OM_Data/OM_GZQDJS.aspx"
                        Target="_blank" Font-Underline="false">
                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />计算工资</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelCondition" runat="server" Width="300px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button ID="QueryButton" runat="server" OnClick="btnimport_OnClick" Text="确认" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClose" runat="server" OnClick="btnqx_import_Click" Text="取消" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>年月：</strong><asp:TextBox ID="tb_yearmonth" Width="120px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                            ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="rptGZQD" runat="server" OnItemDataBound="rptGZQD_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" style="background-color: #B9D3EE;">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        编号
                                    </td>
                                    <td>
                                        年月份
                                    </td>
                                    <td runat="server" id="tdgonghao">
                                        工号
                                    </td>
                                    <td>
                                        姓名
                                    </td>
                                    <td>
                                        合同主体
                                    </td>
                                    <td runat="server" id="tdBumen">
                                        部门
                                    </td>
                                    <td runat="server" id="tdBanzu">
                                        班组
                                    </td>
                                    <td runat="server" id="tdgangwei">
                                        岗位
                                    </td>
                                    <td runat="server" id="tdYCQHJ">
                                        出勤
                                    </td>
                                    <td runat="server" id="tdJRwork">
                                        节加
                                    </td>
                                    <td runat="server" id="tdZhouwork">
                                        周加
                                    </td>
                                    <td runat="server" id="tdRiwork">
                                        延时
                                    </td>
                                    <td runat="server" id="tdYeban">
                                        夜班
                                    </td>
                                    <td runat="server" id="tdBingjia">
                                        病假
                                    </td>
                                    <td runat="server" id="tdShijia">
                                        事假
                                    </td>
                                    <td runat="server" id="tdNianjia">
                                        年假
                                    </td>
                                    <td>
                                        基础工资
                                    </td>
                                    <td>
                                        工龄工资
                                    </td>
                                    <td>
                                        固定工资
                                    </td>
                                    <td>
                                        绩效工资
                                    </td>
                                    <td>
                                        奖励
                                    </td>
                                    <td>
                                        病假工资
                                    </td>
                                    <td>
                                        加班工资
                                    </td>
                                    <td>
                                        加班补发
                                    </td>
                                    <td>
                                        中夜班费
                                    </td>
                                    <td>
                                        中夜班补发
                                    </td>
                                    <td>
                                        年假工资
                                    </td>
                                    <td>
                                        应扣岗位
                                    </td>
                                    <td>
                                        调整/补发
                                    </td>
                                    <td>
                                        调整/补扣
                                    </td>
                                    <td>
                                        交通补贴
                                    </td>
                                    <td>
                                        防暑降温费
                                    </td>
                                    <td>
                                        采暖补贴
                                    </td>
                                    <td>
                                        其他
                                    </td>
                                    <td>
                                        应付合计
                                    </td>
                                    <td runat="server" id="tdYangLBX">
                                        养老保险
                                    </td>
                                    <td runat="server" id="tdSYBX">
                                        失业保险
                                    </td>
                                    <td runat="server" id="tdYiLBX">
                                        医疗保险
                                    </td>
                                    <td runat="server" id="tdDEJiuZhu">
                                        大额救助
                                    </td>
                                    <td runat="server" id="tdBuBX">
                                        补保险
                                    </td>
                                    <td runat="server" id="tdGJJ">
                                        公积金
                                    </td>
                                    <td runat="server" id="tdBGJJ">
                                        补公积金
                                    </td>
                                    <td>
                                        房租/水电费
                                    </td>
                                    <td>
                                        个税
                                    </td>
                                    <td>
                                        代扣小计
                                    </td>
                                    <td>
                                        实发金额
                                    </td>
                                    <td>
                                        扣税基数
                                    </td>
                                    <td>
                                        备注
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="row" class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                    ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:Label runat="server" ID="lbQD_ID" Visible="false" Text='<%#Eval("QD_ID")%>'></asp:Label>
                                        <asp:CheckBox ID="cbxNumber" CssClass="checkBoxCss" runat="server" />
                                        <%# Container.ItemIndex + 1 + (Convert.ToDouble(UCPaging1.CurrentPage) - 1) * (Convert.ToDouble(UCPaging1.PageSize))%>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lbQD_SHBH" runat="server" Text='<%#Eval("QD_SHBH")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_YEARMONTH" runat="server" Text='<%#Eval("QD_YEARMONTH")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_Worknumber" runat="server" align="center">
                                        <asp:Label ID="lbQD_Worknumber" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_Name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_HTZT" runat="server" Text='<%#Eval("QD_HTZT")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_BuMen" runat="server" align="center">
                                        <asp:Label ID="lbQD_BuMen" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_BanZu" runat="server" align="center">
                                        <asp:Label ID="lbQD_BanZu" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_GangWe" runat="server" align="center">
                                        <asp:Label ID="lbQD_GangWei" runat="server" Text='<%#Eval("DEP_NAME_POSITION")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_CHUQIN" runat="server" align="center">
                                        <asp:Label ID="lbKQ_CHUQIN" runat="server" Text='<%#Eval("KQ_CHUQIN").ToString()=="0"?"":Eval("KQ_CHUQIN")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_JRJIAB" runat="server" align="center">
                                        <asp:Label ID="lbKQ_JRJIAB" runat="server" Text='<%#Eval("KQ_JRJIAB").ToString()=="0"?"":Eval("KQ_JRJIAB")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_ZMJBAN" runat="server" align="center">
                                        <asp:Label ID="lbKQ_ZMJBAN" runat="server" Text='<%#Eval("KQ_ZMJBAN").ToString()=="0"?"":Eval("KQ_ZMJBAN")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_YSGZ" runat="server" align="center">
                                        <asp:Label ID="lbKQ_YSGZ" runat="server" Text='<%#Eval("KQ_YSGZ").ToString()=="0"?"":Eval("KQ_YSGZ")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_YEBAN" runat="server" align="center">
                                        <asp:Label ID="lbKQ_YEBAN" runat="server" Text='<%#Eval("KQ_YEBAN").ToString()=="0"?"":Eval("KQ_YEBAN")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_BINGJ" runat="server" align="center">
                                        <asp:Label ID="lbKQ_BINGJ" runat="server" Text='<%#Eval("KQ_BINGJ").ToString()=="0"?"":Eval("KQ_BINGJ")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_SHIJ" runat="server" align="center">
                                        <asp:Label ID="lbKQ_SHIJ" runat="server" Text='<%#Eval("KQ_SHIJ").ToString()=="0"?"":Eval("KQ_SHIJ")%>'></asp:Label>
                                    </td>
                                    <td id="tdKQ_NIANX" runat="server" align="center">
                                        <asp:Label ID="lbKQ_NIANX" runat="server" Text='<%#Eval("KQ_NIANX").ToString()=="0"?"":Eval("KQ_NIANX")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_JCGZ" runat="server" Text='<%#Eval("QD_JCGZ").ToString()=="0"?"":Eval("QD_JCGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_GZGL" runat="server" Text='<%#Eval("QD_GZGL").ToString()=="0"?"":Eval("QD_GZGL")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_GDGZ" runat="server" Text='<%#Eval("QD_GDGZ").ToString()=="0"?"":Eval("QD_GDGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_JXGZ" runat="server" Text='<%#Eval("QD_JXGZ").ToString()=="0"?"":Eval("QD_JXGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_JiangLi" runat="server" Text='<%#Eval("QD_JiangLi").ToString()=="0"?"":Eval("QD_JiangLi")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_BingJiaGZ" runat="server" Text='<%#Eval("QD_BingJiaGZ").ToString()=="0"?"":Eval("QD_BingJiaGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_JiaBanGZ" runat="server" Text='<%#Eval("QD_JiaBanGZ").ToString()=="0"?"":Eval("QD_JiaBanGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_BFJB" runat="server" Text='<%#Eval("QD_BFJB").ToString()=="0"?"":Eval("QD_BFJB")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_ZYBF" runat="server" Text='<%#Eval("QD_ZYBF").ToString()=="0"?"":Eval("QD_ZYBF")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_BFZYB" runat="server" Text='<%#Eval("QD_BFZYB").ToString()=="0"?"":Eval("QD_BFZYB")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_NianJiaGZ" runat="server" Text='<%#Eval("QD_NianJiaGZ").ToString()=="0"?"":Eval("QD_NianJiaGZ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_YKGW" runat="server" Text='<%#Eval("QD_YKGW").ToString()=="0"?"":Eval("QD_YKGW")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_TZBF" runat="server" Text='<%#Eval("QD_TZBF").ToString()=="0"?"":Eval("QD_TZBF")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_TZBK" runat="server" Text='<%#Eval("QD_TZBK").ToString()=="0"?"":Eval("QD_TZBK")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_JTBT" runat="server" Text='<%#Eval("QD_JTBT").ToString()=="0"?"":Eval("QD_JTBT")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_FSJW" runat="server" Text='<%#Eval("QD_FSJW").ToString()=="0"?"":Eval("QD_FSJW")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_CLBT" runat="server" Text='<%#Eval("QD_CLBT").ToString()=="0"?"":Eval("QD_CLBT")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_QTFY" runat="server" Text='<%#Eval("QD_QTFY").ToString()=="0"?"":Eval("QD_QTFY")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_YFHJ" runat="server" Text='<%#Eval("QD_YFHJ").ToString()=="0"?"":Eval("QD_YFHJ")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_YLBX" runat="server" align="center">
                                        <asp:Label ID="lbQD_YLBX" runat="server" Text='<%#Eval("QD_YLBX").ToString()=="0"?"":Eval("QD_YLBX")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_SYBX" runat="server" align="center">
                                        <asp:Label ID="lbQD_SYBX" runat="server" Text='<%#Eval("QD_SYBX").ToString()=="0"?"":Eval("QD_SYBX")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_YiLiaoBX" runat="server" align="center">
                                        <asp:Label ID="lbQD_YiLiaoBX" runat="server" Text='<%#Eval("QD_YiLiaoBX").ToString()=="0"?"":Eval("QD_YiLiaoBX")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_DEJZ" runat="server" align="center">
                                        <asp:Label ID="lbQD_DEJZ" runat="server" Text='<%#Eval("QD_DEJZ").ToString()=="0"?"":Eval("QD_DEJZ")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_BuBX" runat="server" align="center">
                                        <asp:Label ID="lbQD_BuBX" runat="server" Text='<%#Eval("QD_BuBX").ToString()=="0"?"":Eval("QD_BuBX")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_GJJ" runat="server" align="center">
                                        <asp:Label ID="lbQD_GJJ" runat="server" Text='<%#Eval("QD_GJJ").ToString()=="0"?"":Eval("QD_GJJ")%>'></asp:Label>
                                    </td>
                                    <td id="tdQD_BGJJ" runat="server" align="center">
                                        <asp:Label ID="lbQD_BGJJ" runat="server" Text='<%#Eval("QD_BGJJ").ToString()=="0"?"":Eval("QD_BGJJ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_ShuiDian" runat="server" Text='<%#Eval("QD_ShuiDian").ToString()=="0"?"":Eval("QD_ShuiDian")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_GeShui" runat="server" Text='<%#Eval("QD_GeShui").ToString()=="0"?"":Eval("QD_GeShui")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_DaiKouXJ" runat="server" Text='<%#Eval("QD_DaiKouXJ").ToString()=="0"?"":Eval("QD_DaiKouXJ")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_ShiFaJE" runat="server" Text='<%#Eval("QD_ShiFaJE").ToString()=="0"?"":Eval("QD_ShiFaJE")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_KOUSJS" runat="server" Text='<%#Eval("QD_KOUSJS").ToString()=="0"?"":Eval("QD_KOUSJS")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbQD_NOTE" runat="server" Text='<%#Eval("QD_NOTE")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="3" align="right">
                                        合计:
                                    </td>
                                    <td id="tdfoot1" runat="server">
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td id="tdfootbm" runat="server">
                                    </td>
                                    <td id="tdfootbz" runat="server">
                                    </td>
                                    <td id="tdfootgw" runat="server">
                                    </td>
                                    <td id="tdfoot2" colspan="8" runat="server">
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_JCGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_GZGLhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_GDGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_JXGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_JiangLihj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_QD_BingJiaGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_JiaBanGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_BFJBhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_ZYBFhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_BFZYBhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_NianJiaGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_YKGWhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_TZBFhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_TZBKhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_JTBThj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_FSJWhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_CLBThj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_QTFYhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_YFHJhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_YLBXhj" runat="server">
                                        <asp:Label ID="lb_QD_YLBXhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_SYBXhj" runat="server">
                                        <asp:Label ID="lb_QD_SYBXhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_YiLiaoBXhj" runat="server">
                                        <asp:Label ID="lb_QD_YiLiaoBXhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_DEJZhj" runat="server">
                                        <asp:Label ID="lb_QD_DEJZhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_BuBXhj" runat="server">
                                        <asp:Label ID="lb_QD_BuBXhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_GJJhj" runat="server">
                                        <asp:Label ID="lb_QD_GJJhj" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdQD_BGJJhj" runat="server">
                                        <asp:Label ID="lb_QD_BGJJhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_ShuiDianhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_GeShuihj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_DaiKouXJhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_ShiFaJEhj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_QD_KOUSJShj" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="37" align="center">
                                <asp:Panel ID="palNodata" runat="server" ForeColor="Red" Visible="false">
                                    没有记录！</asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
            <table width="100%" border="1" class="nowrap cptable fullwidth" align="center">
                <tr>
                    <td width="20%" align="left">
                        <asp:CheckBox ID="cbxselectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                            OnCheckedChanged="cbxselectall_CheckedChanged" />
                        <asp:Button ID="btnLX" runat="server" Text="连选" OnClick="btnLX_Onclick" />
                        <asp:Button ID="btnQX" runat="server" Text="取消" OnClick="btnQX_Onclick" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
