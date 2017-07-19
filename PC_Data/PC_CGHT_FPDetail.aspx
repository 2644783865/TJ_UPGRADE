<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHT_FPDetail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHT_FPDetail" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    查看发票明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        //修改发票
        function BREditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("PC_CGHT_Bill.aspx?Action=Edit&BRid=" + ID + "&NoUse=" + time, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
            if (sRet == "refresh") {
                //window.history.go(0); 
            }
        }

        //查看发票
        function BRViewDetail(i) {
            var ID = i.title;
            var obj = new Object();

            window.showModalDialog("PC_CGHT_Bill.aspx?Action=View&BRid=" + ID, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div style="height: 40px">
                <table>
                    <tr>
                        <td>
                            合同号：<asp:TextBox ID="txtConId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            合同供方：<asp:TextBox ID="txtGF" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            对方合同号：<asp:TextBox ID="txtDFConId" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            产品名称：<asp:TextBox ID="txtEngName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            备注：<asp:TextBox ID="txtBeizhu" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查 询" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%;">
            <div style="width: 100%; overflow: scroll">
                <asp:GridView ID="grvYKJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" ShowFooter="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BR_HTBH" HeaderText="合同编号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="HT_GF" HeaderText="合同供方">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="HT_GFHTBH" HeaderText="对方编号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BR_KPRQ" HeaderText="开票日期" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BR_KPJE" HeaderText="开票金额" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BR_ENGNAME" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BR_FPDH" HeaderText="发票单号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BR_JBR" HeaderText="经办人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BR_SL" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="BR_BZ" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylfp" runat="server" CssClass="hand" onClick="BREditDetail(this);"
                                    ToolTip='<%# Eval("BR_ID")%>'>
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                    编辑</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylpz" runat="server" CssClass="hand" onClick="BRViewDetail(this);"
                                    ToolTip='<%# Eval("BR_ID")%>'>
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                    查看</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkdel_FP" runat="server" ForeColor="Red" CommandArgument='<%# Eval("BR_ID")%>'
                                 OnClick="Lbtn_Del_OnClick"    OnClientClick="return confirm('确定要删除此记录吗？？？');">
                    删除</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" HorizontalAlign="Center" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
            </div>
        </div>
        <div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
