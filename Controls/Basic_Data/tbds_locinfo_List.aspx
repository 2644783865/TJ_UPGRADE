<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="tbds_locinfo_List.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbds_locinfo_List" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    基础数据</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function Add() {
            var ddl = $("#<%=selectaddgroupd.ClientID%> option:selected").val();
            if (ddl != "0") {
                window.showModalDialog('tbds_locinfo_detail.aspx?action=add&selectaddgroup=' + ddl, '', 'dialogWidth=650px;dialogHeight=400px', true);
            }
        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <td align="right" style="width: 5%">
                            查看：
                        </td>
                        <td align="left" style="width: 20%">
                            一级地区：
                            <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                <%--<asp:ListItem Selected="True">-请选择-</asp:ListItem>
                            <asp:ListItem Text="地区编号" Value="CL_CODE"></asp:ListItem>
                            <asp:ListItem Text="地区名称" Value="CL_NAME"></asp:ListItem>
                            <asp:ListItem Text="维护人" Value="CL_MANCLERK"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSearchchild" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSearchchild_SelectedIndexChanged">
                                <%--<asp:ListItem Selected="True">-请选择-</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <%--<td>按类查询：
                    <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" 
                       onselectedindexchanged="ddlSearch_SelectedIndexChanged" >
                       <asp:ListItem Text="请选择查询类别" Value="CL_NAME"></asp:ListItem>
                        <asp:ListItem Text="地区名称" Value="CL_NAME"></asp:ListItem>
                        <asp:ListItem Text="填写时间" Value="CL_FILLDATE"></asp:ListItem>
                        <asp:ListItem Text="地区编号" Value="CL_CODE"></asp:ListItem>
                        <asp:ListItem Text="维护人" Value="CL_MANCLERK"></asp:ListItem>
                        <asp:ListItem Text="备注" Value="CL_NOTE"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click" />
                </td>--%>
                        <td align="right">
                            添加：
                            <asp:DropDownList ID="selectaddgroupd" runat="server" CausesValidation="false" onchange="Add()">
                                <asp:ListItem Text="--请选择--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="一级地区" Value="1"></asp:ListItem>
                                <asp:ListItem Text="二级地区" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <asp:Repeater ID="Reproject1" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle">
                            <td style="width: 5%">
                                <strong>序号</strong>
                            </td>
                            <td style="width: 5%">
                                <strong>地区编号</strong>
                            </td>
                            <td style="width: 15%">
                                <strong>地区名称</strong>
                            </td>
                            <td style="width: 15%">
                                <strong>维护人</strong>
                            </td>
                            <td style="width: 20%">
                                <strong>填写日期</strong>
                            </td>
                            <td style="width: 20%">
                                <strong>备注</strong>
                            </td>
                            <td style="width: 10%">
                                <strong>修改</strong>
                            </td>
                            <td style="width: 10%">
                                <strong>删除</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center" class="baseGadget" onmouseover="this.className='highlight'" onclick="this.className='clickback'"
                            onmouseout="this.className='baseGadget'">
                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#Eval("CL_CODE")%>'></asp:Label>
                            <td>
                                <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                            </td>
                            <td>
                                <%#Eval("CL_CODE")%>&nbsp;
                            </td>
                            <td>
                                <%#Eval("CL_NAME")%>&nbsp;
                            </td>
                            <td>
                                <%#Eval("CL_MANCLERK")%>&nbsp;
                            </td>
                            <td>
                                <%#Eval("CL_FILLDATE","{0:D}")%>&nbsp;
                            </td>
                            <td>
                                <%#Eval("CL_NOTE")%>&nbsp;
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# editDq(Eval("CL_CODE").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    修改</asp:HyperLink>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDel" CssClass="checkBoxCss" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="NoDataPanel" runat="server">
                    没有记录!
                </asp:Panel>
            </table>
            <div class="PageChange">
                <asp:Button ID="btnDelete" runat="server" Text="  删 除  " OnClick="btnDelete_Click" />
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
