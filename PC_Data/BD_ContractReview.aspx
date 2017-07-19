<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master"   AutoEventWireup="true" CodeBehind="BD_ContractReview.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.BD_ContractReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
  <%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">合同评审人员设置
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

    function Add_info()
    {
       window.showModalDialog('CM_HTPS_Setting_Edit.aspx','',"dialogHeight:370px;dialogWidth:400px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
       
    }
</script>
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </asp:ToolkitScriptManager> 
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

<div class="RightContent">
 <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0">
<asp:TabPanel ID="Tab_SPLD" runat="server" Width="100%" HeaderText="审批领导" TabIndex="0" >
<ContentTemplate>
  <div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
     <table width="100%">
     <tr>
     <td align="left" style="width:20%;">合同评审领导信息</td>
     <td align="center">评审合同类别:&nbsp;&nbsp;
         <asp:DropDownList ID="dplPSHTLB" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplPSHTLB_OnSelectedIndexChanged">
              <asp:ListItem Text="-请选择-" Value="请选择"></asp:ListItem>
              <asp:ListItem Text="商务合同" Value="0"></asp:ListItem>
              <asp:ListItem Text="采购合同" Value="1"></asp:ListItem>
              <asp:ListItem Text="技术外协" Value="2"></asp:ListItem>
              <asp:ListItem Text="生产外协" Value="4"></asp:ListItem>
              <asp:ListItem Text="厂内分包" Value="5"></asp:ListItem>
              <asp:ListItem Text="发运合同" Value="3"></asp:ListItem>
              <asp:ListItem Text="其他合同" Value="6"></asp:ListItem>
              <asp:ListItem Text="询比价" Value="7"></asp:ListItem>
              <asp:ListItem Text="-全部-" Value="%"></asp:ListItem>
         </asp:DropDownList>
     </td>
     <td align="right"><asp:HyperLink ID="hylAddPS" CssClass="hand" runat="server"><asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />&nbsp;添加信息</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"  TargetControlID="hylAddPS" PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-150">
        </asp:PopupControlExtender>
        
     </td>
     </tr>
     </table>
     <asp:Panel ID="palPSHTLB" style="visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>  
         <table width="180px;">
         <tr>
         
         <td>
              <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                  <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
              </div>
              <br />
              <br />
         </td>
         </tr>
         <tr>
         <td align="left">评审合同类别：</td>
         <td>
            <asp:DropDownList ID="dplPSHTLB_Select" runat="server">
              <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
              <asp:ListItem Text="商务合同" Value="0"></asp:ListItem>
              <asp:ListItem Text="采购合同" Value="1"></asp:ListItem>
              <asp:ListItem Text="技术外协" Value="2"></asp:ListItem>
             <asp:ListItem Text="生产外协" Value="4"></asp:ListItem>
             <asp:ListItem Text="厂内分包" Value="5"></asp:ListItem>
            <asp:ListItem Text="发运合同" Value="3"></asp:ListItem>
            <asp:ListItem Text="其他合同" Value="6"></asp:ListItem>
            <asp:ListItem Text="询比价" Value="7"></asp:ListItem>
            </asp:DropDownList>
         </td>
         </tr>
         <tr>
         <td colspan="2" align="center">
         <br />
             <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_OnClick" />
         </td>
         </tr>
         </table>
      </ContentTemplate>
      </asp:UpdatePanel>
     </asp:Panel>
    </div>
    </div>
    </div>
      <div class="box-wrapper" >
        <div class="box-outer" >
            <asp:GridView ID="grvPS" width="100%" CssClass="toptable grid" runat="server" OnRowCommand="grvPS_OnRowCommand"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="金额下限" DataField="REV_MINAM" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="金额上限" DataField="REV_MAXAM" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField HeaderText="货币单位" DataField="REV_UNIT" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField HeaderText="评审人" DataField="REV_PERNM" ItemStyle-HorizontalAlign="Center"  />
              <asp:BoundField HeaderText="级数" DataField="REV_RANK" ItemStyle-HorizontalAlign="Center"  />
              <asp:TemplateField HeaderText="单据类型" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("REV_CATEGORY").ToString()=="0"?"商务合同":Eval("REV_CATEGORY").ToString()=="1"?"采购合同":Eval("REV_CATEGORY").ToString()=="2"?"技术外协":Eval("REV_CATEGORY").ToString()=="3"?"发运合同":Eval("REV_CATEGORY").ToString()=="4"?"生产外协":Eval("REV_CATEGORY").ToString()=="5"?"厂内分包":Eval("REV_CATEGORY").ToString()=="6"?"其他合同":"询比价" %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField HeaderText="维护人" DataField="REV_MANCNM" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField HeaderText="维护时间" DataField="REV_FILLDATE" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">                    
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract" CssClass="link" NavigateUrl='<%#editHt(Eval("REV_CATEGORY").ToString(),Eval("REV_ID").ToString())%>' runat="server">
                        <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" 
                         runat="server" />修改                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                 <ItemTemplate>
                  <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                     <asp:LinkButton ID="btnDelete" runat="server" ToolTip="删除" CausesValidation="False"  EnableViewState="true" OnClientClick="javascript:return confirm('确认删除该条记录吗？');"   CommandArgument='<%# Eval("REV_ID")%>' CommandName="Del" Text="删除"></asp:LinkButton>
                 </ItemTemplate>
               </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="palNoData" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
        </div>
     </div>
 </div>  
 </ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="Tab_BMFZR" runat="server" HeaderText="部门负责人" TabIndex="1">
<ContentTemplate>
<div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
      <table width="100%">
            <tr>
            <td>合同评审部门负责人信息——<asp:Label ID="Label1" runat="server" Text="*注：电气技术、电气质量、电气生产评审人员请添加到技术部、质量部、生产部"
                 ForeColor="Red"></asp:Label></td>            
              <td align="right">
               <asp:HyperLink ID="hylAddContractView" CssClass="hand" runat="server" onClick="Add_info()">
      <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/icons/add.gif" />&nbsp;&nbsp;添加信息</asp:HyperLink>&nbsp;&nbsp;
              </td> 
            </tr>
            </table> 
    </div>
    </div>
    </div>
      <div class="box-wrapper" >
        <div class="box-outer" >
         <asp:GridView ID="GRV_HT_Set" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>                
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="DEP_NAME" HeaderText="部门名称" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="ST_NAME" HeaderText="评审人员" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                 <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" >                    
                    <ItemTemplate>
                        <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("ID") %>' OnClick="lnkDel_OnClick"
                         OnClientClick="javascript:return confirm('确定要删除吗？');">
                         <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" 
                         runat="server" />
                         </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
     </div>
 </div>
</ContentTemplate>
</asp:TabPanel>

</asp:TabContainer>
</div>    
</asp:Content>