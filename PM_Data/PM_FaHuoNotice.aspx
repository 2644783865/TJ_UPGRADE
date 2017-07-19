<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_FaHuoNotice.aspx.cs"
    Inherits="ZCZJ_DPF.PM_Data.PM_FaHuoNotice" MasterPageFile="~/Masters/RightCotentMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    发货通知
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        ul
        {
            list-style: none;
        }
        li
        {
            float: left;
        }
    </style>

    <script type="text/javascript">
        function lbtnSolve_onclick() {
            if (confirm("处理之前请先填写发货时间！！！您确定要处理吗？处理后此发货通知将变为“已发货状态”！！！")) {
                return true;
            }
            else {
                return false;
            }
        }

        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlBz.ClientID%>');
            var acNameClientId = "<%=acName.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <table>
        <tr>
            <td>
                <asp:RadioButtonList runat="server" ID="rblFHZT" AutoPostBack="true" OnSelectedIndexChanged="Query"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Text="全部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="未发货" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="已发货" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right" width="100px">
                搜索条件：
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlBz" runat="server">
                    <asp:ListItem Value="CM_CUSNAME">顾客名称</asp:ListItem>
                    <asp:ListItem Value="CM_PROJ">项目名称</asp:ListItem>
                    <asp:ListItem Value="CM_CONTR">合同号</asp:ListItem>
                    <asp:ListItem Value="TSA_ENGNAME">交货内容</asp:ListItem>
                    <asp:ListItem Value="TSA_MAP">图号</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 42px" align="left" width="300px">
                <asp:TextBox ID="txtBox" runat="server" onkeydown="return OnTxtPersonInfoKeyDown();"></asp:TextBox>
                <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="txtBox" ServicePath="../CM_Data/CM_Customer.asmx"
                    ServiceMethod="GetNotice" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                    EnableCaching="false">
                </asp:AutoCompleteExtender>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="overflow: scroll;">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1" style="cursor: pointer">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <td width="50px">
                                    <strong>序号</strong>
                                </td>
                                <td>
                                    <strong>编号</strong>
                                </td>
                                <td>
                                    <strong>顾客名称</strong>
                                </td>
                                <td>
                                    <strong>项目名称</strong>
                                </td>
                                <%--<td>
                                        <strong>任务号</strong>
                                        </td>--%>
                                <td>
                                    <strong>合同号</strong>
                                </td>
                                <td>
                                    <strong>交货内容</strong>
                                </td>
                                <td>
                                    <strong>图号</strong>
                                </td>
                                <td>
                                    <strong>收货单位</strong>
                                </td>
                                <td>
                                    <strong>交（提）货地点</strong>
                                </td>
                                <td>
                                    <strong>联系人</strong>
                                </td>
                                <td>
                                    <strong>联系方式</strong>
                                </td>
                                <td>
                                    <strong>要求发货时间</strong>
                                </td>
                                <td>
                                    <strong>制单时间</strong>
                                </td>
                                <td>
                                    <strong>经办人</strong>
                                </td>
                                <td>
                                    <strong>查看</strong>
                                </td>
                                <td>
                                    <strong>发货时间</strong>
                                </td>
                                <td>
                                    <strong>处理</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="CM_FID" runat="server" Visible="false" Text='<%#Eval("CM_FID")%>'></asp:Label>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#Eval("ID_Num") %>'></asp:Label>
                                    <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                    <asp:HiddenField runat="server" ID="CM_BIANHAO" Value='<%#Eval("CM_BIANHAO")%>' />
                                    <asp:HiddenField runat="server" ID="CM_ID" Value='<%#Eval("CM_ID")%>' />
                                    <asp:HiddenField runat="server" ID="CM_FHZT" Value='<%#Eval("CM_FHZT")%>' />
                                </td>
                                <td>
                                    <%# Eval("CM_BIANHAO")%>
                                </td>
                                <td>
                                    <%#Eval("CM_CUSNAME")%>
                                </td>
                                <td>
                                    <%#Eval("CM_PROJ")%>
                                </td>
                                <%--<td>
                                             <%#Eval("TSA_ID")%>
                                        </td>--%>
                                <td>
                                    <%#Eval("CM_CONTR")%>
                                </td>
                                <td>
                                    <%#Eval("TSA_ENGNAME")%>
                                </td>
                                <td>
                                    <%#Eval("TSA_MAP")%>
                                </td>
                                <td>
                                    <%#Eval("CM_SH")%>
                                </td>
                                <td>
                                    <div style="width: 150px; white-space: normal">
                                        <asp:Label runat="server" ID="CM_JH" Text='<%#Eval("CM_JH")%>'></asp:Label>
                                    </div>
                                </td>
                                <td>
                                    <%#Eval("CM_LXR")%>
                                </td>
                                <td>
                                    <%#Eval("CM_LXFS")%>
                                </td>
                                <td>
                                    <%#Eval("CM_JHTIME")%>
                                </td>
                                <td>
                                    <%#Eval("CM_ZDTIME")%>
                                </td>
                                <td>
                                    <%#Eval("MANCLERK")%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hyperlink2" runat="server" CssClass="link" NavigateUrl='<%#"../CM_Data/CM_FHNotice.aspx?action=look&id="+Eval("CM_FID") %>'
                                        Target="_blank">
                                        <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                        查看
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CM_FHSJ" class="easyui-datebox" onfocus="this.blur()"
                                        Text='<%#Eval("CM_FHSJ")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lbtnSolve" CommandArgument='<%#Eval("CM_ID") %>'
                                        OnClientClick="return lbtnSolve_onclick()" OnClick="lbtnSolve_OnClick">
                                        <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                        处理
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
                <br />
            </div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
