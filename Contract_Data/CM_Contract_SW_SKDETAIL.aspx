<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Contract_SW_SKDETAIL.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Contract_SW_SKDETAIL" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    收款明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        //查看要款记录
        function BPViewDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=View&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");

        }

        //修改要款-财务确认
        function BPEditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=Edit&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
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
                            对方合同号：<asp:TextBox ID="txtDuifangConId" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            项目名称：<asp:TextBox ID="txtProj" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            款项名称：
                            <asp:DropDownList runat="server" ID="ddrKxmc" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="预付款" Value="预付款"></asp:ListItem>
                                <asp:ListItem Text="进度款" Value="进度款"></asp:ListItem>
                                <asp:ListItem Text="发货款" Value="发货款"></asp:ListItem>
                                <asp:ListItem Text="调试款" Value="调试款"></asp:ListItem>
                                <asp:ListItem Text="质保金" Value="质保金"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            收款状态：
                            <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="已到帐" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未到账" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                        
                           <td>
                            排列顺序：
                            <asp:DropDownList runat="server" ID="ddlPailie" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click">
                               
                                <asp:ListItem Text="合同号" Value="BP_HTBH"></asp:ListItem>
                                <asp:ListItem Text="收款日期" Value="BP_YKRQ"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查 询" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导 出" />
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
                           <asp:BoundField DataField="BP_ID" HeaderText="收款单号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PCON_CUSTMNAME" HeaderText="订货单位">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BP_HTBH" HeaderText="合同号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PCON_YZHTH" HeaderText="对方合同号">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                           <asp:BoundField DataField="PCON_ENGNAME" HeaderText="项目名称">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                              <asp:BoundField DataField="PCON_ENGTYPE" HeaderText="设备名称">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="PCON_JINE" HeaderText="合同额">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="BP_JE" HeaderText="收款金额（万元）">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>      
                        <asp:BoundField DataField="BP_YKRQ" HeaderText="收款日期" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>            
                        <asp:TemplateField HeaderText="收款比例">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="BP_NOTEFST" Text='<%#Eval("BP_NOTEFST")+"%" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                           <asp:BoundField DataField="BP_KXMC" HeaderText="款项类型">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="收款状态">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblstate" Text='<%#Eval("BP_STATE").ToString()=="0"?"未到账":"已到账" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContract" runat="server" CssClass="hand" onClick="BPViewDetail(this);"
                                    ToolTip='<%# Eval("BP_ID")%>'>
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                    查看</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyly1k" runat="server" CssClass="hand" onClick="BPEditDetail(this);"
                                    ToolTip='<%# Eval("BP_ID")%>'>
                                    <asp:Image ID="Image15" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                    编辑</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
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
