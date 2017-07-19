<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="CB_FYFT.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CB_FYFT" Title="直接人工费分摊" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
直接人工费分摊
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100%;">
                                    <strong>当前年月：</strong><asp:Label ID="lbdate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%;">
                                    <strong>厂内机加费用：</strong><asp:TextBox ID="txtcnjj" ForeColor="Gray" runat="server" onfocus="DefaultTextOnFocus(this);"
                                        onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <strong>厂内结构及喷漆喷砂分摊费用：</strong><asp:TextBox ID="txtjgyzft" ForeColor="Gray" runat="server" onfocus="DefaultTextOnFocus(this);"
                                        onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                                    <%--<strong>工资：</strong><asp:TextBox ID="txtgz" ForeColor="Gray" runat="server" onfocus="DefaultTextOnFocus(this);"
                                        onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>--%>
                                        &nbsp;&nbsp;
                                    <asp:Button ID="btnQuery" runat="server" Text="计算" OnClick="btnQuery_Click"></asp:Button>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" Text="保存到当月" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_export" runat="server" Text="导出数据" OnClick="btn_export_Click" />
                                </td>
                                <td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="width: 100%; height: auto; overflow: scroll;
                    display: block;">
                   <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                                border="1">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                                <tr align="center">
                                    <th align="center">
                                        序号
                                    </th>
                                    <th align="center">
                                        任务号
                                    </th>
                                    <th align="center">
                                        工时费用
                                    </th>
                                    <th align="center">
                                        机加分配率
                                    </th>
                                    <th align="center">
                                        厂内机加费用
                                    </th>
                                    <th align="center">
                                        厂内结构及喷漆喷砂
                                    </th>
                                    <th align="center">
                                        厂内结构及喷漆喷砂分配率
                                    </th>
                                    <th align="center">
                                        厂内结构及喷漆喷砂分摊
                                    </th>
                                    <%--<th align="center">
                                        工资分配率
                                    </th>
                                    <th align="center">
                                        工资
                                    </th>--%>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="cbxSelect"
                                        runat="server" Visible="false" Checked="true" />
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbid" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_TSAID")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbgsfy" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_GSFY")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbjjfpl" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_JJFPL")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbcnjj" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_CNJJ")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbjgyz" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_JGYZ")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbjgyzfpl" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_JGYZFPL")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbjgyzft" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_JGYZFT")%>'></asp:TextBox>
                                </td>
                                <%--<td runat="server" align="center">
                                    <asp:TextBox ID="tbgzfpl" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_GZFPL")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbgz" runat="server" Width="80px" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("CB_GZ")%>'></asp:TextBox>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>

                                </td>
                                <td align="center">
                                    合计:
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbgsfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjjfplhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbcnjjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjgyzhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjgyzfplhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjgyzfthj" runat="server"></asp:Label>
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbgzfplhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbgzhj" runat="server"></asp:Label>
                                </td>--%>
                            </tr>
                            </FooterTemplate>
                    </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
