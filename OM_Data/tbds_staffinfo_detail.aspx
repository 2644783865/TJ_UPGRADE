<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbds_staffinfo_detail.aspx.cs" Inherits="testpage.information"
    Title="员工档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta HTTP-EQUIV="Content-Type" content="text/html; charset=gb2312">
 <meta http-equiv="Content-Language" content="zh-cn">
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td>
                    信息查看
                </td>
                <td style="width: 190px; float: right">
                    <asp:Label ID="Label1" runat="server" Text="按部门查看："></asp:Label>
                    <asp:DropDownList ID="DDlpartment" runat="server" AutoPostBack="True" Width="100px"
                        OnSelectedIndexChanged="DDlpartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 190px">
                    <asp:Label ID="group" runat="server" Text="班组/岗位:" Visible="False"></asp:Label>
                    <asp:DropDownList ID="DDlgroup" runat="server" AutoPostBack="True" Width="100px"
                        OnSelectedIndexChanged="DDlgroup_SelectedIndexChanged" Visible="False">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="label2" runat="server" Text="按姓名查看："></asp:Label>
                    <asp:TextBox ID="name" runat="server"></asp:TextBox>
                    <asp:Button ID="search" runat="server" Text="查看" OnClick="search_Click" />
                </td>
                <td>
                <asp:Button ID="Refresh" runat="server" Text="刷新" OnClick="refresh_Click" />
                </td>
                <td align="right">
                    <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:window:showModalDialog('tbds_staffinfo_operate.aspx?action=add','','dialogWidth=800px;dialogHeight=700px');"
                        runat="server">添加员工</asp:HyperLink>
                </td>
            </tr>
        </table>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1" style="cursor: pointer">
                            <asp:Repeater ID="tbds_staffinfoRepeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle">
                                        <td style="width: 100px">
                                            <strong>序号</strong>
                                        </td>
                                        <td style="width: 100px">
                                            <strong>姓名</strong>
                                        </td>
                                        <td style="width: 100px">
                                            <strong>所属部门</strong>
                                        </td>
                                        <td style="width: 280px">
                                            <strong>职位</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td style="width: 80px">
                                            <strong>修改</strong>
                                        </td>
                                        <td style="width: 50px">
                                            <strong>删除</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                        ondblclick="<%# showYg(Eval("ST_ID").ToString()) %>" title="双击查看详情">
                                        <asp:Label ID="ST_CODE" runat="server" Visible="false" Text='<%#Eval("ST_ID")%>'></asp:Label>
                                        <td>
                                            <%#Eval("ID_Num")%>&nbsp;
                                        </td>
                                        <td>
                                            <%#Eval("ST_NAME")%>&nbsp;
                                        </td>
                                        <td>
                                            <%#Eval("DEP_NAME")%>&nbsp;
                                        </td>
                                        <td>
                                            <%#Eval("ST_POSITION") %>&nbsp;
                                        </td>
                                        <td style="display: none">
                                            <asp:Label ID="lbstate" runat="server" Text='<%#Eval("ST_STATE")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <%#Eval("ST_NOTE")%>
                                            &nbsp;
                                        </td>
                                        <td title="点击修改">
                                            <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# editYg(Eval("ST_ID").ToString()) %>'
                                                runat="server">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="checkboxstaff" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                            </asp:CheckBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="NoDataPane" runat="server">
                            </asp:Panel>
                        </table>
                        <div class="PageChange">
                            <asp:Button ID="deletebt" runat="server" Text="删除" OnClick="deletebt_Click" />
                        </div>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                        <div align="center">
                            <asp:Label ID="Lnumber" runat="server" Text=""></asp:Label></div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DDlpartment" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DDlgroup" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="Refresh" EventName="Click"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
