<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Date_closemarshow.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_Date_closemarshow" MasterPageFile="~/Masters/PopupBase.Master"
    Title="行关闭物料查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="JavaScript" type="text/javascript">
        function openclosemodewin() {
            var orderno = "<%=globlempcode%>";
            var autonum = Math.round(10000 * Math.random());
            window.open("PC_TBPC_Marreplace_edit.aspx?autonum=" + autonum + "&mpcode=" + escape(orderno) + "");
        }
        function openclosemodewin1() {
            var orderno = "<%=globlempcode%>";
            var autonum = Math.round(10000 * Math.random());
            window.open("PC_TBPC_marstouseallGB.aspx?autonum=" + autonum + "&pcode=" + escape(orderno) + "");
        }
        function openclosemodewin2() {
            var orderno = "<%=globlempcode%>";
            var autonum = Math.round(10000 * Math.random());
            window.open("PC_TBPC_marstouseall.aspx?autonum=" + autonum + "&pcode=" + escape(orderno) + "");
        }  
    
    </script>

    <div style="border: 1px solid #000000; overflow: auto;">
        <div>
            <table style="border: 0px; border-style: ridge;" align="center" width="100%">
                <tr>
                    <td align="center" colspan="3">
                        <span style="color: Red">行关闭物料查看</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                            Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                        <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                        <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                    </td>
                    <td align="center">
                        注：红色表示为意外关闭，不用进行占用代用处理
                    </td>
                    <td align="right">
                        批号：<asp:Label ID="orderno" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="tb_shape" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="toptable grid" CellPadding="4" ForeColor="#333333" DataKeyNames="ptcode"
                OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                Width="100%" EmptyDataText="没有行关闭了的物料" PageSize="500">
                <PagerTemplate>
                    <table width="100%" style="border: 0px; border-style: ridge;" align="center">
                        <tr>
                            <td style="border-bottom-style: ridge; width: 100%; text-align: center">
                                <asp:Label ID="lblCurrrentPage" runat="server" ForeColor="#CC3300"></asp:Label>
                                <span>跳转至</span>
                                <asp:DropDownList ID="page_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="page_DropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span>页</span>
                                <asp:LinkButton ID="lnkBtnFirst" CommandArgument="First" CommandName="page" runat="server">第一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnPrev" CommandArgument="prev" CommandName="page" runat="server">上一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnNext" CommandArgument="Next" CommandName="page" runat="server">下一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnLast" CommandArgument="Last" CommandName="page" runat="server">最后一页</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="ptcode" runat="server" Text='<%# Eval("ptcode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行关闭备注" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="PUR_ZYDY" runat="server" Text='<%# Eval("PUR_ZYDY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="marid" runat="server" Text='<%# Eval("marid") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField DataField="marnm" HeaderText="名称" SortExpression="marnm" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="margg" HeaderText="规格" SortExpression="margg" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="marcz" HeaderText="材质" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="margb" HeaderText="国标" SortExpression="margb" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="长度" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="length" runat="server" Text='<%# Eval("length") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽度" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="width" runat="server" Text='<%# Eval("width") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField DataField="cgrnm" HeaderText="采购人" SortExpression="cgrnm" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="需用数量" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="num" runat="server" Text='<%# Eval("num") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField DataField="marunit" HeaderText="单位" SortExpression="marunit" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="需用辅助数量" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="fznum" runat="server" Text='<%# Eval("fznum") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField DataField="marfzunit" HeaderText="辅助单位" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PUR_TUHAO" HeaderText="图号/标识号" SortExpression="PUR_TUHAO"
                        ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="状态" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="purstate" runat="server" Text='<%# Eval("purstate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="关闭状态" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="PUR_CSTATE" runat="server" Text='<%# Eval("PUR_CSTATE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click"
                            Visible="false" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_ZY" runat="server" Text="占用库存" OnClick="btn_ZY_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_XSDY" runat="server" Text="相似代用" OnClick="btn_XSDY_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_FClose" runat="server" Text="(合并)反关闭" OnClick="btn_FClose_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_DTFclose" runat="server" Text="单独反关闭" OnClick="btn_DTFclose_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        注：“(合并)反关闭”为正常的反关闭！<br />
                        “单独反关闭”只用于拆分之后，一条记录已经下推比价单或者订单的反关闭！
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
