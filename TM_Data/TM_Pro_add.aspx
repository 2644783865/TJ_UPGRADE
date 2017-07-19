<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Pro_add.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Pro_add" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    添加产品模板信息</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

<script language="javascript" type="text/javascript">
     function chkadd()
    {
        var num=document.getElementById("<%=txtnum.ClientID%>").value;
        if(num!="")
        {
            document.getElementById("<%=add_id.ClientID%>").value="1";   
        }
        else
        {
            document.getElementById("<%=add_id.ClientID%>").value="0";
            alert('请输入行数！');  
        }
    }
</script>
    
     <div class="box-inner">
   <div class="box_right">
    <div class="box-title">
      
        <table width="100%">
        <tr>
            <td><b>增加产品</b></td>
            <td>增加行数:<input ID="txtnum" type="text" value="" runat="server" style="width:80px"/>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="确 定" OnClientClick="chkadd()" onclick="btnAdd_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="保 存" onclick="btnSave_Click" />
            </td>
            <td align="right">
                <asp:Button ID="btnReturn" runat="server" Text="返 回" onclick="btnReturn_Click" />&nbsp;&nbsp;
            </td>
         </tr>
        </table>
       </div>
     </div>
</div>
    
    <div class="box-wrapper">
        <div class="box-outer">
            <%--<table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">--%>
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" DataKeyNames="PDS_ID"
                    onrowdeleting="GridView1_RowDeleting">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PDS_ID") %>'>&gt;</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                 <asp:TextBox ID="txt_name" runat="server" Text='<%# Bind("PDS_NAME") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="英文名称">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_en_name" runat="server" Text='<%# Bind("PDS_ENGNAME") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="工程名称">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_eng_name" runat="server" Text='<%# Bind("PDS_ENGNAME") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工程类型">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_eng_type" runat="server" Text='<%# Bind("PDS_ENGTYPE") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_code" runat="server" Text='<%# Bind("PDS_CODE") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="上级ID">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_father_id" runat="server" Text='<%# Bind("PDS_FATHERID") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否有节点">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_node" runat="server" Text='<%# Bind("PDS_ISYENODE") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_note" runat="server" Text='<%# Bind("PDS_NOTE") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnDelete" runat="server" class="link" CausesValidation="False" CommandName="Delete"
                                     onclientclick="return confirm(&quot;是否删除该记录？&quot;)">
                                <asp:Image ID="DeImage" ImageUrl="~/Assets/images/no.gif" border="0" hspace="2" runat="server" />删除
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    没有数据，请添加产品数据！</asp:Panel>
            <%--</table>--%>
            <input id="add_id" type="text"  runat="server" readonly="readonly" style="display: none" />
        </div>
    </div>
</asp:Content>
