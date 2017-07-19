<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcIn.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GdzcIn" Title="入库管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产入库查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToOrder() {
            window.open("OM_GdzcOrderDetail.aspx?FLAG=add&id=1");
        }

        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=INCODE]").val();
                window.open("OM_GdzcOrderDetail.aspx?FLAG=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:HyperLink runat="server" ID="hplAdd" NavigateUrl="~/OM_Data/OM_GdzcOrderDetail.aspx?FLAG=add&id=1">
                                <asp:Image border="0" hspace="2" ImageAlign="AbsMiddle" Style="cursor: hand" runat="server"
                                    ID="image1" ImageUrl="~/Assets/images/create.gif" />
                                新增入库
                            </asp:HyperLink>
                            <%--<input id="ToOrder" type="button" value="新增入库" onclick="ToOrder()" runat="server" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td style="color: Red">
                            <strong>未填编号条目：<asp:Label ID="num" runat="server"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblSPZT" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            名称：
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            规格或参数：
                            <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp; 类型1：
                            <asp:TextBox ID="txtType" runat="server" Text=""></asp:TextBox>&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                            <td>
                                序号
                            </td>
                            <td>
                                入库单号
                            </td>
                            <td>
                                固定资产编号
                            </td>
                            <td>
                                名称
                            </td>
                            <td>
                                类型1
                            </td>
                            <td>
                                类型2
                            </td>
                            <td>
                                规格或参数
                            </td>
                            <%--<td>
                                        入库数量
                                    </td>--%>
                            <td>
                                使用人
                            </td>
                            <td>
                                使用部门
                            </td>
                            <td>
                                地点
                            </td>
                            <td>
                                收料日期
                            </td>
                            <td>
                                制单人
                            </td>
                            <td>
                                备注
                            </td>
                            <%-- <td>
                                        修改
                                    </td>--%>
                            <td>
                                添加编号
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                            <td>
                                <%#Container.ItemIndex+1%>
                            </td>
                            <td>
                                <asp:Label ID="lblInCode" runat="server" Text='<%#Eval("INCODE")%>'></asp:Label>
                                <input type="hidden" runat="server" id="INCODE" name="INCODE" value='<%#Eval("INCODE")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblb_bh" runat="server" Text='<%#Eval("BIANHAO") %>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("NAME")%>
                            </td>
                            <td>
                                <%#Eval("TYPE")%>
                            </td>
                             <td>
                                <%#Eval("TYPE2")%>
                            </td>
                            <td>
                                <%#Eval("MODEL")%>
                            </td>
                            <%--<td>
                                        <%#Eval("INNUM")%>
                                    </td>--%>
                            <td>
                                <%#Eval("SYR")%>
                            </td>
                            <td>
                                <%#Eval("SYBUMEN")%>
                            </td>
                            <td>
                                <%#Eval("PLACE") %>
                            </td>
                            <td>
                                <asp:Label ID="lblInDate" runat="server" Text='<%#Eval("INDATE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInDoc" runat="server" Text='<%#Eval("CREATER")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("NOTE")%>
                            </td>
                            <%-- <td>
                                        <asp:HyperLink ID="link_xiugai" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_GdzcOrderDetail.aspx?FLAG=mod&id="+Eval("INCODE") %>'></asp:HyperLink>
                                    </td>--%>
                            <td>
                                <asp:HyperLink ID="link_bh" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_GdzcOrderDetail.aspx?FLAG=addbh&id="+Eval("INCODE") %>'>
                                    <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                    添加编号</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                没有相关入库信息!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
