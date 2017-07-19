<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="EQU_Apply.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_Apply" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
设备采购申请及审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript">
    function add()
    {
        var sRet=window.showModalDialog('EQU_ApplyDetail.aspx?action=add','obj','dialogWidth=900px;dialogHeight=600px');
        if(sRet=="refresh")
        {
            window.location.href=window.location.href;
        }
    }
</script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <div class="box-inner">
    <div class="box_right">
      <div class="box-title">
        <asp:Label ID="ControlFinder" runat="server" Text="" Visible="true"></asp:Label>
        <table width="100%">
            <tr>
                <%--<td style="width:60px">审批状态:</td>
                <td align="center" style="width: 311px">
                    <asp:RadioButtonList ID="spzt" runat="server" RepeatDirection="Horizontal" 
                         AutoPostBack="true" >
                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未审批" Value="1"></asp:ListItem>
                    <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已审批" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                    </asp:RadioButtonList></td>--%>
                <td align="center">
                    <asp:Button ID="btnApply" runat="server"  Text="新增设备采购申请" OnClientClick="add()"/></td>
            </tr>
        </table>
      </div>
    </div>
  </div>
  <div class="box-wrapper">      
    <div class="box-outer">
        <asp:GridView ID="gridview1" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主键" ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lbid" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请部门" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:Label ID="sqbm" runat="server" Text='<%#Eval("Dep")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 <asp:BoundField DataField="Name" HeaderText="设备名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Type" HeaderText="设备类型" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Num" HeaderText="设备数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
               <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("Applyer") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="spzt" runat="server" Text='<%#Eval("SPZT").ToString()=="1"?"未审批":Eval("SPZT").ToString()=="2"?"审批中":Eval("SPZT").ToString()=="3"?"已审批":"已驳回" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplmod" runat="server" CssClass="link" 
                             NavigateUrl='<%#"EQU_ApplyDetail.aspx?action=mod&id="+Eval("Code") %>'>
                        <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             修改
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplview" runat="server" CssClass="link" 
                             NavigateUrl='<%#"EQU_ApplyDetail.aspx?action=view&id="+Eval("Code") %>'>
                        <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             查看
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplreview" runat="server" CssClass="link" 
                             NavigateUrl='<%#"EQU_ApplyDetail.aspx?action=review&id="+Eval("Code") %>'>
                        <asp:Image ID="image3" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                             审核
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_OnClick" CommandArgument='<%# Eval("Code")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                        <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0" hspace="2" align="absmiddle" />
                             删除
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">没有记录!</asp:Panel>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
