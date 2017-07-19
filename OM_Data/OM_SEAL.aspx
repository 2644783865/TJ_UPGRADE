<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_SEAL.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.OM_Date.OM_SEAL" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %> 
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

 <asp:Content ID="Content1"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
     印章管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script language="javascript" type="text/javascript">
    function add()
    {
        var sRet = window.showModalDialog('OM_SEAL_Detail.aspx?action=add', 'obj', 'dialogWidth=900px;dialogHeight=600px');
        if (sRet == "refresh") 
        {
            window.location.href = window.location.href;
        }
    }
</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
      <div class="RightContent"> 
      <div class="box_right">
      <div class='box-title'> 
            <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
            <table width="100%">
            <tr>
                <td style="width:60px">审批状态:</td>
                <td align="center" style="width: 311px">
                    <asp:RadioButtonList ID="rbl_tcstate" runat="server" RepeatDirection="Horizontal" 
                         AutoPostBack="true" OnSelectedIndexChanged="tcstate_OnSelectedIndexChanged">
                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未审批" Value="1"></asp:ListItem>
                    <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已审批" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td align="right">
                    <asp:Button ID="btnApply" runat="server"  Text="新增印章申请" OnClientClick="add()"/></td>
             </tr>
           </table>
      </div>
      </div>
      <div class="box-wrapper">
      <div class="box-outer" style="width:99%; overflow:auto;">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
               <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="SM_DEPM" HeaderText="申请部门" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblManapl" runat="server" Text='<%#Eval("SM_MANAPL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="SM_MANAPL" HeaderText="盖章人" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>
                
                <asp:BoundField DataField="SM_FNAME" HeaderText="文件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>
                
                <asp:BoundField DataField="SM_TIME" HeaderText="请办时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>
                
                <asp:BoundField DataField="SM_TYPE" HeaderText="印章类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>

                <asp:BoundField DataField="SM_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="审核状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="tcstate" runat="server" Text='<%#Eval("SM_TCSTATE").ToString()=="1"?"未审批":Eval("SM_TCSTATE").ToString()=="2"?"审批中":Eval("SM_TCSTATE").ToString()=="3"?"已审批":"已驳回" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplmod" runat="server" CssClass="link" 
                             NavigateUrl='<%#"OM_SEAL_Detail.aspx?action=mod&id="+Eval("CODE") %>'>
                        <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             修改
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplview" runat="server" CssClass="link" 
                             NavigateUrl='<%#"OM_SEAL_Detail.aspx?action=view&id="+Eval("CODE") %>'>
                        <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             查看
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplreview" runat="server" CssClass="link" 
                             NavigateUrl='<%#"OM_SEAL_Detail.aspx?action=review&id="+Eval("CODE") %>'>
                        <asp:Image ID="image3" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             审核
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="删除" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                   <ItemTemplate>
                   <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0" hspace="2" align="absmiddle" />
                       <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("CODE")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')" >删除</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
            </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:medium;"><br />
                没有记录!</div></asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
        </div>         
</asp:Content>
    
