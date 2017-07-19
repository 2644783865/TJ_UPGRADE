<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="Sta_detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.Sta_detail" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员信息管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="SM_JS/NameQuery.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .show
        {
            display: block;
        }
    </style>

    <script type="text/javascript">
        function viewCondition() {
            document.getElementById('<%=select.ClientID%>').style.display = 'block';
        }

        function Close() {
            document.getElementById('<%=select.ClientID%>').style.display = 'none';
        }

        function btnPrint_onclick(id) {
            var date = new Date();
            var time = date.getTime();
            window.showModalDialog("Person_Print.aspx?id=" + time + "&&code=" + id, '', "dialogWidth=1200px;dialogHeight=580px;status=no;help=no;");
        }

        function RowClick(obj) {

            //console.log($(obj).parent());
            //判断是否单击的已选择的行，如果是则取消该行选择
            if (obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked == false) {
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;

            }
            else {
                var table = obj.parentNode.parentNode;
                var tr = table.getElementsByTagName("tr");
                var ss = tr.length;
                for (var i = 1; i < ss; i++) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                    tr[i].style.backgroundColor = "white";
                }
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                obj.style.backgroundColor = "#0094ff";
            }

        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ScrollBars="Auto" ActiveTabIndex="0">
        <asp:TabPanel ID="Tab" runat="server" TabIndex="0" HeaderText="人员信息">
            <ContentTemplate>
                <div class="RightContent">
                    <div class="box-inner">
                        <div class="box_right">
                            <div class="box-title">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 180px;">
                                            <strong>按部门查：</strong>
                                            <asp:DropDownList Width="100px" ID="DDlpartment" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDlpartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 180px;">
                                            <strong>岗位序列：</strong>
                                            <asp:DropDownList ID="ddlSequce" runat="server" Width="100px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDlpartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 180px;">
                                            <strong>合同主体：</strong>
                                            <asp:DropDownList ID="ddlHetongZt" runat="server" Width="100px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 180px;">
                                            <strong>户口类型：</strong>
                                            <asp:DropDownList ID="ddlHkType" runat="server" Width="100px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 180px;">
                                            <strong>政治面貌：</strong>
                                            <asp:DropDownList ID="ddlZzmm" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 180px;">
                                            <strong>按学历查：</strong>
                                            <asp:DropDownList ID="DDlxueli" Width="100px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 180px;">
                                            <strong>职称等级：</strong>
                                            <asp:DropDownList ID="ddlZcdj" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                                <asp:ListItem Text="正高级职称" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="副高级职称" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="中级职称" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="初级职称" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 360px;">
                                            <strong>职技等级：</strong>
                                            <asp:DropDownList ID="ddlZjdj" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DDlxueli_SelectedIndexChanged">
                                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                                <asp:ListItem Text="高级技师" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="技师" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="高级技能" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="中级技能" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="初级技能" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp&nbsp <strong>按姓名查：</strong>
                                            <asp:TextBox ID="name" Width="92px" runat="server"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="name"
                                                ServicePath="~/OM_Data/OM_Data_Autocomplete.asmx" CompletionSetCount="100" MinimumPrefixLength="1"
                                                CompletionInterval="100" ServiceMethod="Getdata" FirstRowSelected="true" CompletionListCssClass="completionListElement"
                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="highlightedListItem"
                                                UseContextKey="false" EnableCaching="false">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td style="width: 540px">
                                            <strong>离职时间：&nbsp</strong><asp:TextBox ID="txtLZSJS" Width="100px" class="easyui-datebox"
                                                Height="20px" runat="server"></asp:TextBox>
                                            <strong>-</strong>&nbsp<asp:TextBox ID="txtLZSJE" Width="100px" class="easyui-datebox"
                                                Height="20px" runat="server"></asp:TextBox>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:Button ID="search" runat="server" Text="查 看" OnClick="search_Click" />
                                            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="HyperLink1" CssClass="hand" runat="server"
                                                OnClientClick="viewCondition()">
                                                <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />
                                                其它筛选</asp:LinkButton>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HyperLink1"
                                                PopupControlID="select" Y="102" X="600" CancelControlID="guan">
                                            </asp:ModalPopupExtender>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="select" Style="display: none; border-style: solid; border-width: 1px;
                                    border-color: blue; background-color: Menu;" runat="server">
                                    <table width="400px;">
                                        <tr>
                                            <td>
                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                    <a id="guan" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF; text-align: center;
                                                        text-decoration: none; padding: 5px;" title="关闭">X</a>
                                                </div>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz1" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt1" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy1" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz2" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt2" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy2" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz3" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt3" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy3" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz4" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt4" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy4" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz5" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt5" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy5" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz6" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy6" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz7" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt7" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy7" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic7" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz8" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt8" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy8" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic8" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz9" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt9" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy9" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLogic9" BackColor="AliceBlue" runat="server">
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="khz10" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="(" Value="("></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
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
                                                <asp:TextBox ID="Txt10" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="khy10" runat="server">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right">
                                                <asp:Button ID="reset" runat="server" Text="重 置" OnClick="reset_Click" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnfind" runat="server" Text="搜 索" OnClick="search_Click" />&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lbtips" runat="server" Text="使用提示：部门 包含 公司领导 -可以筛选公司内部所属部门为公司领导的员工信息；（部门 包含 公司领导 ）并且 （ 性别 等于 男）-可以筛选公司内部所属部门为公司领导的男性员工信息。其中，'不包含'指排除该特征的员工，'并且'指前后两括号条件同时成立，'或者'指前后两括号条件满足其一即可"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-inner">
                    <div class="box_right" style="width: 100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 300px">
                                    <asp:RadioButtonList runat="server" ID="rblIfZaizhi" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblIfZaizhi_OnSelectedIndexChanged">
                                        <asp:ListItem Text="全部" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="在职" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="离职" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="实习" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="非司人员" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="调出" Value="5"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 270px">
                                    <asp:CheckBox ID="cbxConWarn" runat="server" AutoPostBack="true" OnCheckedChanged="rblIfZaizhi_OnSelectedIndexChanged" />合同到期提醒<asp:Label
                                        ForeColor="Red" ID="lblConWarn" runat="server"></asp:Label>
                                </td>
                                <td style="width: 360px">
                                    <asp:Button ID="btnexport" runat="server" Text="导 出" OnClick="btnexport_Click" />&nbsp;
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnprint" runat="server" Text="打 印" OnClick="btnprint_Click" />&nbsp;
                                    &nbsp; &nbsp;
                                    <asp:HyperLink ID="HyperLink2" NavigateUrl="Sta_StaffEdit.aspx?action=add" runat="server">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        添加员工</asp:HyperLink>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:HyperLink ID="hplExpotMP" CssClass="hand" NavigateUrl="~/OM_Data/OM_Sta_Edit_Record.aspx"
                                        runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />查询修改记录</asp:HyperLink>&nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                            没有记录!</asp:Panel>
                        <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto;" Width="100%" Height="500px">
                            <asp:SmartGridView ID="SmartGridView1" Width="100%" CssClass="toptable grid" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="SmartGridView1_RowDataBound"
                                Style="white-space: normal">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" HeaderStyle-Height="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="ST_CODE" runat="server" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                            <asp:Label ID="ST_ID" runat="server" Text='<%#Eval("ST_ID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="ST_PD" runat="server" Text='<%#Eval("ST_PD")%>' Visible="false"></asp:Label>
                                            <asp:CheckBox ID="checkboxstaff" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                            </asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink100" NavigateUrl='<%# viewYg(Eval("ST_ID").ToString()) %>'
                                                runat="server" ToolTip="点击查看" Width="60px">
                                                <asp:Image ID="Image100" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />查看</asp:HyperLink></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="姓名">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="ST_NAME" Text='<%#Eval("ST_NAME") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ST_GENDER" HeaderText="性别" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_PEOPLE" HeaderText="民族" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_AGE" HeaderText="年龄" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_XUELINM" HeaderText="学历" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_POLITICAL" HeaderText="政治面貌" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="DEP_NAME" HeaderText="一级机构" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_DEPID1" HeaderText="二级机构" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_SEQUEN" HeaderText="岗位序列" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="岗位" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div style="width: 80px">
                                                <label>
                                                    <%# Eval("DEP_POSITION")%></label></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ST_ZHICH" HeaderText="职称" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_ZHICHXU" HeaderText="职技" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_CONTR" HeaderText="合同主体" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_INTIME" HeaderText="入职时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_REGIST" HeaderText="户口类型" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_CONTRSTART" HeaderText="合同起始时间" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="ST_CONTREND" HeaderText="合同终止时间" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="ST_LZSJ" HeaderText="离职时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="ST_TELE" HeaderText="联系电话" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ST_FILLDATE" HeaderText="添加时间" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="MANCLERK" HeaderText="添加人" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                        Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="ST_NOTE" HeaderText="备注" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                        Visible="false"></asp:BoundField>
                                    <asp:TemplateField HeaderText="修改">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink3" NavigateUrl='<%# editYg(Eval("ST_ID").ToString()) %>'
                                                runat="server" ToolTip="点击修改" Width="60px">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="White" Height="20px" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:SmartGridView>
                        </asp:Panel>
                        <div style="text-align: center; padding-top: 6px">
                            总人数：
                            <asp:Label ID="lb_People" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>&nbsp;人
                        </div>
                        <div style="float: left; padding-top: 10px">
                            <asp:Button ID="deletebt" runat="server" Text="删除" OnClick="deletebt_Click" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlIfzaizhi" OnSelectedIndexChanged="ddlIfzaizhi_OnSelectedIndexChanged">
                                <asp:ListItem Text="-请选择-" Value="3"></asp:ListItem>
                                <asp:ListItem Text="在职" Value="0"></asp:ListItem>
                                <asp:ListItem Text="离职" Value="1"></asp:ListItem>
                                <asp:ListItem Text="实习" Value="2"></asp:ListItem>
                                <asp:ListItem Text="非司人员" Value="4"></asp:ListItem>
                                <asp:ListItem Text="调出" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:UCPaging ID="UCPaging1" runat="server" />
                                    </td>
                                    <td>
                                        每页：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDlpartment_SelectedIndexChanged">
                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="全部" Value="10000"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;行
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="Tab1" runat="server" TabIndex="1" HeaderText="数据统计">
            <HeaderTemplate>
                数据统计
            </HeaderTemplate>
            <ContentTemplate>
                <div style="height: 600px; overflow: scroll;">
                    <table>
                        <tr>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            部门人数
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ1" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    序号
                                                </td>
                                                <td>
                                                    部门名称
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                </td>
                                                <td>
                                                    <%#Eval("DEP_NAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_TOTAL") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                        </td>
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            职称/职技结构
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ3" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    职称
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_ZHICHNM")%>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N2}", Convert.ToDouble(Eval("ST_NUM")) / Convert.ToDouble(Eval("ST_TOTAL")) * 100) + "%"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total2" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Prop2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            合同主体
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ7" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    项目
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_CONTR") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N2}", Convert.ToDouble(Eval("ST_NUM")) / Convert.ToDouble(Eval("ST_TOTAL")) * 100) + "%"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total6" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Prop6" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            学历结构
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ2" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    序号
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_XUELINM") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N2}", Convert.ToDouble(Eval("ST_NUM")) / Convert.ToDouble(Eval("ST_TOTAL")) * 100) + "%"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total1" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Prop1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            年龄结构
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ4" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    年龄结构
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_AREA") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N2}", Convert.ToDouble(Eval("ST_NUM")) / Convert.ToDouble(Eval("ST_TOTAL")) * 100) + "%"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total3" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Prop3" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            序列结构
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ6" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    项目
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_SEQUEN") %>岗位人数
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                                <td>
                                                    <%#string.Format("{0:N2}", Convert.ToDouble(Eval("ST_NUM")) / Convert.ToDouble(Eval("ST_TOTAL")) * 100) + "%"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total5" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Prop5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            男女比例
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rep_TJ5" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    项目名称
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='baseGadget'>
                                                <td>
                                                    <%#Eval("ST_GENDER") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ST_NUM") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            总人数：
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_Total4" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50px">
                            </td>
                            <td valign="top">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="width: 240px">
                                    <tr>
                                        <td colspan="3" align="center">
                                            <label style="color: #F56C0B">
                                                离职人员信息</label>
                                        </td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptLZXX">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td>
                                                    部门
                                                </td>
                                                <td>
                                                    人数
                                                </td>
                                                <td>
                                                    比例
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr align="center" class='baseGadget'>
                                                <td>
                                                    <%#Eval("LZ_BUMEN")%>
                                                </td>
                                                <td>
                                                    <label style="color: #F56C0B">
                                                        <%#Eval("RS")%></label>
                                                </td>
                                                <td>
                                                    <label style="color: #F56C0B">
                                                        <%#string.Format("{0:N2}", Convert.ToDouble(Eval("RS")) / Convert.ToDouble(Eval("ZS")) * 100) + "%"%></label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr align="center">
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbLZHJ" ForeColor="#F56C0B"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbLZHJBL" Text="100%" ForeColor="#F56C0B"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <%--</ContentTemplate>
        
    </asp:UpdatePanel>--%>
</asp:Content>
