<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="tbds_depinfo_detail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbds_depinfo_detail"
    Title="无标题页" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    组织结构管理 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                             <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;按部门查看
                                <asp:DropDownList ID="Dfinddept1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Dfinddept1_SelectedIndexChanged"
                                    Width="80px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="Radiogrouportw" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                    TextAlign="Left" OnSelectedIndexChanged="Radiogrouportw_SelectedIndexChanged"
                                    Visible="False">
                                    <asp:ListItem Text="班组/岗位" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="工种" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 230px">
                                <%--岗位/班组--%>
                                <asp:DropDownList ID="Dfinddept2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Dfinddept2_SelectedIndexChanged"
                                    Width="80px" Visible="False">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="HyperLink3" runat="server">添加</asp:HyperLink>
                                <asp:DropDownList ID="selectaddgroupd" runat="server" CausesValidation="false" OnSelectedIndexChanged="selectaddgroup_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="一级" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="二级" Value="2"></asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1">
                    <asp:Repeater ID="tbds_depinfoRepeater" runat="server">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle">
                                <td style="width: 150px">
                                    <strong>编码</strong>
                                </td>
                                <td style="width: 150px">
                                    <strong>名称</strong>
                                </td>
                                <%-- <td>
                                    <strong>上级部门ID</strong>
                                </td>
                                <td>
                                    <strong>是否叶节点</strong>
                                </td>
                                <td>
                                    <strong>填写日期</strong>
                                </td>
                                <td>
                                    <strong>维护人</strong>
                                </td>--%>
                                <td>
                                    <strong>备注</strong>
                                </td>
                                <td style="width: 100px">
                                    <strong>修改</strong>
                                </td>
                               <%-- <td style="width: 50px">
                                    <strong>删除</strong>
                                </td>--%>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                ondblclick="<%# showBm(Eval("DEP_CODE").ToString()) %>">
                                <asp:Label ID="DEP_CODE" runat="server" Visible="false" Text='<%#Eval("DEP_CODE")%>'></asp:Label>
                                <td>
                                    <%#Eval("DEP_CODE")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("DEP_NAME")%>&nbsp;
                                </td>
                                <%-- <td>
                                    <%#Eval("DEP_FATHERID")%>
                                    &nbsp;
                                </td>
                                <td>
                                    <%#Eval("DEP_ISYENODE")%>
                                    &nbsp;
                                </td>
                                <td>
                                    <%#Eval("DEP_FILLDATE")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("DEP_MANCLERK")%>
                                    &nbsp;
                                </td>--%>
                                <td>
                                    <%#Eval("DEP_NOTE")%>&nbsp;
                                </td>
                                <td>
                                    <asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"tbds_depinfo_operate.aspx?action=update&DEP_CODE="+Eval("DEP_CODE") %>'
                                        runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />修改</asp:HyperLink>
                                </td>
                                <%--<td>
                                    <asp:CheckBox ID="checkboxdep" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                    </asp:CheckBox>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="NoDataPane" runat="server">
                    </asp:Panel>
                </table>
                <%--<div class="PageChange">
                    <asp:Button ID="deletebt" runat="server" Text="删除" OnClick="deletebt_Click" />
                </div>--%>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
                <div align="center">
                    <asp:Label ID="Lnumber" runat="server" Text=""></asp:Label></div>
            </div>
        </div>
    </div>
</asp:Content>
