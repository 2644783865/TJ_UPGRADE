<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Contract.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Contract" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblContractTypeBT" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<script language="javascript" type="text/javascript">
// <!CDATA[

function btnCXTJ_onclick() {
if(cxtj.style.display=="block")
  {
    cxtj.style.display="none";
    document.getElementById("btnCXTJ").value="显示条件";
  }
 else
  {
    cxtj.style.display="block";
    document.getElementById("btnCXTJ").value="隐藏条件";
  }
}

</script>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
              <table width="100%">
              <tr>
              <td style="width:10%;"><asp:Label ID="lblContractType" runat="server"></asp:Label>
                 </td>
               <td align="center">
               <asp:Button ID="btnShowPopup" class="button-outer" runat="server" Text="筛选条件" />&nbsp;
                       
                   </td>    
               <td align="right" style="width:10%;">
                  <asp:HyperLink ID="hlAdd" CssClass="link" runat="server" NavigateUrl='<%#"~/Contract_Data/CM_Contract_Add.aspx?Action=Add&ConForm="+ConForm%>'>
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif"  runat="server" />
                            新增合同
                    </asp:HyperLink>
               </td>             
              </tr>
              </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                    <div style="display:block;" id="cxtj">
   <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div style="width: 100%">
            <table width="100%" style="background-color:#CCCCFF; border:solid 1px black;"> 
                    <tr>
                    <td align="right" valign="bottom" style="width:100px"><strong>项目名称:</strong></td>
                    <td valign="middle">
                    <asp:ComboBox ID="ddlPCON_PJNAME" runat="server" 
                           AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" AutoPostBack="true">
                     </asp:ComboBox>
                     </td>
                    <td align="right"><strong>合同状态:</strong></td>
                     <td >
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="4" runat="server" RepeatDirection="Horizontal" >                
                        <asp:ListItem Value="3" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Text="已完成" Value="2"></asp:ListItem>
                        <asp:ListItem Text="进行中" value="1"></asp:ListItem>
                        <asp:ListItem Text="未开始" Value="0" />                        
                    </asp:RadioButtonList>
                    </td>
                      <td align="right" style="width:100px"><strong>负责部门：</strong></td>
                     <td style="width:100px">
                         <asp:DropDownList ID="dplFZBM" runat="server">
                         </asp:DropDownList>
                     </td>
                   </tr>
                   <tr>
                   <td align="right"><strong>供应商:</strong></td>
                   <td>
                       <asp:ComboBox ID="dplGYS" runat="server" 
                           AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" >
                        </asp:ComboBox>
                        </td>
                     <td align="right"><strong>负责人：</strong></td>
                     <td>
                         <asp:DropDownList ID="dplFZR" runat="server">
                         </asp:DropDownList>
                     </td>
                     <td align="right">
                       <asp:Label ID="lblWW" runat="server" Font-Bold="true" Text="委外类别:" Visible="False"></asp:Label></td>
                   <td>
                       <asp:DropDownList ID="dplWW" runat="server" Visible="False">
                           <asp:ListItem Text="-请选择-" Value="0" Selected="True" />
                           <asp:ListItem Text="委外外协" Value="WW" />
                           <asp:ListItem Text="工序外协" Value="GX" />
                           <asp:ListItem Text="厂内分包" Value="FB" />
                           <asp:ListItem Text="油漆外协" Value="YQ" />
                           <asp:ListItem Text="其他" Value="QT" />
                       </asp:DropDownList>
                   </td>
                    </tr>
                    <tr>
                       <td  colspan="4" ><strong>合同生效日期：</strong>
                          从&nbsp;&nbsp; <asp:TextBox ID="txtStartTime" runat="server" ReadOnly="true" onClick="setday(this);" Width="80px"></asp:TextBox>
                           到&nbsp;&nbsp;<asp:TextBox ID="txtEndTime" runat="server" ReadOnly="true" onClick="setday(this);" Width="80px"></asp:TextBox>
                        &nbsp;&nbsp;<strong>合同编号：</strong><asp:TextBox ID="txtHTH" runat="server"></asp:TextBox></td>
                       <td colspan="2">
                           <asp:Button ID="btnQueryHT" CssClass="button-outer" runat="server" Text="查 询" OnClick="btnQueryHT_OnClick" />
                           &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" CssClass="button-outer" Text="重 置" onclick="btnClear_Click" />
                          &nbsp;&nbsp;<asp:Button ID="btnClose" runat="server" CssClass="button-outer" Text="取 消" OnClick="btnClose_Click" />
                        </td>
                    </tr>
              </table>
     </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" 
                    TargetControlID="btnShowPopup" PopupControlID="UpdatePanelCondition"  Drag="false"  
                    Enabled="True"  DynamicServicePath=""  Y="30" >
       </asp:ModalPopupExtender> 
               <br />                
                </div>
<asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
            <asp:GridView ID="GRV_CON" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("PCON_BCODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_JINE" HeaderText="合同金额" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PCON_YFK" HeaderText="已付金额" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="支付比例" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Eval("PCON_JINE")))*100)+"%" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:BoundField DataField="PCON_FILLDATE" HeaderText="签订日期" ItemStyle-HorizontalAlign="Center" >
                   <ItemStyle HorizontalAlign="Center">
                   </ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PCON_VALIDDATE" HeaderText="启动日期" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>--%>
                <asp:BoundField HeaderText="索赔金额" DataField="PCON_SPJE" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_STATE").ToString()=="0"?"未开始":Eval("PCON_STATE").ToString()=="1"?"进行中":"完成")+"-"+(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.FromName("#333333") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="合同变更" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#"JE("+Eval("PCON_JECHG").ToString()+")"+"-"+"QT("+Eval("PCON_QTCHG").ToString()+")"%>' ForeColor='<%#Eval("PCON_QTCHG").ToString()!="0"?System.Drawing.Color.Red:Eval("PCON_JECHG").ToString()!="0"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract" CssClass="link" 
                         NavigateUrl='<%#"CM_Contract_Add.aspx?Action=Edit&condetail_id="+Eval("PCON_BCODE")+"&ConForm="+ConForm%>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/modify.gif" 
                         runat="server" />                               
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract1" CssClass="link" 
                         NavigateUrl='<%#"CM_Contract_Add.aspx?Action=View&condetail_id="+Eval("PCON_BCODE")+"&ConForm="+ConForm%>' runat="server">
                        <asp:Image ID="Imageview" ImageUrl="~/Assets/images/search.gif" 
                         runat="server" />                                
                        </asp:HyperLink>
                    </ItemTemplate>
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
        </ContentTemplate>
</asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
