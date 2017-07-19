<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadAttachments.ascx.cs"
    Inherits="ZCZJ_DPF.Controls.UploadAttachments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Assets/main.css" rel="stylesheet" type="text/css" />
<link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
<table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
    <tr align="center">
        <td colspan="2" class="right_bg" style="text-align: center">
            附件信息
        </td>
    </tr>
    <tr id="tr_WJMS" runat="server">
        <td style="width: 65px" class="r_bg">
            文件描述:
        </td>
        <td class="right_bg">
            <asp:TextBox ID="txtDesc" runat="server" Rows="3" TextMode="MultiLine" Width="500px"></asp:TextBox>
        </td>
    </tr>
    <tr id="tr_Upload" runat="server">
        <td style="width: 65px" class="r_bg">
            上传文件
        </td>
        <td class="right_bg">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="70%" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpload" runat="server" Text="  上 传  " OnClick="btnUpload_Click" />
        </td>
    </tr>
    <tr id="tr_content" runat="server">
        <td style="width: 65px" class="r_bg">
            文件内容
        </td>
        <td style="text-align: center;" class="right_bg">
            <asp:Label ID="Lbl_remind" runat="server" Text="该文件不存在,请管理员检查或重新上传！" Visible="false"
                ForeColor="Red"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowCommand="GridView1_RowCommand"
                Style="white-space: normal" CssClass="toptable grid" AutoGenerateColumns="False"
                CellPadding="4" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                ForeColor="#333333" EmptyDataText="没有文件！" EmptyDataRowStyle-ForeColor="Red">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle CssClass="bomcolor" ForeColor="#2461BF" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("AT_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AT_NAME" HeaderText="文件名" ItemStyle-Wrap="true" ItemStyle-Width="300px" />
                    <asp:BoundField DataField="AT_DESCRIBE" HeaderText="描述" />
                    <%--<asp:BoundField DataField="remark" HeaderText="备注" />--%>
                    <asp:TemplateField HeaderText="下载">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnattachview" class="link" runat="server" CommandName="attachview">
                                <asp:Image ID="Img_download" runat="server" ImageUrl="~/Assets/images/download.jpg"
                                    Width="20px" Height="20px" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblattachdel" class="link" runat="server" CommandName="attachdel"
                                OnClientClick="return confirm(&quot;是否删除该附件？&quot;)">
                                <asp:Image ID="Img_delete" runat="server" ImageUrl="~/Assets/images/erase.gif" Width="20px"
                                    Height="20px" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
