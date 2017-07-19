<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_FGDZCZY_SP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FGDZCZY_SP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    非固定资产转移审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=DH1]").val();
                window.open("OM_FGdzcTrans.aspx?action=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="我的审批任务" Value="1" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            名称：
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            规格或参数：
                            <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_OnClick" />
                        </td>
                        <%--<td><asp:Button ID="btnAdd" runat="server" Text="添加申请" OnClick="btnAdd_click" /></td>--%>
                        <%--<td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
                                </td>--%>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto">
                <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    style="white-space: normal" border="1">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                <td>
                                    <strong>序号</strong>
                                </td>
                                <td>
                                    <strong>转移单号</strong>
                                </td>
                                <td>
                                    <strong>编号</strong>
                                </td>
                                <td>
                                    <strong>名称</strong>
                                </td>
                                <td>
                                    <strong>规格或参数</strong>
                                </td>
                                <td>
                                    <strong>前使用人</strong>
                                </td>
                                <td>
                                    <strong>前使用部门</strong>
                                </td>
                                <td>
                                    <strong>前使用时间</strong>
                                </td>
                                <td>
                                    <strong>前存放地点</strong>
                                </td>
                                <td>
                                    <strong>现使用人</strong>
                                </td>
                                <td>
                                    <strong>现使用部门</strong>
                                </td>
                                <td>
                                    <strong>现使用时间</strong>
                                </td>
                                <td>
                                    <strong>现存放地点</strong>
                                </td>
                                <td>
                                    <strong>经办人</strong>
                                </td>
                                <td style="width: 100px">
                                    <strong>转移原因</strong>
                                </td>
                                <td>
                                    <strong>审批</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                <td>
                                    <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="DH" Text='<%#Eval("DH")%>'></asp:Label>
                                    <input type="hidden" runat="server" id="DH1" name="DH1" value='<%#Eval("DH")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                        ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                        ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                        ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                        ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink ID="link_bh" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_FGdzcTrans.aspx?action=check&id="+Eval("DH") %>'>
                                        <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                        审批</asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server">
                    没有相关物料信息!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
