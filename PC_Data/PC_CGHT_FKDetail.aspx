<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHT_FKDetail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHT_FKDetail" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    付款明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        //查看要款记录
        function BPViewDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("PC_CGHT_Payment.aspx?Action=View&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");

        }

        //修改要款-财务确认
        function BPEditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("PC_CGHT_Payment.aspx?Action=Edit&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
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
                            市场部合同号：<asp:TextBox ID="scbConId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            款项名称：
                            <asp:DropDownList runat="server" ID="ddrKxmc" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="预付款" Value="预付款"></asp:ListItem>
                                <asp:ListItem Text="进度款" Value="进度款"></asp:ListItem>
                                <asp:ListItem Text="发货款" Value="发货款"></asp:ListItem>
                                <asp:ListItem Text="质保金" Value="质保金"></asp:ListItem>
                            </asp:DropDownList>
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
                        <asp:BoundField DataField="BP_ID" HeaderText="付款单号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_HTBH" HeaderText="合同号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HT_GF" HeaderText="合同供方">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_SCBHTH" HeaderText="市场部合同号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_SHEBEI" HeaderText="设备名称">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HT_HTZJ" HeaderText="合同额">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_JE" HeaderText="付款金额（万元）">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_ZFBL" HeaderText="付款比例">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_YKRQ" HeaderText="付款日期" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style="width: 200px">
                                    <label>
                                        <%# Eval("BP_NOTE")%></label></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <asp:HyperLink ID="hylfp" runat="server" CssClass="hand" onClick="BPEditDetail(this);"
                                    ToolTip='<%# Eval("BP_ID")%>'>
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                    编辑</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("BP_ID") %>'
                                    OnClick="Lbtn_Del_OnClick" OnClientClick="javascript:return confirm('确定要删除吗？');">
                                    <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" runat="server" />删除
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
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
